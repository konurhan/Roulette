using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpecialItemCollection", menuName = "ScriptableObjects/SpecialItemCollection", order = 1)]
public class SpecialItemCollection : ScriptableObject
{
    public List<SpecialItemData> SpecialItems;
    
    public Sprite GetSpriteById(int id)
    {
        return SpecialItems.Find(sI => sI.Id == id).Icon;
    }
}
