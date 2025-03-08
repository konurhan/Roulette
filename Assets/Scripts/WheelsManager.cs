using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelsManager : SingletonGeneric<WheelsManager>
{
    [SerializeField] private Transform wheelParent;
    [SerializeField] private GameObject wheelPrefab;
    
    [SerializeField] private List<RewardDataGroup> bronzeRewards;
    [SerializeField] private List<RewardDataGroup> silverRewards;
    [SerializeField] private List<RewardDataGroup> goldRewards;

    [SerializeField][Range(1,2f)] private float progressMultiplier;

    private List<GameObject> _visibleWheels = new List<GameObject>();


    public void ShowWheel(int currentWheelIndex)
    {
        var wheelType = GetWheelTypeForIndex(currentWheelIndex);
        List<RewardDataGroup> wheelRewardPool = new List<RewardDataGroup>(); 
        switch (wheelType)
        {
            case WheelType.Bronze:
                wheelRewardPool = bronzeRewards;
                break;
            case WheelType.Silver:
                wheelRewardPool = silverRewards;
                break;
            case WheelType.Gold:
                wheelRewardPool = goldRewards;
                break;
        }
        RewardDataGroup rewardDatas = wheelRewardPool[Random.Range(0, wheelRewardPool.Count)];
        List<RewardData> rewardDataList = RewardFactory.Instance.GetRewardDataListFromSO(rewardDatas);
        CreateAndShowWheel(wheelType, rewardDataList, (currentWheelIndex + 1) * progressMultiplier);
    }
    
    private void CreateAndShowWheel(WheelType wheelType, List<RewardData> rewardDatas, float rewardMultiplier)
    {
        GameObject wheel = Instantiate(wheelPrefab, wheelParent);
        wheel.GetComponent<WheelController>().Initialize(wheelType, rewardDatas, rewardMultiplier);
        _visibleWheels.Add(wheel);
    }

    private WheelType GetWheelTypeForIndex(int currentWheelIndex)
    {
        if ((currentWheelIndex + 1) % 30 == 0)//gold wheel
        {
            return WheelType.Gold;
        }
        if ((currentWheelIndex + 1) % 5 == 0)//silver wheel
        {
            return WheelType.Silver;
        }
        return WheelType.Bronze;
    }

    public void ClearWheels()
    {
        foreach (var wheel in _visibleWheels)
        {
            Destroy(wheel);
        }
        _visibleWheels.Clear();
    }
}
