using UnityEngine;

public class PlayerAttackController : DamageAbleBase,IDamageAble
{
    [SerializeField] private DamageHandler _damageHandler;
    
    public int curHP = 100;
    [SerializeField] private AttackData[] _attackDatas;
    [SerializeField] private Transform[] _attackEffectPos;
    void Start()
    {
        _damageHandler = GetComponent<DamageHandler>();
    }
    public override void OnDamage(float damage, WeaponType wType)
    {
        curHP -= Mathf.RoundToInt(damage);
    }
    public void Attack()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _damageHandler.CreateMeleeAttackBox(_attackDatas[0], _attackEffectPos[0], false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        Attack();
    }
}
