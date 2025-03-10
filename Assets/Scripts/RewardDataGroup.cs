using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RewardDataGroup", menuName = "ScriptableObjects/RewardDataGroup", order = 1)]
public class RewardDataGroup : ScriptableObject
{
    public SpecialItemCollection specialItemCollection;
    public List<RewardDataSO> RewardList;
    
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (RewardList != null)
        {
            foreach (var reward in RewardList)
            {
                reward.OnRewardTypeUpdated();
                reward.OnSpecialItemIdUpdated(specialItemCollection);
            }
        }
    }
#endif
}
