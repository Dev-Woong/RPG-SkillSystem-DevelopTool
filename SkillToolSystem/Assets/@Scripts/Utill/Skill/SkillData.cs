using UnityEngine;

public enum SkillHitType
{
    SingleTarget,
    MultiTarget
}
public enum SkillRangeType
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

[CreateAssetMenu(fileName ="NewSkill", menuName = "Skills/SkillData")]
public class SkillData : ScriptableObject
{
    public SkillTargetLayer TargetLayer;
    public string skillName;
    public float coolTime;
    public float damage;
    public int hitCount;
    public Vector2 startSkillPoint;
    public Vector2 skillRange;
    public float hp;
    public float mp;
    public bool superArmor;
    public bool skillCasting;
    public float castingTime;
    public bool onSkillMove;
    public SkillSpecialAbility specialAbility;
    public SkillElement skillElement;
    public SkillHitType skillHitType;
    public SkillRangeType skillRangeType;
    public bool knockBack;
    public Vector2 knockBackForce;
    public AudioClip skillSFX;
    public GameObject SkillEffect;
    public Vector2 effectPos;
    public GameObject HitEffect;
    public Vector2 hitEffectPos;
    public bool isCameraShake;
    public float shakePower;
    public Vector2 shakeDir;
    public float shakeDuration;
}
