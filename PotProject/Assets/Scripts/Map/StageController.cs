using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour {
    
    /// <summary>
    /// スライドの方向
    /// </summary>
    public enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
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
        Vector3 tempPos = new Vector3();
        Vector3 turnPos = new Vector3();
        
        switch (dir)
        {
            case Direction.UP: // 上

                // 折り返し座標を保持
                turnPos = Maps[stageLength - 1][num].transform.position;
                turnPos.z = 90;

                // 折り返しMap以外をスライド
                for (int i = stageLength - 1; i > 0; i--)
                {                    
                    tempPos = Maps[i - 1][num].transform.position;
                    tempPos.z = 90;
                    Maps[i][num].transform.position = tempPos;
                }

                // 折り返し
                Maps[0][num].transform.position = turnPos;

                // スライド終了時の配列内入れ替え
                temp = Maps[0][num];
                for (int i = 0; i < stageLength - 1; i++)
                {
                    Maps[i][num] = Maps[i + 1][num];
                }
                Maps[stageLength - 1][num] = temp;
                break;

            case Direction.DOWN: // 下

                // 折り返し座標を保持
                turnPos = Maps[0][num].transform.position;
                turnPos.z = 90;

                // 折り返しMap以外をスライド
                for (int i = 0; i < stageLength - 1; i++)
                {
                    tempPos = Maps[i + 1][num].transform.position;
                    tempPos.z = 90;
                    
                    Maps[i][num].transform.position = tempPos;
                }
                // 折り返し
                Maps[stageLength - 1][num].transform.position = turnPos;

                // スライド終了時の配列内入れ替え
                temp = Maps[stageLength - 1][num];
                for (int i = 0; i < stageLength - 1; i++)
                {
                    Maps[stageLength -1 -i][num] = Maps[stageLength -2 -i][num];
                }
                Maps[0][num] = temp;
                break;

            case Direction.RIGHT:// 右
                // 折り返し座標を保持
                turnPos = Maps[num][0].transform.position;
                turnPos.z = 90;

                for (int i = 0; i < stageLength - 1; i++)
                {
                    tempPos = Maps[num][i + 1].transform.position;
                    tempPos.z = 90;
                    Maps[num][i].transform.position = tempPos;
                }
                Maps[num][stageLength - 1].transform.position = turnPos;

                // スライド終了時の配列内入れ替え
                temp = Maps[num][stageLength - 1];
                for (int i = 0; i < stageLength - 1; i++)
                {
                    Maps[num][stageLength - 1 - i] = Maps[num][stageLength - 2 - i];
                }
                Maps[num][0] = temp;
                break;

            case Direction.LEFT: // 左
                turnPos = Maps[num][stageLength - 1].transform.position;
                turnPos.z = 90;
                for (int i = stageLength - 1; i > 0; i--)
                {
                    tempPos = Maps[num][i - 1].transform.position;
                    tempPos.z = 90;
                    Maps[num][i].transform.position = tempPos;
                }
                Maps[num][0].transform.position = turnPos;

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

    /// <summary>
    /// ステージ入れ替え
    /// </summary>
    public void StageChange() {

    }

    /// <summary>
    /// ステージシャッフル
    /// </summary>
    public void StageShuffle() {
        GameObject[] tempMap = new GameObject[Maps.Count];
        Vector2 standardPos = Maps[0][0].transform.localPosition;
        int count = 0;
        for (int i = 0; i < Maps.Count; i++) {
            
            for(int j = 0; j < Maps[i].Count; i++) {
                tempMap[count] = Maps[i][j];
                count++;

            }
        }

        for(int i = 0; i <= count; i++) {
            int rand = Random.Range(0, count + 1);
            GameObject temp = tempMap[rand];
            tempMap[rand] = tempMap[i];
            tempMap[i] = temp;
        }
    }
}
