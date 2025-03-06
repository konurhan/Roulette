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


    public void Initialize(WheelType wheelType, List<RewardData> rewards)
    {
        if (rewards.Count != wheelSlots.Count)
        {
            Debug.LogError("Reward count - slot count mismatch");
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
