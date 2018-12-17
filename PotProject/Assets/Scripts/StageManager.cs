using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {

    [SerializeField]
    private bool isInstance = true;
    [SerializeField]
    private Vector2 stageLength = new Vector2(3, 3);
    [SerializeField]
    private MapData[] stageDate = new MapData[9];

    private MapCreator mapCreator;

    private void Awake()
    {
        mapCreator = FindObjectOfType<MapCreator>();
        if (isInstance)
        {
            mapCreator.CreateMap(stageDate);
        }
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.F1))
        //{
        //    mapCreator.CreateMap(stageDate[0]);
        //}

        //if (Input.GetKeyDown(KeyCode.F2))
        //{
        //    mapCreator.CreateMap(stageDate);
        //}

    }

    private void OnValidate()
    {
        stageLength.x = (int)stageLength.x;
        stageLength.y = (int)stageLength.y;

        if (stageDate.Length != stageLength.x * stageLength.y)
        {
            stageDate = new MapData[((int)stageLength.x * (int)stageLength.y)];
        }
    }
}
