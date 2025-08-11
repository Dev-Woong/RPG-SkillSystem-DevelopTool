using UnityEngine;

public class EnemyController : DamageAbleBase, IDamageAble
{
    private BoxCollider2D bc;
    public int curHP = 100;
    public override void OnDamage(float damage, WeaponType wType)
    {
        if (damageAble == true)
        {
            curHP -= Mathf.RoundToInt(damage);
            Debug.Log($"GetDamage : {Mathf.RoundToInt(damage)}, objName : {this.name}");
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
