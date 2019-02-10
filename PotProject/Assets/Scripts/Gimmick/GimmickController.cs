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

    // 各クラス-----------------------
    private StageController sController;
    private MapInfo mInfo;
    private GimmickInfo gInfo;
    private MiniMapController mMapController;
    private BossController bossCon;
    private PlayerController pController;
    private PlayerManager pManager;
    // -------------------------------

    // プレイヤーがスイッチに乗ってるかフラグ----
    private bool onPlayerFlag = false;
    public bool OnPlayerFlag {
        get { return onPlayerFlag; }
        set { onPlayerFlag = value; }
    }
    // ------------------------------------------
    private int inFireCount = 0;
    
    // マップの位置
    private Vector2 mapPos;

    void Awake() {
        if (GameObject.Find("Controller")) {
            sController = GameObject.Find("Controller").GetComponent<StageController>();
            pController = GameObject.Find("Controller").GetComponent<PlayerController>();
        }

        if (GameObject.Find("PlayerStatus")) {
            pManager = GameObject.Find("PlayerStatus").GetComponent<PlayerManager>();
        }

        if (GameObject.Find("MiniMap")) {
            mMapController = GameObject.Find("MiniMap").GetComponent<MiniMapController>();
        }
        
    }

    void Start() {

        mInfo = transform.root.GetComponent<MapInfo>();
        gInfo = GetComponent<GimmickInfo>();
        //if (gInfo.type == GimmickInfo.GimmickType.WATER) {
        //    sController.Waters.Add(gameObject);
        //}
        if (FindObjectOfType<BossController>())
        {
            bossCon = FindObjectOfType<BossController>();
        }
    }

    /// <summary>
    /// あたり判定(OnCollisionEnter)
    /// </summary>
    /// <param name="col"></param>
    public void OnCollisionEnter2D(Collision2D col) {
        MapInfo mInfo = col.transform.root.gameObject.GetComponent<MapInfo>();
        if (col.gameObject.layer != LayerMask.NameToLayer("Player")) { return; }
        switch (gInfo.type) {
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

    /// <summary>
    /// スライド処理（□で呼び出し）
    /// </summary>
    public void StartSride()
    {
        mapPos = new Vector2(mInfo.MapNumX, mInfo.MapNumY);
        switch (gInfo.type)
        {
            case GimmickInfo.GimmickType.UP:
                StartCoroutine(SrideCroutine((int)mapPos.x, StageController.Direction.UP));
                break;
            case GimmickInfo.GimmickType.DOWN:
                StartCoroutine(SrideCroutine((int)mapPos.x, StageController.Direction.DOWN));
                break;
            case GimmickInfo.GimmickType.LEFT:
                StartCoroutine(SrideCroutine((int)mapPos.y, StageController.Direction.LEFT));
                break;
            case GimmickInfo.GimmickType.RIGHT:
                StartCoroutine(SrideCroutine((int)mapPos.y, StageController.Direction.RIGHT));
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
            case GimmickInfo.GimmickType.UP:
            case GimmickInfo.GimmickType.DOWN:
            case GimmickInfo.GimmickType.LEFT:
            case GimmickInfo.GimmickType.RIGHT:
                if (col.gameObject.GetComponent<LegCollider>())
                {
                    onPlayerFlag = true;
                }
                break;
            case GimmickInfo.GimmickType.MAPCHANGE:
                col.transform.parent.transform.parent.transform.SetParent(transform.root.gameObject.transform);
                mMapController.NowMap();
                break;
            //case GimmickInfo.GimmickType.FIREFIELD:
            //    if (!col.GetComponent<MoveController>()) { return; }
            //    var bossMShoot = bossCon.gameObject.transform.GetChild(0).GetComponent<MagicShoot>();
            //    bossMShoot.playerPos = col.transform.position;
            //    bossMShoot.ShootFlag = true;
            //    break;
            //case GimmickInfo.GimmickType.THUNDERFIELD:
            //    if (!col.GetComponent<MoveController>()) { return; }
            //    if(!GameObject.Find(transform.root.name + "/OtherObject/Lion(Clone)")) { return; }
            //    GameObject mObj = GameObject.Find(transform.root.name + "/OtherObject/Lion(Clone)");
            //    MagicShoot magic = mObj.transform.GetChild(0).GetComponent<MagicShoot>();
            //    magic.playerPos = col.transform.position;
            //    magic.ShootFlag = true;
            //    break;

            default:
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.layer != LayerMask.NameToLayer("Player")) { return; }
        if (gInfo.type == GimmickInfo.GimmickType.FIREFIELD)
        {
            var bossMShoot = bossCon.gameObject.transform.GetComponentInChildren<MagicShoot>();
            bossMShoot.playerPos = col.transform.position;
            bossMShoot.ShootFlag = true;
        }
        else if (gInfo.type == GimmickInfo.GimmickType.THUNDERFIELD)
        {
            if (!col.GetComponent<MoveController>()) { return; }
            if (!GameObject.Find(transform.root.name + "/OtherObject/Lion(Clone)")) { return; }
            GameObject mObj = GameObject.Find(transform.root.name + "/OtherObject/Lion(Clone)");
            MagicShoot magic = mObj.transform.GetComponentInChildren<MagicShoot>();
            magic.playerPos = col.transform.position;
            magic.ShootFlag = true;
        }
    }

    /// <summary>
    /// 判定(OnTriggerExit)
    /// </summary>
    /// <param name="col"></param>
    public void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.layer != LayerMask.NameToLayer("Player")) { return; }
        switch (gInfo.type) {
            case GimmickInfo.GimmickType.UP:
            case GimmickInfo.GimmickType.DOWN:
            case GimmickInfo.GimmickType.LEFT:
            case GimmickInfo.GimmickType.RIGHT:
                if (col.gameObject.GetComponent<LegCollider>())
                {
                    onPlayerFlag = false;
                }
                break;
            case GimmickInfo.GimmickType.FIREFIELD:
                if (!col.GetComponent<MoveController>()) { return; }
                var bossMShoot = bossCon.gameObject.transform.GetChild(0).GetComponent<MagicShoot>();
                bossMShoot.playerPos = col.transform.position;
                bossMShoot.ShootFlag = false;
                break;
            case GimmickInfo.GimmickType.THUNDERFIELD:
                if (!col.GetComponent<MoveController>()) { return; }
                if (GameObject.Find(transform.root.name + "/OtherObject/Lion(Clone)")) {
                    var mObj = GameObject.Find(transform.root.name + "/OtherObject/Lion(Clone)");
                    var magic = mObj.transform.GetChild(0).GetComponent<MagicShoot>();
                    magic.ShootFlag = false;
                }
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
        DoorStep(obj);
    }

    public void DoorStep(GameObject obj) {
        bool flag = true;
        obj.GetComponent<OpenDoor>().Open();
        SoundManager.Instance.PlaySe((int)SoundManager.SENAME.SE_STONEDOOR);
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
    public void BakeTree()
    {
        if(pManager.GetSwordType != PlayerStatus.SWORDTYPE.FIRE) { return; }
        StartCoroutine(Bake());
    }

    /// <summary>
    /// 木を焼くコルーチン
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public IEnumerator Bake()
    {
        GameObject FireEffect = EffectManager.Instance.PlayEffect((int)EffectManager.EffectName.Effect_Fire, gameObject.transform.position, 10, gameObject, false);
        FireEffect.SetActive(false);
        for (int i = 2; i >= 0; i--)
        {
            Sprite sp_tree = Resources.Load<Sprite>("Textures/Gimmicks/fire" + i);
            gameObject.GetComponent<SpriteRenderer>().sprite = sp_tree;            
            Debug.Log("sp_tree=" + "Textures/Gimmicks/fire" + i);
            yield return new WaitForSeconds(1f);
            if (i == 2)
            {
                // 燃えるエフェクト
                FireEffect.SetActive(true);
            }
        }
        Destroy(FireEffect);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    /// <summary>
    /// 鍵付き扉
    /// </summary>
    public void UnlockKeyDoor()
    {
        //DoorStep(gameObject);
        GetComponent<OpenDoor>().OpenKey();
        SoundManager.Instance.PlaySe((int)SoundManager.SENAME.SE_STONEDOOR);
    }

    /// <summary>
    /// ボス吹っ飛び
    /// </summary>
    /// <returns></returns>
    public IEnumerator IsSpring()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        pController.AllCommandActive = false;

        transform.parent.transform.parent.DOLocalMoveY(transform.parent.transform.parent.localPosition.y - 0.25f, 1f);
        yield return new WaitForSeconds(1.5f);
        GameObject player = FindObjectOfType<MoveController>().gameObject;
        bossCon.Flying(player);
    }

    /// <summary>
    /// 水出し
    /// </summary>
    public void ActiveWater(GameObject obj) {
        StartCoroutine(ActiveWaterCoroutine(obj));
    }
    
    IEnumerator ActiveWaterCoroutine(GameObject obj) {
        var waters = pController.gameObject.GetComponent<StageController>().Waters;
        transform.parent.GetChild(1).GetComponent<SpriteRenderer>().DOFade(1f, 1f).SetEase(Ease.Linear).OnComplete(() => {
            transform.parent.GetChild(0).GetComponent<SpriteRenderer>().DOFade(0f, 0f).SetEase(Ease.Linear);
        });
        foreach (var i in waters) {
            i.SetActive(true);
        }
        yield return new WaitForSeconds(3f);
        transform.parent.GetChild(0).GetComponent<SpriteRenderer>().DOFade(1f, 0f).SetEase(Ease.Linear).OnComplete(() => {
            transform.parent.GetChild(1).GetComponent<SpriteRenderer>().DOFade(0f, 1f).SetEase(Ease.Linear);
        });
        
        yield return null;
    }

    /// <summary>
    /// 落雷
    /// </summary>
    public void Lightning()
    {
        pController.EventFlag = true;

        var boss = FindObjectOfType<BossController>().gameObject;
        if(boss.transform.root.gameObject != transform.root.gameObject) { return; }
        StartCoroutine(LightningCoroutine(boss));

    }

    public IEnumerator LightningCoroutine(GameObject boss)
    {
        yield return new WaitForSeconds(1f);
        CameraController cameraCon = FindObjectOfType<CameraController>();
        cameraCon.target = boss;
        yield return new WaitForSeconds(1f);
        SoundManager.Instance.PlaySe((int)SoundManager.SENAME.SE_THUNDER);
        // 雷エフェクトをbossの座標の上に表示

        // bossがやられる演出
        boss.GetComponent<SpriteRenderer>().color = new Color(70, 30, 0);
        yield return new WaitForSeconds(3f);
        cameraCon.target = FindObjectOfType<MoveController>().gameObject;
        yield return new WaitForSeconds(1f);
        sController.GetClearPanel.SetActive(true);
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(sController.GetClearPanel.transform.GetChild(0).gameObject);
        yield return null;
    }


}
