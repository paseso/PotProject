using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MapDate : ScriptableObject
{
    public MapArray[] mapDate = new MapArray[20];
    public MapArray[] gimmickDate = new MapArray[20];
    public MapArray[] enemyDate = new MapArray[20];

    public int _sampleIntValue;
}
[System.Serializable]
public class MapArray {

    public int[] mapNum;

}

