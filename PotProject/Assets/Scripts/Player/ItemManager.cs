﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ItemStatus
{
    public enum ITEM
    {
        SLIME = 0,
        GOLEM,
        SNAKE,
        PORTION_G,
        PORTION_R,
        HARB,
        ROPE,
    };

    public ITEM item;
};

public class ItemManager : MonoBehaviour {

    
    [SerializeField]
    private ItemStatus item_status;
    
    /// <summary>
    /// Item Status取得
    /// </summary>
    public ItemStatus.ITEM getItemStatus()
    {
        return item_status.item;
    }

}
