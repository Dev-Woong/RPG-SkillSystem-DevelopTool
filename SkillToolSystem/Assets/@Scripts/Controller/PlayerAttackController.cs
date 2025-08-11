using UnityEngine;

public class PlayerAttackController : DamageAbleBase,IDamageAble
{
    public DamageHandler damageHandler;
    
    public int curHP = 100;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private Transform[] attackEffectPos;
    void Start()
    {
        damageHandler = GetComponent<DamageHandler>();
    }
    public override void OnDamage(float damage, WeaponType wType)
    {
        curHP -= Mathf.RoundToInt(damage);
    }
    public void Attack()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            damageHandler.CreateMeleeAttackBox(attackDatas[0], attackEffectPos[0], false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        Attack();
    }
}
