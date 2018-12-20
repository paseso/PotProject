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

    // スイッチ岩
    private bool _upRock;
    public bool UpRockFlag { get; set; }

    //public int LadderCount
    //{
    //    get { return this.ladderCount; }
    //    private set { value = this.ladderCount; }
    //}

    //public bool UpRock
    //{
    //    get {return this._upRock;}
    //    set {value = this._upRock;}
    //}

    
}
