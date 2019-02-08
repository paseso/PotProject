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
        GameObject cloud = Instantiate(Resources.Load<GameObject>("Prefabs/GimmickTiles/FeatherCloudTile"));
        cloud.transform.SetParent(GameObject.Find(BrotherObj.transform.root.name + "/GimmickObject").transform);
        var dir = BrotherObj.GetComponent<MoveController>().direc;
        if(dir == MoveController.Direction.LEFT) {
            cloud.transform.position = new Vector2(BrotherObj.transform.position.x - cloud.GetComponent<SpriteRenderer>().bounds.size.x, BrotherObj.transform.position.y);
        } else {
            cloud.transform.position = new Vector2(BrotherObj.transform.position.x + cloud.GetComponent<SpriteRenderer>().bounds.size.x, BrotherObj.transform.position.y);
        }
        playerController.ItemUseFlag = true;
    }

    public void Water() {
        var waters = playerController.gameObject.GetComponent<StageController>().Waters;
        foreach(var i in waters) {
            i.SetActive(true);
        }
        playerController.ItemUseFlag = true;
    }
}
