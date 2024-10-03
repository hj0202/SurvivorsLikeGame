using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // Ǯ
    class Pool
    {
        public string PrefabName { get; set; }

        private Queue<GameObject> gameObjects = new Queue<GameObject>();

        public void Push(GameObject obj)
        {
            gameObjects.Enqueue(obj);
        }

        public GameObject Pop()
        {
            GameObject obj = null;

            if (gameObjects.Count > 0)
            {
                obj = gameObjects.Dequeue();
            }
            else
            {
                GameObject prefab = GameManager.Resource.GetPrefab(PrefabName);
                return GameObject.Instantiate(prefab);
            }

            return obj;
        }
    }

    // Ǯ ��ųʸ�
    private Dictionary<string, Pool> pools = new Dictionary<string, Pool>();

    // Attack �������ϸ�
    // Ǯ�Ŵ����� AttackǮ�� �����ؼ� ������.

    public GameObject GetAttack()
    {
        GameObject obj = null;

        // pools�� �ִ°ǰ�...?


        // ������
        // Ǯ���� ������.
        // �ش�.

        // ������


        return obj;
    }
}
