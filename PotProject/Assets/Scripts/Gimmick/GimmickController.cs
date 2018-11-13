using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickController : MonoBehaviour {
    private StageManager sManager = new StageManager();

    [SerializeField]
    private enum GimmickType
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        ROCK,
        LADDER,
    }

    public void IsMapSride(int nowMapNum ,StageManager.Direction dir)
    {
        sManager.SrideStage(nowMapNum, dir);
    }
}
