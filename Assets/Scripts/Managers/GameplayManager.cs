using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : SingletonGeneric<GameplayManager>
{
    private GameState gameState;
    private int _currentWheelIndex = 0;

    [SerializeField] private int maxWheelCount = 200;
    [SerializeField] private bool isBombsEnabled = true;

    public bool IsBombsEnabled => isBombsEnabled;
    public int MAX_WHEEL_COUNT => maxWheelCount;

    public bool IsStarted = false;
    private void Start()
    {
        Application.targetFrameRate = 60;
        
        SetState(GameState.Idle);
        UIManager.Instance.InitializeLevelProgressScroll();
        ShowWheel();
        IsStarted = true;
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
        UIManager.Instance.MoveLevelProgressScroll(_currentWheelIndex);
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
        UIManager.Instance.InitializeLevelProgressScroll();
        WheelsManager.Instance.ClearWheels();
        RewardManager.Instance.ResetRewards();
        ShowWheel();
    }

    public void SetBombsEnabled(bool enabled)
    {
        isBombsEnabled = enabled;
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
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Time.timeScale /= 2;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Time.timeScale *= 2;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Time.timeScale = 1;
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
