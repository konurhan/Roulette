using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeScaleButton : MonoBehaviour
{
    private Button button;
    private TMP_InputField inputField;
    private void Awake()
    {
        button = GetComponent<Button>();
        inputField = GetComponentInChildren<TMP_InputField>();
        
        button.onClick.AddListener(OnButtonClicked);
        inputField.text = Time.timeScale.ToString();
    }

    private void OnButtonClicked()
    {
        Time.timeScale = float.Parse(inputField.text);
    }
}
