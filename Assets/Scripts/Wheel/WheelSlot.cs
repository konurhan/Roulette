
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WheelSlot : MonoBehaviour
{
    [SerializeField] private Image rewardImage;
    [SerializeField] private TextMeshProUGUI rewardAmountText;
    
    private RewardData _rewardData;
    private bool _isBomb;
    private WheelController _wheelController;

    public bool IsBomb => _isBomb;

    public void Initialize(RewardData rewardData, bool isBomb, WheelController wheelController)
    {
        _wheelController = wheelController;
        _rewardData = rewardData;
        _isBomb = isBomb;
        //TODO: if bomb set bomb sprite
        if (!isBomb)
        {
            rewardImage.sprite = rewardData.sprite;
            if (rewardData.amount > 1000)
            {
                rewardAmountText.text = "x" + (float)Math.Round(rewardData.amount / 1000f, 2) + "K";
            }
            else
            {
                rewardAmountText.text = "x" + rewardData.amount.ToString();
            }
            
        }
        else
        {
            rewardImage.sprite = _wheelController.GetBombSprite();
            rewardAmountText.gameObject.SetActive(false);
        }
        
    }

    public float GetProbability()
    {
        return _rewardData.probability;
    }

    public RewardData GetRewardData()
    {
        return _rewardData;
    }
}
