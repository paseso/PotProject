using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ItemStatus {
    public enum Type {
        VAJURA = 0,
        MIC,
        LAMP,
        KEYROD,
        CLAY,
        SNAKE,
        LIZARD,
        WOOD,
        CROWN,
        SMOKE,
        CRYSTAL,
        CLOUD,
        POWDER,
        FLOWER,
    };

    public Type type;
};

public class ItemManager : MonoBehaviour {

    
    [SerializeField]
    private ItemStatus item_status;
    
    /// <summary>
    /// Item Status取得
    /// </summary>
    public ItemStatus.Type getItemStatus()
    {
        return item_status.type;
    }

}
