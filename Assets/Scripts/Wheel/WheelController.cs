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

    [SerializeField] private float idleRotationSpeed = 50f;
    
    [SerializeField] private Sprite bombSprite;
    
    [SerializeField] private Animator _spinButtonAnimator;
    
    private WheelType _wheelType;
    private bool _hasBomb;
    
    public void Initialize(WheelType wheelType, List<RewardData> rewards, float rewardMultiplier)//set wheel sprite by type
    {
        if (rewards.Count != wheelSlots.Count)
        {
            Debug.LogError("Reward count - slot count mismatch");
        }
        
        spinButton.gameObject.SetActive(false);
        spinButton.gameObject.GetComponent<SpinButtonAnimationEventListener>().Initialize(this);
        
        wheelImage.sprite = WheelsManager.Instance.GetWheelSprite(wheelType);
        indicatorImage.sprite = WheelsManager.Instance.GetIndicatorSprite(wheelType);

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
            wheelSlots[i].Initialize(rewards[i], _hasBomb && i == bombIndex, this);
        }
    }

    public void OnReachedActivePosition()
    {
        ShowSpinButton();
        spinButton.onClick.AddListener(OnSpinButtonClicked);
        //wheel idle rotation
        wheelBody.transform.DOLocalRotate(Vector3.forward * idleRotationSpeed, 1f, RotateMode.FastBeyond360)
            .SetRelative()
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Incremental);
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
        HideSpinButton();
    }

    private void GiveChosenSlotReward(WheelSlot chosenSlot)
    {
        //TODO: bunu başka yere taşıyabilir miyiz, direkt slot rewardData referansı üzerinden vermek doğru mu, controller'ın üzerinde tutma da düşünülebilir
        if (chosenSlot.IsBomb)
        {
            if (GameplayManager.Instance.IsBombsEnabled)
            {
                GameplayManager.Instance.SetLost();
            }
            else
            {
                GameplayManager.Instance.HideWheel();
            }
            
        }
        else
        {
            var slotReward = chosenSlot.GetRewardData();
            RewardManager.Instance.GiveReward(slotReward);
            GameplayManager.Instance.HideWheel();
        }
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
        wheelBody.transform.DOKill();
        wheelBody.transform.DOLocalRotate(targetRotation, 4f, RotateMode.FastBeyond360)
            .SetEase(wheelAnimationCurve)
            .onComplete += () => { giveRewardAction(); };
    }

    public Sprite GetBombSprite()
    {
        return bombSprite;
    }

    private void ShowSpinButton()
    {
        spinButton.gameObject.SetActive(true);
        spinButton.interactable = false;
        _spinButtonAnimator.Play("SpinButtonBorn");
    }

    private void HideSpinButton()
    {
        spinButton.interactable = false;
        _spinButtonAnimator.Play("SpinButtonDisappear");
    }
    
    #region AnimationEvents

    public void SpinBornAnimComplete()
    {
        spinButton.interactable = true;
    }
    
    public void SpinDisappearAnimComplete()
    {
        spinButton.gameObject.SetActive(false);
    }

    #endregion
}


public enum WheelType
{
    Bronze,
    Silver,
    Gold,
}
