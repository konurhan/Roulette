using System;
using UnityEngine;

public class RewardFactory : SingletonGeneric<RewardFactory>
{
    [SerializeField] private GameObject rewardItemViewPrefab;
    public Tuple<RewardItem, RewardItemView> CreateRewardItem(RewardData rewardData)
    {
        RewardItem rewardItem = null;

        switch (rewardData.rewardType)
        {
            case RewardType.Currency:
                rewardItem = new CurrencyRewardItem(rewardData.amount, rewardData.currencyType);
                break;
            case RewardType.EquipmentPoint:
                rewardItem = new EquipmentRewardItem(rewardData.amount, rewardData.equipmentPointType);
                break;
            case RewardType.Chest:
                rewardItem = new ChestRewardItem(rewardData.amount, rewardData.chestType);
                break;
            case RewardType.Skin:
                rewardItem = new SkinRewardItem(rewardData.amount, rewardData.skinId);
                break;
        }
        
        GameObject rewardItemViewGo = Instantiate(rewardItemViewPrefab);
        RewardItemView rewardItemView = rewardItemViewGo.GetComponent<RewardItemView>();
        rewardItemView.Initalize(rewardData);
        return new Tuple<RewardItem, RewardItemView>(rewardItem, rewardItemView);
    }
}
