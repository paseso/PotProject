using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {

    [SerializeField]
    private MapDate[] stageDate = new MapDate[9];

    [SerializeField]
    private int stageLength = 3;


    private void OnValidate()
    {
        if (stageDate.Length != stageLength*stageLength)
        stageDate = new MapDate[stageLength * stageLength];
    }
}
