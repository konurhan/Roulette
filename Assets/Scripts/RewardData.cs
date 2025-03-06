using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RewardData
{
    public int probability = 0;
    public int amount = 0;
    public RewardType rewardType;
    public CurrencyType currencyType = CurrencyType.None;
    public EquipmentPointType equipmentPointType = EquipmentPointType.None;
    public ChestType chestType = ChestType.None;
    public int skinId = -1;
    public Sprite sprite;

    // Copy constructor
    public RewardData(RewardData other)
    {
        if (other == null) return;

        this.probability = other.probability;
        this.amount = other.amount;
        this.rewardType = other.rewardType;
        this.currencyType = other.currencyType;
        this.equipmentPointType = other.equipmentPointType;
        this.chestType = other.chestType;
        this.skinId = other.skinId;
        this.sprite = other.sprite; // Note: Sprites are reference types
    }

#if UNITY_EDITOR
    [NonSerialized] private RewardType previousRewardType; // Track last rewardType
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
                    skinId = -1;
                    break;
                case RewardType.EquipmentPoint:
                    currencyType = CurrencyType.None;
                    equipmentPointType = 0;
                    chestType = ChestType.None;
                    skinId = -1;
                    break;
                case RewardType.Chest:
                    currencyType = CurrencyType.None;
                    equipmentPointType = EquipmentPointType.None;
                    chestType = 0;
                    skinId = -1;
                    break;
                case RewardType.Skin:
                    currencyType = CurrencyType.None;
                    equipmentPointType = EquipmentPointType.None;
                    chestType = ChestType.None;
                    skinId = 0;
                    break;
                default:
                    currencyType = CurrencyType.None;
                    equipmentPointType = EquipmentPointType.None;
                    chestType = ChestType.None;
                    skinId = -1;
                    break;
            }
            previousRewardType = rewardType;
        }
        
    }
#endif
}




