using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RewardItemView : MonoBehaviour
{
    [SerializeField] private Image _rewardImage;
    [SerializeField] private TextMeshProUGUI _rewardAmountText;

    private int _currentRewardAmount = 0;

    private Coroutine _textIncreaseRoutine = null;
    private bool _textIncreasing = false;
    private void Awake()
    {
        //_rewardImage.
        _rewardAmountText.gameObject.SetActive(false);
        _rewardImage.gameObject.SetActive(false);
    }

    public void Initalize(RewardData rewardData)
    {
        _rewardImage.sprite = rewardData.sprite;
        _rewardAmountText.text = "0";
        _currentRewardAmount = rewardData.amount;
        
        _rewardImage.gameObject.SetActive(true);
        _rewardAmountText.gameObject.SetActive(true);
        
        StartIncreaseAnimation();
    }

    public void IncreaseRewardAmount(int totalRewardAmount)
    {
        _currentRewardAmount = totalRewardAmount;
        StartIncreaseAnimation();
    }

    private void StartIncreaseAnimation()//TODO: play image bounce tween once when each reward tween reaches rewardItemView 
    {
        //increase rewardAmount to current value
        PlayTextIncreaseTween();
        PlayImageIncreaseTween();
    }

    private void PlayImageIncreaseTween()
    {
        _rewardImage.DOKill();
        _rewardImage.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.3f).SetLoops(6 ,LoopType.Yoyo);
    }
    
    private void PlayTextIncreaseTween()
    {
        if (_textIncreasing && _textIncreaseRoutine != null)
        {
            StopCoroutine(_textIncreaseRoutine);
        }
        _textIncreaseRoutine = StartCoroutine(IncreaseText(_currentRewardAmount));
    }

    private IEnumerator IncreaseText(int target)
    {
        int start = _rewardAmountText.text != null ? int.Parse(_rewardAmountText.text) : 0;
        if (start >= target) yield break;
        
        _textIncreasing = true;
        _rewardAmountText.transform.DOKill();
        _rewardAmountText.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.3f).SetLoops(-1 ,LoopType.Yoyo);
        
        for (int i = start + 1; i < target + 1; i++)
        {
            _rewardAmountText.text = i.ToString();
            yield return null;
        }
        
        _rewardAmountText.transform.DOKill();
        _textIncreasing = false;
    }
    
}
