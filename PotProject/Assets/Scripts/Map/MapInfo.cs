using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// マップ情報
/// </summary>
public class MapInfo : MonoBehaviour {

    // どの場所にあるマップか
    private int mapNumX;
    private int mapNumY;

    // このマップが踏破済か
    private bool isAccessMap = false;

    public GameObject rock;
    // 伸びる木
    private bool _tree;
    public bool GrowTreeFlag { get; set; }

    public int MapNumX{
        get { return mapNumX; }
        set { mapNumX = value; }
    }

    public int MapNumY {
        get { return mapNumY; }
        set { mapNumY = value; }
    }

    public bool IsAccessMap
    {
        get { return isAccessMap; }
        set { isAccessMap = value; }
    }

    // スイッチ岩
    private bool _upRock;
    public bool UpRockFlag { get; set; }
}
