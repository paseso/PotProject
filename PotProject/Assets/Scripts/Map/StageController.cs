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

    public List<List<GameObject>> GetMaps
    {
        get { return Maps; }
    }

    [SerializeField]
    private GameObject[] mapLists;

    [SerializeField]
    private int stageLength;

    void Awake() {
        SetList();
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
    public void SrideStage(int num, Direction dir)
    {
        GameObject temp;
        Vector3 tempPos = new Vector3();
        Vector3 turnPos = new Vector3();
        int turnPosX = 0;
        int turnPosY = 0;

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
                turnPosY = Maps[0][num].GetComponent<MapInfo>().MapNumY;
                for (int i = 0; i < stageLength - 1; i++)
                {
                    Maps[i][num] = Maps[i + 1][num];
                    Maps[i][num].GetComponent<MapInfo>().MapNumY = Maps[i + 1][num].GetComponent<MapInfo>().MapNumY;
                }
                Maps[stageLength - 1][num] = temp;
                Maps[stageLength - 1][num].GetComponent<MapInfo>().MapNumY = turnPosY;
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
                turnPosY= Maps[stageLength - 1][num].GetComponent<MapInfo>().MapNumY;
                for (int i = 0; i < stageLength - 1; i++)
                {
                    Maps[stageLength - 1 - i][num] = Maps[stageLength - 2 - i][num];
                    Maps[stageLength - 1][num].GetComponent<MapInfo>().MapNumY = Maps[stageLength - 2][num].GetComponent<MapInfo>().MapNumY;
                }
                Maps[0][num] = temp;
                Maps[0][num].GetComponent<MapInfo>().MapNumY = turnPosY;
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
                turnPosX = Maps[num][stageLength - 1].GetComponent<MapInfo>().MapNumX;
                for (int i = 0; i < stageLength - 1; i++)
                {
                    Maps[num][stageLength - 1 - i] = Maps[num][stageLength - 2 - i];
                    Maps[num][stageLength - 1 - i].GetComponent<MapInfo>().MapNumX = Maps[num][stageLength - 2 - i].GetComponent<MapInfo>().MapNumX;
                }
                Maps[num][0] = temp;
                Maps[num][0].GetComponent<MapInfo>().MapNumX = turnPosX;
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
                turnPosX = Maps[num][0].GetComponent<MapInfo>().MapNumX;
                for (int i = 0; i < stageLength - 1; i++)
                {
                    Maps[num][i] = Maps[num][i + 1];
                    Maps[num][i].GetComponent<MapInfo>().MapNumX = Maps[num][i + 1].GetComponent<MapInfo>().MapNumX;
                }
                Maps[num][stageLength - 1] = temp;
                Maps[num][stageLength - 1].GetComponent<MapInfo>().MapNumX = turnPosX;

                break;
            default:
                break;
        }
    }

    /// <summary>
    /// ステージシャッフル
    /// </summary>
    //public void StageShuffle() {
    //    GameObject[] tempMap = new GameObject[Maps.Count * 3];
    //    Vector2[] mapPos = new Vector2[Maps.Count * 3];
    //    Vector2 standardPos = Maps[0][0].transform.localPosition;
    //    int count = 0;
    //    for (int i = 0; i < Maps.Count; i++) {

    //        for(int j = 0; j < Maps[i].Count; i++) {
    //            tempMap[count] = Maps[i][j];
    //            mapPos[count] = tempMap[count].transform.localPosition;
    //            count++;
    //        }
    //    }

    //    // シャッフル
    //    for(int i = 0; i <= count; i++) {
    //        int rand = Random.Range(0, count + 1);
    //        GameObject temp = tempMap[rand];
    //        tempMap[rand] = tempMap[i];
    //        tempMap[i] = temp;
    //    }

    //    // 再配置
    //    int num = 0;
    //    for(int i = 0; i < stageLength; i++)
    //    {
    //        for (int j = 0; j < stageLength; j++)
    //        {
    //            tempMap[num].transform.localPosition = mapPos[num];
    //            tempMap[num].GetComponent<MapInfo>().mapNumX = j;
    //            tempMap[num].GetComponent<MapInfo>().mapNumY = i;
    //            Maps[i][j] = tempMap[num];
    //            num++;
    //        }
    //    }
    //}

    /// <summary>
    /// マップ入れ替え
    /// </summary>
    public void MapExchange(GameObject map1,GameObject map2)
    {
        int count = 0;
        
        int tenpPosX = 0;
        int tenpPosY = 0;

        Vector2 tempPos = map1.transform.localPosition;
        map1.transform.localPosition = map2.transform.localPosition;
        map2.transform.localPosition = tempPos;

        tenpPosX = map1.GetComponent<MapInfo>().MapNumX;
        map2.GetComponent<MapInfo>().MapNumX = map2.GetComponent<MapInfo>().MapNumX;
        map2.GetComponent<MapInfo>().MapNumX = tenpPosX;

        tenpPosY = map1.GetComponent<MapInfo>().MapNumY;
        map1.GetComponent<MapInfo>().MapNumY = map2.GetComponent<MapInfo>().MapNumY;
        map2.GetComponent<MapInfo>().MapNumY = tenpPosY;
    }
}
