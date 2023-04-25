using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinUI : MonoBehaviour
{
    [SerializeField] GameObject winUI;
    public static WinUI instance { get; private set; }

    public float displayTime = 4.0f;
    float timerDisplay;


    void Awake()
    {
        instance = this;
        winUI.SetActive(false);
    }

    public void ShowWin()
    {
        timerDisplay = displayTime;
        winUI.SetActive(true);
    }

    void Update()
    {
        if (timerDisplay >= 0)
        {
            timerDisplay -= Time.deltaTime;
            if (timerDisplay < 0)
            {
                winUI.SetActive(false);
            }
        }
    }
}
