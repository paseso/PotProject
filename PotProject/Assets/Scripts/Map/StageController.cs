using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StageController : MonoBehaviour
{

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
    private GameObject[] mapLists = new GameObject[9];

    public GameObject[] SetMapList
    {
        set { mapLists = value; }
    }

    private int stageLength = 3;

    void Start()
    {
        SetList();
    }

    /// <summary>
    /// ListにMapを入れる
    /// </summary>
    public void SetList()
    {
        int count = 0;

        for (int i = 0; i < 3; i++)
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
    /// カメラ切り替えとスライド関数呼び出し
    /// </summary>
    /// <param name="num"></param>
    /// <param name="dir"></param>
    /// <param name="pos"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    public IEnumerator Sride(int num, Direction dir, Vector2 pos, float size)
    {
        CameraManager cManager = GetComponent<CameraManager>();
        cManager.SwitchingCameraSub(pos, size);
        yield return new WaitForSeconds(1f);
        SrideStage(num, dir);
        cManager.SwitchingCameraMain();
        yield return new WaitForSeconds(1f);
    }

    /// <summary>
    /// ステージスライド処理
    /// </summary>
    /// <param name="num"></param>
    /// <param name="dir"></param>
    public void SrideStage(int num, Direction dir)
    {
        if (isMove) { return; }

        GameObject temp;
        Vector3 tempPos = new Vector3();
        Vector3 turnPos = new Vector3();
        float mapSize = 0;
        int turnPosX = 0;
        int turnPosY = 0;
        int mapCount = 0;

        switch (dir)
        {
            case Direction.UP: // 上

                // 折り返し座標を保持
                turnPos = Maps[stageLength - 1][num].transform.position;
                turnPos.z = 90;

                mapSize = Maps[stageLength - 1][num].GetComponent<SpriteRenderer>().bounds.size.y;
                Maps[0][num].transform.localPosition = new Vector2(turnPos.x, turnPos.y - mapSize);

                // 折り返しMap以外をスライド
                for (int i = stageLength - 1; i > 0; i--)
                {
                    tempPos = Maps[i - 1][num].transform.position;
                    tempPos.z = 90;
                    StartCoroutine(SrideAnimation(Maps[i][num],tempPos,mapCount));
                    //Maps[i][num].transform.localPosition = tempPos;

                    mapCount++;
                }

                // 折り返し
                //Maps[0][num].transform.localPosition = turnPos;
                StartCoroutine(SrideAnimation(Maps[0][num], turnPos, mapCount));

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

                mapSize = Maps[stageLength - 1][num].GetComponent<SpriteRenderer>().bounds.size.y;
                Maps[stageLength - 1][num].transform.localPosition = new Vector2(turnPos.x, turnPos.y + mapSize);

                // 折り返しMap以外をスライド
                for (int i = 0; i < stageLength - 1; i++)
                {
                    tempPos = Maps[i + 1][num].transform.position;
                    tempPos.z = 90;

                    StartCoroutine(SrideAnimation(Maps[i][num],tempPos,mapCount));
                    //Maps[i][num].transform.localPosition = tempPos;

                    mapCount++;
                }

                // 折り返し
                //Maps[stageLength - 1][num].transform.localPosition = turnPos;
                StartCoroutine(SrideAnimation(Maps[stageLength - 1][num], turnPos, mapCount));

                // スライド終了時の配列内入れ替え
                temp = Maps[stageLength - 1][num];
                turnPosY = Maps[stageLength - 1][num].GetComponent<MapInfo>().MapNumY;
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

                // 折り返しMap以外をスライド
                for (int i = 0; i < stageLength - 1; i++)
                {
                    tempPos = Maps[num][i + 1].transform.position;
                    tempPos.z = 90;
                    StartCoroutine(SrideAnimation(Maps[num][i],tempPos,mapCount));
                    //Maps[num][i].transform.localPosition = tempPos;

                    mapCount++;
                }

                mapSize = Maps[stageLength - 1][num].transform.GetChild(0).GetComponent<SpriteRenderer>().bounds.size.y;
                Maps[num][stageLength - 1].transform.localPosition = new Vector2(turnPos.x - mapSize, turnPos.y);

                // 折り返し
                //Maps[num][stageLength - 1].transform.localPosition = turnPos;
                StartCoroutine(SrideAnimation(Maps[num][stageLength - 1], turnPos, mapCount));

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

                //　折り返し座標からマップサイズ分ずれた場所に移動
                mapSize = Maps[stageLength - 1][num].GetComponent<SpriteRenderer>().bounds.size.y;
                Maps[num][0].transform.localPosition = new Vector2(turnPos.x + mapSize, turnPos.y);

                // 折り返しMap以外をスライド
                for (int i = stageLength - 1; i > 0; i--)
                {
                    tempPos = Maps[num][i - 1].transform.position;
                    tempPos.z = 90;
                    StartCoroutine(SrideAnimation(Maps[num][i], tempPos, mapCount));
                    //Maps[num][i].transform.localPosition = tempPos;

                    mapCount++;
                }

                // 折り返し
                //Maps[num][0].transform.localPosition = turnPos;
                StartCoroutine(SrideAnimation(Maps[num][0], turnPos, mapCount));

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
    /// マップ入れ替え
    /// </summary>
    public void MapExchange(GameObject map1, GameObject map2)
    {
        int tenpPosX = 0;
        int tenpPosY = 0;

        Vector2 tempPos = map1.transform.localPosition;
        map1.transform.localPosition = map2.transform.localPosition;
        map2.transform.localPosition = tempPos;

        // X座標
        tenpPosX = map1.GetComponent<MapInfo>().MapNumX;
        map2.GetComponent<MapInfo>().MapNumX = map2.GetComponent<MapInfo>().MapNumX;
        map2.GetComponent<MapInfo>().MapNumX = tenpPosX;

        // Y座標
        tenpPosY = map1.GetComponent<MapInfo>().MapNumY;
        map1.GetComponent<MapInfo>().MapNumY = map2.GetComponent<MapInfo>().MapNumY;
        map2.GetComponent<MapInfo>().MapNumY = tenpPosY;
    }

    private bool isMove = false;

    /// <summary>
    /// スライドアニメーション
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="pos"></param>
    /// <returns></returns>
    public IEnumerator SrideAnimation(GameObject obj, Vector2 pos, int count)
    {   
        if (count == 2) { isMove = true; }
        obj.layer = LayerMask.NameToLayer("MoveMap");
        obj.transform.DOLocalMove(pos, 3f).SetEase(Ease.Linear).OnComplete(() => MoveComplete(obj));
        yield return null;
           
    }

    public void MoveComplete(GameObject obj)
    {
        isMove = false;
        obj.layer = LayerMask.NameToLayer("Default");
    }
}
