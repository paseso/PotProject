using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotController : MonoBehaviour {

    [SerializeField]
    private PlayerManager manager;
    [SerializeField]
    private BringCollider bring_col;
    [SerializeField]
    private MoveController move_ctr;
    

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        MoveCollider();
	}

    /// <summary>
    /// 持つ範囲コライダーを左右に合わせて移動
    /// </summary>
    private void MoveCollider()
    {
        if (move_ctr._onLeft || move_ctr._onRight)
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x * -1, gameObject.transform.position.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Monster")
        {
            if (bring_col._Tubohit || move_ctr._itemFall)
            {
                Destroy(col.gameObject);
                manager.ItemAlchemy(ItemStatus.ITEM.SLIME);
            }
        }

    }
}
