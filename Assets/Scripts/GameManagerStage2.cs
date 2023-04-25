using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerStage2 : GameManager
{
    private void Awake()
    {
        instance = this;
        hasLost = false;
        hasWon = false;
        currentStage = 1;
        rubycontroller = FindObjectOfType<Rubycontroller1>();
    }

    public override bool hasPassedStage()
    {
        return AllRobotsFixed;
    }

    public override void LooseStage()
    {
        AudioManager.instance.PlayDefeat();
        LooseUI.instance.ShowLoose();
        CanRestart = true;
        rubycontroller.StopMovement();
    }

    public override void RestartStage(bool fromStage0)
    {
        TalkedToJambi = false;
        AllRobotsFixed = false;
        SceneManager.LoadScene(fromStage0 ? 0 : currentStage);
        AudioManager.instance.PlayBackground();
    }

    public override void TryWinStage()
    {
        if (!hasPassedStage()) return;
        AudioManager.instance.PlaySuccess();
        CanRestart = true;
        HasWon = true;
    }
}
