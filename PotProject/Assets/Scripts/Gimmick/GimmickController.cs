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

    // Use this for initialization
    void Start() {
        if (GameObject.Find("Controller") != null)
        {
            sController = GameObject.Find("Controller").GetComponent<StageController>();
        }

        mInfo = transform.root.GetComponent<MapInfo>();
        gInfo = GetComponent<GimmickInfo>();
    }

    void Update() {

    }

    /// <summary>
    /// あたり判定(OnCollisionEnter)
    /// </summary>
    /// <param name="col"></param>
    public void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player")) {
            switch (gInfo.type) {
                case GimmickInfo.GimmickType.UP:
                    sController.MapExchange(sController.GetMaps[0][0], sController.GetMaps[1][2]);
                    break;
                case GimmickInfo.GimmickType.DOWN:
                    sController.SrideStage(1, StageController.Direction.UP);
                    break;
                case GimmickInfo.GimmickType.LEFT:
                    break;
                case GimmickInfo.GimmickType.RIGHT:
                    sController.SrideStage(0, StageController.Direction.RIGHT);
                    break;
                case GimmickInfo.GimmickType.BAKETREE:
                    BakeTree(gameObject);
                    break;
                case GimmickInfo.GimmickType.GRASS:
                    DropHarb();
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
            case GimmickInfo.GimmickType.ROCK:
                mInfo.rock.transform.DOScaleY(0f, 1.0f).SetEase(Ease.Linear);
                mInfo.UpRockFlag = true;
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
                mInfo.GrowTreeFlag = false;
                Debug.Log("TreeFlag: " + mInfo.GrowTreeFlag);
                break;

            case GimmickInfo.GimmickType.LADDER:

                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 木(ツル)を成長させる
    /// </summary>
    public void Grow() {
        Vector2 defaultScale = mInfo.tree.transform.localScale;
        mInfo.tree.transform.DOScaleY(1f, 1f).SetEase(Ease.Linear);
        float time = 0;
        while(time < 3)
        {
            time += 1 * Time.deltaTime;
        }
        mInfo.tree.transform.DOScaleY(defaultScale.y, 1f).SetEase(Ease.Linear);
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
    /// 水でタイルを浮かせる
    /// </summary>
    public void FloatingTile(GameObject obj)
    {

    }

    /// <summary>
    /// 薬草ドロップ
    /// </summary>
    public void DropHarb()
    {
        // ドロップする薬草をResourcesから生成
        GameObject harb = Instantiate(Resources.Load<GameObject>("Prefabs/HarbPrefab"),transform.parent.transform);
        harb.transform.localPosition = transform.localPosition;
        Destroy(gameObject);
    }

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
