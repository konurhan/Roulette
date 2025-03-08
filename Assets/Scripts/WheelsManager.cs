using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class WheelsManager : SingletonGeneric<WheelsManager>
{
    [SerializeField] private Transform wheelActiveTransform;
    [SerializeField] private Transform wheelCreationTransform;
    [SerializeField] private Transform wheelDestructionTransform;
    [SerializeField] private GameObject wheelPrefab;
    
    [SerializeField] private List<RewardDataGroup> bronzeRewards;
    [SerializeField] private List<RewardDataGroup> silverRewards;
    [SerializeField] private List<RewardDataGroup> goldRewards;

    [SerializeField] private Sprite bronzeWheelSprite;
    [SerializeField] private Sprite silverWheelSprite;
    [SerializeField] private Sprite goldWheelSprite;
    
    [SerializeField] private Sprite bronzeIndicatorSprite;
    [SerializeField] private Sprite silverIndicatorSprite;
    [SerializeField] private Sprite goldIndicatorSprite;

    [SerializeField][Range(1,2f)] private float progressMultiplier;

    private Dictionary<int, GameObject> _activeWheels = new Dictionary<int, GameObject>();


    public Sprite GetWheelSprite(WheelType wheelType)
    {
        switch (wheelType)
        {
            case WheelType.Gold:
                return goldWheelSprite;
            case WheelType.Silver:
                return silverWheelSprite;
            case WheelType.Bronze:
                return bronzeWheelSprite;
        }
        return null;
    }
    
    public Sprite GetIndicatorSprite(WheelType wheelType)
    {
        switch (wheelType)
        {
            case WheelType.Gold:
                return goldIndicatorSprite;
            case WheelType.Silver:
                return silverIndicatorSprite;
            case WheelType.Bronze:
                return bronzeIndicatorSprite;
        }
        return null;
    }
    
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
        var newWheel = CreateAndShowWheel(wheelType, rewardDataList, (currentWheelIndex + 1) * progressMultiplier);
        _activeWheels.Add(currentWheelIndex, newWheel);
    }

    public void HideWheel(int currentWheelIndex)
    {
        MoveOutAndDestroyWheel(currentWheelIndex);
    }
    
    private GameObject CreateAndShowWheel(WheelType wheelType, List<RewardData> rewardDatas, float rewardMultiplier)
    {
        GameObject wheel = Instantiate(wheelPrefab, wheelCreationTransform);
        wheel.transform.localScale = Vector3.one * 0.66f;
        WheelController wheelController = wheel.GetComponent<WheelController>();
        wheelController.Initialize(wheelType, rewardDatas, rewardMultiplier);
        MoveWheelToActiveTransform(wheel, wheelController.OnReachedActivePosition);
        return wheel;
    }

    private void MoveOutAndDestroyWheel(int currentWheelIndex)
    {
        var wheel = _activeWheels[currentWheelIndex];
        MoveWheelToDestructionTransform(wheel, () =>
        {
            _activeWheels.Remove(currentWheelIndex);
            Destroy(wheel);
        });
    }

    private void MoveWheelToActiveTransform(GameObject wheel, Action onComplete)
    {
        wheel.transform.SetParent(wheelActiveTransform);
        wheel.transform.DOScale(Vector3.one, 2f);
        wheel.transform.DOLocalMove(Vector3.zero, 2f).onComplete += () =>
        {
            onComplete();
            GameplayManager.Instance.SetState(GameState.Idle);
        };
    }
    
    private void MoveWheelToDestructionTransform(GameObject wheel, Action onComplete)
    {
        wheel.transform.SetParent(wheelDestructionTransform);
        wheel.transform.DOScale(Vector3.one * 0.66f, 2f);
        wheel.transform.DOLocalMove(Vector3.zero, 2f).onComplete += () =>
        {
            onComplete();
        };;
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
        foreach (var wheel in _activeWheels.ToList())
        {
            _activeWheels.Remove(wheel.Key);
            Destroy(wheel.Value);
        }
        _activeWheels.Clear();
    }
}
