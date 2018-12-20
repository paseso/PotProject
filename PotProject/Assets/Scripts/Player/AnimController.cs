using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour {

    public struct AnimState
    {
        public enum AnimType
        {
            JUMP = 0,
            IDLE,
            LEFT_WALK,
            RIGHT_WALK,
            LADDER_UP,
            LADDER_DOWN,
        }
    }

    public AnimState animstate;

    private Animator anim;

	// Use this for initialization
	void Start () {
        anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// アニメーションの状態変更処理
    /// </summary>
    /// <param name="type"></param>
    public void ChangeAnimatorState(AnimState.AnimType type)
    {
        switch (type)
        {
            case AnimState.AnimType.JUMP:
                anim.SetBool("isLeftWalk", false);
                anim.SetBool("isIdle", false);
                break;

            case AnimState.AnimType.IDLE:
                anim.SetBool("isLeftWalk", false);
                anim.SetBool("isIdle", true);
                break;

            case AnimState.AnimType.LEFT_WALK:
                anim.SetBool("isLeftWalk", true);
                anim.SetBool("isIdle", false);
                break;

            case AnimState.AnimType.RIGHT_WALK:
                anim.SetBool("isLeftWalk", false);
                anim.SetBool("isIdle", false);
                break;

            case AnimState.AnimType.LADDER_UP:
                anim.SetBool("isLeftWalk", false);
                anim.SetBool("isIdle", false);
                break;

            case AnimState.AnimType.LADDER_DOWN:
                anim.SetBool("isLeftWalk", false);
                anim.SetBool("isIdle", false);
                break;
        }
    }
}
