using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T instance = null;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                // 씬에서 싱글톤을 찾는다.
                instance = FindObjectOfType<T>();
                // 그래도 없으면 새로 만든다.
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    instance = obj.AddComponent<T>();
                }
            }

            return instance;
        }
    }
}
