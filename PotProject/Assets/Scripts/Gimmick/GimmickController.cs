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
    private BossController bossCon;
    private PlayerController pController;
    private int inFireZone = 0;

    public bool switchFlag() {
        if (gInfo.type == GimmickInfo.GimmickType.WATER) { return false; }
        if (gInfo.type == GimmickInfo.GimmickType.WATER) { return false; }
        if (gInfo.type == GimmickInfo.GimmickType.WATER) { return false; }
        if (gInfo.type == GimmickInfo.GimmickType.WATER) { return false; }
        return true;
    }

    private void Awake() {
        if (GameObject.Find("Controller") != null) {
            sController = GameObject.Find("Controller").GetComponent<StageController>();
        }

        
    }

    void Start() {
        gInfo = GetComponent<GimmickInfo>();
        if (gInfo.type == GimmickInfo.GimmickType.WATER) {
            sController.Waters.Add(gameObject);
        }

        if (FindObjectOfType<BossController>()) {
            bossCon = FindObjectOfType<BossController>();
        }

        mMapController = FindObjectOfType<MiniMapController>();
        mInfo = transform.root.GetComponent<MapInfo>();

        if (switchFlag()) {
            pController = GameObject.Find("Controller").GetComponent<PlayerController>();
        }
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
                StartCoroutine(SrideCroutine(objInfo.MapNumX, StageController.Direction.UP));
                break;
            case GimmickInfo.GimmickType.DOWN:
                StartCoroutine(SrideCroutine(objInfo.MapNumX, StageController.Direction.DOWN));
                break;
            case GimmickInfo.GimmickType.LEFT:
                StartCoroutine(SrideCroutine(objInfo.MapNumY, StageController.Direction.LEFT));
                break;
            case GimmickInfo.GimmickType.RIGHT:
                StartCoroutine(SrideCroutine(objInfo.MapNumY, StageController.Direction.RIGHT));
                break;
            case GimmickInfo.GimmickType.ROCK:
                RockDoorOpen();
                break;
            case GimmickInfo.GimmickType.SPRING:
                StartCoroutine(IsSpring());
                break;
            default:
                break;
        }
    }

    IEnumerator SrideCroutine(int infoDir,StageController.Direction dir) {
        pController.AllCommandActive = false;
        transform.DOLocalMoveY(transform.parent.transform.parent.localPosition.y - 0.25f, 1f);
        yield return new WaitForSeconds(1f);
        sController.Sride(infoDir, dir);
        transform.DOLocalMoveY(transform.parent.transform.parent.localPosition.y, 1f);
        yield return null;
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
                if (!col.GetComponent<MoveController>()) { return; }
                bossCon.IsMagicAttack = true;
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
        if (col.gameObject.layer != LayerMask.NameToLayer("Player")) { return; }
        switch (gInfo.type) {
            case GimmickInfo.GimmickType.FIREFIELD:
                if (!col.GetComponent<MoveController>()) { return; }
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
            StartCoroutine(SwitchDoorStep(rock));
        }
    }

    // ドアが開くコルーチン
    public IEnumerator SwitchDoorStep(GameObject obj) {
        transform.parent.transform.parent.DOLocalMoveY(transform.parent.transform.parent.localPosition.y - 0.25f, 1f);
        yield return new WaitForSeconds(1.5f);
        obj.transform.DOScaleY(0f, 2.0f).SetEase(Ease.Linear);
        StartCoroutine(DoorStep(obj));
    }

    public IEnumerator DoorStep(GameObject obj) {
        bool flag = true;
        obj.transform.DOScaleY(0f, 2.0f).SetEase(Ease.Linear);
        while (flag) {
            obj.transform.localPosition = new Vector2(obj.transform.localPosition.x + 0.05f, obj.transform.localPosition.y);
            yield return new WaitForSeconds(0.05f);
            obj.transform.localPosition = new Vector2(obj.transform.localPosition.x - 0.05f, obj.transform.localPosition.y);
            yield return new WaitForSeconds(0.05f);
            if(obj.transform.localScale.y <= 0) {
                flag = false;
            }
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

    /// <summary>
    /// 鍵付き扉
    /// </summary>
    public void UnlockKeyDoor()
    {
        StartCoroutine(DoorStep(gameObject));
    }

    public IEnumerator IsSpring()
    {
        GetComponent<BoxCollider2D>().enabled = false;   
        GameObject obj = FindObjectOfType<PlayerController>().gameObject;
        obj.GetComponent<PlayerController>().AllCommandActive = false;

        transform.parent.transform.parent.DOLocalMoveY(transform.parent.transform.parent.localPosition.y - 0.25f, 1f);
        yield return new WaitForSeconds(1.5f);
        GameObject player = FindObjectOfType<MoveController>().gameObject;
        bossCon.Flying(player);
    }
}
