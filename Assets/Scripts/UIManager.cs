using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletonGeneric<UIManager>
{
    [SerializeField] private Transform popupRoot;
    
    [SerializeField] private List<Button> uiButtons = new List<Button>();
    [SerializeField] private GameObject failPopupPrefab;

    public void ShowFailPopup()
    {
        SetButtonsActive(false);
        GameObject failPopup = Instantiate(failPopupPrefab, popupRoot);
    }

    public void SetButtonsActive(bool active)
    {
        foreach (var button in uiButtons)
        {
            button.interactable = active;
        }
    }
}
