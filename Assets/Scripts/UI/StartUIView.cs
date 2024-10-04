using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUIView : MonoBehaviour
{
    [SerializeField] Button startButton;

    private void Awake()
    {
        startButton.onClick.AddListener(OnStartClick);
    }

    public void OnStartClick()
    {
        GameManager.Instance.StartGame();
    }
}
