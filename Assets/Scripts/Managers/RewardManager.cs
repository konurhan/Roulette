using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RewardManager : SingletonGeneric<RewardManager>
{
    [SerializeField] private RewardAreaController rewardAreaController;

    public void GiveReward(RewardData rewardData)
    {
        rewardAreaController.AddRewardItem(rewardData);
    }

    public void ResetRewards()
    {
        rewardAreaController.ResetRewards();
    }

    
}
