using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelProgressViewItem : BaseScrollableContent
{
    [SerializeField] private TextMeshProUGUI levelText;
    
    public override void Initialize(BaseScrollableContentData content)
    {
        var data = content as LevelProgressItemData;
        levelText.text = (data.LevelNumber + 1).ToString();
        levelText.color = data.LevelColorText;
    }
}

public class LevelProgressItemData : BaseScrollableContentData
{
    public int LevelNumber;
    public Color LevelColorText;

    public LevelProgressItemData(int levelNumber, Color levelColorText)
    {
        LevelNumber = levelNumber;
        LevelColorText = levelColorText;
    }
}
