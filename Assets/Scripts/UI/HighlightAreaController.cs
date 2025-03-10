using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HighlightAreaController : MonoBehaviour
{
    [SerializeField] private Image highlightBgStatic;
    [SerializeField] private Image highlightBgDynamicRight;
    [SerializeField] private Image highlightBgDynamicLeft;

    [SerializeField] private float dynamicImageSizeX = 70f;
    [SerializeField] private float dynamicImageExpandDuration = 0.5f;
    [SerializeField] private float dynamicImageExpandDelay = 0.1f;

    [SerializeField] private Sprite goldWheelHighlight;
    [SerializeField] private Sprite silverWheelHighlight;
    [SerializeField] private Sprite bronzeWheelHighlight;

    private Dictionary<WheelType, Sprite> wheelTypeToSprite;
    
    private void Start()
    {
        wheelTypeToSprite = new Dictionary<WheelType, Sprite>()
        {
            {WheelType.Bronze, bronzeWheelHighlight},
            {WheelType.Silver, silverWheelHighlight},
            {WheelType.Gold, goldWheelHighlight}
        };
    }

    public void PlayHighlightMove(WheelType wheelTypeNext, WheelType wheelTypeCurrent)
    {
        highlightBgStatic.enabled = true;
        
        //hide current
        highlightBgDynamicRight.gameObject.SetActive(false);
        highlightBgDynamicLeft.sprite = wheelTypeToSprite[wheelTypeCurrent];
        var dynamicImageRectLeft = highlightBgDynamicLeft.GetComponent<RectTransform>();
        dynamicImageRectLeft.sizeDelta = new Vector2(dynamicImageSizeX, dynamicImageRectLeft.sizeDelta.y);
        highlightBgDynamicLeft.gameObject.SetActive(true);
        dynamicImageRectLeft.DOSizeDelta(new Vector2(dynamicImageSizeX, dynamicImageRectLeft.sizeDelta.y), dynamicImageExpandDelay)
            .OnComplete(() =>
            {
                highlightBgDynamicLeft.gameObject.SetActive(false);
                highlightBgDynamicRight.gameObject.SetActive(true);
            });
        
        //show next
        highlightBgDynamicRight.sprite = wheelTypeToSprite[wheelTypeNext];
        var dynamicImageRectRight = highlightBgDynamicRight.GetComponent<RectTransform>();
        dynamicImageRectRight.sizeDelta = new Vector2(0, dynamicImageRectRight.sizeDelta.y);
        dynamicImageRectRight.DOSizeDelta(new Vector2(dynamicImageSizeX, dynamicImageRectRight.sizeDelta.y), dynamicImageExpandDuration)
            .SetDelay(dynamicImageExpandDelay);
            /*.OnComplete(() => highlightBgDynamicRight.gameObject.SetActive(false));*/
    }
    
}
    
