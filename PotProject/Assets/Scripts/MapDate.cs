using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MapDate : ScriptableObject {

    public MapArray[] mapArray = new MapArray[20];    

    public int _sampleIntValue;
    [SerializeField]
    private int[,] _mapDataList;


    public int[,] MapDataList
    {
        get
        {
            return _mapDataList;
        }
//#if UNITY_EDITOR
        set
        {
            _mapDataList = value;
        }
//#endif
    }
}

[System.Serializable]
public class MapArray {

    public int[] mapNum;

}

