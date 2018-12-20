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

    // サブカメラの描画幅
    private float subCameraSize = 70;

    private List<List<GameObject>> Maps = new List<List<GameObject>>();

    private PlayerController pController;

    public List<List<GameObject>> GetMaps
    {
        get { return Maps; }
    }

    private float srideTine = 3;

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
        pController = FindObjectOfType<PlayerController>();
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
                MapInfo info1 = mapLists[count].GetComponent<MapInfo>();

                info1.MapNumX = j;
                info1.MapNumY = i;
                //mapLists[count].GetComponent<MapInfo>().MapNumX = j;
                //mapLists[count].GetComponent<MapInfo>().MapNumY = i;
                varMap.Add(mapLists[count]);
                count++;
            }
            Maps.Add(varMap);
        }
    }

    /// <summary>
    /// スライド処理呼び出し
    /// </summary>
    /// <param name="num"></param>
    /// <param name="dir"></param>
    /// <param name="pos"></param>
    /// <param name="size"></param>
    public void Sride(int num, Direction dir) {
        if (isMove) { return; }
        StartCoroutine(SrideInFade(num,dir, GetMaps[1][1].transform.localPosition));
    }

    /// <summary>
    /// カメラ切り替えとスライド関数呼び出し
    /// </summary>
    /// <param name="num"></param>
    /// <param name="dir"></param>
    /// <param name="pos"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    public IEnumerator SrideInFade(int num, Direction dir, Vector2 pos)
    {
        CameraManager cManager = FindObjectOfType<CameraManager>();
        cManager.SwitchingCameraSub(pos, subCameraSize);
        yield return new WaitForSeconds(1.5f);
        SrideStage(num, dir);
        yield return new WaitForSeconds(srideTine);
        cManager.SwitchingCameraMain();
        yield return new WaitForSeconds(1.5f);
        pController.SetCommandActive = true;
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
        
        float mapSize = mapSize = Maps[stageLength - 1][num].transform.GetChild(0).GetComponent<SpriteRenderer>().bounds.size.x * 20;
        int mapCount = 0;

        switch (dir)
        {
            // 上-------------------------------------------------------------------
            case Direction.UP:

                // 一番上を保持
                turnPos = Maps[stageLength - 1][num].transform.position;
                turnPos.z = 90;

                // 折り返しMap以外をスライド
                for (int i = stageLength - 1; i > 0; i--)
                {
                    tempPos = Maps[i - 1][num].transform.position;
                    tempPos.z = 90;
                    StartCoroutine(SrideAnimation(Maps[i][num],tempPos,mapCount));
                    //Maps[i][num].transform.localPosition = tempPos;

                    mapCount++;
                }

                Maps[0][num].transform.localPosition = new Vector2(turnPos.x, turnPos.y - mapSize);

                // 折り返し
                //Maps[0][num].transform.localPosition = turnPos;
                StartCoroutine(SrideAnimation(Maps[0][num], turnPos, mapCount));

                // スライド終了時の配列内入れ替え
                temp = Maps[0][num];
                int turnMInfoUp = Maps[0][num].GetComponent<MapInfo>().MapNumX;
                for (int i = 0; i < stageLength - 1; i++)
                {
                    MapInfo mInfo1 = Maps[i][num].GetComponent<MapInfo>();
                    MapInfo mInfo2 = Maps[i + 1][num].GetComponent<MapInfo>();

                    Maps[i][num] = Maps[i + 1][num];

                    mInfo1.MapNumY = mInfo2.MapNumY;
                }
                Maps[stageLength - 1][num] = temp;
                MapInfo minfo3Up = Maps[num][0].GetComponent<MapInfo>();
                minfo3Up.MapNumY = turnMInfoUp;
                break;

            // 上-------------------------------------------------------------------＊

            // 下-------------------------------------------------------------------
            case Direction.DOWN:

                // 折り返し座標を保持
                turnPos = Maps[0][num].transform.position;
                turnPos.z = 90;

                // 折り返しMap以外をスライド
                for (int i = 0; i < stageLength - 1; i++)
                {
                    tempPos = Maps[i + 1][num].transform.position;
                    tempPos.z = 90;

                    StartCoroutine(SrideAnimation(Maps[i][num],tempPos,mapCount));
                    //Maps[i][num].transform.localPosition = tempPos;

                    mapCount++;
                }

                Maps[stageLength - 1][num].transform.localPosition = new Vector2(turnPos.x, turnPos.y + mapSize);

                // 折り返し
                //Maps[stageLength - 1][num].transform.localPosition = turnPos;
                StartCoroutine(SrideAnimation(Maps[stageLength - 1][num], turnPos, mapCount));

                // スライド終了時の配列内入れ替え
                temp = Maps[stageLength - 1][num];
                int turnMInfoDown = Maps[stageLength - 1][num].GetComponent<MapInfo>().MapNumX;
                for (int i = 0; i < stageLength - 1; i++)
                {
                    MapInfo mInfo1 = Maps[stageLength - 1][num].GetComponent<MapInfo>();
                    MapInfo mInfo2 = Maps[stageLength - 2][num].GetComponent<MapInfo>();

                    Maps[stageLength - 1 - i][num] = Maps[stageLength - 2 - i][num];

                    mInfo1.MapNumY = mInfo2.MapNumY;
                }
                Maps[0][num] = temp;
                MapInfo minfo3Down = Maps[num][0].GetComponent<MapInfo>();
                minfo3Down.MapNumY = turnMInfoDown;
                break;

            // 下-------------------------------------------------------------------＊

            // 右-------------------------------------------------------------------
            case Direction.RIGHT:
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

                Maps[num][stageLength - 1].transform.localPosition = new Vector2(turnPos.x - mapSize, turnPos.y);

                // 折り返し
                //Maps[num][stageLength - 1].transform.localPosition = turnPos;
                StartCoroutine(SrideAnimation(Maps[num][stageLength - 1], turnPos, mapCount));

                // スライド終了時の配列内入れ替え
                temp = Maps[num][stageLength - 1];
                int turnMInfoRight = Maps[num][stageLength - 1].GetComponent<MapInfo>().MapNumX;

                for (int i = 0; i < stageLength - 1; i++)
                {
                    MapInfo mInfo1 = Maps[num][i].GetComponent<MapInfo>();
                    MapInfo mInfo2 = Maps[num][i + 1].GetComponent<MapInfo>();

                    Maps[num][i] = Maps[num][i + 1];

                   mInfo1.MapNumX = mInfo2.MapNumX;
                }
                Maps[num][stageLength - 1] = temp;
                MapInfo minfo3Right = Maps[num][0].GetComponent<MapInfo>();
                minfo3Right.MapNumX = turnMInfoRight;
                break;

            // 右-------------------------------------------------------------------＊

            // 左-------------------------------------------------------------------
            case Direction.LEFT: 
                turnPos = Maps[num][stageLength - 1].transform.position;
                turnPos.z = 90;

                // 折り返しMap以外をスライド
                for (int i = stageLength - 1; i > 0; i--)
                {
                    tempPos = Maps[num][i - 1].transform.position;
                    tempPos.z = 90;
                    StartCoroutine(SrideAnimation(Maps[num][i], tempPos, mapCount));
                    //Maps[num][i].transform.localPosition = tempPos;

                    mapCount++;
                }

                //　折り返し座標からマップサイズ分ずれた場所に移動
                Maps[num][0].transform.localPosition = new Vector2(turnPos.x + mapSize, turnPos.y);

                // 折り返し
                //Maps[num][0].transform.localPosition = turnPos;
                StartCoroutine(SrideAnimation(Maps[num][0], turnPos, mapCount));

                MapInfo a = Maps[num][0].GetComponent<MapInfo>();
                MapInfo b = Maps[num][1].GetComponent<MapInfo>();
                MapInfo c = Maps[num][2].GetComponent<MapInfo>();
                // スライド終了時の配列内入れ替え
                temp = Maps[num][stageLength - 1];
                int turnMInfoLeft = Maps[num][0].GetComponent<MapInfo>().MapNumX;
                for (int i = 0; i < stageLength - 1; i++)
                {
                    MapInfo mInfo1 = Maps[num][i].GetComponent<MapInfo>();
                    MapInfo mInfo2 = Maps[num][i + 1].GetComponent<MapInfo>();

                    Maps[num][i] = Maps[num][i + 1];
                    
                    mInfo1.MapNumX = mInfo2.MapNumX;
                }
                MapInfo minfo3Left = Maps[num][0].GetComponent<MapInfo>();

                Maps[num][0] = temp;

                minfo3Left.MapNumX = turnMInfoLeft;

                a = Maps[num][0].GetComponent<MapInfo>();
                b = Maps[num][1].GetComponent<MapInfo>();
                c = Maps[num][2].GetComponent<MapInfo>();
                break;
            // 左-------------------------------------------------------------------＊
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
        obj.transform.DOLocalMove(pos, srideTine).SetEase(Ease.Linear).OnComplete(() => MoveComplete(obj));
        yield return null;
           
    }

    public void MoveComplete(GameObject obj)
    {
        isMove = false;
        obj.layer = LayerMask.NameToLayer("Default");
    }
}
