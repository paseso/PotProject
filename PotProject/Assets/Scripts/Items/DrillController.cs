using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillController : MonoBehaviour {
    MoveController mController;

    void Start()
    {
        mController = FindObjectOfType<MoveController>();
        
    }
 
    /// <summary>
    /// ドリルコルーチン
    /// </summary>
    /// <param name="obj"></param>
    IEnumerator Drill(GameObject obj)
    {
        // 置くブロックがないならReturn
        if (obj == null) { yield return null; }

        // BlockじゃないならReturn
        if (obj.layer != LayerMask.NameToLayer("Block")) { yield return null; }
        // Rayの始点を設定--------------------------------------------------------------
        var height = obj.GetComponent<SpriteRenderer>().bounds.size.y;
        var radius = height * 0.5f + 0.1f;
        Vector2 sidePos = new Vector2();
        
        // Rayの始点を設定[END]--------------------------------------------------------------

        RaycastHit2D hitSide = new RaycastHit2D();
        // Rayを飛ばす
        
        switch (mController.direc)
        {
            case MoveController.Direction.LEFT:
                sidePos = new Vector2(obj.transform.position.x - radius, obj.transform.position.y);
                hitSide = Physics2D.Raycast(sidePos, Vector2.left, 1);
                break;
            case MoveController.Direction.RIGHT:
                sidePos = new Vector2(obj.transform.position.x + radius, obj.transform.position.y);
                hitSide = Physics2D.Raycast(sidePos, Vector2.right, 1);
                break;
        }

        if (hitSide.collider == null) { yield return null; }

        if (hitSide.collider.gameObject.layer == LayerMask.NameToLayer("Block"))
        {
            Destroy(hitSide.collider.gameObject);
            // アイテムリストから削除
        }
        yield return null;

    }
}
