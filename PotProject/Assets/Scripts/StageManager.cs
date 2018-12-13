using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {


    [SerializeField]
    private Vector2 stageLength = new Vector2(3, 3);
    [SerializeField]
    private MapDate[] stageDate = new MapDate[9];

    private void OnValidate()
    {
        stageLength.x = (int)stageLength.x;
        stageLength.y = (int)stageLength.y;

        if (stageDate.Length != stageLength.x * stageLength.y)
        {
            stageDate = new MapDate[((int)stageLength.x * (int)stageLength.y)];
        }
    }
}
