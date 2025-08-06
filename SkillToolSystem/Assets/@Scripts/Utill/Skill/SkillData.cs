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
    [Header("��ų Ÿ�ٷ��̾�")]
    public SkillTargetLayer TargetLayer; 

    [Header("��ų ����")]
    public string skillName;
    public float coolDown;
    public float damage;
    public int hitCount;
    public Vector2 startSkillPoint;
    public Vector2 skillRange;

    [Header("�Ҹ� �ڿ�")]
    public float hp;
    public float mp;

    [Header("���۾Ƹ�")]
    public bool superArmor;

    [Header("��ų ĳ����")]
    public bool skillCasting;
    public float castingTime;

    [Header("��ų ���� �� ����")]
    public bool onSkillMove;

    [Header("��ų Ư������")]
    public SkillSpecialAbility specialAbility;

    [Header("��ų �Ӽ�")]
    public SkillElement skillElement;

    [Header("��ų ����or���� Ÿ��")]
    public SkillHitType skillHitType;

    [Header("��ų ���� Ÿ��")]
    public SkillRangeType skillRangeType;   

    [Header("��ų �˹�����")]
    public bool knockBack;
    public Vector2 knockBackForce;

    [Header("��ų ����")]
    public AudioClip skillSFX;

    [Header("��ų ����Ʈ")]
    public GameObject SkillEffect;
    public Vector2 effectPos;

    [Header("Ÿ�� ����Ʈ")]
    public GameObject HitEffect;
    public Vector2 hitEffectPos;

    [Header("ī�޶� ����")]
    public bool isCameraShake;
    public float shakePower;
    public Vector2 shakeDir;
    public float shakeDuration;
}
