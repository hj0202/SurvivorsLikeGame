using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultUIView : MonoBehaviour
{
    [SerializeField] Button restartButton;
    [SerializeField] Text timeText;

    private void Awake()
    {
        restartButton.onClick.AddListener(OnRestartClick);
    }

    public void OnRestartClick()
    {
        GameManager.Instance.StartGame();
    }

    public void UpdateTimeText(int time)
    {
        int m = time / 60;
        int s = time % 60;
        timeText.text = $"Time  {m.ToString("D2")}:{s.ToString("D2")}";
    }
}
