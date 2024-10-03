using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    // ������ ����(�̸�, �����տ�����Ʈ)
    Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>();

    // ������ ��������
    public GameObject GetPrefab(string prefabName)
    {
        GameObject obj = null;

        if (prefabs.ContainsKey(prefabName))
        {
            obj = prefabs[prefabName];
        }
        else
        {
            obj = Resources.Load<GameObject>($"Prefabs/{prefabName}");
            prefabs.Add(prefabName, obj);
        }

        return obj;
    }
}
