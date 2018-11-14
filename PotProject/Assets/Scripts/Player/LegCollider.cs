using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegCollider : MonoBehaviour {

    private MoveController move_ctr;

	// Use this for initialization
	void Start () {
        move_ctr = transform.parent.GetComponent<MoveController>();
	}

    /// <summary>
    /// 足の部分のIsTriggerのON
    /// </summary>
    public void OnIsTrigger()
    {
        gameObject.GetComponent<Collider2D>().isTrigger = true;
        Debug.Log("On");
    }

    /// <summary>
    /// 足の部分のIsTriggerのOFF
    /// </summary>
    public void OffIsTrigger()
    {
        gameObject.GetComponent<Collider2D>().isTrigger = false;
        Debug.Log("Off");
    }
	
    private void OnCollisionStay2D(Collision2D col)
    {
        move_ctr._isJump = true;
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        move_ctr._isJump = false;
    }
}
