using Unity.VisualScripting;
using UnityEngine;

public enum SkillHitType
{
    SingleTarget,
    MultiTarget
}
public enum SkillType
{
    Melee,
    Range,
    AOE
}
public enum SkillSpecialAbility
{
    None,
    Buff,
    DeBuff
}
public enum SkillElement
{
    None,
    Dark,
    Light,
    Fire,
    Water
}
public enum SkillTargetLayer
{
    Player,
    Enemy
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
public class SkillData : ScriptableObject
{
    public SkillTargetLayer TargetLayer;
    public Sprite skillIcon;
    [TextArea]
    public string skillDescription;
    public string skillName;
    public float coolTime;
    public float damage;
    public int hitCount;
    public Vector2 startSkillPoint; 
    public Vector2 skillRange;
    public bool usingLevelSystem;
    public int skillCurLevel;
    public int skillMaxLevel;
    public float levelIncreaseValue;
    public float hp;
    public float mp;
    public bool superArmor;
    public bool skillCasting;
    public float castingTime;
    public bool canMove;
    public string ProjectilePrefabName;
    public float projectileForce;
    public SkillElement skillElement;
    public SkillHitType skillHitType;
    public SkillType skillType;
    public SkillSpecialAbility specialAbility;
    public SetDebuffType setDebuffType;
    public SetBuffType setBuffType; 
    public bool useKnockBack;
    public Vector2 knockBackForce;
    public AudioClip skillSFX;
    public string SkillEffectPrefabName;
    public Vector2 effectPos;
    public string HitEffectPrefabName;
    public Vector2 hitEffectPos;
    public bool useCameraShake;
    public float shakePower;
    public Vector2 shakeDir;
    public float shakeDuration;
}
