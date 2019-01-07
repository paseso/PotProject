using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 各ギミックの処理
/// </summary>
[RequireComponent(typeof(GimmickInfo))]
public class GimmickController :MonoBehaviour {
    private StageController sController;
    private MapInfo mInfo;
    private GimmickInfo gInfo;
    private MiniMapController mMapController;

    // Use this for initialization
    void Start() {
        if (GameObject.Find("Controller") != null)
        {
            sController = GameObject.Find("Controller").GetComponent<StageController>();
        }

        mMapController = FindObjectOfType<MiniMapController>();
        mInfo = transform.root.GetComponent<MapInfo>();
        gInfo = GetComponent<GimmickInfo>();
    }

    /// <summary>
    /// あたり判定(OnCollisionEnter)
    /// </summary>
    /// <param name="col"></param>
    public void OnCollisionEnter2D(Collision2D col) {
        MapInfo objInfo = col.transform.root.gameObject.GetComponent<MapInfo>();
        if (col.gameObject.tag == "Player") {
            switch (gInfo.type) {
                case GimmickInfo.GimmickType.UP:
                    
                    sController.Sride(objInfo.MapNumX, StageController.Direction.UP);
                    break;
                case GimmickInfo.GimmickType.DOWN:
                    
                    sController.Sride(objInfo.MapNumX, StageController.Direction.DOWN);
                    break;
                case GimmickInfo.GimmickType.LEFT:
                    
                    sController.Sride(objInfo.MapNumY, StageController.Direction.LEFT);
                    break;
                case GimmickInfo.GimmickType.RIGHT:
                    
                    sController.Sride(objInfo.MapNumY, StageController.Direction.RIGHT);
                    break;
                case GimmickInfo.GimmickType.BAKETREE:
                    BakeTree(gameObject);
                    break;
                case GimmickInfo.GimmickType.ROCK:
                    RockDoorOpen();
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// 判定(OnTriggerEnter)
    /// </summary>
    /// <param name="col"></param>
    public void OnTriggerEnter2D(Collider2D col) {
        switch (gInfo.type) {
            case GimmickInfo.GimmickType.GROWTREE:
                mInfo.GrowTreeFlag = true;
                Debug.Log("TreeFlag: " + mInfo.GrowTreeFlag);
                break;

            case GimmickInfo.GimmickType.LADDER:

                break;
            case GimmickInfo.GimmickType.MAPCHANGE:
                col.transform.parent.transform.parent.transform.SetParent(transform.root.gameObject.transform);
                mMapController.NowMap();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 判定(OnTriggerExit)
    /// </summary>
    /// <param name="col"></param>
    public void OnTriggerExit2D(Collider2D col) {
        switch (gInfo.type) {
            case GimmickInfo.GimmickType.GROWTREE:
                mInfo.GrowTreeFlag = true;
                //Debug.Log("TreeFlag: " + mInfo.GrowTreeFlag);
                break;

            case GimmickInfo.GimmickType.LADDER:

                break;
            default:
                break;
        }
    }

    public void RockDoorOpen() {
        GameObject rock = GameObject.FindGameObjectWithTag("Rock");
        rock.transform.DOScaleY(0f, 1.0f).SetEase(Ease.Linear);
    }

    public void IsWater() {
        var waters = FindObjectsOfType<BuoyancyEffector2D>();
        foreach(var i in waters) {
            i.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 木(ツル)を成長させる
    /// </summary>
    public void Grow() {
        GameObject growTree = FindObjectOfType<TreeGrow>().gameObject;
        growTree.GetComponent<TreeGrow>().Grow();
    }

    /// <summary>
    /// 木を焼く
    /// </summary>
    /// <param name="obj"></param>
    public void BakeTree(GameObject obj)
    {
        StartCoroutine(Bake(obj));
    }

    /// <summary>
    /// 木を焼くコルーチン
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public IEnumerator Bake(GameObject obj)
    {
        for (int i = 2; i >= 0; i--)
        {
            Debug.Log("bake" + i);
            Sprite sp_tree = Resources.Load<Sprite>("Textures/Gimmicks/fire" + i);
            gameObject.GetComponent<SpriteRenderer>().sprite = sp_tree;
            yield return new WaitForSeconds(1f);
            if (i == 2)
            {
                // 燃えるエフェクト
            }
        }
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(1f);
        // Destroy
    }

    // 浮く座標(仮)
    private float endPos;

    /// <summary>
    /// 鍵付き扉
    /// </summary>
    public void UnlockKeyDoor()
    {
        // 扉（仮）
        GameObject door = new GameObject();
        door.transform.DOScaleY(0f, 1.0f).SetEase(Ease.Linear);

    }
}
