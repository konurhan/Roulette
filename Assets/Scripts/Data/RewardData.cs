using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RewardData
{
    public float probability = 0;
    public int amount = 0;
    public RewardType rewardType;
    public CurrencyType currencyType = CurrencyType.None;
    public EquipmentPointType equipmentPointType = EquipmentPointType.None;
    public ChestType chestType = ChestType.None;
    public int specialItemID = -1;
    public Sprite sprite;
    
    public RewardData(RewardDataSO scriptableData)
    {
        if (scriptableData == null) return;

        this.probability = scriptableData.probability;
        this.amount = scriptableData.amount;
        this.rewardType = scriptableData.rewardType;
        this.currencyType = scriptableData.currencyType;
        this.equipmentPointType = scriptableData.equipmentPointType;
        this.chestType = scriptableData.chestType;
        this.specialItemID = scriptableData.specialItemID;
        this.sprite = scriptableData.sprite; // Sprite is reference type
    }


}




