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

    private MiniMapController mMapController;

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

    private int changeFlag;

    /// <summary>
    /// マップ切り替えフラグ
    /// </summary>
    public int ChangeFlag
    {
        get { return changeFlag; }
        set { changeFlag = value; }
    }

    private int stageLength = 3;

    private CameraManager cManager;

    private GameObject[] waters;

    public GameObject[] Waters {
        get {return waters; }
        set { waters = value; }
    }

    private GameObject clearPanel;

    void Start()
    {
        SetList();
        clearPanel = FindObjectOfType<GameClear>().gameObject;
        clearPanel.SetActive(false);
        SetWater();
        pController = FindObjectOfType<PlayerController>();
        mMapController = FindObjectOfType<MiniMapController>();
        cManager = FindObjectOfType<CameraManager>();
        if (waters != null) {
            foreach (var i in Waters) {
                i.SetActive(false);
            }
        }
        SoundManager.Instance.PlayBgm((int)SoundManager.BGMNAME.BGM_MAINFAST);
    }

    void SetWater() {
        var temp = GameObject.FindGameObjectsWithTag("Water");
        Waters = temp;
    }

    void ActiveWater() {
        foreach (var i in Waters) {
            i.SetActive(true);
        }
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
    /// <returns></returns>
    public IEnumerator SrideInFade(int num, Direction dir, Vector2 pos)
    {
        cManager.SwitchingCameraSub(pos, subCameraSize);
        yield return new WaitForSeconds(1.5f);
        SoundManager.Instance.PlaySe((int)SoundManager.SENAME.SE_SLIDESTAGE);
        SrideStage(num, dir);
        mMapController.NowMap();
        yield return new WaitForSeconds(4.5f);
        pController.AllCommandActive = true;

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
        
        float mapSize = 40;
        int mapCount = 0;

        mMapController.miniMapSride(dir);
        switch (dir)
        {
            // 上-------------------------------------------------------------------
            case Direction.UP:

                // 一番上を保持
                turnPos = Maps[stageLength - 1][num].transform.position;
                turnPos.z = 90;

                // 折り返しMap以外をスライド
                for (int i = stageLength - 1; i >= 0; i--)
                {
                    tempPos = Maps[i][num].transform.position;
                    tempPos.z = 90;
                    StartCoroutine(SrideAnimation(Maps[i][num], turnPos,new Vector2(tempPos.x,tempPos.y + mapSize),mapCount));

                    mapCount++;
                }

                // スライド終了時の配列内入れ替え
                temp = Maps[0][num];

                for (int i = 0; i < stageLength - 1; i++) {
                    Maps[i][num] = Maps[i + 1][num];
                    Maps[i][num].GetComponent<MapInfo>().MapNumY = i;
                }
                Maps[stageLength - 1][num] = temp;

                Maps[stageLength - 1][num].GetComponent<MapInfo>().MapNumY = 2;
                break;

            // 上-------------------------------------------------------------------＊

            // 下-------------------------------------------------------------------
            case Direction.DOWN:

                // 折り返し座標を保持
                turnPos = Maps[0][num].transform.position;
                turnPos.z = 90;

                // 折り返しMap以外をスライド
                for (int i = 0; i <= stageLength - 1; i++)
                {
                    tempPos = Maps[i][num].transform.position;
                    tempPos.z = 90;

                    StartCoroutine(SrideAnimation(Maps[i][num], turnPos, new Vector2(tempPos.x,tempPos.y - mapSize),mapCount));

                    mapCount++;
                }

                // スライド終了時の配列内入れ替え
                temp = Maps[stageLength - 1][num];
                
                for (int i = 0; i < stageLength - 1; i++) {
                    Maps[stageLength - 1 - i][num] = Maps[stageLength - 2 - i][num];
                    Maps[stageLength -  1 - i][num].GetComponent<MapInfo>().MapNumY = stageLength - 1 - i;
                }
                Maps[0][num] = temp;

                Maps[0][num].GetComponent<MapInfo>().MapNumY = 0;
                break;

            // 下-------------------------------------------------------------------＊

            // 右-------------------------------------------------------------------
            case Direction.RIGHT:
                // 折り返し座標を保持
                turnPos = Maps[num][0].transform.position;
                turnPos.z = 90;

                // 折り返しMap以外をスライド
                for (int i = 0; i <= stageLength - 1; i++)
                {
                    tempPos = Maps[num][i].transform.position;
                    tempPos.z = 90;
                    StartCoroutine(SrideAnimation(Maps[num][i], turnPos,new Vector2(tempPos.x + mapSize,tempPos.y),mapCount));

                    mapCount++;
                }

                // スライド終了時の配列内入れ替え
                temp = Maps[num][stageLength - 1];
                for (int i = 0; i < stageLength - 1; i++) {
                    Maps[num][stageLength - 1 - i] = Maps[num][stageLength - 2 - i];
                    Maps[num][stageLength - 1 - i].GetComponent<MapInfo>().MapNumX = stageLength - 1 - i;
                }
                Maps[num][0] = temp;

                Maps[num][0].GetComponent<MapInfo>().MapNumX = 0;
                break;

            // 右-------------------------------------------------------------------＊

            // 左-------------------------------------------------------------------
            case Direction.LEFT: 
                turnPos = Maps[num][stageLength - 1].transform.position;
                turnPos.z = 90;

                // 折り返しMap以外をスライド
                for (int i = stageLength - 1; i >= 0; i--)
                {
                    tempPos = Maps[num][i].transform.position;
                    tempPos.z = 90;
                    StartCoroutine(SrideAnimation(Maps[num][i], turnPos,new Vector2(tempPos.x - mapSize,tempPos.y), mapCount));

                    mapCount++;
                }

                // スライド終了時の配列内入れ替え
                temp = Maps[num][0];
                
                for (int i = 0; i < stageLength - 1; i++)
                {
                    Maps[num][i] = Maps[num][i + 1];
                    Maps[num][i].GetComponent<MapInfo>().MapNumX = i;
                }
                Maps[num][stageLength - 1] = temp;

                Maps[num][stageLength - 1].GetComponent<MapInfo>().MapNumX = 2;

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
    /// <param name="turn"></param>
    /// <param name="pos"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public IEnumerator SrideAnimation(GameObject obj,Vector2 turn, Vector2 pos, int count)
    {   
        if (count == 2) { isMove = true; }
        obj.layer = LayerMask.NameToLayer("MoveMap");
        obj.transform.DOLocalMove(pos, srideTine).SetEase(Ease.Linear).OnComplete(() => MoveComplete(obj));
        yield return new WaitForSeconds(srideTine);
        if (count == 2)
        {
            cManager.SwitchingCameraMain();
            yield return new WaitForSeconds(0.5f);
            obj.transform.localPosition = turn;
        }

    }

    public void MoveComplete(GameObject obj)
    {
        isMove = false;
        obj.layer = LayerMask.NameToLayer("Default");
    }
}
