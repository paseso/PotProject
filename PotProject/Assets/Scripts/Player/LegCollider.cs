using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegCollider : MonoBehaviour {

    private MoveController move_ctr;
    private MapInfo mInfo;
    private int floor_count = 0;
    [HideInInspector]
    public bool _ActiveTrigger = false;

	// Use this for initialization
	void Start () {
        move_ctr = transform.parent.GetComponent<MoveController>();
        mInfo = gameObject.transform.root.GetComponent<MapInfo>();
        floor_count = 0;
        _ActiveTrigger = false;
	}

    /// <summary>
    /// floor_countを0にする
    /// </summary>
    public void ClearFloorCount()
    {
        floor_count = 0;
    }

    /// <summary>
    /// 足の部分のIsTriggerのON
    /// </summary>
    public void OnIsTrigger()
    {
        gameObject.GetComponent<Collider2D>().isTrigger = true;
        _ActiveTrigger = true;
        Debug.Log("IsTrigger On");
    }

    /// <summary>
    /// 足の部分のIsTriggerのOFF
    /// </summary>
    public void OffIsTrigger()
    {
        gameObject.GetComponent<Collider2D>().isTrigger = false;
        _ActiveTrigger = false;
        Debug.Log("IsTrigger Off");
    }
	
    private void OnCollisionStay2D(Collision2D col)
    {
        move_ctr._isJump = true;
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        move_ctr._isJump = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "floor")
        {
            floor_count++;
            if(floor_count >= 2)
            {
                OffIsTrigger();
                Debug.Log("二回目");
                move_ctr.GimmickLadderOut();
            }
            else if(floor_count == 1)
            {
            }
        }
    }
}
