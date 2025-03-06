using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelsManager : SingletonGeneric<WheelsManager>
{
    [SerializeField] private GameObject wheelPrefab;

    private List<GameObject> _visibleWheels = new List<GameObject>();

    public void CreateAndShowWheel(WheelType wheelType, List<RewardData> rewardDatas)
    {
        GameObject wheel = Instantiate(wheelPrefab);
        wheel.GetComponent<WheelController>().Initialize(wheelType, rewardDatas);
    }
}
