using UnityEngine;

public class EnemyController : DamageAbleBase, IDamageAble
{
    private BoxCollider2D _boxCollider;
    public int CurHp = 100;
    public override void OnDamage(float damage, WeaponType wType)
    {
        if (DamageAble == true)
        {
            CurHp -= Mathf.RoundToInt(damage);
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
