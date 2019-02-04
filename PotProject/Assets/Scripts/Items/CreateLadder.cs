using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// はしご生成スクリプト
/// </summary>
public class CreateLadder : MonoBehaviour {
    private string ladderPrefab = "Prefabs/GimmickTiles/Ladder";

    void Start()
    {
        PutOnLadder(gameObject);
    }

    /// <summary>
    /// はしご生成
    /// </summary>
    /// <param name="obj">はしごをかけるブロック</param>
    public void PutOnLadder(GameObject obj) {
        StartCoroutine(Create(obj));
    }

    /// <summary>
    /// はしご生成コルーチン
    /// </summary>
    /// <param name="obj"></param>
    IEnumerator Create(GameObject obj) {
        // かけるブロックがないならReturn
        if(obj == null) { yield break; }

        // BlockじゃないならReturn
        if(obj.layer != LayerMask.NameToLayer("Block")) { yield break; }

        // Rayの始点を設定--------------------------------------------------------------
        var height = obj.GetComponent<SpriteRenderer>().bounds.size.y;
        var radius = height * 0.5f + 0.1f;
        Vector2 startPos = new Vector2(obj.transform.position.x, obj.transform.position.y - radius);
        // Rayの始点を設定--------------------------------------------------------------*

        // Rayを飛ばす
        RaycastHit2D hit = Physics2D.Raycast(startPos, Vector2.down, Mathf.Infinity);

        // 1ブロック分の幅がなければReturn
        if(hit.distance < height) { yield break; }

        // あたったObjectがBlockじゃないならReturn
        if(hit.collider.gameObject.layer != LayerMask.NameToLayer("Block")) { yield break; }

        // objのレイヤーを変更
        obj.gameObject.layer = LayerMask.NameToLayer("LadderBlock");

        // 生成するはしごの数を指定
        var ladderCount = (hit.distance + 0.1f) / height;
        ladderCount = Mathf.RoundToInt(ladderCount);

        // 生成
        for (int i = 0; i <= ladderCount; i++) {
            GameObject ladder = Instantiate(Resources.Load<GameObject>(ladderPrefab));
            ladder.transform.localPosition = new Vector2(startPos.x, obj.transform.position.y - (height * i));
            ladder.transform.SetParent(obj.transform.root.transform);
            EffectManager.Instance.PlayEffect((int)EffectManager.EffectName.Effect_Grow, ladder.transform.position, 3, obj, true);
            yield return new WaitForSeconds(0.3f);
        }
    }
}
