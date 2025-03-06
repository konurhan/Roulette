using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RewardItem
{
    protected RewardType _rewardType;
    protected int _rewardAmount;
    
    public int TotalAmount => _rewardAmount;

    protected RewardItem(RewardType rewardType, int rewardAmount)
    {
        _rewardType = rewardType;
        _rewardAmount = rewardAmount;
    }

    public virtual bool IsSameType(RewardData rewardData)
    {
        return false;
    }

    public void IncreaseAmount(int newRewardAmount)
    {
        _rewardAmount += newRewardAmount;
    }
}

public class CurrencyRewardItem : RewardItem
{
    private CurrencyType _currencyType {get;}
    
    public CurrencyRewardItem(int amount, CurrencyType currencyType) : base(RewardType.Currency, amount)
    {
        _currencyType = currencyType;
    }

    public override bool IsSameType(RewardData rewardData)
    {
        return rewardData.currencyType != CurrencyType.None && rewardData.currencyType == this._currencyType;
    }
}

public class EquipmentRewardItem : RewardItem
{
    private EquipmentPointType _equipmentPointType {get;}
    
    public EquipmentRewardItem(int amount, EquipmentPointType equipmentPointType) : base(RewardType.EquipmentPoint, amount)
    {
        _equipmentPointType = equipmentPointType;
    }

    public override bool IsSameType(RewardData rewardData)
    {
        return rewardData.equipmentPointType != EquipmentPointType.None && rewardData.equipmentPointType == this._equipmentPointType;
    }
}

public class ChestRewardItem : RewardItem
{
    private ChestType _chestType {get;}
    
    public ChestRewardItem(int amount, ChestType chestType) : base(RewardType.Chest, amount)
    {
        _chestType = chestType;
    }

    public override bool IsSameType(RewardData rewardData)
    {
        return rewardData.chestType != ChestType.None && rewardData.chestType == this._chestType;
    }
}

public class SkinRewardItem : RewardItem
{
    private int _skinID {get;}
    
    public SkinRewardItem(int amount, int skinID) : base(RewardType.Chest, amount)
    {
        _skinID = skinID;
    }

    public override bool IsSameType(RewardData rewardData)
    {
        return rewardData.skinId != -1 && rewardData.skinId == this._skinID;
    }
}


public enum RewardType
{
    Currency,
    EquipmentPoint,
    Chest,
    Skin
}

public enum CurrencyType
{
    Coin,
    Cash,
    None
}

public enum EquipmentPointType
{
    ArmorPoint,
    KnifePoint,
    PistolPoint,
    RiflePoint,
    ShotgunPoint,
    SniperPoint,
    SubmachinePoint,
    None
}

public enum ChestType
{
    DarkGreen,
    Orange,
    Gold,
    Silver,
    LightGreen,
    Yellow,
    Red,
    None
}