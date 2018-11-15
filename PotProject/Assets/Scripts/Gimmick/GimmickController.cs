using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 各ギミックの処理
/// </summary>
public class GimmickController :MonoBehaviour {
    private StageManager sManager;
    private MapInfo mInfo;
    private GimmickInfo gInfo;

    // Use this for initialization
    void Start() {
        sManager = GameObject.Find("Controller").GetComponent<StageManager>();
        mInfo = transform.root.GetComponent<MapInfo>();
        gInfo = GetComponent<GimmickInfo>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.G)) {
            Grow();
        }
    }

    /// <summary>
    /// あたり判定(OnCollisionEnter)
    /// </summary>
    /// <param name="col"></param>
    public void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Player") {
            switch (gInfo.type) {
                case GimmickInfo.GimmickType.UP:
                    break;
                case GimmickInfo.GimmickType.DOWN:
                    sManager.SrideStage(1, StageManager.Direction.DOWN);
                    break;
                case GimmickInfo.GimmickType.LEFT:
                    break;
                case GimmickInfo.GimmickType.RIGHT:
                    sManager.SrideStage(0, StageManager.Direction.RIGHT);
                    break;
                case GimmickInfo.GimmickType.ROCK:
                    mInfo.rock.transform.DOScaleY(0f, 1.0f).SetEase(Ease.Linear);
                    mInfo.UpRockFlag = true;
                    break;
            }
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 判定(OnTriggerEnter)
    /// </summary>
    /// <param name="col"></param>
    public void OnTriggerEnter2D(Collider2D col) {
        switch (gInfo.type) {
            case GimmickInfo.GimmickType.TREE:
                mInfo.GrowTreeFlag = true;
                break;

            case GimmickInfo.GimmickType.LADDER:
                mInfo.LadderFlag = true;
                break;
        }
    }

    /// <summary>
    /// 判定(OnTriggerExit)
    /// </summary>
    /// <param name="col"></param>
    public void OnTriggerExit2D(Collider2D col) {
        switch (gInfo.type) {
            case GimmickInfo.GimmickType.TREE:
                mInfo.GrowTreeFlag = false;
                break;

            case GimmickInfo.GimmickType.LADDER:
                mInfo.LadderFlag = false;
                break;
        }
    }

    /// <summary>
    /// 木(ツル)を成長させる
    /// </summary>
    public void Grow() {
        if (!mInfo.GrowTreeFlag) return;

        mInfo.tree.transform.DOScaleY(1f, 1f).SetEase(Ease.Linear);
    }
}
