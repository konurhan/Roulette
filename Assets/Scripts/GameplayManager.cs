using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonGeneric<GameManager>
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
        
    }
}

public enum GameState
{
    Idle,
    Spinning,
    GameOver,
}
