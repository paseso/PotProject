using UnityEngine;
using Anima2D;

public class AnimController : MonoBehaviour {

    public struct AnimState
    {
        public enum AnimType
        {
            RIGHTJUMP = 0,
            LEFTJUMP,
            LEFTIDLE,
            RIGHTIDLE,
            LEFT_WALK,
            RIGHT_WALK,
            LADDER_UP,
            LADDER_DOWN,
        }
        public AnimType animtype;
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
            case AnimState.AnimType.LEFTJUMP:
            case AnimState.AnimType.LEFTIDLE:
            case AnimState.AnimType.LEFT_WALK:
                //剣
                gameObject.transform.GetChild(4).GetComponent<SpriteRenderer>().sortingOrder = 8;
                //頭
                gameObject.transform.GetChild(5).GetComponent<SpriteMeshAnimation>().frame = 0;
                //首モフ
                gameObject.transform.GetChild(6).GetComponent<SpriteMeshAnimation>().frame = 0;
                //LeftArm
                gameObject.transform.GetChild(7).GetChild(0).GetComponent<SpriteMeshInstance>().sortingOrder = 0;
                gameObject.transform.GetChild(7).GetChild(0).GetComponent<SpriteMeshAnimation>().frame = 0;
                gameObject.transform.GetChild(7).GetChild(0).GetChild(0).GetComponent<SpriteMeshAnimation>().frame = 0;
                gameObject.transform.GetChild(7).GetChild(0).GetChild(0).GetComponent<SpriteMeshInstance>().sortingOrder = 0;
                //Stomach
                gameObject.transform.GetChild(8).GetComponent<SpriteMeshInstance>().sortingOrder = 4;
                gameObject.transform.GetChild(8).GetComponent<SpriteMeshAnimation>().frame = 0;
                //RightArm
                gameObject.transform.GetChild(9).GetChild(0).GetComponent<SpriteMeshInstance>().sortingOrder = 8;
                gameObject.transform.GetChild(9).GetChild(0).GetChild(0).GetComponent<SpriteMeshInstance>().sortingOrder = 8;
                gameObject.transform.GetChild(9).GetChild(0).GetComponent<SpriteMeshAnimation>().frame = 0;
                gameObject.transform.GetChild(9).GetChild(0).GetChild(0).GetComponent<SpriteMeshAnimation>().frame = 0;
                //Waist
                gameObject.transform.GetChild(10).GetComponent<SpriteMeshAnimation>().frame = 0;
                //LeftMant
                gameObject.transform.GetChild(11).GetComponent<SpriteMeshInstance>().sortingOrder = 0;
                gameObject.transform.GetChild(11).GetComponent<SpriteMeshAnimation>().frame = 0;
                //RightMant
                gameObject.transform.GetChild(12).GetComponent<SpriteMeshInstance>().sortingOrder = 4;
                gameObject.transform.GetChild(12).GetComponent<SpriteMeshAnimation>().frame = 0;
                //LeftThings
                gameObject.transform.GetChild(13).GetComponent<SpriteMeshInstance>().sortingOrder = 1;
                gameObject.transform.GetChild(13).GetComponent<SpriteMeshAnimation>().frame = 0;
                //RightThings
                gameObject.transform.GetChild(14).GetComponent<SpriteMeshInstance>().sortingOrder = 1;
                gameObject.transform.GetChild(14).GetComponent<SpriteMeshAnimation>().frame = 0;
                //LeftLeg
                gameObject.transform.GetChild(15).GetComponent<SpriteMeshInstance>().sortingOrder = 1;
                gameObject.transform.GetChild(15).GetComponent<SpriteMeshAnimation>().frame = 0;
                //RightLeg
                gameObject.transform.GetChild(16).GetComponent<SpriteMeshInstance>().sortingOrder = 2;
                gameObject.transform.GetChild(16).GetComponent<SpriteMeshAnimation>().frame = 0;
                break;

            case AnimState.AnimType.RIGHTJUMP:
            case AnimState.AnimType.RIGHTIDLE:
            case AnimState.AnimType.RIGHT_WALK:
                //剣
                gameObject.transform.GetChild(4).GetComponent<SpriteRenderer>().sortingOrder = 1;
                //頭
                gameObject.transform.GetChild(5).GetComponent<SpriteMeshAnimation>().frame = 1;
                //首モフ
                gameObject.transform.GetChild(6).GetComponent<SpriteMeshAnimation>().frame = 1;
                //LeftArm
                gameObject.transform.GetChild(7).GetChild(0).GetComponent<SpriteMeshInstance>().sortingOrder = 10;
                gameObject.transform.GetChild(7).GetChild(0).GetChild(0).GetComponent<SpriteMeshAnimation>().frame = 1;
                gameObject.transform.GetChild(7).GetChild(0).GetChild(0).GetComponent<SpriteMeshAnimation>().frame = 1;
                gameObject.transform.GetChild(7).GetChild(0).GetChild(0).GetComponent<SpriteMeshInstance>().sortingOrder = 9;
                //Stomach
                gameObject.transform.GetChild(8).GetComponent<SpriteMeshInstance>().sortingOrder = 1;
                gameObject.transform.GetChild(8).GetComponent<SpriteMeshAnimation>().frame = 1;
                //RightArm
                gameObject.transform.GetChild(9).GetChild(0).GetComponent<SpriteMeshInstance>().sortingOrder = 0;
                gameObject.transform.GetChild(9).GetChild(0).GetChild(0).GetComponent<SpriteMeshInstance>().sortingOrder = 0;
                gameObject.transform.GetChild(9).GetChild(0).GetComponent<SpriteMeshAnimation>().frame = 1;
                gameObject.transform.GetChild(9).GetChild(0).GetChild(0).GetComponent<SpriteMeshAnimation>().frame = 1;
                //Waist
                gameObject.transform.GetChild(10).GetComponent<SpriteMeshAnimation>().frame = 1;
                //LeftMant
                gameObject.transform.GetChild(11).GetComponent<SpriteMeshInstance>().sortingOrder = 4;
                gameObject.transform.GetChild(11).GetComponent<SpriteMeshAnimation>().frame = 1;
                //RightMant
                gameObject.transform.GetChild(12).GetComponent<SpriteMeshInstance>().sortingOrder = 0;
                gameObject.transform.GetChild(12).GetComponent<SpriteMeshAnimation>().frame = 1;
                //LeftThings
                gameObject.transform.GetChild(13).GetComponent<SpriteMeshInstance>().sortingOrder = 2;
                gameObject.transform.GetChild(13).GetComponent<SpriteMeshAnimation>().frame = 1;
                //RightThings
                gameObject.transform.GetChild(14).GetComponent<SpriteMeshInstance>().sortingOrder = 1;
                gameObject.transform.GetChild(14).GetComponent<SpriteMeshAnimation>().frame = 1;
                //LeftLeg
                gameObject.transform.GetChild(15).GetComponent<SpriteMeshInstance>().sortingOrder = 3;
                gameObject.transform.GetChild(15).GetComponent<SpriteMeshAnimation>().frame = 1;
                //RightLeg
                gameObject.transform.GetChild(16).GetComponent<SpriteMeshInstance>().sortingOrder = 2;
                gameObject.transform.GetChild(16).GetComponent<SpriteMeshAnimation>().frame = 1;
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
            case AnimState.AnimType.RIGHTJUMP:
                animstate.animtype = AnimState.AnimType.RIGHTJUMP;
                anim.SetBool("isRightJump", true);
                anim.SetBool("isLeftJump", false);
                anim.SetBool("isLeftWalk", false);
                anim.SetBool("isRightWalk", false);
                anim.SetBool("isLeftIdle", false);
                anim.SetBool("isRightIdle", false);
                break;

            case AnimState.AnimType.LEFTJUMP:
                animstate.animtype = AnimState.AnimType.LEFTJUMP;
                anim.SetBool("isRightJump", false);
                anim.SetBool("isLeftJump", true);
                anim.SetBool("isLeftWalk", false);
                anim.SetBool("isRightWalk", false);
                anim.SetBool("isLeftIdle", false);
                anim.SetBool("isRightIdle", false);
                break;

            case AnimState.AnimType.LEFTIDLE:
                animstate.animtype = AnimState.AnimType.LEFTIDLE;
                anim.SetBool("isRightJump", false);
                anim.SetBool("isLeftJump", false);
                anim.SetBool("isLeftWalk", false);
                anim.SetBool("isRightWalk", false);
                anim.SetBool("isLeftIdle", true);
                anim.SetBool("isRightIdle", false);
                break;

            case AnimState.AnimType.RIGHTIDLE:
                animstate.animtype = AnimState.AnimType.RIGHTIDLE;
                anim.SetBool("isRightJump", false);
                anim.SetBool("isLeftJump", false);
                anim.SetBool("isLeftWalk", false);
                anim.SetBool("isRightWalk", false);
                anim.SetBool("isLeftIdle", false);
                anim.SetBool("isRightIdle", true);
                break;

            case AnimState.AnimType.LEFT_WALK:
                animstate.animtype = AnimState.AnimType.LEFT_WALK;
                anim.SetBool("isRightJump", false);
                anim.SetBool("isLeftJump", false);
                anim.SetBool("isLeftWalk", true);
                anim.SetBool("isRightWalk", false);
                anim.SetBool("isLeftIdle", false);
                anim.SetBool("isRightIdle", false);
                break;

            case AnimState.AnimType.RIGHT_WALK:
                animstate.animtype = AnimState.AnimType.RIGHT_WALK;
                anim.SetBool("isRightJump", false);
                anim.SetBool("isLeftJump", false);
                anim.SetBool("isLeftWalk", false);
                anim.SetBool("isRightWalk", true);
                anim.SetBool("isLeftIdle", false);
                anim.SetBool("isRightIdle", false);
                break;

            case AnimState.AnimType.LADDER_UP:
                animstate.animtype = AnimState.AnimType.LADDER_UP;
                anim.SetBool("isRightJump", false);
                anim.SetBool("isLeftJump", false);
                anim.SetBool("isLeftWalk", false);
                anim.SetBool("isRightWalk", false);
                anim.SetBool("isLeftIdle", false);
                anim.SetBool("isRightIdle", false);
                break;

            case AnimState.AnimType.LADDER_DOWN:
                animstate.animtype = AnimState.AnimType.LADDER_DOWN;
                anim.SetBool("isRightJump", false);
                anim.SetBool("isLeftJump", false);
                anim.SetBool("isLeftWalk", false);
                anim.SetBool("isRightWalk", false);
                anim.SetBool("isLeftIdle", false);
                anim.SetBool("isRightIdle", false);
                break;
        }
    }
}
