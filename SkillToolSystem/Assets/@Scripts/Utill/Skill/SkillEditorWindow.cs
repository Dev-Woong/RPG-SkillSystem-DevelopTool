using UnityEngine;
using UnityEditor;
public class SkillEditorWindow : EditorWindow
{
    private SkillData selectedSkill;

    [MenuItem("Tools/Skill Editor")]
    public static void OpenWindow()
    {
        GetWindow<SkillEditorWindow>("Skill Editor");
    }

    private void OnGUI()
    {
        GUILayout.Label("��ų ������ ������", EditorStyles.boldLabel);

        selectedSkill = (SkillData)EditorGUILayout.ObjectField("��ų ����", selectedSkill, typeof(SkillData), false);

        if (selectedSkill != null)
        {
            EditorGUI.BeginChangeCheck();

            selectedSkill.skillName = EditorGUILayout.TextField("�̸�", selectedSkill.skillName);
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(selectedSkill);
            }
        }
        else
        {
            EditorGUILayout.HelpBox("������ SkillData ������ �����ϼ���.", MessageType.Info);
        }
    }
}
