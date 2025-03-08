using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : SingletonGeneric<GameplayManager>
{
    private GameState gameState;
    private int _currentWheelIndex = 0;

    private void Start()
    {
        SetState(GameState.Idle);
        ShowWheel();
    }

    public void SetState(GameState state)
    {
        this.gameState = state;
        if (state == GameState.Idle)
        {
            UIManager.Instance.SetButtonsActive(true);
        }
        else
        {
            UIManager.Instance.SetButtonsActive(false);
        }
    }
    
    public int GetCurrentWheelIndex()
    {
        return _currentWheelIndex;
    }

    public void NextWheel()//successful spin
    {
        _currentWheelIndex++;
    }

    public void ResetProgress()//lost
    {
        _currentWheelIndex = 0;
    }

    public void ShowWheel()
    {
        SetState(GameState.Moving);
        WheelsManager.Instance.ShowWheel(_currentWheelIndex);
    }

    public void HideWheel()
    {
        SetState(GameState.Moving);
        WheelsManager.Instance.HideWheel(_currentWheelIndex);
        NextWheel();
        ShowWheel();
    }

    public void SetLost()
    {
        SetState(GameState.GameOver);
        UIManager.Instance.ShowFailPopup();
    }

    public void Revive()
    {
        
    }

    public void Restart()
    {
        SetState(GameState.Idle);
        ResetProgress();
        WheelsManager.Instance.ClearWheels();
        RewardManager.Instance.ResetRewards();
        ShowWheel();
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            WheelsManager.Instance.ClearWheels();
            ShowWheel();
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            SetLost();
        }
    }
#endif
}

public enum GameState
{
    Idle,
    Moving,
    Spinning,
    GameOver,
}
