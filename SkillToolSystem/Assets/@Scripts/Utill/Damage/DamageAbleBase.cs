using UnityEngine;
public interface IDamageAble
{
    public void TakeDamage(float Damage, WeaponType wType);
}
public abstract class DamageAbleBase : MonoBehaviour, IDamageAble   
{
    public bool damageAble = true;
    public void TakeDamage(float Damage, WeaponType wType)
    {
        if (damageAble == true)
        {
            OnDamage(Damage, wType);
        }
    }
    public abstract void OnDamage(float damage, WeaponType wType);

}
