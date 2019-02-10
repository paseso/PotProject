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
        playerController.HPUp(2);
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
        if(playerController.OnBlock == null) { return; }
        if (playerController.OnBlock.layer != LayerMask.NameToLayer("Block")) { return; }
        if (playerController.OnBlock.GetComponent<CreateLadder>()) {
            playerController.OnBlock.GetComponent<CreateLadder>().PutOnLadder();
            return;
        }
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
        int mask = LayerMask.GetMask(new string[] { "Block", "LadderBlock" });

        RaycastHit2D hit = new RaycastHit2D();
        var startPos = new Vector2(BrotherObj.transform.position.x, BrotherObj.transform.position.y);
        var dir = BrotherObj.GetComponent<MoveController>().direc;
        var cloudObj = Resources.Load<GameObject>("Prefabs/GimmickTiles/FeatherCloudTile");
        if (dir == MoveController.Direction.LEFT) {
            hit = Physics2D.BoxCast(startPos,new Vector2(1,1),0, Vector2.left, Mathf.Infinity,mask);
        } else {
            hit = Physics2D.BoxCast(startPos, new Vector2(1, 1), 0, Vector2.right, Mathf.Infinity, mask);
        }

        if (hit.distance < cloudObj.GetComponent<SpriteRenderer>().bounds.size.x) { return; }

        GameObject cloud = Instantiate(cloudObj);
        cloud.transform.SetParent(GameObject.Find(BrotherObj.transform.root.name + "/GimmickObject").transform);
        
        if(dir == MoveController.Direction.LEFT) {
            cloud.transform.position = new Vector2(BrotherObj.transform.position.x - cloud.GetComponent<SpriteRenderer>().bounds.size.x, BrotherObj.transform.position.y);
        } else {
            cloud.transform.position = new Vector2(BrotherObj.transform.position.x + cloud.GetComponent<SpriteRenderer>().bounds.size.x, BrotherObj.transform.position.y);
        }
        playerController.ItemUseFlag = true;
    }

    /// <summary>
    /// 雨雲から雨を降らせる
    /// </summary>
    public void Rain() {
        if (!playerController.OnGimmick.GetComponent<GimmickInfo>()) { return; }
        var info = playerController.OnGimmick.GetComponent<GimmickInfo>().gameObject;
        if (info.GetComponent<GimmickInfo>().type != GimmickInfo.GimmickType.RAINCLOUD) { return; }
        info.GetComponent<GimmickController>().ActiveWater(info);
        playerController.ItemUseFlag = true;
    }
}
