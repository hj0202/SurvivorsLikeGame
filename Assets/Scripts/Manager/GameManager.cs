using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private const int MAX_TIME = 600;

    private static ResourceManager resourceManager = new ResourceManager();

    public static ResourceManager Resource { get { return resourceManager; } }

    [SerializeField] private PlayerController player;
    [SerializeField] private EnemyFactory enemyFactory;

    private int time = 0;
    private IEnumerator timeCounter;

    private void Awake()
    {
        UIManager.Instance.HideUIViewAll();
    }

    private void Start()
    {
        UIManager.Instance.ShowStartUIView();
    }

    #region Game Flow

    public void StartGame()
    {
        // 씬 초기화
        // 오브젝트(몬스터, 스킬 등) 전부 삭제
        // 플레이어 상태 초기화, 위치 초기화
        player.Init();
        enemyFactory.Init();

        // 플레이어 입력 O
        player.EnableInput();

        // 몬스터 생성 O
        enemyFactory.Init();
        enemyFactory.StartSpawnEnemy();

        // Play View 표시
        UIManager.Instance.ShowPlayUIView();

        // 시간 측정 O
        StartCountTime();

    }

    public void EndGame()
    {
        // 플레이어 입력 X
        player.DisableInput();

        // 몬스터 생성 X
        enemyFactory.StopSpawnEnemy();

        // Result View 표시
        UIManager.Instance.ShowResultUIView();
        UIManager.Instance.UpdateTime(time);

        // 시간 측정 X
        StopCountTime();
    }
    #endregion

    #region Time

    private void StartCountTime()
    {
        timeCounter = TimeCounter();
        StartCoroutine(timeCounter);
    }

    private void StopCountTime()
    {
        StopCoroutine(timeCounter);
    }

    IEnumerator TimeCounter()
    {
        time = 0;
        UIManager.Instance.UpdateTime(time);
        while (true)
        {
            yield return new WaitForSeconds(1f);
            time++;
            UIManager.Instance.UpdateTime(time);

            if (time >= MAX_TIME)
                EndGame();
        }
    }
    #endregion
}
