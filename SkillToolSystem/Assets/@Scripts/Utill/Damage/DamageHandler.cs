using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class DamageHandler : MonoBehaviour
{
    // public Transform target;
    public BoxCollider2D boxCollider2D;
    public float minYPos;
    public float maxXPos;
    private Vector3 hitStartPos;
    private readonly HashSet<IDamageAble> damagedTargets = new();
    private readonly WaitForSeconds Interval = new(0.04f);
    private Coroutine coDamageProcess;
    public void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        
        Debug.Log(minYPos);
    }
    public void CreateMeleeAttackBox(AttackData attackData,Transform AttackEffectPoint,bool attackEffectFlip)
    {
        if (attackData.attackType != AttackType.Melee || attackData ==null)
            return;
        if (attackData.attackSFX != null)
            SFXManager.Instance.PlaySFX(attackData.attackSFX);
        damagedTargets.Clear();
        minYPos = boxCollider2D.bounds.min.y;
        maxXPos = boxCollider2D.bounds.center.x;
        hitStartPos = new Vector3(maxXPos +(attackData.attackRange.x / 2)+ attackData.startAttackPoint.x, minYPos +(attackData.attackRange.y/2) + attackData.startAttackPoint.y, 0);
        Debug.Log(hitStartPos);
        Collider2D[] hits = Physics2D.OverlapBoxAll(hitStartPos, attackData.attackRange, 0, attackData.TargetLayer);
        Debug.DrawLine(hitStartPos,attackData.attackRange, Color.red, 2f);
        if (attackData.attackHitType == AttackHitType.MultiTarget)
        {
            foreach (var hit in hits)
            {
                if (hit.bounds.min.y >= (hitStartPos.y - (attackData.attackRange.y / 2)) && hit.bounds.min.y <= (hitStartPos.y + (attackData.attackRange.y / 2)))
                {
                    IDamageAble dmg = hit.GetComponent<IDamageAble>();
                    if (dmg != null && !damagedTargets.Contains(dmg))
                    {
                        damagedTargets.Add(dmg);
                        if (hit.GetComponent<DamageAbleBase>().damageAble == true)
                        {
                            Debug.Log("MultiTargetProcess");
                            coDamageProcess = StartCoroutine(HitDamage(dmg, attackData, hit.gameObject, AttackEffectPoint, attackEffectFlip));
                        }
                    }
                }
            }
        }
        if (attackData.attackHitType == AttackHitType.SingleTarget)
        {
            float minDistance = float.MaxValue;
            float curDistance;
            int curHits = 0;
            for (int i = 0; i < hits.Length; i++)
            {
                curDistance = transform.position.x - hits[i].transform.position.x;
                if (curDistance < 0)
                {
                    curDistance *= -1;
                }
                if (curDistance < minDistance)
                {
                    minDistance = curDistance;
                    curHits = i;
                }
            }
            IDamageAble dmg = hits[curHits].GetComponent<IDamageAble>();
            Debug.Log($"SingleTargetProcess : {curHits}, {minDistance}");
            coDamageProcess = StartCoroutine(HitDamage(dmg, attackData, hits[curHits].gameObject, AttackEffectPoint, attackEffectFlip));
        }
    }
    IEnumerator HitDamage(IDamageAble dmg, AttackData attackData, GameObject target, Transform AttackEffectPoint, bool attackEffectFlip)
    {
        int currentHits = 0;
        Vector3 effectPos = new Vector3(attackData.effectPos.x, attackData.effectPos.y, 0);

        if (attackData.attackEffectPrefabName != "")
        {
            var attackEffect = ObjectPoolManager.instance.GetObject(attackData.attackEffectPrefabName);
            if (attackEffectFlip == true)
            {
                attackEffect.GetComponent<SpriteRenderer>().flipX = true;
            }
            attackEffect.transform.position = AttackEffectPoint.position;
        }
        else yield return null;
        if (target.GetComponent<ObjectStatus>().onKnockBack == true) // 공격 넉백 프로세스
        {
            float xKnockbackForce = attackData.knockBackForce.x;
            target.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;
            if (target.transform.position.x < transform.position.x)
            {
                xKnockbackForce = -xKnockbackForce;
            }
            target.GetComponent<Rigidbody2D>().AddForce(new Vector3(xKnockbackForce, attackData.knockBackForce.y, 0), ForceMode2D.Impulse);
        }
        while (currentHits < attackData.hitCount)
        {
            dmg.TakeDamage((attackData.damage), attackData.weaponType);
            if (attackData.HitEffectPrefabName != "")
            {
                if (target.GetComponent<ObjectStatus>().isDie == false)
                {
                    var effect = ObjectPoolManager.instance.GetObject(attackData.HitEffectPrefabName);

                    effect.transform.position = target.transform.position + effectPos;
                    if (transform.localScale.x == -1)
                    {
                        effect.GetComponent<SpriteRenderer>().flipX = true;
                    }
                }
                else yield return null;
            }
            else yield return null;
            currentHits++;
            yield return Interval;
        }
        yield return new WaitForSeconds(0.4f);
    }
}