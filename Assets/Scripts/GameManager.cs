using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool TalkedToJambi = false;
    public bool AllRobotsFixed = false;
    public bool CanRestart = false;
    public bool HasWon = false;

    protected int currentStage;
    protected bool hasLost;
    protected bool hasWon;

    protected Rubycontroller1 rubycontroller;

    public abstract bool hasPassedStage();
    public abstract void TryWinStage();
    public abstract void LooseStage();
    public abstract void RestartStage(bool fromStage0);
}
