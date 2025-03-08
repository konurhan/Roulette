using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : SingletonGeneric<GameplayManager>
{
    private int _currentWheelIndex = 0;

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
        WheelsManager.Instance.ShowWheel(_currentWheelIndex);
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            WheelsManager.Instance.ClearWheels();
            ShowWheel();
        }
    }
#endif
}

public enum GameState
{
    Idle,
    Spinning,
    GameOver,
}
