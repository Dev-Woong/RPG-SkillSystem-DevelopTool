
using UnityEngine;
/// <summary>
/// SetWeaponType what you want
/// </summary>
public enum WeaponType
{
    Hand,
    Sword,
    Gun,
    Bow,
    Staff
}

public enum AttackHitType
{
    SingleTarget,
    MultiTarget
}
public enum AttackType
{
    All,
    Melee,
    Range,
    AOE
}
public enum AttackSpecialAbility
{
    None,
    Buff,
    DeBuff
}
public enum Element
{
    None,
    Dark,
    Light,
    Fire,
    Water
}
public enum SetDebuffType
{
    Stun,
    Forzen,
    Burning
}
public enum SetBuffType
{
    ExampleBuff,
    ExampleBuff2,
}

[CreateAssetMenu(fileName ="NewAttackData", menuName = "Attacks/AttackData")]
public class AttackData : ScriptableObject
{
    public AttackType AttackType;
    public WeaponType WeaponType;
    [TextArea]
    public string AttackDescription;
    public Sprite AttackIcon;
    public LayerMask TargetLayer;
    public string AttackName;
    public float CoolTime;
    public float Damage;
    public int HitCount;
    public AttackHitType AttackHitType;
    public Element AttackElement;
    public AttackSpecialAbility SpecialAbility;
    public SetBuffType SetBuffType; 
    public SetDebuffType SetDebuffType;
    public Vector2 StartAttackPoint; 
    public Vector2 AttackRange;

    public bool UsingLevelSystem;
    public int AttackCurLevel;
    public int AttackMaxLevel;
    public float LevelIncreaseValue;
    public float Hp;
    public float Mp;
    public bool CanMove;
    public bool SuperArmor;
    public bool UseKnockBack;
    public Vector2 KnockBackForce;
    public bool AttackCasting;
    public float CastingTime;

    public string ProjectilePrefabName;
    public float ProjectileForce;
   
    public AudioClip AttackSFX;
    public string AttackEffectPrefabName;
    public Vector2 EffectPos;

    public string HitEffectPrefabName;
    public Vector2 HitEffectPos;

    public bool UseCameraShake;
    public float ShakePower;
    public Vector2 ShakeDir;
    public float ShakeDuration;
}
