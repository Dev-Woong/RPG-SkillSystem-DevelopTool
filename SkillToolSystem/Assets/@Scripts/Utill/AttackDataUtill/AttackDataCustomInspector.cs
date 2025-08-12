using System;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
[CustomEditor(typeof(AttackData))]
public class AttackDataCustomInspector : Editor
{
    private bool _showBasic = true;
    private bool _showCost = true;
    private bool _showEffect = true;
    private bool _showCamera = true;
    #region PropertyField
    private SerializedProperty _attackType; // ��ųŸ�� ������
    private SerializedProperty _targetLayer; // ��ų Ÿ�ٷ��̾�
    
    // ** ��ų �⺻ ���� ** // 
    //- ��ų �̸�
    private SerializedProperty _attackName;
     
    //- ������ ����Ÿ��
    
    private SerializedProperty _weaponType;
    
    //- ��ų ����
    private SerializedProperty _attackDescription;
    
    //- ��ų ������
    private SerializedProperty _attackIcon;
    
    //- ��Ÿ��
    private SerializedProperty _coolTime;
    
    //- ������
    private SerializedProperty _damage;
    
    //- ��Ʈ��
    private SerializedProperty _hitCount;
    
    // ����, ����Ÿ��
    private SerializedProperty _attackHitType;
    
    // ���� �Ӽ�
    private SerializedProperty _attackElement;
     
     // ��ų Ư�� �ɷ�
    private SerializedProperty _specialAbility;
   
   // ���� Ÿ��
    private SerializedProperty _setBuffType;
    
    // ����� Ÿ��
    private SerializedProperty _setDebuffType;
     
     // ��ų ����
    private SerializedProperty _startAttackPoint;
    private SerializedProperty _attackRange;
     
    // ----------------------------------------------
    
    // ** ��ų �����ý��� ������ ** //
    
    //- ��ų ���� �ý��� ��� ����
    private SerializedProperty _usingLevelSystem;
    
    //- ��ų ���緹��
    private SerializedProperty _attackCurLevel;
    
     //- ��ų �ִ뷹��
    private SerializedProperty _attackMaxLevel;
   
     //- ��ų ������ ������ ���ġ
    private SerializedProperty _attackIncreaseValue;

    // ----------------------------------------------

    // ** �Ҹ��ڽ�Ʈ  
    private SerializedProperty _hpCost;
    private SerializedProperty _mpCost;

    // ** ��ų ����� �̵�����
    private SerializedProperty _canMove;

    // ** ���۾Ƹ�
    private SerializedProperty _onSuperArmor;

    // ** �˹�
    private SerializedProperty _useKnockBack;
    private SerializedProperty _knockBackForce;

    // ** ��ų ĳ����
    private SerializedProperty _doAttackCasting;
    private SerializedProperty _attackCastingTime;

    // ** ����ü
    private SerializedProperty _projectilePrefabName;
    private SerializedProperty _projectileForce;

    // ** ��ų ���� ** // 
    
    //- ��ų ���� ����
    private SerializedProperty _attackSFX;

    //- ��ų ����Ʈ
    private SerializedProperty _attackEffectName;
    private SerializedProperty _attackEffectPos;

    //- Ÿ�� ����Ʈ
    private SerializedProperty _hitEffectName;
    private SerializedProperty _hitEffectPos;

    // ----------------------------------------------

    // ** ī�޶� ���� ** //

    //- ī�޶� ���� ��뿩��
    private SerializedProperty _useCameraShake;

    //- ī�޶� ����ũ ��
    private SerializedProperty _shakeCameraForce;

    //- ī�޶� ����ũ ����
    private SerializedProperty _shakeCameraDir;

    //- ī�޶� ����ũ �����ð�
    private SerializedProperty _shakeCameraDuration;

    // ----------------------------------------------
    #endregion
    private void OnEnable()
    {
        _attackType = serializedObject.FindProperty("AttackType");
        _weaponType = serializedObject.FindProperty("WeaponType");
        _attackDescription = serializedObject.FindProperty("AttackDescription");
        _attackIcon = serializedObject.FindProperty("AttackIcon");
        _targetLayer = serializedObject.FindProperty("TargetLayer");
        _attackName = serializedObject.FindProperty("AttackName");
        _coolTime = serializedObject.FindProperty("CoolTime");
        _damage = serializedObject.FindProperty("Damage");
        _hitCount = serializedObject.FindProperty("HitCount");
        _attackHitType = serializedObject.FindProperty("AttackHitType");
        _attackElement = serializedObject.FindProperty("AttackElement");
        _specialAbility = serializedObject.FindProperty("SpecialAbility");
        _setBuffType = serializedObject.FindProperty("SetBuffType");
        _setDebuffType = serializedObject.FindProperty("SetDebuffType");
        _startAttackPoint = serializedObject.FindProperty("StartAttackPoint");
        _attackRange = serializedObject.FindProperty("AttackRange");

        _usingLevelSystem = serializedObject.FindProperty("UsingLevelSystem");
        _attackCurLevel = serializedObject.FindProperty("AttackCurLevel");
        _attackMaxLevel = serializedObject.FindProperty("AttackMaxLevel");
        _attackIncreaseValue = serializedObject.FindProperty("LevelIncreaseValue");

        _hpCost = serializedObject.FindProperty("Hp");
        _mpCost = serializedObject.FindProperty("Mp");
        _canMove = serializedObject.FindProperty("CanMove");
        _onSuperArmor = serializedObject.FindProperty("SuperArmor");

        _useKnockBack = serializedObject.FindProperty("UseKnockBack");
        _knockBackForce = serializedObject.FindProperty("KnockBackForce");

        _doAttackCasting = serializedObject.FindProperty("AttackCasting");
        _attackCastingTime = serializedObject.FindProperty("CastingTime");

        _projectilePrefabName = serializedObject.FindProperty("ProjectilePrefabName");
        _projectileForce = serializedObject.FindProperty("ProjectileForce");

        _attackSFX = serializedObject.FindProperty("AttackSFX");
        _attackEffectName = serializedObject.FindProperty("AttackEffectPrefabName");
        _attackEffectPos = serializedObject.FindProperty("EffectPos");
        _hitEffectName = serializedObject.FindProperty("HitEffectPrefabName");
        _hitEffectPos = serializedObject.FindProperty("HitEffectPos");

        _useCameraShake = serializedObject.FindProperty("UseCameraShake");
        _shakeCameraForce = serializedObject.FindProperty("ShakePower");
        _shakeCameraDir = serializedObject.FindProperty("ShakeDir");
        _shakeCameraDuration = serializedObject.FindProperty("ShakeDuration");
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

        EditorGUILayout.PropertyField(_attackType);
        switch (aData.AttackType)
        {
            //���� ���� Ÿ�� �ν�����
            #region MeleeAttackType
            case AttackType.Melee:
                _showBasic = EditorGUILayout.Foldout(_showBasic, "����Ÿ�� �⺻ ����", true);
                if (_showBasic)
                {
                    EditorGUILayout.PropertyField(_targetLayer);
                    EditorGUILayout.PropertyField(_weaponType);
                    EditorGUILayout.PropertyField(_attackIcon);
                    EditorGUILayout.PropertyField(_attackName);
                    EditorGUILayout.PropertyField(_attackDescription);
                    EditorGUILayout.PropertyField(_coolTime);
                    EditorGUILayout.PropertyField(_damage);
                    EditorGUILayout.PropertyField(_hitCount);
                    EditorGUILayout.PropertyField(_attackHitType);
                    EditorGUILayout.PropertyField(_attackElement);
                    EditorGUILayout.PropertyField(_specialAbility);
                    if (aData.SpecialAbility == AttackSpecialAbility.Buff)
                    {
                        EditorGUILayout.PropertyField(_setBuffType);
                    }
                    else if (aData.SpecialAbility == AttackSpecialAbility.DeBuff)
                    {
                        EditorGUILayout.PropertyField(_setDebuffType);
                    }

                    EditorGUILayout.Space();

                    EditorGUILayout.LabelField("���� ����", EditorStyles.boldLabel);
                    EditorGUILayout.PropertyField(_startAttackPoint);
                    EditorGUILayout.PropertyField(_attackRange);
                    EditorGUILayout.Space();
                    EditorGUILayout.PropertyField(_usingLevelSystem);
                    if (aData.UsingLevelSystem)
                    {
                        EditorGUILayout.PropertyField(_attackCurLevel);
                        EditorGUILayout.PropertyField(_attackMaxLevel);
                        EditorGUILayout.LabelField("���ݵ����� ������ ������ ������", EditorStyles.miniBoldLabel);
                        EditorGUILayout.PropertyField(_attackIncreaseValue);
                    }
                }
                EditorGUILayout.Space();
                _showCost = EditorGUILayout.Foldout(_showCost, "�ڽ�Ʈ �� �ΰ� ȿ��", true);
                if (_showCost)
                {
                    EditorGUILayout.PropertyField(_hpCost);
                    EditorGUILayout.PropertyField(_mpCost);
                    EditorGUILayout.PropertyField(_canMove);
                    EditorGUILayout.PropertyField(_onSuperArmor);
                    EditorGUILayout.PropertyField(_useKnockBack);
                    if (aData.UseKnockBack)
                    {
                        EditorGUILayout.PropertyField(_knockBackForce);
                    }
                }
                EditorGUILayout.Space();
                _showEffect = EditorGUILayout.Foldout(_showEffect, "��ų ����", true);
                if (_showEffect)
                {
                    EditorGUILayout.PropertyField(_doAttackCasting);
                    if (aData.AttackCasting)
                    {
                        EditorGUILayout.PropertyField(_attackCastingTime);
                    }

                    EditorGUILayout.PropertyField(_attackEffectName);
                    EditorGUILayout.PropertyField(_attackEffectPos);
                    EditorGUILayout.PropertyField(_hitEffectName);
                    EditorGUILayout.PropertyField(_hitEffectPos);
                    EditorGUILayout.PropertyField(_attackSFX);
                    if (_attackSFX != null)
                    {
                        if (GUILayout.Button("ȿ���� ���"))
                        {
                            if (Application.isPlaying == true)
                            {
                                Debug.LogWarning("�÷��̸�忡�� ����� �� �����ϴ�.");
                                return;
                            }
                            if (Application.isPlaying == false && aData.AttackSFX == null)
                            {
                                Debug.LogWarning($"attackSFX�� {aData.AttackSFX} �Դϴ�.");
                                return;
                            }
                            OnEditorModePlayClip(aData.AttackSFX);
                            Debug.Log($"ȿ���� ��� {aData.AttackSFX}");
                        }
                    }
                }

                EditorGUILayout.Space();
                _showCamera = EditorGUILayout.Foldout(_showCamera, "ī�޶� ����", true);
                if (_showCamera)
                {
                    EditorGUILayout.PropertyField(_useCameraShake);
                    if (aData.UseCameraShake)
                    {
                        EditorGUILayout.PropertyField(_shakeCameraForce);
                        EditorGUILayout.PropertyField(_shakeCameraDir);
                        EditorGUILayout.PropertyField(_shakeCameraDuration);
                    }
                }
                break;

            #endregion 
                
            //���Ÿ� ���� Ÿ�� �ν�����
            #region RangeAttackCase
            case AttackType.Range:
                _showBasic = EditorGUILayout.Foldout(_showBasic, "����üŸ�� �⺻ ����", true);
                if (_showBasic)
                {
                    EditorGUILayout.PropertyField(_targetLayer);
                    EditorGUILayout.PropertyField(_weaponType);
                    EditorGUILayout.PropertyField(_attackIcon);
                    EditorGUILayout.PropertyField(_attackName);
                    EditorGUILayout.PropertyField(_attackDescription);
                    EditorGUILayout.PropertyField(_coolTime);
                    EditorGUILayout.PropertyField(_damage);
                    EditorGUILayout.PropertyField(_hitCount);
                    EditorGUILayout.PropertyField(_attackHitType);
                    EditorGUILayout.PropertyField(_attackElement);
                    EditorGUILayout.PropertyField(_specialAbility);
                    if (aData.SpecialAbility == AttackSpecialAbility.Buff)
                    {
                        EditorGUILayout.PropertyField(_setBuffType);
                    }
                    else if (aData.SpecialAbility == AttackSpecialAbility.DeBuff)
                    {
                        EditorGUILayout.PropertyField(_setDebuffType);
                    }
                    EditorGUILayout.Space();
                    EditorGUILayout.LabelField("����ü ������ ����", EditorStyles.miniBoldLabel);
                    EditorGUILayout.PropertyField(_projectilePrefabName);
                    EditorGUILayout.PropertyField(_projectileForce);
                    EditorGUILayout.Space();
                    EditorGUILayout.PropertyField(_usingLevelSystem);
                    if (aData.UsingLevelSystem)
                    {
                        EditorGUILayout.PropertyField(_attackCurLevel);
                        EditorGUILayout.PropertyField(_attackMaxLevel);
                        EditorGUILayout.LabelField("��ų ������ ������ ������", EditorStyles.miniBoldLabel);
                        EditorGUILayout.PropertyField(_attackIncreaseValue);
                    }
                }
                
                
                EditorGUILayout.Space();
                _showCost = EditorGUILayout.Foldout(_showCost, "�ڽ�Ʈ �� �ΰ� ȿ��", true);
                if (_showCost)
                {
                    EditorGUILayout.PropertyField(_hpCost);
                    EditorGUILayout.PropertyField(_mpCost);
                    EditorGUILayout.PropertyField(_onSuperArmor);
                    EditorGUILayout.PropertyField(_useKnockBack);
                    if (aData.UseKnockBack)
                    {
                        EditorGUILayout.PropertyField(_knockBackForce);
                    }
                }

                EditorGUILayout.Space();
                _showEffect = EditorGUILayout.Foldout(_showEffect, "��ų ����", true);
                if (_showEffect)
                {
                    EditorGUILayout.PropertyField(_doAttackCasting);
                    if (aData.AttackCasting)
                    {
                        EditorGUILayout.PropertyField(_attackCastingTime);
                    }

                    EditorGUILayout.PropertyField(_canMove);
                    EditorGUILayout.PropertyField(_attackEffectName);
                    EditorGUILayout.PropertyField(_attackEffectPos);
                    EditorGUILayout.PropertyField(_hitEffectName);
                    EditorGUILayout.PropertyField(_hitEffectPos);
                    EditorGUILayout.PropertyField(_attackSFX);
                    if (_attackSFX != null)
                    {
                        if (GUILayout.Button("ȿ���� ���"))
                        {
                            if (Application.isPlaying == true)
                            {
                                Debug.LogWarning("�÷��̸�忡�� ����� �� �����ϴ�.");
                                return;
                            }
                            if (Application.isPlaying == false && aData.AttackSFX == null)
                            {
                                Debug.LogWarning($"Attack SFX�� {aData.AttackSFX} �Դϴ�.");
                                return;
                            }
                            OnEditorModePlayClip(aData.AttackSFX);
                            Debug.Log($"ȿ���� ��� {aData.AttackSFX}");
                        }
                    }
                }

                EditorGUILayout.Space();
                _showCamera = EditorGUILayout.Foldout(_showCamera, "ī�޶� ����", true);
                if (_showCamera)
                {
                    EditorGUILayout.PropertyField(_useCameraShake);
                    if (aData.UseCameraShake)
                    {
                        EditorGUILayout.PropertyField(_shakeCameraForce);
                        EditorGUILayout.PropertyField(_shakeCameraDir);
                        EditorGUILayout.PropertyField(_shakeCameraDuration);
                    }
                }
                break;

            #endregion

            //���� ���� Ÿ�� �ν�����
            #region AOEAttackType
            case AttackType.AOE:
                _showBasic = EditorGUILayout.Foldout(_showBasic, "AOEŸ�� �⺻ ����", true);
                if (_showBasic)
                {
                    EditorGUILayout.PropertyField(_targetLayer);
                    EditorGUILayout.PropertyField(_weaponType);
                    EditorGUILayout.PropertyField(_attackIcon);
                    EditorGUILayout.PropertyField(_attackName);
                    EditorGUILayout.PropertyField(_attackDescription);
                    EditorGUILayout.PropertyField(_coolTime);
                    EditorGUILayout.PropertyField(_damage);
                    EditorGUILayout.PropertyField(_hitCount);
                    EditorGUILayout.PropertyField(_attackHitType);
                    EditorGUILayout.PropertyField(_attackElement);
                    EditorGUILayout.PropertyField(_specialAbility);
                    if (aData.SpecialAbility == AttackSpecialAbility.Buff)
                    {
                        EditorGUILayout.PropertyField(_setBuffType);
                    }
                    else if (aData.SpecialAbility == AttackSpecialAbility.DeBuff)
                    {
                        EditorGUILayout.PropertyField(_setDebuffType);
                    }

                    EditorGUILayout.Space();

                    EditorGUILayout.LabelField("��ų ����", EditorStyles.boldLabel);
                    EditorGUILayout.PropertyField(_startAttackPoint);
                    EditorGUILayout.PropertyField(_attackRange);
                    EditorGUILayout.Space();
                    EditorGUILayout.PropertyField(_usingLevelSystem);
                    if (aData.UsingLevelSystem)
                    {
                        EditorGUILayout.PropertyField(_attackCurLevel);
                        EditorGUILayout.PropertyField(_attackMaxLevel);
                        EditorGUILayout.LabelField("��ų ������ ������ ������", EditorStyles.miniBoldLabel);
                        EditorGUILayout.PropertyField(_attackIncreaseValue);
                    }
                }



                EditorGUILayout.Space();
                _showCost = EditorGUILayout.Foldout(_showCost, "�ڽ�Ʈ �� �ΰ� ȿ��", true);
                if (_showCost)
                {
                    EditorGUILayout.PropertyField(_hpCost);
                    EditorGUILayout.PropertyField(_mpCost);
                    EditorGUILayout.PropertyField(_canMove);
                    EditorGUILayout.PropertyField(_onSuperArmor);
                    EditorGUILayout.PropertyField(_useKnockBack);
                    if (aData.UseKnockBack)
                    {
                        EditorGUILayout.PropertyField(_knockBackForce);
                    }
                }
                EditorGUILayout.Space();
                _showEffect = EditorGUILayout.Foldout(_showEffect, "��ų ����", true);
                if (_showEffect)
                {
                    EditorGUILayout.PropertyField(_doAttackCasting);
                    if (aData.AttackCasting)
                    {
                        EditorGUILayout.PropertyField(_attackCastingTime);
                    }

                    EditorGUILayout.PropertyField(_attackEffectName);
                    EditorGUILayout.PropertyField(_attackEffectPos);
                    EditorGUILayout.PropertyField(_hitEffectName);
                    EditorGUILayout.PropertyField(_hitEffectPos);
                    EditorGUILayout.PropertyField(_attackSFX);
                    if (_attackSFX != null)
                    {
                        if (GUILayout.Button("ȿ���� ���"))
                        {
                            if (Application.isPlaying == true)
                            {
                                Debug.LogWarning("�÷��̸�忡�� ����� �� �����ϴ�.");
                                return;
                            }
                            if (Application.isPlaying == false && aData.AttackSFX == null)
                            {
                                Debug.LogWarning($"skillSFX�� {aData.AttackSFX} �Դϴ�.");
                                return;
                            }
                            OnEditorModePlayClip(aData.AttackSFX);
                            Debug.Log($"ȿ���� ��� {aData.AttackSFX}");
                        }
                    }
                }

                EditorGUILayout.Space();
                _showCamera = EditorGUILayout.Foldout(_showCamera, "ī�޶� ����", true);
                if (_showCamera)
                {
                    EditorGUILayout.PropertyField(_useCameraShake);
                    if (aData.UseCameraShake)
                    {
                        EditorGUILayout.PropertyField(_shakeCameraForce);
                        EditorGUILayout.PropertyField(_shakeCameraDir);
                        EditorGUILayout.PropertyField(_shakeCameraDuration);
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
