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
    
    public override void OnInspectorGUI()
    {
        SkillData skill = (SkillData)target;

        serializedObject.Update();
        EditorGUILayout.Space();
        showBasic = EditorGUILayout.Foldout(showBasic, "기본 정보", true);
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
            switch (skill.specialAbility)
            {
                case SkillSpecialAbility.Buff:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("setBuffType"));
                    break;
                case SkillSpecialAbility.DeBuff:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("setDebuffType"));
                    break;
            }
        }

        EditorGUILayout.Space();
        showCost = EditorGUILayout.Foldout(showCost, "코스트 및 부가 효과", true);
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
        showEffect = EditorGUILayout.Foldout(showEffect, "스킬 연출", true);
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
            EditorGUILayout.PropertyField(serializedObject.FindProperty("isCameraShake"));
            if (skill.isCameraShake)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("shakePower"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("shakeDir"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("shakeDuration"));
            }
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("스킬 범위", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("startSkillPoint"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("skillRange"));

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
