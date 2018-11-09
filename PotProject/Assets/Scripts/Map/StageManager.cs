﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {
    
    /// <summary>
    /// スライドの方向
    /// </summary>
    public enum Direction
    {
        up,
        down,
        left,
        right
    }

    private List<List<GameObject>> Maps = new List<List<GameObject>>();

    [SerializeField]
    private GameObject[] mapLists;

    [SerializeField]
    private int stageLength;

    void Awake() {
        SetList();
    }

    void Start () {

	}
	
	void Update () {
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            Debug.Log("上");
            SrideStage(0, Direction.up);
        }

        if(Input.GetKeyDown(KeyCode.DownArrow)){
            Debug.Log("下");
            SrideStage(0, Direction.down);
        }

        if(Input.GetKeyDown(KeyCode.RightArrow)){
            Debug.Log("右");
            SrideStage(1, Direction.right);
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            Debug.Log("左");
            SrideStage(1, Direction.left);
        }

    }

    /// <summary>
    /// ListにMapを入れる
    /// </summary>
    public void SetList()
    {
        int count = 0;
        
        for(int i = 0; i < 3; i++)
        {
            List<GameObject> varMap = new List<GameObject>();
            for (int j = 0; j < 3; j++)
            {
                varMap.Add(mapLists[count]);
                count++;
            }
            Maps.Add(varMap);
        }
    }

    /// <summary>
    /// ステージスライド処理
    /// </summary>
    /// <param name="num"></param>
    /// <param name="dir"></param>
    public void SrideStage(int num,Direction dir)
    {
        GameObject temp;
        Vector2 tempPos = new Vector2();
        Vector2 turnPos = new Vector2();
        
        switch (dir)
        {
            case Direction.up: // 上

                // 折り返し座標を保持
                turnPos = Maps[stageLength - 1][num].transform.localPosition;

                // 折り返しMap以外をスライド
                for (int i = stageLength - 1; i > 0; i--)
                {                    
                    tempPos = Maps[i - 1][num].transform.localPosition;
                    Maps[i][num].transform.localPosition = tempPos;
                }

                // 折り返し
                Maps[0][num].transform.localPosition = turnPos;

                // スライド終了時の配列内入れ替え
                temp = Maps[0][num];
                for (int i = 0; i < stageLength - 1; i++)
                {
                    int tempNum = 0;
                    tempNum = Maps[i + 1][num].GetComponent<MapInfo>().mapNum;
                    Maps[i][num] = Maps[i + 1][num];
                    
                }
                Maps[stageLength - 1][num] = temp;
                break;

            case Direction.down: // 下

                // 折り返し座標を保持
                turnPos = Maps[0][num].transform.localPosition;

                // 折り返しMap以外をスライド
                for (int i = 0; i < stageLength - 1; i++)
                {
                    tempPos = Maps[i + 1][num].transform.localPosition;
                    Maps[i][num].transform.localPosition = tempPos;
                }
                // 折り返し
                Maps[stageLength - 1][num].transform.localPosition = turnPos;

                // スライド終了時の配列内入れ替え
                temp = Maps[stageLength - 1][num];
                for (int i = 0; i < stageLength - 1; i++)
                {
                    Maps[stageLength -1 -i][num] = Maps[stageLength -2 -i][num];
                }
                Maps[0][num] = temp;
                break;

            case Direction.right:// 右
                // 折り返し座標を保持
                turnPos = Maps[num][0].transform.localPosition;

                for (int i = 0; i < stageLength - 1; i++)
                {
                    tempPos = Maps[num][i + 1].transform.localPosition;
                    Maps[num][i].transform.localPosition = tempPos;
                }
                Maps[num][stageLength - 1].transform.localPosition = turnPos;

                // スライド終了時の配列内入れ替え
                temp = Maps[num][stageLength - 1];
                for (int i = 0; i < stageLength - 1; i++)
                {
                    Maps[num][stageLength - 1 - i] = Maps[num][stageLength - 2 - i];
                    Debug.Log(Maps[num][stageLength - 1 - i].name);
                }
                Maps[num][0] = temp;
                break;

            case Direction.left: // 左
                turnPos = Maps[num][stageLength - 1].transform.localPosition;
                for (int i = stageLength - 1; i > 0; i--)
                {
                    tempPos = Maps[num][i - 1].transform.localPosition;
                    Maps[num][i].transform.localPosition = tempPos;
                }
                Maps[num][0].transform.localPosition = turnPos;

                // スライド終了時の配列内入れ替え
                temp = Maps[num][0];
                for (int i = 0; i < stageLength - 1; i++)
                {
                    Maps[num][i] = Maps[num][i + 1];
                }
                Maps[num][stageLength - 1] = temp;
                break;
            default:
                break;
        }
    }
}