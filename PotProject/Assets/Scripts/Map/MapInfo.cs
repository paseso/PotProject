using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInfo : MonoBehaviour {

    // どの場所にあるマップか
    public int mapNum;
    public GameObject rock;
    [SerializeField]
    private bool _wood; // 伸びる木
    [SerializeField]
    private bool _upRock; // スイッチ岩
    [SerializeField]
    private int ladderCount;

    private bool[] _upLadders; // 縄ハシゴ
    private bool[] _downLadders; // 縄ハシゴ

    void Awake() {
        this._upLadders = new bool[this.ladderCount];
        this._downLadders = new bool[this.ladderCount];

        Debug.Log(_upLadders.Length);
    }
}
