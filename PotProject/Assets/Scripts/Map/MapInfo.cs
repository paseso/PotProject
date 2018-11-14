using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInfo : MonoBehaviour {

    // どの場所にあるマップか
    public int mapNumX;
    public int mapNumY;

    public GameObject rock;
    public GameObject tree;
    // 伸びる木
    private bool _tree;
    public bool GrowTreeFlag { get; set; }

    // スイッチ岩
    private bool _upRock;
    public bool UpRockFlag { get; set; }

    // はしごギミック判定
    private bool ladderFlag;
    public bool LadderFlag { get; set; }

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
