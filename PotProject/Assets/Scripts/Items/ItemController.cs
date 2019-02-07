using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテム効果スクリプト
/// </summary>
public class ItemController : MonoBehaviour {

    private PlayerController playerController;

    private GameObject BrotherObj;

    public bool useFlag { get; set; }

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        BrotherObj = FindObjectOfType<MoveController>().gameObject;
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
        if (playerController.rideTreeFlag) {
            FindObjectOfType<TreeGrow>().Grow();
            playerController.ItemUseFlag = true;
        }
    }

    /// <summary>
    /// バリア発動
    /// </summary>
    public void CreateBarrier()
    {
        GameObject prefab = Resources.Load("Prefabs/Items/Barrier") as GameObject;
        Instantiate(prefab, BrotherObj.transform);
        playerController.ItemUseFlag = true;
    }

    /// <summary>
    /// はしご生成
    /// </summary>
    public void LadderCreate()
    {
        if (playerController.OnBlock.GetComponent<CreateLadder>()) { return; }
        playerController.OnBlock.AddComponent<CreateLadder>();
        playerController.OnBlock.GetComponent<CreateLadder>().PutOnLadder();
    }

    /// <summary>
    /// 鍵の処理
    /// </summary>
    public void OpenKeyDoor()
    {
        if (BrotherObj.GetComponent<MoveController>().keyDoorFlag)
        {
            GameObject keySwitch = GameObject.FindGameObjectWithTag("KeyDoor");
            keySwitch.GetComponent<GimmickController>().UnlockKeyDoor();
            playerController.ItemUseFlag = true;
            return;
        }
    }

    /// <summary>
    /// 雲生成
    /// </summary>
    public void CreateCloud() {

    }
}
