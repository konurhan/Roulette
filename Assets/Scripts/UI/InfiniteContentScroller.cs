using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class InfiniteContentScroller : MonoBehaviour
{
    [SerializeField] private HorizontalLayoutGroup contentLayoutGroup;
    [SerializeField] private RectTransform viewport;
    [SerializeField] private RectTransform content;
    [SerializeField] private ScrollRect scrollRect;
    
    [SerializeField] private GameObject contentItemViewPrefab;
    [SerializeField] private int contentItemViewCount = 7;
    [SerializeField] private int contentItemStartIndex = 0;//index to start visualizing content data from
    [SerializeField] private float contentStartPosX = 250f;
    [SerializeField] private float scrollSpeed = 0.5f;
    
    private List<RectTransform> _contentViewList = new List<RectTransform>();
    private List<BaseScrollableContentData> _scrollableContentDataList = new List<BaseScrollableContentData>();
    
    private bool _contentPositionUpdated = false;
    private Vector2 _previousFrameScrollVelocity = Vector2.zero;
    
    private bool _isInitialized = false;
    private bool _isContentMoving = false;

    public void Initialize(List<BaseScrollableContentData> scrollableContentDataList)
    {
        contentItemStartIndex = 0;
        
        foreach (var contentView in _contentViewList.ToList())
        {
            _contentViewList.Remove(contentView);
            Destroy(contentView.gameObject);
        }
        
        content.anchoredPosition = new Vector2(contentStartPosX, content.anchoredPosition.y);
        _scrollableContentDataList = scrollableContentDataList;
        
        for (int i = 0; i < contentItemViewCount; i++)
        {
            GameObject itemView = Instantiate(contentItemViewPrefab, content);
            itemView.GetComponent<BaseScrollableContent>().Initialize(_scrollableContentDataList[contentItemStartIndex + i]);
            _contentViewList.Add(itemView.GetComponent<RectTransform>());
        }
        
        _isInitialized = true;
    }
    
    private void UpdateContentViewItemWithCurrentDataSet()//called after repositioning content rect
    {
        for (int i = 0; i < contentItemViewCount; i++)
        {
            GameObject itemView = _contentViewList[i].gameObject;
            itemView.GetComponent<BaseScrollableContent>().Initialize(_scrollableContentDataList[contentItemStartIndex + i]);
        }
    }

    public void MoveToTheNextItem()
    {
        float distance = _contentViewList[0].rect.width + contentLayoutGroup.spacing;
        _isContentMoving = true;
        content.DOAnchorPosX(content.anchoredPosition.x - distance, scrollSpeed).OnComplete(()=>_isContentMoving=false);
    }

    
    void Update()
    {
        if (!_isInitialized) return;
        if (_isContentMoving) return;
        if (_contentPositionUpdated)
        {
            scrollRect.enabled = true;
            _contentPositionUpdated = false;
            scrollRect.velocity = _previousFrameScrollVelocity;
        }
        
        if (content.anchoredPosition.x < 0 - (_contentViewList[0].rect.width + contentLayoutGroup.spacing))
        {
            content.DOKill();
            _contentPositionUpdated = true;
            _previousFrameScrollVelocity = scrollRect.velocity;
            content.anchoredPosition += new Vector2(_contentViewList[0].rect.width + contentLayoutGroup.spacing, 0);
            contentItemStartIndex++;//to update view items
            UpdateContentViewItemWithCurrentDataSet();
            scrollRect.enabled = false;
            Canvas.ForceUpdateCanvases();
        }
    }
}
