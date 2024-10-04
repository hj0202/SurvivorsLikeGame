using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] Image hpBar;

    [SerializeField] GameObject bossUI;
    [SerializeField] Image bossHpBar;

    [SerializeField] StartUIView startUIView;
    [SerializeField] PlayUIView playUIView;
    [SerializeField] ResultUIView resultUIView;

    public void HideUIViewAll()
    {
        startUIView.gameObject.SetActive(false);
        playUIView.gameObject.SetActive(false);
        resultUIView.gameObject.SetActive(false);
    }

    public void ShowStartUIView()
    {
        startUIView.gameObject.SetActive(true);
        playUIView.gameObject.SetActive(false);
        resultUIView.gameObject.SetActive(false);
    }

    public void ShowPlayUIView()
    {
        startUIView.gameObject.SetActive(false);
        playUIView.gameObject.SetActive(true);
        resultUIView.gameObject.SetActive(false);
    }

    public void ShowResultUIView()
    {
        startUIView.gameObject.SetActive(false);
        playUIView.gameObject.SetActive(false);
        resultUIView.gameObject.SetActive(true);
    }

    public void UpdateTime(int time)
    {
        playUIView.UpdateTimeText(time);
        resultUIView.UpdateTimeText(time);
    }

    public void UpdateHpBar(float hp, float maxHp)
    {
        hpBar.fillAmount = hp / maxHp;
    }

    public void UpdateBossHpBar(float hp, float maxHp)
    {
        bossHpBar.fillAmount = hp / maxHp;
    }
}
