
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
public enum AttackElement
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

[CreateAssetMenu(fileName ="NewSkill", menuName = "Skills/SkillData")]
public class AttackData : ScriptableObject
{
    public AttackType attackType;
    public WeaponType weaponType;
    [TextArea]
    public string attackDescription;
    public Sprite AttackIcon;
    public LayerMask TargetLayer;
    public string attackName;
    public float coolTime;
    public float damage;
    public int hitCount;
    public AttackHitType attackHitType;
    public AttackElement attackElement;
    public AttackSpecialAbility specialAbility;
    public SetBuffType setBuffType; 
    public SetDebuffType setDebuffType;
    public Vector2 startAttackPoint; 
    public Vector2 attackRange;

    public bool usingLevelSystem;
    public int attackCurLevel;
    public int attackMaxLevel;
    public float levelIncreaseValue;
    public float hp;
    public float mp;
    public bool canMove;
    public bool superArmor;
    public bool useKnockBack;
    public Vector2 knockBackForce;
    public bool attackCasting;
    public float castingTime;
    public string ProjectilePrefabName;
    public float projectileForce;
   
    public AudioClip attackSFX;
    public string attackEffectPrefabName;
    public Vector2 effectPos;

    public string HitEffectPrefabName;
    public Vector2 hitEffectPos;

    public bool useCameraShake;
    public float shakePower;
    public Vector2 shakeDir;
    public float shakeDuration;
}
