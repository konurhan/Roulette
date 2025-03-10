using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FailPopup : MonoBehaviour
{
    [SerializeField] private Button giveUpButton;
    [SerializeField] private Button reviveButton;

    [SerializeField] private Transform skullShine;
    [SerializeField] private Transform cardGlow;
    
    //TODO: açılma kapanma animasyonu ekle
    void Start()
    {
        giveUpButton.onClick.AddListener(OnGiveUpButtonClicked);
        
        skullShine.DOLocalRotate(Vector3.forward * 20, 1f, RotateMode.FastBeyond360)
            .SetRelative()
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Incremental);
        skullShine.DOScale(Vector3.one * 1.5f, 1f).SetLoops(-1, LoopType.Yoyo);
        skullShine.gameObject.GetComponent<Image>().DOFade(0.4f, 2f).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnGiveUpButtonClicked()
    {
        skullShine.DOKill();
        GameplayManager.Instance.Restart();
        Destroy(gameObject);
    }
}
