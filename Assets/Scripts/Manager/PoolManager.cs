using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // 풀
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

    // 풀 딕셔너리
    private Dictionary<string, Pool> pools = new Dictionary<string, Pool>();

    // Attack 꺼내줘하면
    // 풀매니저가 Attack풀에 접근해서 꺼낸다.

    public GameObject GetAttack()
    {
        GameObject obj = null;

        // pools에 있는건가...?


        // 있으면
        // 풀에서 꺼낸다.
        // 준다.

        // 없으면


        return obj;
    }
}
