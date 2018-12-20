using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour {

    public struct AnimState
    {
        public enum AnimType
        {
            JUMP = 0,
            LEFTIDLE,
            RIGHTIDLE,
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
    /// AnimState,AnimTypeによって描画順を変更する処理
    /// </summary>
    private void StateOrderInLayer(AnimState.AnimType type)
    {
        switch (type)
        {
            case AnimState.AnimType.JUMP:
                
                break;

            case AnimState.AnimType.LEFTIDLE:
            case AnimState.AnimType.LEFT_WALK:
                //LeftArm
                gameObject.transform.GetChild(7).GetChild(0).GetComponent<Anima2D.SpriteMeshInstance>().sortingOrder = 0;
                gameObject.transform.GetChild(7).GetChild(0).GetChild(0).GetComponent<Anima2D.SpriteMeshInstance>().sortingOrder = 0;
                //Stomach
                gameObject.transform.GetChild(8).GetComponent<Anima2D.SpriteMeshInstance>().sortingOrder = 4;
                //RightArm
                gameObject.transform.GetChild(9).GetChild(0).GetComponent<Anima2D.SpriteMeshInstance>().sortingOrder = 6;
                gameObject.transform.GetChild(9).GetChild(0).GetChild(0).GetComponent<Anima2D.SpriteMeshInstance>().sortingOrder = 6;
                //LeftMant
                gameObject.transform.GetChild(11).GetComponent<Anima2D.SpriteMeshInstance>().sortingOrder = 0;
                //RightMant
                gameObject.transform.GetChild(12).GetComponent<Anima2D.SpriteMeshInstance>().sortingOrder = 4;
                //LeftThings
                gameObject.transform.GetChild(13).GetComponent<Anima2D.SpriteMeshInstance>().sortingOrder = 1;
                //RightThings
                gameObject.transform.GetChild(14).GetComponent<Anima2D.SpriteMeshInstance>().sortingOrder = 1;
                //LeftLeg
                gameObject.transform.GetChild(15).GetComponent<Anima2D.SpriteMeshInstance>().sortingOrder = 1;
                //RightLeg
                gameObject.transform.GetChild(16).GetComponent<Anima2D.SpriteMeshInstance>().sortingOrder = 2;
                break;

            case AnimState.AnimType.RIGHTIDLE:
            case AnimState.AnimType.RIGHT_WALK:
                //LeftArm
                gameObject.transform.GetChild(7).GetChild(0).GetComponent<Anima2D.SpriteMeshInstance>().sortingOrder = 6;
                gameObject.transform.GetChild(7).GetChild(0).GetChild(0).GetComponent<Anima2D.SpriteMeshInstance>().sortingOrder = 6;
                //Stomach
                gameObject.transform.GetChild(8).GetComponent<Anima2D.SpriteMeshInstance>().sortingOrder = 1;
                //RightArm
                gameObject.transform.GetChild(9).GetChild(0).GetComponent<Anima2D.SpriteMeshInstance>().sortingOrder = 0;
                gameObject.transform.GetChild(9).GetChild(0).GetChild(0).GetComponent<Anima2D.SpriteMeshInstance>().sortingOrder = 0;
                //LeftMant
                gameObject.transform.GetChild(11).GetComponent<Anima2D.SpriteMeshInstance>().sortingOrder = 4;
                //RightMant
                gameObject.transform.GetChild(12).GetComponent<Anima2D.SpriteMeshInstance>().sortingOrder = 0;
                //LeftThings
                gameObject.transform.GetChild(13).GetComponent<Anima2D.SpriteMeshInstance>().sortingOrder = 2;
                //RightThings
                gameObject.transform.GetChild(14).GetComponent<Anima2D.SpriteMeshInstance>().sortingOrder = 1;
                //LeftLeg
                gameObject.transform.GetChild(15).GetComponent<Anima2D.SpriteMeshInstance>().sortingOrder = 3;
                //RightLeg
                gameObject.transform.GetChild(16).GetComponent<Anima2D.SpriteMeshInstance>().sortingOrder = 2;
                break;

            case AnimState.AnimType.LADDER_UP:
                
                break;

            case AnimState.AnimType.LADDER_DOWN:
                
                break;
        }
    }

    /// <summary>
    /// アニメーションの状態変更処理
    /// </summary>
    /// <param name="type"></param>
    public void ChangeAnimatorState(AnimState.AnimType type)
    {
        StateOrderInLayer(type);
        switch (type)
        {
            case AnimState.AnimType.JUMP:
                anim.SetBool("isLeftWalk", false);
                anim.SetBool("isRightWalk", false);
                anim.SetBool("isLeftIdle", false);
                anim.SetBool("isRightIdle", false);
                break;

            case AnimState.AnimType.LEFTIDLE:
                anim.SetBool("isLeftWalk", false);
                anim.SetBool("isRightWalk", false);
                anim.SetBool("isLeftIdle", true);
                anim.SetBool("isRightIdle", false);
                break;

            case AnimState.AnimType.RIGHTIDLE:
                anim.SetBool("isLeftWalk", false);
                anim.SetBool("isRightWalk", false);
                anim.SetBool("isLeftIdle", false);
                anim.SetBool("isRightIdle", true);
                break;

            case AnimState.AnimType.LEFT_WALK:
                anim.SetBool("isLeftWalk", true);
                anim.SetBool("isRightWalk", false);
                anim.SetBool("isLeftIdle", false);
                anim.SetBool("isRightIdle", false);
                break;

            case AnimState.AnimType.RIGHT_WALK:
                anim.SetBool("isLeftWalk", false);
                anim.SetBool("isRightWalk", true);
                anim.SetBool("isLeftIdle", false);
                anim.SetBool("isRightIdle", false);
                break;

            case AnimState.AnimType.LADDER_UP:
                anim.SetBool("isLeftWalk", false);
                anim.SetBool("isRightWalk", false);
                anim.SetBool("isLeftIdle", false);
                anim.SetBool("isRightIdle", false);
                break;

            case AnimState.AnimType.LADDER_DOWN:
                anim.SetBool("isLeftWalk", false);
                anim.SetBool("isRightWalk", false);
                anim.SetBool("isLeftIdle", false);
                anim.SetBool("isRightIdle", false);
                break;
        }
    }
}
