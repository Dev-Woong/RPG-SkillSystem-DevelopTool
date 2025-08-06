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
    [Header("스킬 타겟레이어")]
    public SkillTargetLayer TargetLayer; 

    [Header("스킬 정보")]
    public string skillName;
    public float coolDown;
    public float damage;
    public int hitCount;
    public Vector2 startSkillPoint;
    public Vector2 skillRange;

    [Header("소모 자원")]
    public float hp;
    public float mp;

    [Header("슈퍼아머")]
    public bool superArmor;

    [Header("스킬 캐스팅")]
    public bool skillCasting;
    public float castingTime;

    [Header("스킬 시전 중 조작")]
    public bool onSkillMove;

    [Header("스킬 특수버프")]
    public SkillSpecialAbility specialAbility;

    [Header("스킬 속성")]
    public SkillElement skillElement;

    [Header("스킬 단일or다중 타겟")]
    public SkillHitType skillHitType;

    [Header("스킬 범위 타입")]
    public SkillRangeType skillRangeType;   

    [Header("스킬 넉백정보")]
    public bool knockBack;
    public Vector2 knockBackForce;

    [Header("스킬 사운드")]
    public AudioClip skillSFX;

    [Header("스킬 이펙트")]
    public GameObject SkillEffect;
    public Vector2 effectPos;

    [Header("타격 이펙트")]
    public GameObject HitEffect;
    public Vector2 hitEffectPos;

    [Header("카메라 연출")]
    public bool isCameraShake;
    public float shakePower;
    public Vector2 shakeDir;
    public float shakeDuration;
}
