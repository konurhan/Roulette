using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelController : MonoBehaviour
{
    [SerializeField] private Image wheelImage;
    [SerializeField] private Image indicatorImage;
    
    [SerializeField] private List<WheelSlot> wheelSlots;
    
    private WheelType _wheelType;
    private bool _hasBomb;


    public void Initialize(WheelType wheelType, List<RewardData> rewards, float rewardMultiplier)
    {
        if (rewards.Count != wheelSlots.Count)
        {
            Debug.LogError("Reward count - slot count mismatch");
        }

        List<RewardData> copyRewards = new List<RewardData>();
        foreach (var rewardData in rewards)
        {
            copyRewards.Add(new RewardData(rewardData));
        }

        if (wheelType == WheelType.Bronze)
        {
            _hasBomb = true;
        }
        else
        {
            _hasBomb = false;
        }

        int bombIndex = 0;
        if (_hasBomb)
        {
            bombIndex = Random.Range(0, wheelSlots.Count);
        }
        
        for (int i = 0; i < wheelSlots.Count; i++)
        {
            copyRewards[i].amount = (int)(copyRewards[i].amount * rewardMultiplier);
            wheelSlots[i].Initialize(copyRewards[i], _hasBomb && i == bombIndex);
        }
    }

    public void RotateToChosenSlot(WheelSlot wheelSlot)
    {
        
    }
}


public enum WheelType
{
    Bronze,
    Silver,
    Gold,
}
