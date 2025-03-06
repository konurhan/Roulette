using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RewardDataGroup", menuName = "ScriptableObjects/RewardDataGroup", order = 1)]
public class RewardDataGroup : ScriptableObject
{
    public List<RewardData> RewardList;
    
    private void OnValidate()
    {
        if (RewardList != null)
        {
            foreach (var reward in RewardList)
            {
                reward.OnRewardTypeUpdated();
            }
        }
    }
}
