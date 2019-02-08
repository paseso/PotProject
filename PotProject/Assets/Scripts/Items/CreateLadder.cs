using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// はしご生成スクリプト
/// </summary>
public class CreateLadder : MonoBehaviour {
    private string ladderPrefab = "Prefabs/GimmickTiles/Ladder";
    private PlayerController pController;

    void Start()
    {
        //PutOnLadder(gameObject);
    }

    /// <summary>
    /// はしご生成
    /// </summary>
    /// <param name="obj">はしごをかけるブロック</param>
    public void PutOnLadder() {
        StartCoroutine(Create());
    }

    /// <summary>
    /// はしご生成コルーチン
    /// </summary>
    /// <param name="obj"></param>
    IEnumerator Create() {
        // かけるブロックがないならReturn
        //if(obj == null) {
        //    pController.ItemUseFlag = false;
        //    yield break;
        //}

        // BlockじゃないならReturn
        if(gameObject.layer != LayerMask.NameToLayer("Block")) {
            yield break;
        }

        // Rayの始点を設定--------------------------------------------------------------
        var height = gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
        var radius = height * 0.5f + 0.1f;
        Vector2 startPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - radius);
        // Rayの始点を設定--------------------------------------------------------------*

        // Rayを飛ばす
        RaycastHit2D hit = Physics2D.Raycast(startPos, Vector2.down, Mathf.Infinity);

        // 1ブロック分の幅がなければReturn
        if(hit.distance < height) {
            yield break;
        }

        // あたったObjectがBlockじゃないならReturn
        if(hit.collider.gameObject.layer != LayerMask.NameToLayer("Block")) {
            yield break;
        }

        // objのレイヤーを変更
        gameObject.gameObject.layer = LayerMask.NameToLayer("LadderBlock");

        // 生成するはしごの数を指定
        var ladderCount = (hit.distance + 0.1f) / height;
        ladderCount = Mathf.RoundToInt(ladderCount);

        pController = GameObject.Find("Controller").GetComponent<PlayerController>();
        pController.ItemUseFlag = true;
        // 生成
        for (int i = 0; i <= ladderCount; i++) {
            GameObject ladder = Instantiate(Resources.Load<GameObject>(ladderPrefab));
            ladder.transform.localPosition = new Vector2(startPos.x, gameObject.transform.position.y - (height * i));
            ladder.transform.SetParent(gameObject.transform.root.transform);
            EffectManager.Instance.PlayEffect((int)EffectManager.EffectName.Effect_Grow, ladder.transform.position, 3, gameObject, true);
            yield return new WaitForSeconds(0.3f);
        }
    }
}
