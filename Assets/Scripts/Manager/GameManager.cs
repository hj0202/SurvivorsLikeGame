using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private static ResourceManager resourceManager = new ResourceManager();

    public static ResourceManager Resource { get { return resourceManager; } }

    [SerializeField] private PlayerController player;
    [SerializeField] private GameObject bossPrefab;
    private int score;
    private bool isBossMode;

    //겜시작
    // 플레이어생성
    // UI나오게
    private void Awake()
    {
        score = 0;
        UIManager.Instance.UpdateScore(score);

        isBossMode = false;
    }

    public void PlusScore()
    {
        score++;
        UIManager.Instance.UpdateScore(score);

        if (score >= 10)
        {
            if (!isBossMode)
            {
                isBossMode = true;
                StartBossStage();
            }
        }
    }


    public void Roll()
    {
        player.Roll();
    }

    public void StartBossStage()
    {
        // Boss 생성
        // Boss UI active true
        Vector3 position = player.GetPosition() + Vector3.forward * 10f;
        Quaternion rotation = Quaternion.identity;
        GameObject bossObj = Instantiate(bossPrefab, position, rotation);
        bossObj.GetComponent<EnemyController>().Init(player.gameObject, null);
        UIManager.Instance.ShowBossUI();
    }

    public void EndBossStage()
    {
        // Boss 파괴
        // Boss UI active false
    }
}
