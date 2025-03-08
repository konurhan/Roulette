using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class WheelController : MonoBehaviour
{
    [SerializeField] private Transform wheelBody;
    [SerializeField] private Image wheelImage;
    [SerializeField] private Image indicatorImage;
    [SerializeField] private Button spinButton;
    
    [SerializeField] private List<WheelSlot> wheelSlots;
    
    [SerializeField] private AnimationCurve wheelAnimationCurve;
    
    private WheelType _wheelType;
    private bool _hasBomb;


    public void Initialize(WheelType wheelType, List<RewardData> rewards, float rewardMultiplier)
    {
        if (rewards.Count != wheelSlots.Count)
        {
            Debug.LogError("Reward count - slot count mismatch");
        }

        if (wheelType == WheelType.Bronze)
        {
            _hasBomb = true;
        }
        else
        {
            _hasBomb = false;
        }

        int bombIndex = 0;
        if (_hasBomb)
        {
            bombIndex = Random.Range(0, wheelSlots.Count);
        }
        
        for (int i = 0; i < wheelSlots.Count; i++)
        {
            rewards[i].amount = (int)(rewards[i].amount * rewardMultiplier);
            wheelSlots[i].Initialize(rewards[i], _hasBomb && i == bombIndex);
        }
        
        spinButton.onClick.AddListener(OnSpinButtonClicked);
    }

    private void OnSpinButtonClicked()
    {
        int probability = Random.Range(0, 100);
        float totalProbability = 0f;
        WheelSlot chosenSlot = wheelSlots[0];
        foreach (var wheelSlot in wheelSlots)
        {
            totalProbability += wheelSlot.GetProbability();
            if (totalProbability >= probability)
            {
                chosenSlot = wheelSlot;
                break;
            }
        }
        RotateToChosenSlot(chosenSlot, () => GiveChosenSlotReward(chosenSlot));
    }

    private void GiveChosenSlotReward(WheelSlot chosenSlot)
    {
        //TODO: bunu başka yere taşıyabilir miyiz, direkt slot rewardData referansı üzerinden vermek doğru mu, controller'ın üzerinde tutma da düşünülebilir
        var slotReward = chosenSlot.GetRewardData();
        RewardManager.Instance.GiveReward(slotReward);
    }
    
    public void RotateToChosenSlot(WheelSlot wheelSlot, Action giveRewardAction)//move indicator
    {
        transform.DOKill();
        float slotRotationZ = wheelSlot.transform.localRotation.eulerAngles.z;
        float wheelBodyTargetRotationZ = slotRotationZ * -1;
        float wheelBodyCurrentZRotation = wheelBody.localRotation.eulerAngles.z;
        float wheelBodyCurrentZRotationModulo = wheelBodyCurrentZRotation % 360;
        float totalZRotationDistance = 720;
        if (wheelBodyCurrentZRotationModulo > wheelBodyTargetRotationZ)
        {
            totalZRotationDistance += 360 - wheelBodyCurrentZRotationModulo + wheelBodyTargetRotationZ;
        }
        else
        {
            totalZRotationDistance += wheelBodyTargetRotationZ - wheelBodyCurrentZRotationModulo;
        }

        Vector3 targetRotation = new Vector3(wheelBody.localRotation.eulerAngles.x, 
                                            wheelBody.localRotation.eulerAngles.y, 
                                            wheelBodyCurrentZRotation + totalZRotationDistance);
        
        wheelBody.transform.DOLocalRotate(targetRotation, 4f, RotateMode.FastBeyond360)
            .SetEase(wheelAnimationCurve)
            .onComplete += () => { giveRewardAction(); };
    }
}


public enum WheelType
{
    Bronze,
    Silver,
    Gold,
}
