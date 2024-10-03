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
                // ������ �̱����� ã�´�.
                instance = FindObjectOfType<T>();
                // �׷��� ������ ���� �����.
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
