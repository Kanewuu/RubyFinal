using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    public int damagedRobots = 5;
    public int fixedRobots = 0;
    public static EnemiesManager instance { get; private set; }

    GameManager GameManager;

    void Awake()
    {
        instance = this;
        GameManager = FindObjectOfType<GameManager>();
    }

    public void FixRobot()
    {
        fixedRobots++;
        RobotsCountUI.instance.SetValue(1);

        if (fixedRobots == damagedRobots)
        {
            WinUI.instance.ShowWin();
            GameManager.AllRobotsFixed = true;
            GameManager.TryWinStage();
        }
    }
}
