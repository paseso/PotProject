using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ScriptableObjectSample : ScriptableObject {

    [SerializeField]
    private int _sampleIntValue;
    [SerializeField]
    private int[,] _mapData = new int[20, 20];

    public int SampleIntValue
    {
        get { return _sampleIntValue; }
#if UNITY_EDITOR
        set { _sampleIntValue = Mathf.Clamp(value, 0, int.MaxValue); }
#endif
    }

    public int[,] MapData
    {
        get { return _mapData; }
#if UNITY_EDITOR
        set { _mapData = value; }
    }
#endif

}
