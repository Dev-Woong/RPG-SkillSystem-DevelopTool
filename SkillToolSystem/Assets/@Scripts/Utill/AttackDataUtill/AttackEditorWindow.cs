using System.Linq;
using UnityEditor;
using UnityEngine;

public class AttackEditorWindow : EditorWindow
{
    private Vector2 _listScrollPos;
    private Vector2 _inspectorScrollPos;

    private string _searchText = "";
    private AttackData[] _allAttackDatas;
    private AttackData _selectedAttackData;

    private AttackType _selectedFilter = AttackType.All; // ���� ����

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
        _allAttackDatas = guids.Select(g => AssetDatabase.LoadAssetAtPath<AttackData>(AssetDatabase.GUIDToAssetPath(g))).ToArray();
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();

        // ===== ��ų ��� =====
        GUILayout.BeginVertical(GUILayout.Width(300));

        // ��� ��ư
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("�� ���� ������ �����", GUILayout.Height(25)))
            CreateNewAttackData();

        if (GUILayout.Button("���ΰ�ħ", GUILayout.Width(80), GUILayout.Height(25)))
            LoadAllSkills();
        GUILayout.EndHorizontal();

        EditorGUILayout.Space();

        // ī�װ� ����
        GUILayout.BeginHorizontal();
        foreach (AttackType filterType in System.Enum.GetValues(typeof(AttackType)))
        {
            if (GUILayout.Toggle(_selectedFilter == filterType, filterType.ToString(), "Button"))
                _selectedFilter = filterType;
        }
        GUILayout.EndHorizontal();

        EditorGUILayout.Space();
        _searchText = EditorGUILayout.TextField("�˻�", _searchText);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("���� ������ ���", EditorStyles.boldLabel);

        // ��ų ����Ʈ
        _listScrollPos = GUILayout.BeginScrollView(_listScrollPos);
        foreach (var skill in _allAttackDatas.Where(s => IsSkillMatchFilter(s) &&
                                                   (string.IsNullOrEmpty(_searchText) || s.name.ToLower().Contains(_searchText.ToLower()))))
        {
            GUILayout.BeginHorizontal();

            // ���� ��ư
            if (GUILayout.Button(skill.name, (_selectedAttackData == skill) ? EditorStyles.toolbarButton : EditorStyles.miniButton))
            {
                _selectedAttackData = skill;
                EditorGUIUtility.PingObject(skill);
            }

            // ���� ��ư
            GUI.color = Color.red;
            if (GUILayout.Button("X", GUILayout.Width(25)))
            {
                if (EditorUtility.DisplayDialog("������ ����", $"�ش� ({skill.name}) �����͸� �����Ͻðڽ��ϱ�?", "����", "���"))
                {
                    string path = AssetDatabase.GetAssetPath(skill);
                    AssetDatabase.DeleteAsset(path);
                    AssetDatabase.SaveAssets();
                    LoadAllSkills();

                    if (_selectedAttackData == skill) _selectedAttackData = null;
                }
            }
            GUI.color = Color.white;

            GUILayout.EndHorizontal();
        }
        GUILayout.EndScrollView();

        GUILayout.EndVertical();

        // ===== ����: �ν����� =====
        GUILayout.BeginVertical();

        EditorGUILayout.LabelField("���õ� ���� ������", EditorStyles.boldLabel);
        _inspectorScrollPos = GUILayout.BeginScrollView(_inspectorScrollPos);

        if (_selectedAttackData != null)
        {
            Editor editor = Editor.CreateEditor(_selectedAttackData);
            if (editor != null) editor.OnInspectorGUI();
        }
        else
        {
            EditorGUILayout.HelpBox("���� �����͸� �����ϸ� ���⿡�� ������ �� �ֽ��ϴ�.", MessageType.Info);
        }

        GUILayout.EndScrollView();
        GUILayout.EndVertical();

        GUILayout.EndHorizontal();
    }

    private bool IsSkillMatchFilter(AttackData skill)
    {
        if (_selectedFilter == AttackType.All) return true;
        return skill.AttackType == _selectedFilter;
    }

    private void CreateNewAttackData()
    {
        AttackData newSkill = ScriptableObject.CreateInstance<AttackData>();
        string path = EditorUtility.SaveFilePanelInProject("�� ���� ������ ����", "NewAttackData", "asset", "���� �����͸� ������ ��ġ�� �����ϼ���");
        if (!string.IsNullOrEmpty(path))
        {
            AssetDatabase.CreateAsset(newSkill, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            LoadAllSkills();
        }
    }
}
