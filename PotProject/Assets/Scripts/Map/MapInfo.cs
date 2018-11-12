using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInfo : MonoBehaviour {

    // どの場所にあるマップか
    public int mapNum;
    public GameObject rock;
    [SerializeField]
    private bool _wood; // 伸びる木
    private bool _upRock; // スイッチ岩
    [SerializeField]
    private int ladderCount;

    private bool[] _upLadders; // 縄ハシゴ
    private bool[] _downLadders; // 縄ハシゴ

    public bool UpRock
    {
        get {return this._upRock;}
        set {value = this._upRock;}
    }

    public bool[] UpLaddersFlag
    {
        get { return this._upLadders; }
        set { value = this._upLadders; }
    }

    public bool[] DownLaddersFlag
    {
        get { return this._downLadders; }
        set { value = this._downLadders; }
    }

    void Awake() {
        this._upLadders = new bool[this.ladderCount];
        this._downLadders = new bool[this.ladderCount];
    }
}
