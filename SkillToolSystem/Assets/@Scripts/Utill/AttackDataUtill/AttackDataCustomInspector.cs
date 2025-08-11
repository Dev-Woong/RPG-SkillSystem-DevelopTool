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
    SerializedProperty attackType; // ��ųŸ�� ������
    SerializedProperty TargetLayer; // ��ų Ÿ�ٷ��̾�

    // ** ��ų �⺻ ���� ** // 
    //- ��ų �̸�
    SerializedProperty attackName;

    //- ������ ����Ÿ��

    SerializedProperty weaponType;

    //- ��ų ����
    SerializedProperty attackDescription;

    //- ��ų ������
    SerializedProperty attackIcon;

    //- ��Ÿ��
    SerializedProperty coolTime;

    //- ������
    SerializedProperty damage;

    //- ��Ʈ��
    SerializedProperty hitCount;

    // ����, ����Ÿ��
    SerializedProperty attackHitType;

    // ���� �Ӽ�
    SerializedProperty attackElement;

    // ��ų Ư�� �ɷ�
    SerializedProperty specialAbility;

    // ���� Ÿ��
    SerializedProperty setBuffType;

    // ����� Ÿ��
    SerializedProperty setDebuffType;

    // ��ų ����
    SerializedProperty startAttackPoint;
    SerializedProperty attackRange;

    // ----------------------------------------------

    // ** ��ų �����ý��� ������ ** //

    //- ��ų ���� �ý��� ��� ����
    SerializedProperty usingLevelSystem;

    //- ��ų ���緹��
    SerializedProperty attackCurLevel;

    //- ��ų �ִ뷹��
    SerializedProperty attackMaxLevel;

    //- ��ų ������ ������ ���ġ
    SerializedProperty attackIncreaseValue;

    // ----------------------------------------------

    // ** �Ҹ��ڽ�Ʈ  
    SerializedProperty hpCost;
    SerializedProperty mpCost;

    // ** ��ų ����� �̵�����
    SerializedProperty canMove;

    // ** ���۾Ƹ�
    SerializedProperty onSuperArmor;

    // ** �˹�
    SerializedProperty useKnockBack;
    SerializedProperty knockBackForce;

    // ** ��ų ĳ����
    SerializedProperty doAttackCasting;
    SerializedProperty attackCastingTime;

    // ** ����ü
    SerializedProperty ProjectilePrefabName;
    SerializedProperty projectileForce;

    // ** ��ų ���� ** // 
    
    //- ��ų ���� ����
    SerializedProperty attackSFX;

    //- ��ų ����Ʈ
    SerializedProperty attackEffectName;
    SerializedProperty attackEffectPos;

    //- Ÿ�� ����Ʈ
    SerializedProperty hitEffectName;
    SerializedProperty hitEffectPos;

    // ----------------------------------------------

    // ** ī�޶� ���� ** //

    //- ī�޶� ���� ��뿩��
    SerializedProperty useCameraShake;

    //- ī�޶� ����ũ ��
    SerializedProperty shakeCameraForce;

    //- ī�޶� ����ũ ����
    SerializedProperty shakeCameraDir;

    //- ī�޶� ����ũ �����ð�
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
            //���� ���� Ÿ�� �ν�����
            #region MeleeAttackType
            case AttackType.Melee:
                showBasic = EditorGUILayout.Foldout(showBasic, "����Ÿ�� �⺻ ����", true);
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

                    EditorGUILayout.LabelField("���� ����", EditorStyles.boldLabel);
                    EditorGUILayout.PropertyField(startAttackPoint);
                    EditorGUILayout.PropertyField(attackRange);
                    EditorGUILayout.Space();
                    EditorGUILayout.PropertyField(usingLevelSystem);
                    if (aData.usingLevelSystem)
                    {
                        EditorGUILayout.PropertyField(attackCurLevel);
                        EditorGUILayout.PropertyField(attackMaxLevel);
                        EditorGUILayout.LabelField("���ݵ����� ������ ������ ������", EditorStyles.miniBoldLabel);
                        EditorGUILayout.PropertyField(attackIncreaseValue);
                    }
                }
                EditorGUILayout.Space();
                showCost = EditorGUILayout.Foldout(showCost, "�ڽ�Ʈ �� �ΰ� ȿ��", true);
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
                showEffect = EditorGUILayout.Foldout(showEffect, "��ų ����", true);
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
                        if (GUILayout.Button("ȿ���� ���"))
                        {
                            if (Application.isPlaying == true)
                            {
                                Debug.LogWarning("�÷��̸�忡�� ����� �� �����ϴ�.");
                                return;
                            }
                            if (Application.isPlaying == false && aData.attackSFX == null)
                            {
                                Debug.LogWarning($"attackSFX�� {aData.attackSFX} �Դϴ�.");
                                return;
                            }
                            OnEditorModePlayClip(aData.attackSFX);
                            Debug.Log($"ȿ���� ��� {aData.attackSFX}");
                        }
                    }
                }

                EditorGUILayout.Space();
                showCamera = EditorGUILayout.Foldout(showCamera, "ī�޶� ����", true);
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
                
            //���Ÿ� ���� Ÿ�� �ν�����
            #region RangeAttackCase
            case AttackType.Range:
                showBasic = EditorGUILayout.Foldout(showBasic, "����üŸ�� �⺻ ����", true);
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
                    EditorGUILayout.LabelField("����ü ������ ����", EditorStyles.miniBoldLabel);
                    EditorGUILayout.PropertyField(ProjectilePrefabName);
                    EditorGUILayout.PropertyField(projectileForce);
                    EditorGUILayout.Space();
                    EditorGUILayout.PropertyField(usingLevelSystem);
                    if (aData.usingLevelSystem)
                    {
                        EditorGUILayout.PropertyField(attackCurLevel);
                        EditorGUILayout.PropertyField(attackMaxLevel);
                        EditorGUILayout.LabelField("��ų ������ ������ ������", EditorStyles.miniBoldLabel);
                        EditorGUILayout.PropertyField(attackIncreaseValue);
                    }
                }
                
                
                EditorGUILayout.Space();
                showCost = EditorGUILayout.Foldout(showCost, "�ڽ�Ʈ �� �ΰ� ȿ��", true);
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
                showEffect = EditorGUILayout.Foldout(showEffect, "��ų ����", true);
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
                        if (GUILayout.Button("ȿ���� ���"))
                        {
                            if (Application.isPlaying == true)
                            {
                                Debug.LogWarning("�÷��̸�忡�� ����� �� �����ϴ�.");
                                return;
                            }
                            if (Application.isPlaying == false && aData.attackSFX == null)
                            {
                                Debug.LogWarning($"skillSFX�� {aData.attackSFX} �Դϴ�.");
                                return;
                            }
                            OnEditorModePlayClip(aData.attackSFX);
                            Debug.Log($"ȿ���� ��� {aData.attackSFX}");
                        }
                    }
                }

                EditorGUILayout.Space();
                showCamera = EditorGUILayout.Foldout(showCamera, "ī�޶� ����", true);
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

            //���� ���� Ÿ�� �ν�����
            #region AOEAttackType
            case AttackType.AOE:
                showBasic = EditorGUILayout.Foldout(showBasic, "AOEŸ�� �⺻ ����", true);
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

                    EditorGUILayout.LabelField("��ų ����", EditorStyles.boldLabel);
                    EditorGUILayout.PropertyField(startAttackPoint);
                    EditorGUILayout.PropertyField(attackRange);
                    EditorGUILayout.Space();
                    EditorGUILayout.PropertyField(usingLevelSystem);
                    if (aData.usingLevelSystem)
                    {
                        EditorGUILayout.PropertyField(attackCurLevel);
                        EditorGUILayout.PropertyField(attackMaxLevel);
                        EditorGUILayout.LabelField("��ų ������ ������ ������", EditorStyles.miniBoldLabel);
                        EditorGUILayout.PropertyField(attackIncreaseValue);
                    }
                }



                EditorGUILayout.Space();
                showCost = EditorGUILayout.Foldout(showCost, "�ڽ�Ʈ �� �ΰ� ȿ��", true);
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
                showEffect = EditorGUILayout.Foldout(showEffect, "��ų ����", true);
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
                        if (GUILayout.Button("ȿ���� ���"))
                        {
                            if (Application.isPlaying == true)
                            {
                                Debug.LogWarning("�÷��̸�忡�� ����� �� �����ϴ�.");
                                return;
                            }
                            if (Application.isPlaying == false && aData.attackSFX == null)
                            {
                                Debug.LogWarning($"skillSFX�� {aData.attackSFX} �Դϴ�.");
                                return;
                            }
                            OnEditorModePlayClip(aData.attackSFX);
                            Debug.Log($"ȿ���� ��� {aData.attackSFX}");
                        }
                    }
                }

                EditorGUILayout.Space();
                showCamera = EditorGUILayout.Foldout(showCamera, "ī�޶� ����", true);
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
    
    // �����͸�忡�� ȿ���� ���
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
