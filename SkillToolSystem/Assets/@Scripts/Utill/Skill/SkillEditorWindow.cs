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
        GUILayout.Label("스킬 데이터 편집기", EditorStyles.boldLabel);

        selectedSkill = (SkillData)EditorGUILayout.ObjectField("스킬 선택", selectedSkill, typeof(SkillData), false);

        if (selectedSkill != null)
        {
            EditorGUI.BeginChangeCheck();

            selectedSkill.skillName = EditorGUILayout.TextField("이름", selectedSkill.skillName);
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(selectedSkill);
            }
        }
        else
        {
            EditorGUILayout.HelpBox("편집할 SkillData 파일을 선택하세요.", MessageType.Info);
        }
    }
}
