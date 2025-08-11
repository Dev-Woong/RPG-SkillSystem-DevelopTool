using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : PoolAble 
{ 
    [System.Serializable]
    private class ObjectInfo
    {
        public string objectName;
        public GameObject prefab;
        public int count;
    }

    public static ObjectPoolManager instance;
    public bool IsReady { get; private set; }

    [SerializeField] private ObjectInfo[] objectInfos;

    private Dictionary<string, IObjectPool<GameObject>> objectPoolDic = new Dictionary<string, IObjectPool<GameObject>>();
    private Dictionary<string, GameObject> prefabDic = new Dictionary<string, GameObject>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(this.gameObject);
            return;
        }

        Init();
    }

    private void Init()
    {
        IsReady = false;

        foreach (var info in objectInfos)
        {
            if (prefabDic.ContainsKey(info.objectName))
            {
                Debug.LogWarning($"{info.objectName} �� �̹� ��ϵ� ������Ʈ�Դϴ�.");
                continue;
            }

            prefabDic.Add(info.objectName, info.prefab);

            var pool = new ObjectPool<GameObject>(
                () => CreatePooledItem(info.objectName),
                OnTakeFromPool,
                OnReturnedToPool,
                OnDestroyPoolObject,
                true,
                info.count,
                info.count
            );

            objectPoolDic.Add(info.objectName, pool);

            // �̸� �����ؼ� Ǯ�� ä���ֱ�
            for (int i = 0; i < info.count; i++)
            {
                var obj = pool.Get();
                obj.GetComponent<PoolAble>().ReleaseObject();
            }
        }

        Debug.Log("������ƮǮ �غ� �Ϸ�");
        IsReady = true;
    }

    private GameObject CreatePooledItem(string objectName)
    {
        GameObject poolObject = Instantiate(prefabDic[objectName]);
        poolObject.GetComponent<PoolAble>().Pool = objectPoolDic[objectName];
        return poolObject;
    }

    private void OnTakeFromPool(GameObject obj)
    {
        obj.SetActive(true);
    }

    private void OnReturnedToPool(GameObject obj)
    {
        obj.SetActive(false);
    }

    private void OnDestroyPoolObject(GameObject obj)
    {
        Destroy(obj);
    }

    public GameObject GetObject(string objectName)
    {
        if (!objectPoolDic.ContainsKey(objectName))
        {
            Debug.LogError($"{objectName} �� ������ƮǮ�� ��ϵǾ� ���� �ʽ��ϴ�.");
            return null;
        }

        return objectPoolDic[objectName].Get();
    }
    public void ReleaseObject(GameObject obj)
    {
        var poolable = obj.GetComponent<PoolAble>();
        if (poolable == null)
        {
            Debug.LogError("PoolAble ������Ʈ�� �����ϴ�. Ǯ�� ��ȯ�� �� �����ϴ�.");
            return;
        }

        poolable.ReleaseObject();
    }
}
