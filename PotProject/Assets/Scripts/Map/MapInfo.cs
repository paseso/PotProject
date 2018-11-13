using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInfo : MonoBehaviour {

    // どの場所にあるマップか
    public int mapNumX;
    public int mapNumY;

    public GameObject rock;
    [SerializeField]
    private bool _wood; // 伸びる木
    [SerializeField]
    private bool _upRock; // スイッチ岩
    [SerializeField]
    private int ladderCount; // 縄ハシゴ数

    public int LadderCount
    {
        get { return ladderCount; }
        private set { value = ladderCount; }
    }

    public bool UpRock
    {
        get {return this._upRock;}
        set {value = this._upRock;}
    }

    void Awake() {
        
    }
}
