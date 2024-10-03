using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    
    [SerializeField] private GameObject[] enemyPrefab;
    [SerializeField] private GameObject target;

    [Header("Spawn")]
    [SerializeField] private float spawnTime;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private Transform[] spawnPositions;

    Queue<GameObject> enemyQueue = new Queue<GameObject>();

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
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
        }
        else 
        {
            // 비활성화 된거 없으면
            enemyObj = Instantiate(enemyPrefab[1], spawnPosition, Quaternion.identity);
        }

        EnemyController enemyController = enemyObj.GetComponent<EnemyController>();
        enemyController.Init(target, this);
    }

    public void Die(GameObject enemyObj)
    {
        enemyObj.SetActive(false);
        enemyQueue.Enqueue(enemyObj);
    }
}
