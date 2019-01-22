﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテム効果スクリプト
/// </summary>
public class ItemController : MonoBehaviour {

    private PlayerController playerController;
    private GimmickController gimmick_ctr;

    private GameObject BrotherObj;

    private void Start()
    {
        playerController = GameObject.Find("Controller").GetComponent<PlayerController>();
        BrotherObj = FindObjectOfType<AnimController>().gameObject;
    }

    /// <summary>
    /// 回復ポーション
    /// </summary>
    public void HPPortion() {
        playerController.HPUp(1);
    }

    /// <summary>
    /// 攻撃力UPポーション
    /// </summary>
    public void ATKPortion() {
        playerController.ATKChange(1);
    }

    /// <summary>
    /// 木を成長させるポーション
    /// </summary>
    public void TreePortion()
    {
        gimmick_ctr = FindObjectOfType<GimmickController>();
        gimmick_ctr.Grow();
    }

    /// <summary>
    /// バリア発動
    /// </summary>
    public void CreateBarrier()
    {
        GameObject prefab = Resources.Load("Prefabs/Items/Barrier") as GameObject;
        GameObject obj = Instantiate(prefab, BrotherObj.transform);
    }

    /// <summary>
    /// はしご生成
    /// </summary>
    public void LadderCreate()
    {
        playerController.OnBlock.AddComponent<CreateLadder>();
        
    }
}
