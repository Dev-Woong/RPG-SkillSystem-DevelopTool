using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(SkillData))]
public class SkillDataCustomEditor : Editor
{
    private bool showBasic = true;
    private bool showCost = true;
    private bool showEffect = true;
    private bool showCamera = true;
    
    public override void OnInspectorGUI()
    {
        SkillData skill = (SkillData)target;

        serializedObject.Update();
        EditorGUILayout.Space();
        showBasic = EditorGUILayout.Foldout(showBasic, "�⺻ ����", true);
        if (showBasic)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("TargetLayer"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("skillName"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("coolTime"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("damage"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("hitCount"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("skillHitType"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("skillRangeType"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("skillElement"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("specialAbility"));
        }

        EditorGUILayout.Space();
        showCost = EditorGUILayout.Foldout(showCost, "�ڽ�Ʈ �� �ΰ� ȿ��", true);
        if (showCost)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("hp"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("mp"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("superArmor"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("knockBack"));
            if (skill.knockBack)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("knockBackForce"));
            }
        }

        EditorGUILayout.Space();
        showEffect = EditorGUILayout.Foldout(showEffect, "��ų ����", true);
        if (showEffect)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("skillCasting"));
            if (skill.skillCasting)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("castingTime"));
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("onSkillMove"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("SkillEffect"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("effectPos"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("HitEffect"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("hitEffectPos"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("skillSFX"));
            if (serializedObject.FindProperty("skillSFX") != null)
            {
                if (GUILayout.Button("ȿ���� ���"))
                {
                    
                }

            }
        }

        EditorGUILayout.Space();
        showCamera = EditorGUILayout.Foldout(showCamera, "ī�޶� ����", true);
        if (showCamera)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("isCameraShake"));
            if (skill.isCameraShake)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("shakePower"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("shakeDir"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("shakeDuration"));
            }
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("��ų ����", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("startSkillPoint"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("skillRange"));

        serializedObject.ApplyModifiedProperties();
    }
}