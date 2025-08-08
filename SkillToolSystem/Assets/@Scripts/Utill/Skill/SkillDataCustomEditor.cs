using System;
using System.Reflection;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
[CustomEditor(typeof(SkillData))]
public class SkillDataCustomEditor : Editor
{
    private bool showBasic = true;
    private bool showCost = true;
    private bool showEffect = true;
    private bool showCamera = true;
    #region PropertyField
    SerializedProperty skillType; // 스킬타입 대전제
    SerializedProperty TargetLayer; // 스킬 타겟레이어

    // ** 스킬 기본 정보 ** // 
    //- 스킬 이름
    SerializedProperty skillName;

    //- 스킬 설명
    SerializedProperty skillDescription;

    //- 스킬 아이콘
    SerializedProperty skillIcon;

    //- 쿨타임
    SerializedProperty coolTime;

    //- 데미지
    SerializedProperty damage;

    //- 히트수
    SerializedProperty hitCount;

    // 다중, 단일타겟
    SerializedProperty skillHitType;

    // 공격 속성
    SerializedProperty skillElement;

    // 스킬 특수 능력
    SerializedProperty specialAbility;

    // 버프 타입
    SerializedProperty setBuffType;

    // 디버프 타입
    SerializedProperty setDebuffType;

    // 스킬 범위
    SerializedProperty startSkillPoint;
    SerializedProperty skillRange;

    // ----------------------------------------------

    // ** 스킬 레벨시스템 디자인 ** //

    //- 스킬 레벨 시스템 사용 여부
    SerializedProperty usingLevelSystem;

    //- 스킬 현재레벨
    SerializedProperty skillCurLevel;

    //- 스킬 최대레벨
    SerializedProperty skillMaxLevel;

    //- 스킬 레벨당 데미지 상승치
    SerializedProperty skillIncreaseValue;

    // ----------------------------------------------

    // ** 소모코스트  
    SerializedProperty hpCost;
    SerializedProperty mpCost;

    // ** 스킬 사용중 이동여부
    SerializedProperty canMove;

    // ** 슈퍼아머
    SerializedProperty onSuperArmor;

    // ** 넉백
    SerializedProperty useKnockBack;
    SerializedProperty knockBackForce;

    // ** 스킬 캐스팅
    SerializedProperty doSkillCasting;
    SerializedProperty skillCastingTime;

    // ** 투사체
    SerializedProperty ProjectilePrefabName;
    SerializedProperty projectileForce;

    // ** 스킬 연출 ** // 
    
    //- 스킬 시전 사운드
    SerializedProperty skillSFX;

    //- 스킬 이펙트
    SerializedProperty skillEffectName;
    SerializedProperty skillEffectPos;

    //- 타격 이펙트
    SerializedProperty hitEffectName;
    SerializedProperty hitEffectPos;

    // ----------------------------------------------

    // ** 카메라 연출 ** //

    //- 카메라 연출 사용여부
    SerializedProperty useCameraShake;

    //- 카메라 쉐이크 힘
    SerializedProperty shakeCameraForce;

    //- 카메라 쉐이크 방향
    SerializedProperty shakeCameraDir;

    //- 카메라 쉐이크 유지시간
    SerializedProperty shakeCameraDuration;

    // ----------------------------------------------
    #endregion
    private void OnEnable()
    {
        skillType = serializedObject.FindProperty("skillType");
        skillDescription = serializedObject.FindProperty("skillDescription");
        skillIcon = serializedObject.FindProperty("skillIcon");
        TargetLayer = serializedObject.FindProperty("TargetLayer");
        skillName = serializedObject.FindProperty("skillName");
        coolTime = serializedObject.FindProperty("coolTime");
        damage = serializedObject.FindProperty("damage");
        hitCount = serializedObject.FindProperty("hitCount");
        skillHitType = serializedObject.FindProperty("skillHitType");
        skillElement = serializedObject.FindProperty("skillElement");
        specialAbility = serializedObject.FindProperty("specialAbility");
        setBuffType = serializedObject.FindProperty("setBuffType");
        setDebuffType = serializedObject.FindProperty("setDebuffType");
        startSkillPoint = serializedObject.FindProperty("startSkillPoint");
        skillRange = serializedObject.FindProperty("skillRange");

        usingLevelSystem = serializedObject.FindProperty("usingLevelSystem");
        skillCurLevel = serializedObject.FindProperty("skillCurLevel");
        skillMaxLevel = serializedObject.FindProperty("skillMaxLevel");
        skillIncreaseValue = serializedObject.FindProperty("levelIncreaseValue");

        hpCost = serializedObject.FindProperty("hp");
        mpCost = serializedObject.FindProperty("mp");
        canMove = serializedObject.FindProperty("canMove");
        onSuperArmor = serializedObject.FindProperty("superArmor");

        useKnockBack = serializedObject.FindProperty("useKnockBack");
        knockBackForce = serializedObject.FindProperty("knockBackForce");

        doSkillCasting = serializedObject.FindProperty("skillCasting");
        skillCastingTime = serializedObject.FindProperty("castingTime");

        ProjectilePrefabName = serializedObject.FindProperty("ProjectilePrefabName");
        projectileForce = serializedObject.FindProperty("projectileForce");

        skillSFX = serializedObject.FindProperty("skillSFX");
        skillEffectName = serializedObject.FindProperty("SkillEffectPrefabName");
        skillEffectPos = serializedObject.FindProperty("effectPos");
        hitEffectName = serializedObject.FindProperty("HitEffectPrefabName");
        hitEffectPos = serializedObject.FindProperty("hitEffectPos");

        useCameraShake = serializedObject.FindProperty("useCameraShake");
        shakeCameraForce = serializedObject.FindProperty("shakePower");
        shakeCameraDir = serializedObject.FindProperty("shakeDir");
        shakeCameraDuration = serializedObject.FindProperty("shakeDuration");
    }
    public override void OnInspectorGUI()
    {
        if (target == null)
        {
            Debug.LogError("target is null!");
            return;
        }

        SkillData skill = (SkillData)target;
        if (skill == null)
        {
            Debug.LogError("skill is null!");
            return;
        }
        serializedObject.Update();
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("skillType"));
        switch (skill.skillType)
        {
            case SkillType.Melee:
                showBasic = EditorGUILayout.Foldout(showBasic, "근접타입 기본 정보", true);
                if (showBasic)
                {
                    EditorGUILayout.PropertyField(TargetLayer);
                    EditorGUILayout.PropertyField(skillIcon);
                    EditorGUILayout.PropertyField(skillName);
                    EditorGUILayout.PropertyField(skillDescription);
                    EditorGUILayout.PropertyField(coolTime);
                    EditorGUILayout.PropertyField(damage);
                    EditorGUILayout.PropertyField(hitCount);
                    EditorGUILayout.PropertyField(skillHitType);
                    EditorGUILayout.PropertyField(skillElement);
                    EditorGUILayout.PropertyField(specialAbility);
                    if (skill.specialAbility == SkillSpecialAbility.Buff)
                    {
                        EditorGUILayout.PropertyField(setBuffType);
                    }
                    else if (skill.specialAbility == SkillSpecialAbility.DeBuff)
                    {
                        EditorGUILayout.PropertyField(setDebuffType);
                    }

                    EditorGUILayout.Space();

                    EditorGUILayout.LabelField("스킬 범위", EditorStyles.boldLabel);
                    EditorGUILayout.PropertyField(startSkillPoint);
                    EditorGUILayout.PropertyField(skillRange);
                    EditorGUILayout.Space();
                    EditorGUILayout.PropertyField(usingLevelSystem);
                    if (skill.usingLevelSystem)
                    {
                        EditorGUILayout.PropertyField(skillCurLevel);
                        EditorGUILayout.PropertyField(skillMaxLevel);
                        EditorGUILayout.LabelField("스킬 레벨당 데미지 증가값", EditorStyles.miniBoldLabel);
                        EditorGUILayout.PropertyField(skillIncreaseValue);
                    }
                }
                EditorGUILayout.Space();
                showCost = EditorGUILayout.Foldout(showCost, "코스트 및 부가 효과", true);
                if (showCost)
                {
                    EditorGUILayout.PropertyField(hpCost);
                    EditorGUILayout.PropertyField(mpCost);
                    EditorGUILayout.PropertyField(canMove);
                    EditorGUILayout.PropertyField(onSuperArmor);
                    EditorGUILayout.PropertyField(useKnockBack);
                    if (skill.useKnockBack)
                    {
                        EditorGUILayout.PropertyField(knockBackForce);
                    }
                }
                EditorGUILayout.Space();
                showEffect = EditorGUILayout.Foldout(showEffect, "스킬 연출", true);
                if (showEffect)
                {
                    EditorGUILayout.PropertyField(doSkillCasting);
                    if (skill.skillCasting)
                    {
                        EditorGUILayout.PropertyField(skillCastingTime);
                    }

                    EditorGUILayout.PropertyField(skillEffectName);
                    EditorGUILayout.PropertyField(skillEffectPos);
                    EditorGUILayout.PropertyField(hitEffectName);
                    EditorGUILayout.PropertyField(hitEffectPos);
                    EditorGUILayout.PropertyField(skillSFX);
                    if (skillSFX != null)
                    {
                        if (GUILayout.Button("효과음 재생"))
                        {
                            if (Application.isPlaying == true)
                            {
                                Debug.LogWarning("플레이모드에선 재생할 수 없습니다.");
                                return;
                            }
                            if (Application.isPlaying == false && skill.skillSFX == null)
                            {
                                Debug.LogWarning($"skillSFX가 {skill.skillSFX} 입니다.");
                                return;
                            }
                            PlayClip(skill.skillSFX);
                            Debug.Log($"효과음 재생 {skill.skillSFX}");
                        }
                    }
                }

                EditorGUILayout.Space();
                showCamera = EditorGUILayout.Foldout(showCamera, "카메라 연출", true);
                if (showCamera)
                {
                    EditorGUILayout.PropertyField(useCameraShake);
                    if (skill.useCameraShake)
                    {
                        EditorGUILayout.PropertyField(shakeCameraForce);
                        EditorGUILayout.PropertyField(shakeCameraDir);
                        EditorGUILayout.PropertyField(shakeCameraDuration);
                    }
                }
                break;






            case SkillType.Range:
                showBasic = EditorGUILayout.Foldout(showBasic, "투사체타입 기본 정보", true);
                if (showBasic)
                {
                    EditorGUILayout.PropertyField(TargetLayer);
                    EditorGUILayout.PropertyField(skillIcon);
                    EditorGUILayout.PropertyField(skillName);
                    EditorGUILayout.PropertyField(skillDescription);
                    EditorGUILayout.PropertyField(coolTime);
                    EditorGUILayout.PropertyField(damage);
                    EditorGUILayout.PropertyField(hitCount);
                    EditorGUILayout.PropertyField(skillHitType);
                    EditorGUILayout.PropertyField(skillElement);
                    EditorGUILayout.PropertyField(specialAbility);
                    if (skill.specialAbility == SkillSpecialAbility.Buff)
                    {
                        EditorGUILayout.PropertyField(setBuffType);
                    }
                    else if (skill.specialAbility == SkillSpecialAbility.DeBuff)
                    {
                        EditorGUILayout.PropertyField(setDebuffType);
                    }
                    EditorGUILayout.Space();
                    EditorGUILayout.LabelField("투사체 프리팹 정보", EditorStyles.miniBoldLabel);
                    EditorGUILayout.PropertyField(ProjectilePrefabName);
                    EditorGUILayout.PropertyField(projectileForce);
                    EditorGUILayout.Space();
                    EditorGUILayout.PropertyField(usingLevelSystem);
                    if (skill.usingLevelSystem)
                    {
                        EditorGUILayout.PropertyField(skillCurLevel);
                        EditorGUILayout.PropertyField(skillMaxLevel);
                        EditorGUILayout.LabelField("스킬 레벨당 데미지 증가값", EditorStyles.miniBoldLabel);
                        EditorGUILayout.PropertyField(skillIncreaseValue);
                    }
                }
                
                
                EditorGUILayout.Space();
                showCost = EditorGUILayout.Foldout(showCost, "코스트 및 부가 효과", true);
                if (showCost)
                {
                    EditorGUILayout.PropertyField(hpCost);
                    EditorGUILayout.PropertyField(mpCost);
                    EditorGUILayout.PropertyField(onSuperArmor);
                    EditorGUILayout.PropertyField(useKnockBack);
                    if (skill.useKnockBack)
                    {
                        EditorGUILayout.PropertyField(knockBackForce);
                    }
                }

                EditorGUILayout.Space();
                showEffect = EditorGUILayout.Foldout(showEffect, "스킬 연출", true);
                if (showEffect)
                {
                    EditorGUILayout.PropertyField(doSkillCasting);
                    if (skill.skillCasting)
                    {
                        EditorGUILayout.PropertyField(skillCastingTime);
                    }

                    EditorGUILayout.PropertyField(canMove);
                    EditorGUILayout.PropertyField(skillEffectName);
                    EditorGUILayout.PropertyField(skillEffectPos);
                    EditorGUILayout.PropertyField(hitEffectName);
                    EditorGUILayout.PropertyField(hitEffectPos);
                    EditorGUILayout.PropertyField(skillSFX);
                    if (skillSFX != null)
                    {
                        if (GUILayout.Button("효과음 재생"))
                        {
                            if (Application.isPlaying == true)
                            {
                                Debug.LogWarning("플레이모드에선 재생할 수 없습니다.");
                                return;
                            }
                            if (Application.isPlaying == false && skill.skillSFX == null)
                            {
                                Debug.LogWarning($"skillSFX가 {skill.skillSFX} 입니다.");
                                return;
                            }
                            PlayClip(skill.skillSFX);
                            Debug.Log($"효과음 재생 {skill.skillSFX}");
                        }
                    }
                }

                EditorGUILayout.Space();
                showCamera = EditorGUILayout.Foldout(showCamera, "카메라 연출", true);
                if (showCamera)
                {
                    EditorGUILayout.PropertyField(useCameraShake);
                    if (skill.useCameraShake)
                    {
                        EditorGUILayout.PropertyField(shakeCameraForce);
                        EditorGUILayout.PropertyField(shakeCameraDir);
                        EditorGUILayout.PropertyField(shakeCameraDuration);
                    }
                }
                break;






            case SkillType.AOE:
                showBasic = EditorGUILayout.Foldout(showBasic, "AOE타입 기본 정보", true);
                if (showBasic)
                {
                    EditorGUILayout.PropertyField(TargetLayer);
                    EditorGUILayout.PropertyField(skillIcon);
                    EditorGUILayout.PropertyField(skillName);
                    EditorGUILayout.PropertyField(skillDescription);
                    EditorGUILayout.PropertyField(coolTime);
                    EditorGUILayout.PropertyField(damage);
                    EditorGUILayout.PropertyField(hitCount);
                    EditorGUILayout.PropertyField(skillHitType);
                    EditorGUILayout.PropertyField(skillElement);
                    EditorGUILayout.PropertyField(specialAbility);
                    if (skill.specialAbility == SkillSpecialAbility.Buff)
                    {
                        EditorGUILayout.PropertyField(setBuffType);
                    }
                    else if (skill.specialAbility == SkillSpecialAbility.DeBuff)
                    {
                        EditorGUILayout.PropertyField(setDebuffType);
                    }

                    EditorGUILayout.Space();

                    EditorGUILayout.LabelField("스킬 범위", EditorStyles.boldLabel);
                    EditorGUILayout.PropertyField(startSkillPoint);
                    EditorGUILayout.PropertyField(skillRange);
                    EditorGUILayout.Space();
                    EditorGUILayout.PropertyField(usingLevelSystem);
                    if (skill.usingLevelSystem)
                    {
                        EditorGUILayout.PropertyField(skillCurLevel);
                        EditorGUILayout.PropertyField(skillMaxLevel);
                        EditorGUILayout.LabelField("스킬 레벨당 데미지 증가값", EditorStyles.miniBoldLabel);
                        EditorGUILayout.PropertyField(skillIncreaseValue);
                    }
                }



                EditorGUILayout.Space();
                showCost = EditorGUILayout.Foldout(showCost, "코스트 및 부가 효과", true);
                if (showCost)
                {
                    EditorGUILayout.PropertyField(hpCost);
                    EditorGUILayout.PropertyField(mpCost);
                    EditorGUILayout.PropertyField(canMove);
                    EditorGUILayout.PropertyField(onSuperArmor);
                    EditorGUILayout.PropertyField(useKnockBack);
                    if (skill.useKnockBack)
                    {
                        EditorGUILayout.PropertyField(knockBackForce);
                    }
                }
                EditorGUILayout.Space();
                showEffect = EditorGUILayout.Foldout(showEffect, "스킬 연출", true);
                if (showEffect)
                {
                    EditorGUILayout.PropertyField(doSkillCasting);
                    if (skill.skillCasting)
                    {
                        EditorGUILayout.PropertyField(skillCastingTime);
                    }

                    EditorGUILayout.PropertyField(skillEffectName);
                    EditorGUILayout.PropertyField(skillEffectPos);
                    EditorGUILayout.PropertyField(hitEffectName);
                    EditorGUILayout.PropertyField(hitEffectPos);
                    EditorGUILayout.PropertyField(skillSFX);
                    if (skillSFX != null)
                    {
                        if (GUILayout.Button("효과음 재생"))
                        {
                            if (Application.isPlaying == true)
                            {
                                Debug.LogWarning("플레이모드에선 재생할 수 없습니다.");
                                return;
                            }
                            if (Application.isPlaying == false && skill.skillSFX == null)
                            {
                                Debug.LogWarning($"skillSFX가 {skill.skillSFX} 입니다.");
                                return;
                            }
                            PlayClip(skill.skillSFX);
                            Debug.Log($"효과음 재생 {skill.skillSFX}");
                        }
                    }
                }

                EditorGUILayout.Space();
                showCamera = EditorGUILayout.Foldout(showCamera, "카메라 연출", true);
                if (showCamera)
                {
                    EditorGUILayout.PropertyField(useCameraShake);
                    if (skill.useCameraShake)
                    {
                        EditorGUILayout.PropertyField(shakeCameraForce);
                        EditorGUILayout.PropertyField(shakeCameraDir);
                        EditorGUILayout.PropertyField(shakeCameraDuration);
                    }
                }
                break;
        }
        serializedObject.ApplyModifiedProperties();
    }
    private static MethodInfo playClipMethod;
    private static void PlayClip(AudioClip clip)
    {
        #if UNITY_EDITOR
        if (clip == null) return;

        if (playClipMethod == null)
        {
            var audioUtil = typeof(AudioImporter).Assembly.GetType("UnityEditor.AudioUtil");
            playClipMethod = audioUtil.GetMethod("PlayClip", BindingFlags.Static | BindingFlags.Public, null, new Type[] { typeof(AudioClip) }, null);
        }

        playClipMethod?.Invoke(null, new object[] { clip });
        #endif
    }
}
