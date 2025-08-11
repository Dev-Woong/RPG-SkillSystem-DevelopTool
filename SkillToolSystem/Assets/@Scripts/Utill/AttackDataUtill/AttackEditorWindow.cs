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

    private AttackType selectedFilter = AttackType.All; // ���� ����

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

        // ===== ��ų ��� =====
        GUILayout.BeginVertical(GUILayout.Width(300));

        // ��� ��ư
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("�� ������ �����", GUILayout.Height(25)))
            CreateNewSkill();

        if (GUILayout.Button("���ΰ�ħ", GUILayout.Width(80), GUILayout.Height(25)))
            LoadAllSkills();
        GUILayout.EndHorizontal();

        EditorGUILayout.Space();

        // ī�װ� ����
        GUILayout.BeginHorizontal();
        foreach (AttackType filterType in System.Enum.GetValues(typeof(AttackType)))
        {
            if (GUILayout.Toggle(selectedFilter == filterType, filterType.ToString(), "Button"))
                selectedFilter = filterType;
        }
        GUILayout.EndHorizontal();

        EditorGUILayout.Space();
        searchText = EditorGUILayout.TextField("�˻�", searchText);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("��ų ���", EditorStyles.boldLabel);

        // ��ų ����Ʈ
        listScrollPos = GUILayout.BeginScrollView(listScrollPos);
        foreach (var skill in allAttackDatas.Where(s => IsSkillMatchFilter(s) &&
                                                   (string.IsNullOrEmpty(searchText) || s.name.ToLower().Contains(searchText.ToLower()))))
        {
            GUILayout.BeginHorizontal();

            // ���� ��ư
            if (GUILayout.Button(skill.name, (selectedAttackData == skill) ? EditorStyles.toolbarButton : EditorStyles.miniButton))
            {
                selectedAttackData = skill;
                EditorGUIUtility.PingObject(skill);
            }

            // ���� ��ư
            GUI.color = Color.red;
            if (GUILayout.Button("X", GUILayout.Width(25)))
            {
                if (EditorUtility.DisplayDialog("��ų ����", $"{skill.name} ��ų�� �����Ͻðڽ��ϱ�?", "����", "���"))
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

        // ===== ����: �ν����� =====
        GUILayout.BeginVertical();

        EditorGUILayout.LabelField("���õ� ��ų", EditorStyles.boldLabel);
        inspectorScrollPos = GUILayout.BeginScrollView(inspectorScrollPos);

        if (selectedAttackData != null)
        {
            Editor editor = Editor.CreateEditor(selectedAttackData);
            if (editor != null) editor.OnInspectorGUI();
        }
        else
        {
            EditorGUILayout.HelpBox("��ų�� �����ϸ� ���⿡�� ������ �� �ֽ��ϴ�.", MessageType.Info);
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
        string path = EditorUtility.SaveFilePanelInProject("�� ��ų ����", "NewSkillData", "asset", "��ų �����͸� ������ ��ġ�� �����ϼ���");
        if (!string.IsNullOrEmpty(path))
        {
            AssetDatabase.CreateAsset(newSkill, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            LoadAllSkills();
        }
    }
}
