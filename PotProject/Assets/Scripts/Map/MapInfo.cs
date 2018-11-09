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
    private bool[] _ladders; // 縄ハシゴ
    
}
