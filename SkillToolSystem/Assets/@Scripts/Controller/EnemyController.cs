using UnityEngine;

public class EnemyController : DamageAbleBase, IDamageAble
{
    [SerializeField] private ObjectStatus _objectStatus;
    public int CurHp = 100;
    void Start()
    {
        _objectStatus = GetComponent<ObjectStatus>();
    }
    public override void OnDamage(float damage, WeaponType wType)
    {
        if (DamageAble == true)
        {
            switch (_objectStatus.ObjectElement)
            {

            }
            CurHp -= Mathf.RoundToInt(damage);
            Debug.Log($"GetDamage : {Mathf.RoundToInt(damage)}, objName : {this.name}");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
