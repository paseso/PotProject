using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MapDate : ScriptableObject {

    public MapArray[] mapArray;    

    [SerializeField]
    private int _sampleIntValue;
    [SerializeField]
    private int[,] _mapDataList = new int[20,20];
    [SerializeField]
    private int[] testArray = new int[5];
    
    //MapDate()
    //{
    //    if (_mapDataList == null)
    //    {

    //        //_mapDataList = new int[20, 20];
    //    }
    //}




    public int SampleIntValue
    {
        get { return _sampleIntValue; }
#if UNITY_EDITOR
        set { _sampleIntValue = Mathf.Clamp(value, 0, int.MaxValue); }
#endif
    }

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

    public int[] TestArray
    {
        get
        {
            return testArray;
        }

        set
        {
            testArray = value;
        }
    }
}

[System.Serializable]
public class MapArray {

    public int[] mapNumber;    

}

