using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテム効果スクリプト
/// </summary>
public class ItemController : MonoBehaviour {

    private PlayerController playerController;

    private GameObject BrotherObj;

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
        FindObjectOfType<TreeGrow>().Grow();
    }

    /// <summary>
    /// バリア発動
    /// </summary>
    public void CreateBarrier()
    {
        GameObject prefab = Resources.Load("Prefabs/Items/Barrier") as GameObject;
        Instantiate(prefab, BrotherObj.transform);
    }

    /// <summary>
    /// はしご生成
    /// </summary>
    public void LadderCreate()
    {
        playerController.OnBlock.AddComponent<CreateLadder>();
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
            return;
        }
    }

    /// <summary>
    /// 雲生成
    /// </summary>
    public void CreateCloud() {

    }
}
