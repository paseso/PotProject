﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegCollider : MonoBehaviour {

    private MoveController move_ctr;

	// Use this for initialization
	void Start () {
        move_ctr = transform.parent.GetComponent<MoveController>();
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
