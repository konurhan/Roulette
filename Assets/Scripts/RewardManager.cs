using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : SingletonGeneric<RewardManager>
{
    [SerializeField] private Transform rewardViewsParent;
    private Dictionary<RewardItem, RewardItemView> _activeRewardItems = new Dictionary<RewardItem, RewardItemView>();


    public void GiveReward(RewardData rewardData)
    {
        RewardItem existingItem = null;
        foreach (var pair in _activeRewardItems)
        {
            if (pair.Key.IsSameType(rewardData))
            {
                existingItem = pair.Key;
                break;
            }
        }

        if (existingItem != null)
        {
            existingItem.IncreaseAmount(rewardData.amount);
            _activeRewardItems[existingItem].IncreaseRewardAmount(existingItem.TotalAmount);
        }
        else
        {
            var newItemTuple = RewardFactory.Instance.CreateRewardItem(rewardData);
            var rewardItem = newItemTuple.Item1;
            var rewardItemView = newItemTuple.Item2;
            _activeRewardItems[rewardItem] = rewardItemView;
            rewardItemView.transform.SetParent(rewardViewsParent);
        }
    }
}
