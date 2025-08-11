using System.Linq;
using UnityEditor;
using UnityEngine;

public class AttackEditorWindow : EditorWindow
{
    private Vector2 listScrollPos;
    private Vector2 inspectorScrollPos;

    private string searchText = "";
    private AttackData[] allAttackDatas;
    private AttackData selectedAttackData;

    private AttackType selectedFilter = AttackType.All; // 필터 상태

    [MenuItem("Window/AttackData Tool")]
    public static void ShowWindow()
    {
        GetWindow<AttackEditorWindow>("AttackData Tool");
    }

    private void OnEnable()
    {
        LoadAllSkills();
    }

    private void LoadAllSkills()
    {
        string[] guids = AssetDatabase.FindAssets("t:AttackData");
        allAttackDatas = guids.Select(g => AssetDatabase.LoadAssetAtPath<AttackData>(AssetDatabase.GUIDToAssetPath(g))).ToArray();
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();

        // ===== 스킬 목록 =====
        GUILayout.BeginVertical(GUILayout.Width(300));

        // 상단 버튼
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("새 데이터 만들기", GUILayout.Height(25)))
            CreateNewSkill();

        if (GUILayout.Button("새로고침", GUILayout.Width(80), GUILayout.Height(25)))
            LoadAllSkills();
        GUILayout.EndHorizontal();

        EditorGUILayout.Space();

        // 카테고리 필터
        GUILayout.BeginHorizontal();
        foreach (AttackType filterType in System.Enum.GetValues(typeof(AttackType)))
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
        foreach (var skill in allAttackDatas.Where(s => IsSkillMatchFilter(s) &&
                                                   (string.IsNullOrEmpty(searchText) || s.name.ToLower().Contains(searchText.ToLower()))))
        {
            GUILayout.BeginHorizontal();

            // 선택 버튼
            if (GUILayout.Button(skill.name, (selectedAttackData == skill) ? EditorStyles.toolbarButton : EditorStyles.miniButton))
            {
                selectedAttackData = skill;
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

                    if (selectedAttackData == skill) selectedAttackData = null;
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

        if (selectedAttackData != null)
        {
            Editor editor = Editor.CreateEditor(selectedAttackData);
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

    private bool IsSkillMatchFilter(AttackData skill)
    {
        if (selectedFilter == AttackType.All) return true;
        return skill.attackType == selectedFilter;
    }

    private void CreateNewSkill()
    {
        AttackData newSkill = ScriptableObject.CreateInstance<AttackData>();
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
