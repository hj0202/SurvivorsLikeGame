using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    // 프리팹 저장(이름, 프리팹오브젝트)
    Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>();

    // 프리팹 가져오기
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
