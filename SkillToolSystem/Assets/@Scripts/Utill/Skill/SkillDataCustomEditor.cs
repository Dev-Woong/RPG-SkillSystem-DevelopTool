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
    SerializedProperty skillType; // ��ųŸ�� ������
    SerializedProperty TargetLayer; // ��ų Ÿ�ٷ��̾�

    // ** ��ų �⺻ ���� ** // 
    //- ��ų �̸�
    SerializedProperty skillName;

    //- ��ų ����
    SerializedProperty skillDescription;

    //- ��ų ������
    SerializedProperty skillIcon;

    //- ��Ÿ��
    SerializedProperty coolTime;

    //- ������
    SerializedProperty damage;

    //- ��Ʈ��
    SerializedProperty hitCount;

    // ����, ����Ÿ��
    SerializedProperty skillHitType;

    // ���� �Ӽ�
    SerializedProperty skillElement;

    // ��ų Ư�� �ɷ�
    SerializedProperty specialAbility;

    // ���� Ÿ��
    SerializedProperty setBuffType;

    // ����� Ÿ��
    SerializedProperty setDebuffType;

    // ��ų ����
    SerializedProperty startSkillPoint;
    SerializedProperty skillRange;

    // ----------------------------------------------

    // ** ��ų �����ý��� ������ ** //

    //- ��ų ���� �ý��� ��� ����
    SerializedProperty usingLevelSystem;

    //- ��ų ���緹��
    SerializedProperty skillCurLevel;

    //- ��ų �ִ뷹��
    SerializedProperty skillMaxLevel;

    //- ��ų ������ ������ ���ġ
    SerializedProperty skillIncreaseValue;

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
    SerializedProperty doSkillCasting;
    SerializedProperty skillCastingTime;

    // ** ����ü
    SerializedProperty ProjectilePrefabName;
    SerializedProperty projectileForce;

    // ** ��ų ���� ** // 
    
    //- ��ų ���� ����
    SerializedProperty skillSFX;

    //- ��ų ����Ʈ
    SerializedProperty skillEffectName;
    SerializedProperty skillEffectPos;

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
                showBasic = EditorGUILayout.Foldout(showBasic, "����Ÿ�� �⺻ ����", true);
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

                    EditorGUILayout.LabelField("��ų ����", EditorStyles.boldLabel);
                    EditorGUILayout.PropertyField(startSkillPoint);
                    EditorGUILayout.PropertyField(skillRange);
                    EditorGUILayout.Space();
                    EditorGUILayout.PropertyField(usingLevelSystem);
                    if (skill.usingLevelSystem)
                    {
                        EditorGUILayout.PropertyField(skillCurLevel);
                        EditorGUILayout.PropertyField(skillMaxLevel);
                        EditorGUILayout.LabelField("��ų ������ ������ ������", EditorStyles.miniBoldLabel);
                        EditorGUILayout.PropertyField(skillIncreaseValue);
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
                    if (skill.useKnockBack)
                    {
                        EditorGUILayout.PropertyField(knockBackForce);
                    }
                }
                EditorGUILayout.Space();
                showEffect = EditorGUILayout.Foldout(showEffect, "��ų ����", true);
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
                        if (GUILayout.Button("ȿ���� ���"))
                        {
                            if (Application.isPlaying == true)
                            {
                                Debug.LogWarning("�÷��̸�忡�� ����� �� �����ϴ�.");
                                return;
                            }
                            if (Application.isPlaying == false && skill.skillSFX == null)
                            {
                                Debug.LogWarning($"skillSFX�� {skill.skillSFX} �Դϴ�.");
                                return;
                            }
                            PlayClip(skill.skillSFX);
                            Debug.Log($"ȿ���� ��� {skill.skillSFX}");
                        }
                    }
                }

                EditorGUILayout.Space();
                showCamera = EditorGUILayout.Foldout(showCamera, "ī�޶� ����", true);
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
                showBasic = EditorGUILayout.Foldout(showBasic, "����üŸ�� �⺻ ����", true);
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
                    EditorGUILayout.LabelField("����ü ������ ����", EditorStyles.miniBoldLabel);
                    EditorGUILayout.PropertyField(ProjectilePrefabName);
                    EditorGUILayout.PropertyField(projectileForce);
                    EditorGUILayout.Space();
                    EditorGUILayout.PropertyField(usingLevelSystem);
                    if (skill.usingLevelSystem)
                    {
                        EditorGUILayout.PropertyField(skillCurLevel);
                        EditorGUILayout.PropertyField(skillMaxLevel);
                        EditorGUILayout.LabelField("��ų ������ ������ ������", EditorStyles.miniBoldLabel);
                        EditorGUILayout.PropertyField(skillIncreaseValue);
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
                    if (skill.useKnockBack)
                    {
                        EditorGUILayout.PropertyField(knockBackForce);
                    }
                }

                EditorGUILayout.Space();
                showEffect = EditorGUILayout.Foldout(showEffect, "��ų ����", true);
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
                        if (GUILayout.Button("ȿ���� ���"))
                        {
                            if (Application.isPlaying == true)
                            {
                                Debug.LogWarning("�÷��̸�忡�� ����� �� �����ϴ�.");
                                return;
                            }
                            if (Application.isPlaying == false && skill.skillSFX == null)
                            {
                                Debug.LogWarning($"skillSFX�� {skill.skillSFX} �Դϴ�.");
                                return;
                            }
                            PlayClip(skill.skillSFX);
                            Debug.Log($"ȿ���� ��� {skill.skillSFX}");
                        }
                    }
                }

                EditorGUILayout.Space();
                showCamera = EditorGUILayout.Foldout(showCamera, "ī�޶� ����", true);
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
                showBasic = EditorGUILayout.Foldout(showBasic, "AOEŸ�� �⺻ ����", true);
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

                    EditorGUILayout.LabelField("��ų ����", EditorStyles.boldLabel);
                    EditorGUILayout.PropertyField(startSkillPoint);
                    EditorGUILayout.PropertyField(skillRange);
                    EditorGUILayout.Space();
                    EditorGUILayout.PropertyField(usingLevelSystem);
                    if (skill.usingLevelSystem)
                    {
                        EditorGUILayout.PropertyField(skillCurLevel);
                        EditorGUILayout.PropertyField(skillMaxLevel);
                        EditorGUILayout.LabelField("��ų ������ ������ ������", EditorStyles.miniBoldLabel);
                        EditorGUILayout.PropertyField(skillIncreaseValue);
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
                    if (skill.useKnockBack)
                    {
                        EditorGUILayout.PropertyField(knockBackForce);
                    }
                }
                EditorGUILayout.Space();
                showEffect = EditorGUILayout.Foldout(showEffect, "��ų ����", true);
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
                        if (GUILayout.Button("ȿ���� ���"))
                        {
                            if (Application.isPlaying == true)
                            {
                                Debug.LogWarning("�÷��̸�忡�� ����� �� �����ϴ�.");
                                return;
                            }
                            if (Application.isPlaying == false && skill.skillSFX == null)
                            {
                                Debug.LogWarning($"skillSFX�� {skill.skillSFX} �Դϴ�.");
                                return;
                            }
                            PlayClip(skill.skillSFX);
                            Debug.Log($"ȿ���� ��� {skill.skillSFX}");
                        }
                    }
                }

                EditorGUILayout.Space();
                showCamera = EditorGUILayout.Foldout(showCamera, "ī�޶� ����", true);
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
