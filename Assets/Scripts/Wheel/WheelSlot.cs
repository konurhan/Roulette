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

    public void Initialize(RewardData rewardData, bool isBomb)
    {
        _rewardData = rewardData;
        _isBomb = isBomb;
        rewardImage.sprite = rewardData.sprite;
        rewardAmountText.text = rewardData.amount.ToString();
    }
}
