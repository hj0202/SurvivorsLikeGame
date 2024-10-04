using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyFactory : MonoBehaviour
{
    
    [SerializeField] private GameObject[] enemyPrefab;
    [SerializeField] private GameObject target;

    [Header("Spawn")]
    [SerializeField] private float spawnTime;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private Transform[] spawnPositions;

    Queue<GameObject> enemyQueue = new Queue<GameObject>();
    UnityEvent onDie = new UnityEvent();
    public void Init()
    {
    }

    public void StartSpawnEnemy()
    {
        StartCoroutine(SpawnEnemy());
    }

    public void StopSpawnEnemy()
    {
        StopAllCoroutines();
        // 살아있는 몬스터는 전부 죽인다.
        onDie.Invoke();
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            Spawn();
        }
    }

    public void Spawn()
    {
        int spawnIndex = Random.Range(0, spawnPositions.Length);
        Vector3 spawnPosition = spawnPositions[spawnIndex].position;

        GameObject enemyObj = null;
        if (enemyQueue.Count > 0)
        {
            // 비활성화 된거 있으면
            enemyObj = enemyQueue.Dequeue();
            enemyObj.transform.position = spawnPosition;
            enemyObj.SetActive(true);
            EnemyController enemyController = enemyObj.GetComponent<EnemyController>();
            enemyController.Init(target, this);
        }
        else 
        {
            // 비활성화 된거 없으면
            enemyObj = Instantiate(enemyPrefab[1], spawnPosition, Quaternion.identity);
            EnemyController enemyController = enemyObj.GetComponent<EnemyController>();
            enemyController.Init(target, this);
            onDie.AddListener(enemyController.Die);
        }
    }

    public void Die(GameObject enemyObj)
    {
        enemyObj.SetActive(false);
        enemyQueue.Enqueue(enemyObj);
    }
}
