using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombsEnabledButton : MonoBehaviour
{
    private Image buttonImage;
    private Button button;
    private void Awake()
    {
        buttonImage = GetComponent<Image>();
        button = GetComponent<Button>();
    }

    private IEnumerator Start()
    {
        yield return new WaitUntil(() => GameplayManager.Instance.IsStarted);
        SetButtonColor();
        button.onClick.AddListener(OnButtonClicked);
    }

    private void SetButtonColor()
    {
        if (GameplayManager.Instance.IsBombsEnabled)
        {
            buttonImage.color = Color.green;
        }
        else
        {
            buttonImage.color = Color.gray;
        }
    }
    
    private void OnButtonClicked()
    {
        GameplayManager.Instance.SetBombsEnabled(!GameplayManager.Instance.IsBombsEnabled);
        SetButtonColor();
    }
}
