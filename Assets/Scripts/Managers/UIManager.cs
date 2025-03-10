using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletonGeneric<UIManager>
{
    [SerializeField] private Transform popupRoot;
    
    [SerializeField] private List<Button> uiButtons = new List<Button>();
    [SerializeField] private GameObject failPopupPrefab;

    [SerializeField] private InfiniteContentScroller LevelProgressScroll;
    [SerializeField] private HighlightAreaController highlightAreaController;

    [SerializeField] private Color textColorGold = Color.green;
    [SerializeField] private Color textColorSilver = Color.green;
    [SerializeField] private Color textColorBronze = Color.white;
    
    public void ShowFailPopup()
    {
        SetButtonsActive(false);
        GameObject failPopup = Instantiate(failPopupPrefab, popupRoot);
    }

    public void InitializeLevelProgressScroll()
    {
        var colorsByWheelType = new Dictionary<WheelType, Color>()
        {
            { WheelType.Bronze , textColorBronze},
            { WheelType.Gold , textColorGold},
            { WheelType.Silver , textColorSilver}
        };
        List<BaseScrollableContentData> scrollableContents = new List<BaseScrollableContentData>();
        var wheelsManager = WheelsManager.Instance;
        for (int i = 0; i < GameplayManager.Instance.MAX_WHEEL_COUNT; i++)
        {
            scrollableContents.Add(new LevelProgressItemData(i, colorsByWheelType[wheelsManager.GetWheelTypeForIndex(i)]));
        }
        LevelProgressScroll.Initialize(scrollableContents);
    }

    public void MoveLevelProgressScroll(int currentWheelIndex)
    {
        LevelProgressScroll.MoveToTheNextItem();
        highlightAreaController.PlayHighlightMove(WheelsManager.Instance.GetWheelTypeForIndex(currentWheelIndex + 1), WheelsManager.Instance.GetWheelTypeForIndex(currentWheelIndex));
    }
    
    public void SetButtonsActive(bool active)
    {
        foreach (var button in uiButtons)
        {
            button.interactable = active;
        }
    }
}
