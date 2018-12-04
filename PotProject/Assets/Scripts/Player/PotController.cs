using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotController : MonoBehaviour {

    [SerializeField]
    private PlayerController manager;
    [SerializeField]
    private BringCollider bring_col;
    [SerializeField]
    private MoveController move_ctr;

    private Rigidbody2D rig;

	// Use this for initialization
	void Start () {
        rig = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        PotJump();
	}
    
    /// <summary>
    /// プレイヤーがジャンプした時壺も一緒にジャンプする処理
    /// </summary>
    private void PotJump()
    {
        if (move_ctr.Jumping)
        {
            rig.velocity = new Vector2(0, 1f * move_ctr.speed);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<MonsterInfo>())
        {
            Debug.Log("きてる！");
            if (!bring_col._Tubohit || !move_ctr._itemFall)
                return;

            Destroy(col.gameObject);
            MonsterInfo monInfo = col.gameObject.GetComponent<MonsterInfo>();
            switch (monInfo.type)
            {
                case MonsterInfo.MonsterType.WATER:
                    manager.setItemList(ItemStatus.ITEM.SLIME);
                    break;

                case MonsterInfo.MonsterType.SNAKE:
                    manager.setItemList(ItemStatus.ITEM.SNAKE);
                    break;

                default:
                    Debug.Log("Type: " + monInfo.type);
                    break;
            }
            Debug.Log("Type: " + monInfo.type);
        }

        if (col.gameObject.GetComponent<GimmickInfo>())
        {

        }
    }
}
