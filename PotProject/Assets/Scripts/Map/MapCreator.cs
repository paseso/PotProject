using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[DisallowMultipleComponent,DefaultExecutionOrder(-1)]
public class MapCreator : MonoBehaviour
{
    [HideInInspector]
    public Tile[]  tiles;
    [HideInInspector]
    public Gimmick[] gimmicks;
    [HideInInspector]
    public Enemy[] enemies;
    [HideInInspector]
    public MapData map;
    [SerializeField]
    GameObject playerPrefab;
    [SerializeField]
    [Header("背景画像 ノーマル、炎、氷、雷、闇、空用の順に")]
    private Sprite[] backImages;

    private GameObject[] mapObjects = new GameObject[9];

    public Tile[] GetTiles() { return tiles; }
    public Tile GetTile(int index) { return tiles[index]; }
    public Gimmick[] GetGimmicks() { return gimmicks; }
    public Gimmick GetGimmick(int index) { return gimmicks[index]; }
    public Enemy[] GetEnemies() { return enemies; }
    public Enemy GetEnemy(int index) { return enemies[index]; }
    public Sprite[] GetBackImages() { return backImages; }


    public MapData Map
    {
        get{ return map; }
        set{ map = value; }
    }

    /// <summary>
    /// マップを生成 1つだけ
    /// </summary>
    public void CreateMap()
    {
        if (!this.Map)
        {
            #if UNITY_EDITOR
            EditorUtility.DisplayDialog("Error", "マップデータがセットされていません", "OK");
            #endif
            return;
        }
        //  配列の長さを取得
        int xLength = Map.mapDate[0].mapNum.Length;
        int yLength = Map.mapDate.Length;

        //  タイルのサイズを取得
        float tileSize = 2;
        //  ルートオブジェクトの作成
        var rootObj = new GameObject("StageRootObject");
        //  エディター側では左上が始点なので、その分場所の移動
        Vector2 startPos = rootObj.transform.position + new Vector3(tileSize / 2, tileSize * yLength - tileSize / 2, 0);
        //  オブジェクトの生成
        for (int y = 0; y < yLength; y++)
        {
            for (int x = 0; x < xLength; x++)
            {
                int tilenum = Map.mapDate[y].mapNum[x];
                int gimmickNum = Map.gimmickDate[y].mapNum[x];
                int enemyNum = Map.enemyDate[y].mapNum[x];
                //  通常タイルの生成
                if (tilenum != 0)
                {
                    var tileObj = Instantiate(GetTile(tilenum).TileObj);
                    tileObj.transform.parent = rootObj.transform;
                    tileObj.transform.position = startPos + new Vector2(tileSize * x, -tileSize * y);
                    //  周りにはしごギミックがあったらレイヤーを変更
                    if (gimmickNum != 0 && GetGimmick(gimmickNum).GimmickObj.GetComponent<GimmickInfo>() != null)
                    {
                        if (x != xLength && Map.gimmickDate[y].mapNum[x + 1] != 0 && GetGimmick(Map.gimmickDate[y].mapNum[x + 1]).GimmickObj.GetComponent<GimmickInfo>() != null)
                        {
                            if (GetGimmick(gimmickNum).GimmickObj.GetComponent<GimmickInfo>().type == GimmickInfo.GimmickType.LADDER)
                            {
                                tileObj.layer = LayerMask.NameToLayer("LadderBlock");
                            }
                        }
                    }
                }
                //  ギミックの生成
                if (gimmickNum != 0)
                {
                    var gimmickObj = Instantiate(GetGimmick(gimmickNum).GimmickObj);
                    gimmickObj.transform.parent = rootObj.transform;
                    gimmickObj.transform.position = startPos + new Vector2(tileSize * x, -tileSize * y);
                }
                //  エネミーやポジションの生成
                if (enemyNum != 0)
                {
                    var enemyObj = Instantiate(GetEnemy(enemyNum).EnemyObj);
                    enemyObj.transform.parent = rootObj.transform;
                    enemyObj.transform.position = startPos + new Vector2(tileSize * x, -tileSize * y);
                }
            }
        }
    }


    /// <summary>
    /// マップの生成 9つ分
    /// </summary>
    /// <param name="datas"></param>
    public void CreateMap(MapData[] datas)
    {
        //  プレイヤーの初期地点
        GameObject startPositionObject = new GameObject();

        //  配列の長さを取得 再編集
        int xLength = datas[0].mapDate.Length;
        int yLength = datas[0].mapDate.Length;
        //  タイルのサイズを取得
        float tileSize = 2;
        float stageSize = tileSize * xLength;
        for (int i = 0; i < datas.Length; i++)
        {
            int quo = i / 3;
            int rem = i % 3;

            //  Y軸方向に3ステージ分あげる
            Vector2 startPos = new Vector2(0, stageSize * 3);
            //  ルートオブジェクトの作成
            string rootName = datas[i].name;
            var rootObj = new GameObject(rootName);
            rootObj.AddComponent<MapInfo>();
            rootObj.GetComponent<MapInfo>().MapNumX = rem;
            rootObj.GetComponent<MapInfo>().MapNumY = quo;
            rootObj.transform.position = startPos + new Vector2(stageSize / 2, -stageSize / 2) + new Vector2(rem * stageSize, -quo * stageSize);
            //  ジャンル別の空のオブジェクト生成
            var tileObjectGroupe = new GameObject("TileObject");
            var gimmickObjectGroupe = new GameObject("GimmickObject");
            var OtherObjectGroupe = new GameObject("OtherObject");
            tileObjectGroupe.transform.position = rootObj.transform.position;
            gimmickObjectGroupe.transform.position = rootObj.transform.position;
            OtherObjectGroupe.transform.position = rootObj.transform.position;
            //  親子付け
            tileObjectGroupe.transform.parent = rootObj.transform;
            gimmickObjectGroupe.transform.parent = rootObj.transform;
            OtherObjectGroupe.transform.parent = rootObj.transform;

            //  背景オブジェクトの生成
            GameObject backGroundObject = new GameObject("BG");
            backGroundObject.layer = 2;
            
            backGroundObject.AddComponent<SpriteRenderer>();
            backGroundObject.GetComponent<SpriteRenderer>().sortingOrder = -1;
            backGroundObject.AddComponent<BoxCollider2D>();
            backGroundObject.GetComponent<BoxCollider2D>().size = new Vector2(xLength * 0.5f, xLength * 0.5f);
            backGroundObject.transform.parent = rootObj.transform;
            backGroundObject.transform.localPosition = Vector3.zero + new Vector3(-tileSize * 0.5f, tileSize * 0.5f, 0);
            backGroundObject.GetComponent<SpriteRenderer>().sprite = backImages[datas[i].backGroundNum];
            
            backGroundObject.transform.localScale = new Vector3(stageSize / backGroundObject.GetComponent<SpriteRenderer>().size.x, stageSize / backGroundObject.GetComponent<SpriteRenderer>().size.y, 0);
            if (backGroundObject.GetComponent<SpriteRenderer>().sprite.name != "Empty" && backGroundObject.GetComponent<SpriteRenderer>().sprite.name != "Empty2")
            {
                backGroundObject.GetComponent<BoxCollider2D>().isTrigger = true;
                backGroundObject.AddComponent<MapChange>();
            }            

            //  オブジェクトの生成
            for (int y = 0; y < yLength; y++)
            {
                for (int x = 0; x < xLength; x++)
                {
                    int tilenum = datas[i].mapDate[y].mapNum[x];
                    int gimmickNum = datas[i].gimmickDate[y].mapNum[x];
                    int enemyNum = datas[i].enemyDate[y].mapNum[x];
                    //  通常タイル
                    if (tilenum != 0)
                    {
                        //  生成
                        var tileObj = Instantiate(GetTile(tilenum).TileObj);
                        tileObj.name = tileObj.name + x + "-" + y;
                        tileObj.transform.position = startPos + new Vector2(tileSize * x, -tileSize * y) + new Vector2(tileSize * xLength * rem, -tileSize * yLength * quo);
                        tileObj.transform.parent = tileObjectGroupe.transform;
                        //周りにはしごギミックがあったらレイヤーを変更
                        if (gimmickNum != 0)
                        {
                            if (GetGimmick(gimmickNum).GimmickObj.GetComponent<GimmickInfo>() != null && GetGimmick(gimmickNum).GimmickObj.GetComponent<GimmickInfo>().type == GimmickInfo.GimmickType.LADDER)
                            {
                                //  レイヤー変更
                                Debug.Log("aaa");
                                tileObj.layer = LayerMask.NameToLayer("LadderBlock");
                            }

                            //////  1つ先のブロックも検索する
                            //if (x <= xLength && datas[i].gimmickDate[y].mapNum[x + 1] != 0 && GetGimmick(datas[i].gimmickDate[y].mapNum[x + 1]).GimmickObj.GetComponent<GimmickInfo>() != null)

                            if (GetGimmick(gimmickNum).GimmickObj.transform.childCount != 0  && GetGimmick(gimmickNum).GimmickObj.transform.GetChild(0).GetComponent<GimmickInfo>() != null && GetGimmick(gimmickNum).GimmickObj.transform.GetChild(0).GetComponent<GimmickInfo>().type == GimmickInfo.GimmickType.LADDER)
                            {
                                tileObj.layer = LayerMask.NameToLayer("LadderBlock");
                            }
                        }
                    }
                    //  ギミック
                    if (gimmickNum != 0)
                    {
                        //  生成
                        var gimmickObj = Instantiate(GetGimmick(gimmickNum).GimmickObj);
                        gimmickObj.name = gimmickObj.name + x + "-" + y;
                        gimmickObj.transform.position = startPos + new Vector2(tileSize * x, -tileSize * y) + new Vector2(tileSize * xLength * rem, -tileSize * yLength * quo);
                        gimmickObj.transform.parent = gimmickObjectGroupe.transform;
                    }
                    //  エネミーやポジション
                    if (enemyNum != 0)
                    {
                        var enemyObj = Instantiate(GetEnemy(enemyNum).EnemyObj);
                        //  スタート位置の配置
                        if (enemyObj.tag == "StartPos")
                        {
                            startPositionObject = enemyObj;
                            SetParentPlayer(startPositionObject);
                        }
                        enemyObj.transform.position = startPos + new Vector2(tileSize * x, -tileSize * y) + new Vector2(tileSize * xLength * rem, -tileSize * yLength * quo);
                        enemyObj.transform.parent = OtherObjectGroupe.transform;
                    }
                }
            }
            mapObjects[i] = rootObj;
        }
        StageController stageController = FindObjectOfType<StageController>();
        stageController.SetMapList = mapObjects;
    }

    /// <summary>
    /// プレイヤーをゲームスタート時に生成する
    /// </summary>
    /// <param name="parent"></param>
    public void SetParentPlayer(GameObject parent)
    {
        GameObject broOld = Instantiate(playerPrefab);
        broOld.transform.SetParent(parent.transform);
    }
}

[System.Serializable]
public class Tile
{
    [SerializeField]
    private Texture tileImage;
    [SerializeField]
    private GameObject tileObj;

    public Texture TileImage
    {
        get { return tileImage; }
        //set { tileImage = value; }
    }

    public GameObject TileObj
    {
        get { return tileObj; }
        //set { tileObj = value; }
    }
}

[System.Serializable]
public class Gimmick
{
    [SerializeField]
    private Texture gimmickImage;
    [SerializeField]
    private Vector2 gimmickSize;
    [SerializeField]
    private GameObject gimmickObj;

    public Texture GimmickImage
    {
        get { return gimmickImage; }
        //set { gimmickImage = value; }
    }
    public Vector2 GimmickSize
    {
        get { return gimmickSize; }
        //set { gimmickSize = value; }
    }
    public GameObject GimmickObj
    {
        get { return gimmickObj; }
        //set { gimmickObj = value; }
    }
}

[System.Serializable]
public class Enemy
{
    [SerializeField]
    private Texture enemyImage;
    [SerializeField]
    private GameObject enemyObj;

    public Texture EnemyImage
    {
        get { return enemyImage; }
        //set { enemyImage = value; }
    }

    public GameObject EnemyObj
    {
        get { return enemyObj; }
        //set { enemyObj = value; }
    }
}

