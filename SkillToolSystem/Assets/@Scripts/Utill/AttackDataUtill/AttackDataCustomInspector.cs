using System;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
[CustomEditor(typeof(AttackData))]
public class AttackDataCustomInspector : Editor
{
    private bool showBasic = true;
    private bool showCost = true;
    private bool showEffect = true;
    private bool showCamera = true;
    #region PropertyField
    SerializedProperty attackType; // 스킬타입 대전제
    SerializedProperty TargetLayer; // 스킬 타겟레이어

    // ** 스킬 기본 정보 ** // 
    //- 스킬 이름
    SerializedProperty attackName;

    //- 공격자 무기타입

    SerializedProperty weaponType;

    //- 스킬 설명
    SerializedProperty attackDescription;

    //- 스킬 아이콘
    SerializedProperty attackIcon;

    //- 쿨타임
    SerializedProperty coolTime;

    //- 데미지
    SerializedProperty damage;

    //- 히트수
    SerializedProperty hitCount;

    // 다중, 단일타겟
    SerializedProperty attackHitType;

    // 공격 속성
    SerializedProperty attackElement;

    // 스킬 특수 능력
    SerializedProperty specialAbility;

    // 버프 타입
    SerializedProperty setBuffType;

    // 디버프 타입
    SerializedProperty setDebuffType;

    // 스킬 범위
    SerializedProperty startAttackPoint;
    SerializedProperty attackRange;

    // ----------------------------------------------

    // ** 스킬 레벨시스템 디자인 ** //

    //- 스킬 레벨 시스템 사용 여부
    SerializedProperty usingLevelSystem;

    //- 스킬 현재레벨
    SerializedProperty attackCurLevel;

    //- 스킬 최대레벨
    SerializedProperty attackMaxLevel;

    //- 스킬 레벨당 데미지 상승치
    SerializedProperty attackIncreaseValue;

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
    SerializedProperty doAttackCasting;
    SerializedProperty attackCastingTime;

    // ** 투사체
    SerializedProperty ProjectilePrefabName;
    SerializedProperty projectileForce;

    // ** 스킬 연출 ** // 
    
    //- 스킬 시전 사운드
    SerializedProperty attackSFX;

    //- 스킬 이펙트
    SerializedProperty attackEffectName;
    SerializedProperty attackEffectPos;

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

        
        attackType = serializedObject.FindProperty("attackType");
        weaponType = serializedObject.FindProperty("weaponType");
        attackDescription = serializedObject.FindProperty("attackDescription");
        attackIcon = serializedObject.FindProperty("AttackIcon");
        TargetLayer = serializedObject.FindProperty("TargetLayer");
        attackName = serializedObject.FindProperty("attackName");
        coolTime = serializedObject.FindProperty("coolTime");
        damage = serializedObject.FindProperty("damage");
        hitCount = serializedObject.FindProperty("hitCount");
        attackHitType = serializedObject.FindProperty("attackHitType");
        attackElement = serializedObject.FindProperty("attackElement");
        specialAbility = serializedObject.FindProperty("specialAbility");
        setBuffType = serializedObject.FindProperty("setBuffType");
        setDebuffType = serializedObject.FindProperty("setDebuffType");
        startAttackPoint = serializedObject.FindProperty("startAttackPoint");
        attackRange = serializedObject.FindProperty("attackRange");

        usingLevelSystem = serializedObject.FindProperty("usingLevelSystem");
        attackCurLevel = serializedObject.FindProperty("attackCurLevel");
        attackMaxLevel = serializedObject.FindProperty("attackMaxLevel");
        attackIncreaseValue = serializedObject.FindProperty("levelIncreaseValue");

        hpCost = serializedObject.FindProperty("hp");
        mpCost = serializedObject.FindProperty("mp");
        canMove = serializedObject.FindProperty("canMove");
        onSuperArmor = serializedObject.FindProperty("superArmor");

        useKnockBack = serializedObject.FindProperty("useKnockBack");
        knockBackForce = serializedObject.FindProperty("knockBackForce");

        doAttackCasting = serializedObject.FindProperty("attackCasting");
        attackCastingTime = serializedObject.FindProperty("castingTime");

        ProjectilePrefabName = serializedObject.FindProperty("ProjectilePrefabName");
        projectileForce = serializedObject.FindProperty("projectileForce");

        attackSFX = serializedObject.FindProperty("attackSFX");
        attackEffectName = serializedObject.FindProperty("attackEffectPrefabName");
        attackEffectPos = serializedObject.FindProperty("effectPos");
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

        AttackData aData = (AttackData)target;
        if (aData == null)
        {
            Debug.LogError("attackData is null!");
            return;
        }
        serializedObject.Update();
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(attackType);
        switch (aData.attackType)
        {
            //근접 공격 타입 인스펙터
            #region MeleeAttackType
            case AttackType.Melee:
                showBasic = EditorGUILayout.Foldout(showBasic, "근접타입 기본 정보", true);
                if (showBasic)
                {
                    EditorGUILayout.PropertyField(TargetLayer);
                    EditorGUILayout.PropertyField(weaponType);
                    EditorGUILayout.PropertyField(attackIcon);
                    EditorGUILayout.PropertyField(attackName);
                    EditorGUILayout.PropertyField(attackDescription);
                    EditorGUILayout.PropertyField(coolTime);
                    EditorGUILayout.PropertyField(damage);
                    EditorGUILayout.PropertyField(hitCount);
                    EditorGUILayout.PropertyField(attackHitType);
                    EditorGUILayout.PropertyField(attackElement);
                    EditorGUILayout.PropertyField(specialAbility);
                    if (aData.specialAbility == AttackSpecialAbility.Buff)
                    {
                        EditorGUILayout.PropertyField(setBuffType);
                    }
                    else if (aData.specialAbility == AttackSpecialAbility.DeBuff)
                    {
                        EditorGUILayout.PropertyField(setDebuffType);
                    }

                    EditorGUILayout.Space();

                    EditorGUILayout.LabelField("공격 범위", EditorStyles.boldLabel);
                    EditorGUILayout.PropertyField(startAttackPoint);
                    EditorGUILayout.PropertyField(attackRange);
                    EditorGUILayout.Space();
                    EditorGUILayout.PropertyField(usingLevelSystem);
                    if (aData.usingLevelSystem)
                    {
                        EditorGUILayout.PropertyField(attackCurLevel);
                        EditorGUILayout.PropertyField(attackMaxLevel);
                        EditorGUILayout.LabelField("공격데이터 레벨당 데미지 증가값", EditorStyles.miniBoldLabel);
                        EditorGUILayout.PropertyField(attackIncreaseValue);
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
                    if (aData.useKnockBack)
                    {
                        EditorGUILayout.PropertyField(knockBackForce);
                    }
                }
                EditorGUILayout.Space();
                showEffect = EditorGUILayout.Foldout(showEffect, "스킬 연출", true);
                if (showEffect)
                {
                    EditorGUILayout.PropertyField(doAttackCasting);
                    if (aData.attackCasting)
                    {
                        EditorGUILayout.PropertyField(attackCastingTime);
                    }

                    EditorGUILayout.PropertyField(attackEffectName);
                    EditorGUILayout.PropertyField(attackEffectPos);
                    EditorGUILayout.PropertyField(hitEffectName);
                    EditorGUILayout.PropertyField(hitEffectPos);
                    EditorGUILayout.PropertyField(attackSFX);
                    if (attackSFX != null)
                    {
                        if (GUILayout.Button("효과음 재생"))
                        {
                            if (Application.isPlaying == true)
                            {
                                Debug.LogWarning("플레이모드에선 재생할 수 없습니다.");
                                return;
                            }
                            if (Application.isPlaying == false && aData.attackSFX == null)
                            {
                                Debug.LogWarning($"attackSFX가 {aData.attackSFX} 입니다.");
                                return;
                            }
                            OnEditorModePlayClip(aData.attackSFX);
                            Debug.Log($"효과음 재생 {aData.attackSFX}");
                        }
                    }
                }

                EditorGUILayout.Space();
                showCamera = EditorGUILayout.Foldout(showCamera, "카메라 연출", true);
                if (showCamera)
                {
                    EditorGUILayout.PropertyField(useCameraShake);
                    if (aData.useCameraShake)
                    {
                        EditorGUILayout.PropertyField(shakeCameraForce);
                        EditorGUILayout.PropertyField(shakeCameraDir);
                        EditorGUILayout.PropertyField(shakeCameraDuration);
                    }
                }
                break;

            #endregion 
                
            //원거리 공격 타입 인스펙터
            #region RangeAttackCase
            case AttackType.Range:
                showBasic = EditorGUILayout.Foldout(showBasic, "투사체타입 기본 정보", true);
                if (showBasic)
                {
                    EditorGUILayout.PropertyField(TargetLayer);
                    EditorGUILayout.PropertyField(weaponType);
                    EditorGUILayout.PropertyField(attackIcon);
                    EditorGUILayout.PropertyField(attackName);
                    EditorGUILayout.PropertyField(attackDescription);
                    EditorGUILayout.PropertyField(coolTime);
                    EditorGUILayout.PropertyField(damage);
                    EditorGUILayout.PropertyField(hitCount);
                    EditorGUILayout.PropertyField(attackHitType);
                    EditorGUILayout.PropertyField(attackElement);
                    EditorGUILayout.PropertyField(specialAbility);
                    if (aData.specialAbility == AttackSpecialAbility.Buff)
                    {
                        EditorGUILayout.PropertyField(setBuffType);
                    }
                    else if (aData.specialAbility == AttackSpecialAbility.DeBuff)
                    {
                        EditorGUILayout.PropertyField(setDebuffType);
                    }
                    EditorGUILayout.Space();
                    EditorGUILayout.LabelField("투사체 프리팹 정보", EditorStyles.miniBoldLabel);
                    EditorGUILayout.PropertyField(ProjectilePrefabName);
                    EditorGUILayout.PropertyField(projectileForce);
                    EditorGUILayout.Space();
                    EditorGUILayout.PropertyField(usingLevelSystem);
                    if (aData.usingLevelSystem)
                    {
                        EditorGUILayout.PropertyField(attackCurLevel);
                        EditorGUILayout.PropertyField(attackMaxLevel);
                        EditorGUILayout.LabelField("스킬 레벨당 데미지 증가값", EditorStyles.miniBoldLabel);
                        EditorGUILayout.PropertyField(attackIncreaseValue);
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
                    if (aData.useKnockBack)
                    {
                        EditorGUILayout.PropertyField(knockBackForce);
                    }
                }

                EditorGUILayout.Space();
                showEffect = EditorGUILayout.Foldout(showEffect, "스킬 연출", true);
                if (showEffect)
                {
                    EditorGUILayout.PropertyField(doAttackCasting);
                    if (aData.attackCasting)
                    {
                        EditorGUILayout.PropertyField(attackCastingTime);
                    }

                    EditorGUILayout.PropertyField(canMove);
                    EditorGUILayout.PropertyField(attackEffectName);
                    EditorGUILayout.PropertyField(attackEffectPos);
                    EditorGUILayout.PropertyField(hitEffectName);
                    EditorGUILayout.PropertyField(hitEffectPos);
                    EditorGUILayout.PropertyField(attackSFX);
                    if (attackSFX != null)
                    {
                        if (GUILayout.Button("효과음 재생"))
                        {
                            if (Application.isPlaying == true)
                            {
                                Debug.LogWarning("플레이모드에선 재생할 수 없습니다.");
                                return;
                            }
                            if (Application.isPlaying == false && aData.attackSFX == null)
                            {
                                Debug.LogWarning($"skillSFX가 {aData.attackSFX} 입니다.");
                                return;
                            }
                            OnEditorModePlayClip(aData.attackSFX);
                            Debug.Log($"효과음 재생 {aData.attackSFX}");
                        }
                    }
                }

                EditorGUILayout.Space();
                showCamera = EditorGUILayout.Foldout(showCamera, "카메라 연출", true);
                if (showCamera)
                {
                    EditorGUILayout.PropertyField(useCameraShake);
                    if (aData.useCameraShake)
                    {
                        EditorGUILayout.PropertyField(shakeCameraForce);
                        EditorGUILayout.PropertyField(shakeCameraDir);
                        EditorGUILayout.PropertyField(shakeCameraDuration);
                    }
                }
                break;

            #endregion

            //범위 공격 타입 인스펙터
            #region AOEAttackType
            case AttackType.AOE:
                showBasic = EditorGUILayout.Foldout(showBasic, "AOE타입 기본 정보", true);
                if (showBasic)
                {
                    EditorGUILayout.PropertyField(TargetLayer);
                    EditorGUILayout.PropertyField(weaponType);
                    EditorGUILayout.PropertyField(attackIcon);
                    EditorGUILayout.PropertyField(attackName);
                    EditorGUILayout.PropertyField(attackDescription);
                    EditorGUILayout.PropertyField(coolTime);
                    EditorGUILayout.PropertyField(damage);
                    EditorGUILayout.PropertyField(hitCount);
                    EditorGUILayout.PropertyField(attackHitType);
                    EditorGUILayout.PropertyField(attackElement);
                    EditorGUILayout.PropertyField(specialAbility);
                    if (aData.specialAbility == AttackSpecialAbility.Buff)
                    {
                        EditorGUILayout.PropertyField(setBuffType);
                    }
                    else if (aData.specialAbility == AttackSpecialAbility.DeBuff)
                    {
                        EditorGUILayout.PropertyField(setDebuffType);
                    }

                    EditorGUILayout.Space();

                    EditorGUILayout.LabelField("스킬 범위", EditorStyles.boldLabel);
                    EditorGUILayout.PropertyField(startAttackPoint);
                    EditorGUILayout.PropertyField(attackRange);
                    EditorGUILayout.Space();
                    EditorGUILayout.PropertyField(usingLevelSystem);
                    if (aData.usingLevelSystem)
                    {
                        EditorGUILayout.PropertyField(attackCurLevel);
                        EditorGUILayout.PropertyField(attackMaxLevel);
                        EditorGUILayout.LabelField("스킬 레벨당 데미지 증가값", EditorStyles.miniBoldLabel);
                        EditorGUILayout.PropertyField(attackIncreaseValue);
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
                    if (aData.useKnockBack)
                    {
                        EditorGUILayout.PropertyField(knockBackForce);
                    }
                }
                EditorGUILayout.Space();
                showEffect = EditorGUILayout.Foldout(showEffect, "스킬 연출", true);
                if (showEffect)
                {
                    EditorGUILayout.PropertyField(doAttackCasting);
                    if (aData.attackCasting)
                    {
                        EditorGUILayout.PropertyField(attackCastingTime);
                    }

                    EditorGUILayout.PropertyField(attackEffectName);
                    EditorGUILayout.PropertyField(attackEffectPos);
                    EditorGUILayout.PropertyField(hitEffectName);
                    EditorGUILayout.PropertyField(hitEffectPos);
                    EditorGUILayout.PropertyField(attackSFX);
                    if (attackSFX != null)
                    {
                        if (GUILayout.Button("효과음 재생"))
                        {
                            if (Application.isPlaying == true)
                            {
                                Debug.LogWarning("플레이모드에선 재생할 수 없습니다.");
                                return;
                            }
                            if (Application.isPlaying == false && aData.attackSFX == null)
                            {
                                Debug.LogWarning($"skillSFX가 {aData.attackSFX} 입니다.");
                                return;
                            }
                            OnEditorModePlayClip(aData.attackSFX);
                            Debug.Log($"효과음 재생 {aData.attackSFX}");
                        }
                    }
                }

                EditorGUILayout.Space();
                showCamera = EditorGUILayout.Foldout(showCamera, "카메라 연출", true);
                if (showCamera)
                {
                    EditorGUILayout.PropertyField(useCameraShake);
                    if (aData.useCameraShake)
                    {
                        EditorGUILayout.PropertyField(shakeCameraForce);
                        EditorGUILayout.PropertyField(shakeCameraDir);
                        EditorGUILayout.PropertyField(shakeCameraDuration);
                    }
                }
                break;
                #endregion
        }
        serializedObject.ApplyModifiedProperties();
    }
    private static MethodInfo playClipMethod;
    
    // 에디터모드에서 효과음 재생
    private static void OnEditorModePlayClip(AudioClip clip)
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
