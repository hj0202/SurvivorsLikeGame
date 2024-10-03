using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] Image hpBar;
    [SerializeField] Text scoreText;

    [SerializeField] GameObject gameOverText;
    [SerializeField] Button restartButton;

    [SerializeField] Button rollButton;

    [SerializeField] GameObject bossUI;
    [SerializeField] Image bossHpBar;

    private void Awake()
    {
        restartButton.onClick.AddListener(Restart);
        rollButton.onClick.AddListener(OnClickRoll);
    }

    public void OnClickRoll()
    {
        GameManager.Instance.Roll();
    }

    public void UpdateHpBar(float hp, float maxHp)
    {
        hpBar.fillAmount = hp / maxHp;
    }

    public void UpdateScore(int score)
    {
        scoreText.text = $"Score: {score}";
    }

    public void ShowGameOverUI()
    {
        gameOverText.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }

    public void ShowBossUI()
    {
        bossUI.SetActive(true);
    }

    public void HideBossUI()
    {
        bossUI.SetActive(false);
    }

    public void UpdateBossHpBar(float hp, float maxHp)
    {
        bossHpBar.fillAmount = hp / maxHp;
    }
}
