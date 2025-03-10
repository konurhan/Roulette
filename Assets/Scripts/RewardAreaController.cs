using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RewardAreaController : MonoBehaviour
{
    [SerializeField] private Transform rewardViewsParent;
    [SerializeField] private RectTransform rewardScrollContent;
    [SerializeField] private RectTransform rewardScrollViewport;
    [SerializeField] private VerticalLayoutGroup rewardLayoutGroup;
    
    private Dictionary<RewardItem, RewardItemView> _activeRewardItems = new Dictionary<RewardItem, RewardItemView>();
    
    public void AddRewardItem(RewardData rewardData)
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
            ScrollToGivenReward( _activeRewardItems[existingItem]);
        }
        else
        {
            var newItemTuple = RewardFactory.Instance.CreateRewardItem(rewardData);
            var rewardItem = newItemTuple.Item1;
            var rewardItemView = newItemTuple.Item2;
            _activeRewardItems[rewardItem] = rewardItemView;
            rewardItemView.transform.SetParent(rewardViewsParent);
            rewardItemView.transform.localScale = Vector3.one;
            ScrollToGivenReward(rewardItemView);
        }
    }

    public void ScrollToGivenReward(RewardItemView rewardItemView)
    {
        int rewardIndex = rewardItemView.transform.GetSiblingIndex(); 
        float scrollContentHeight = rewardScrollContent.rect.height;
        float viewPortHeight = rewardScrollViewport.rect.height;
        float singleItemLength = rewardLayoutGroup.spacing + rewardScrollContent.GetChild(0).GetComponent<RectTransform>().sizeDelta.y;//debug

        float itemTop = (rewardIndex) * singleItemLength;
        float itemBottom = (rewardIndex + 1) * singleItemLength;

        float scrollVisibleAreaTop = rewardScrollContent.anchoredPosition.y;
        float scrollVisibleAreaBottom = rewardScrollContent.anchoredPosition.y + viewPortHeight;
        bool isFullyVisible = itemTop >= scrollVisibleAreaTop && itemBottom <= scrollVisibleAreaBottom;

        if (isFullyVisible) return;

        bool scrollToTop = Mathf.Abs(itemTop - scrollVisibleAreaTop) < Mathf.Abs(itemTop - scrollVisibleAreaBottom);

        if (scrollToTop)
        {
            Vector2 newPos = new Vector2(rewardScrollContent.anchoredPosition.x, rewardScrollContent.anchoredPosition.y + itemTop - scrollVisibleAreaTop);
            rewardScrollContent.DOAnchorPos(newPos, 1);
        }
        else
        {
            Vector2 newPos = new Vector2(rewardScrollContent.anchoredPosition.x, rewardScrollContent.anchoredPosition.y + itemBottom - scrollVisibleAreaBottom);
            rewardScrollContent.DOAnchorPos(newPos, 1);
        }
    }
    
    public void ResetRewards()
    {
        foreach (var pair in _activeRewardItems.ToList())
        {
            Destroy(pair.Value.gameObject);
            _activeRewardItems.Remove(pair.Key);
        }

        rewardScrollContent.DOKill();
        rewardScrollContent.anchoredPosition = Vector2.zero;
    }
}
