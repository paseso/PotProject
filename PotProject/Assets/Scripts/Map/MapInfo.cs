using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// マップ情報
/// </summary>
public class MapInfo : MonoBehaviour {

    // このマップが踏破済か
    private bool isAccessMap = false;

    public GameObject rock;
    // 伸びる木
    private bool _tree;
    public bool GrowTreeFlag { get; set; }

    public int MapNumX{ get; set; }

    public int MapNumY { get; set; }

    public bool IsAccessMap
    {
        get { return isAccessMap; }
        set { isAccessMap = value; }
    }

    // スイッチ岩
    private bool _upRock;
    public bool UpRockFlag { get; set; }

    // 属性
    public enum Attribute
    {
        NORMAL,
        FIRE,
        THUNDER,
        ICE,
        DARK,
        ROCK,
    }

    public Attribute attribute;
}
