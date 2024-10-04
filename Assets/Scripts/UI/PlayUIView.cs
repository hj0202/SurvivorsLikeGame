using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayUIView : MonoBehaviour
{
    [SerializeField] Text timeText;

    public void UpdateTimeText(int time)
    {
        int m = time / 60;
        int s = time % 60;
        timeText.text = $"Time  {m.ToString("D2")}:{s.ToString("D2")}";
    }
}
