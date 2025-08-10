using System.Linq;
using UnityEditor;
using UnityEngine;

public class SkillEditorWindow : EditorWindow
{
    private Vector2 listScrollPos;
    private Vector2 inspectorScrollPos;

    private string searchText = "";
    private SkillData[] allSkills;
    private SkillData selectedSkill;

    private SkillType selectedFilter = SkillType.All; // 필터 상태

    [MenuItem("Window/Skill Tool")]
    public static void ShowWindow()
    {
        GetWindow<SkillEditorWindow>("Skill Tool");
    }

    private void OnEnable()
    {
        LoadAllSkills();
    }

    private void LoadAllSkills()
    {
        string[] guids = AssetDatabase.FindAssets("t:SkillData");
        allSkills = guids.Select(g => AssetDatabase.LoadAssetAtPath<SkillData>(AssetDatabase.GUIDToAssetPath(g))).ToArray();
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();

        // ===== 스킬 목록 =====
        GUILayout.BeginVertical(GUILayout.Width(300));

        // 상단 버튼
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("새 스킬 만들기", GUILayout.Height(25)))
            CreateNewSkill();

        if (GUILayout.Button("새로고침", GUILayout.Width(80), GUILayout.Height(25)))
            LoadAllSkills();
        GUILayout.EndHorizontal();

        EditorGUILayout.Space();

        // 카테고리 필터
        GUILayout.BeginHorizontal();
        foreach (SkillType filterType in System.Enum.GetValues(typeof(SkillType)))
        {
            if (GUILayout.Toggle(selectedFilter == filterType, filterType.ToString(), "Button"))
                selectedFilter = filterType;
        }
        GUILayout.EndHorizontal();

        EditorGUILayout.Space();
        searchText = EditorGUILayout.TextField("검색", searchText);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("스킬 목록", EditorStyles.boldLabel);

        // 스킬 리스트
        listScrollPos = GUILayout.BeginScrollView(listScrollPos);
        foreach (var skill in allSkills.Where(s => IsSkillMatchFilter(s) &&
                                                   (string.IsNullOrEmpty(searchText) || s.name.ToLower().Contains(searchText.ToLower()))))
        {
            GUILayout.BeginHorizontal();

            // 선택 버튼
            if (GUILayout.Button(skill.name, (selectedSkill == skill) ? EditorStyles.toolbarButton : EditorStyles.miniButton))
            {
                selectedSkill = skill;
                EditorGUIUtility.PingObject(skill);
            }

            // 삭제 버튼
            GUI.color = Color.red;
            if (GUILayout.Button("X", GUILayout.Width(25)))
            {
                if (EditorUtility.DisplayDialog("스킬 삭제", $"{skill.name} 스킬을 삭제하시겠습니까?", "삭제", "취소"))
                {
                    string path = AssetDatabase.GetAssetPath(skill);
                    AssetDatabase.DeleteAsset(path);
                    AssetDatabase.SaveAssets();
                    LoadAllSkills();

                    if (selectedSkill == skill) selectedSkill = null;
                }
            }
            GUI.color = Color.white;

            GUILayout.EndHorizontal();
        }
        GUILayout.EndScrollView();

        GUILayout.EndVertical();

        // ===== 우측: 인스펙터 =====
        GUILayout.BeginVertical();

        EditorGUILayout.LabelField("선택된 스킬", EditorStyles.boldLabel);
        inspectorScrollPos = GUILayout.BeginScrollView(inspectorScrollPos);

        if (selectedSkill != null)
        {
            Editor editor = Editor.CreateEditor(selectedSkill);
            if (editor != null) editor.OnInspectorGUI();
        }
        else
        {
            EditorGUILayout.HelpBox("스킬을 선택하면 여기에서 편집할 수 있습니다.", MessageType.Info);
        }

        GUILayout.EndScrollView();
        GUILayout.EndVertical();

        GUILayout.EndHorizontal();
    }

    private bool IsSkillMatchFilter(SkillData skill)
    {
        if (selectedFilter == SkillType.All) return true;
        return skill.skillType == selectedFilter;
    }

    private void CreateNewSkill()
    {
        SkillData newSkill = ScriptableObject.CreateInstance<SkillData>();
        string path = EditorUtility.SaveFilePanelInProject("새 스킬 생성", "NewSkillData", "asset", "스킬 데이터를 저장할 위치를 선택하세요");
        if (!string.IsNullOrEmpty(path))
        {
            AssetDatabase.CreateAsset(newSkill, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            LoadAllSkills();
        }
    }
}
