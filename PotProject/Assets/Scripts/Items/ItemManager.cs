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
        CLAY_N,
        CLAY_D,
        CLAY_F,
        CLAY_T,
        CLAY_I,
        SNAKE,
        LIZARD,
        WOOD,
        CROWN,
        SMOKE,
        CRYSTAL,
        CLOUD,
        POWDER,
        FLOWER,
        SMOKESCREEN,
        EXPLOSIVE,
        FAIRY,
    };

    public Type type;
};

public class ItemManager : MonoBehaviour {

    [SerializeField]
    private ItemStatus.Type item_status = ItemStatus.Type.CLAY_N;
    
    /// <summary>
    /// Item Status取得
    /// </summary>
    public ItemStatus.Type getItemStatus()
    {
        return item_status;
    }

}
