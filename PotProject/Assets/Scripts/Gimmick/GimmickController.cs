﻿using System;
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
    private BossController bossCon;
    private int inFireZone = 0;

    // Use this for initialization
    void Start() {
        if (GameObject.Find("Controller") != null)
        {
            sController = GameObject.Find("Controller").GetComponent<StageController>();
        }

        if (FindObjectOfType<BossController>())
        {
            bossCon = FindObjectOfType<BossController>();
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
        if (col.gameObject.layer != LayerMask.NameToLayer("Player")) { return; }
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

    /// <summary>
    /// 判定(OnTriggerEnter)
    /// </summary>
    /// <param name="col"></param>
    public void OnTriggerEnter2D(Collider2D col) {
        
        if(col.gameObject.layer != LayerMask.NameToLayer("Player")) { return; }
        switch (gInfo.type) {
            case GimmickInfo.GimmickType.MAPCHANGE:
                col.transform.parent.transform.parent.transform.SetParent(transform.root.gameObject.transform);
                mMapController.NowMap();
                break;
            case GimmickInfo.GimmickType.FIREFIELD:
                bossCon.IsMagicAttack = true;
                break;
            case GimmickInfo.GimmickType.SPRING:
                
            default:
                break;
        }
    }

    /// <summary>
    /// 判定(OnTriggerExit)
    /// </summary>
    /// <param name="col"></param>
    public void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.layer != LayerMask.NameToLayer("Player")) { return; }
        switch (gInfo.type) {
            case GimmickInfo.GimmickType.FIREFIELD:
                inFireZone--;
                bossCon.IsMagicAttack = false;
                break;
            default:
                break;
        }
    }

    public void RockDoorOpen() {
        if (!mInfo.UpRockFlag) {
            mInfo.UpRockFlag = true;
            GameObject rock = GameObject.FindGameObjectWithTag("Rock");
            StartCoroutine(DoorStep(rock));
        }
    }

    // ドアが開くコルーチン
    public IEnumerator DoorStep(GameObject obj) {
        transform.parent.transform.parent.DOLocalMoveY(transform.parent.transform.parent.localPosition.y - 0.25f, 1f);
        yield return new WaitForSeconds(1.5f);
        obj.transform.DOScaleY(0f, 2.0f).SetEase(Ease.Linear);
        while (true) {
            obj.transform.localPosition = new Vector2(obj.transform.localPosition.x + 0.05f, obj.transform.localPosition.y);
            yield return new WaitForSeconds(0.05f);
            obj.transform.localPosition = new Vector2(obj.transform.localPosition.x - 0.05f, obj.transform.localPosition.y);
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void IsWater() {
        var waters = FindObjectsOfType<BuoyancyEffector2D>();
        foreach(var i in waters) {
            i.gameObject.SetActive(true);
        }
    }

    public void BossSpring(GameObject obj) {

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
        PlayerManager manager = FindObjectOfType<PlayerManager>();
        if(manager.Status.swordtype != PlayerStatus.SWORDTYPE.KEY) { return; }
        GameObject door = GameObject.FindGameObjectWithTag("KeyDoor");
        DoorStep(door);
        //door.transform.DOScaleY(0f, 1.0f).SetEase(Ease.Linear);
    }
}
