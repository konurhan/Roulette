using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RewardDataSO
{
    public float probability = 0;
    public int amount = 0;
    public RewardType rewardType;
    public CurrencyType currencyType = CurrencyType.None;
    public EquipmentPointType equipmentPointType = EquipmentPointType.None;
    public ChestType chestType = ChestType.None;
    public int specialItemID = -1;
    public Sprite sprite;


#if UNITY_EDITOR
    [SerializeField] private RewardType previousRewardType; // Track last rewardType
    [SerializeField] private int previousSpecialID = -1;
    public void OnRewardTypeUpdated()
    {
        if (rewardType != previousRewardType)
        {
            switch (rewardType)
            {
                case RewardType.Currency:
                    currencyType = 0;
                    equipmentPointType = EquipmentPointType.None;
                    chestType = ChestType.None;
                    specialItemID = -1;
                    break;
                case RewardType.EquipmentPoint:
                    currencyType = CurrencyType.None;
                    equipmentPointType = 0;
                    chestType = ChestType.None;
                    specialItemID = -1;
                    break;
                case RewardType.Chest:
                    currencyType = CurrencyType.None;
                    equipmentPointType = EquipmentPointType.None;
                    chestType = 0;
                    specialItemID = -1;
                    break;
                case RewardType.Special:
                    currencyType = CurrencyType.None;
                    equipmentPointType = EquipmentPointType.None;
                    chestType = ChestType.None;
                    specialItemID = 0;
                    break;
                default:
                    currencyType = CurrencyType.None;
                    equipmentPointType = EquipmentPointType.None;
                    chestType = ChestType.None;
                    specialItemID = -1;
                    break;
            }
            previousRewardType = rewardType;
        }
    }
    
    public void OnSpecialItemIdUpdated(SpecialItemCollection specialItemCollection)
    {
        if (rewardType == RewardType.Special && previousSpecialID != specialItemID)
        {
            sprite = specialItemCollection.GetSpriteById(specialItemID);
            previousSpecialID = specialItemID;
        }
    }
#endif
}
