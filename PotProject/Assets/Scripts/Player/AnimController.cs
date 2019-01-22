using UnityEngine;
using Anima2D;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

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
            SORDATTACK_LEFT,
            SORDATTACK_RIGHT,
            LADDER_UP,
            LADDER_DOWN,
            LEFT_GETITEM,
            RIGHT_GETITEM,
        }
        public AnimType animtype;
    }

    public AnimState animstate;

    private Animator anim;
    private AttackZoneController attack_ctr;
    private BringCollider bring_col;
    private Animator pot_anim;
    private EffectManager effect_mng;
    private PlayerController player_ctr;

    //拾うアニメーションの時に使う
    private GameObject Itemtarget;
    private bool _attackStart = false;

    public GameObject setItemtaget
    {
        set { Itemtarget = value; }
    }

	// Use this for initialization
	void Start () {
        player_ctr = GameObject.Find("Controller").GetComponent<PlayerController>();
        anim = gameObject.GetComponent<Animator>();
        pot_anim = gameObject.transform.parent.GetComponentInChildren<PotController>().gameObject.GetComponent<Animator>();
        attack_ctr = gameObject.transform.parent.GetComponentInChildren<AttackZoneController>();
        bring_col = gameObject.transform.GetComponentInChildren<BringCollider>();
        effect_mng = GameObject.Find("EffectManager").GetComponent<EffectManager>();
        _attackStart = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// AnimState.AnimTypeによって描画順を変更する処理
    /// </summary>
    private void StateOrderInLayer(AnimState.AnimType type)
    {
        switch (type)
        {
            case AnimState.AnimType.LEFTJUMP:
            case AnimState.AnimType.LEFTIDLE:
            case AnimState.AnimType.LEFT_WALK:
            case AnimState.AnimType.SORDATTACK_LEFT:
                //剣
                gameObject.transform.GetChild(4).GetComponent<SpriteRenderer>().sortingOrder = 10;
                //頭
                gameObject.transform.GetChild(5).GetComponent<SpriteMeshAnimation>().frame = 0;
                gameObject.transform.GetChild(5).GetChild(0).GetChild(0).GetComponent<SpriteMeshInstance>().sortingOrder = 8;
                gameObject.transform.GetChild(5).GetChild(0).GetChild(1).GetComponent<SpriteMeshInstance>().sortingOrder = 8;
                gameObject.transform.GetChild(5).GetChild(0).GetChild(0).GetComponent<SpriteMeshAnimation>().frame = 0;
                gameObject.transform.GetChild(5).GetChild(0).GetChild(1).GetComponent<SpriteMeshAnimation>().frame = 0;
                //首モフ
                gameObject.transform.GetChild(6).GetComponent<SpriteMeshAnimation>().frame = 0;
                //LeftArm
                gameObject.transform.GetChild(7).GetChild(0).GetComponent<SpriteMeshInstance>().sortingOrder = 0;
                gameObject.transform.GetChild(7).GetChild(0).GetChild(0).GetComponent<SpriteMeshInstance>().sortingOrder = 0;
                gameObject.transform.GetChild(7).GetChild(0).GetComponent<SpriteMeshAnimation>().frame = 0;
                gameObject.transform.GetChild(7).GetChild(0).GetChild(0).GetComponent<SpriteMeshAnimation>().frame = 0;
                //Stomach
                gameObject.transform.GetChild(8).GetComponent<SpriteMeshInstance>().sortingOrder = 4;
                gameObject.transform.GetChild(8).GetComponent<SpriteMeshAnimation>().frame = 0;
                //RightArm
                gameObject.transform.GetChild(9).GetChild(0).GetComponent<SpriteMeshInstance>().sortingOrder = 6;
                gameObject.transform.GetChild(9).GetChild(0).GetChild(0).GetComponent<SpriteMeshInstance>().sortingOrder = 10;
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
            case AnimState.AnimType.SORDATTACK_RIGHT:
                //剣
                gameObject.transform.GetChild(4).GetComponent<SpriteRenderer>().sortingOrder = 1;
                //頭
                gameObject.transform.GetChild(5).GetComponent<SpriteMeshAnimation>().frame = 1;
                gameObject.transform.GetChild(5).GetChild(0).GetChild(0).GetComponent<SpriteMeshInstance>().sortingOrder = 8;
                gameObject.transform.GetChild(5).GetChild(0).GetChild(1).GetComponent<SpriteMeshInstance>().sortingOrder = 8;
                gameObject.transform.GetChild(5).GetChild(0).GetChild(0).GetComponent<SpriteMeshAnimation>().frame = 1;
                gameObject.transform.GetChild(5).GetChild(0).GetChild(1).GetComponent<SpriteMeshAnimation>().frame = 1;
                //首モフ
                gameObject.transform.GetChild(6).GetComponent<SpriteMeshAnimation>().frame = 1;
                //LeftArm
                gameObject.transform.GetChild(7).GetChild(0).GetComponent<SpriteMeshInstance>().sortingOrder = 10;
                gameObject.transform.GetChild(7).GetChild(0).GetChild(0).GetComponent<SpriteMeshInstance>().sortingOrder = 9;
                gameObject.transform.GetChild(7).GetChild(0).GetComponent<SpriteMeshAnimation>().frame = 1;
                gameObject.transform.GetChild(7).GetChild(0).GetChild(0).GetComponent<SpriteMeshAnimation>().frame = 1;
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
            case AnimState.AnimType.LADDER_DOWN:
                //剣
                gameObject.transform.GetChild(4).GetComponent<SpriteRenderer>().sortingOrder = 0;
                //頭
                gameObject.transform.GetChild(5).GetComponent<SpriteMeshInstance>().sortingOrder = 5;
                gameObject.transform.GetChild(5).GetComponent<SpriteMeshAnimation>().frame = 2;
                gameObject.transform.GetChild(5).GetChild(0).GetChild(0).GetComponent<SpriteMeshInstance>().sortingOrder = 1;
                gameObject.transform.GetChild(5).GetChild(0).GetChild(1).GetComponent<SpriteMeshInstance>().sortingOrder = 1;
                gameObject.transform.GetChild(5).GetChild(0).GetChild(0).GetComponent<SpriteMeshAnimation>().frame = 2;
                gameObject.transform.GetChild(5).GetChild(0).GetChild(1).GetComponent<SpriteMeshAnimation>().frame = 2;
                //首モフ
                gameObject.transform.GetChild(6).GetComponent<SpriteMeshAnimation>().frame = 5;
                //LeftArm
                gameObject.transform.GetChild(7).GetChild(0).GetComponent<SpriteMeshInstance>().sortingOrder = 4;
                gameObject.transform.GetChild(7).GetChild(0).GetChild(0).GetComponent<SpriteMeshInstance>().sortingOrder = 3;
                gameObject.transform.GetChild(7).GetChild(0).GetComponent<SpriteMeshAnimation>().frame = 2;
                gameObject.transform.GetChild(7).GetChild(0).GetChild(0).GetComponent<SpriteMeshAnimation>().frame = 2;
                //Stomach
                gameObject.transform.GetChild(8).GetComponent<SpriteMeshInstance>().sortingOrder = 3;
                gameObject.transform.GetChild(8).GetComponent<SpriteMeshAnimation>().frame = 2;
                //RightArm
                gameObject.transform.GetChild(9).GetChild(0).GetComponent<SpriteMeshInstance>().sortingOrder = 4;
                gameObject.transform.GetChild(9).GetChild(0).GetChild(0).GetComponent<SpriteMeshInstance>().sortingOrder = 3;
                gameObject.transform.GetChild(9).GetChild(0).GetComponent<SpriteMeshAnimation>().frame = 2;
                gameObject.transform.GetChild(9).GetChild(0).GetChild(0).GetComponent<SpriteMeshAnimation>().frame = 2;
                //Waist
                gameObject.transform.GetChild(10).GetComponent<SpriteMeshAnimation>().frame = 2;
                //LeftMant
                gameObject.transform.GetChild(11).GetComponent<SpriteMeshInstance>().sortingOrder = 10;
                gameObject.transform.GetChild(11).GetComponent<SpriteMeshAnimation>().frame = 2;
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
                gameObject.transform.GetChild(15).GetComponent<SpriteMeshInstance>().sortingOrder = 2;
                gameObject.transform.GetChild(15).GetComponent<SpriteMeshAnimation>().frame = 2;
                //RightLeg
                gameObject.transform.GetChild(16).GetComponent<SpriteMeshInstance>().sortingOrder = 2;
                gameObject.transform.GetChild(16).GetComponent<SpriteMeshAnimation>().frame = 2;

                break;
        }
        _attackStart = false;
    }

    /// <summary>
    /// 攻撃アニメーションの時に一瞬描画順を変える処理
    /// </summary>
    /// <param name="animtype"></param>
    public void AttackAnimOrderInLayer(AnimState.AnimType animtype)
    {
        if (_attackStart)
            return;

        switch (animtype)
        {
            case AnimState.AnimType.SORDATTACK_LEFT:
                //RightArm
                gameObject.transform.GetChild(9).GetChild(0).GetComponent<SpriteMeshInstance>().sortingOrder = 10;
                gameObject.transform.GetChild(9).GetChild(0).GetChild(0).GetComponent<SpriteMeshInstance>().sortingOrder = 10;
                gameObject.transform.GetChild(9).GetChild(0).GetComponent<SpriteMeshAnimation>().frame = 0;
                gameObject.transform.GetChild(9).GetChild(0).GetChild(0).GetComponent<SpriteMeshAnimation>().frame = 0;
                break;

            case AnimState.AnimType.SORDATTACK_RIGHT:
                //剣
                gameObject.transform.GetChild(4).GetComponent<SpriteRenderer>().sortingOrder = 10;
                //LeftArm
                gameObject.transform.GetChild(7).GetChild(0).GetComponent<SpriteMeshInstance>().sortingOrder = 10;
                gameObject.transform.GetChild(7).GetChild(0).GetChild(0).GetComponent<SpriteMeshInstance>().sortingOrder = 10;
                gameObject.transform.GetChild(7).GetChild(0).GetComponent<SpriteMeshAnimation>().frame = 1;
                gameObject.transform.GetChild(7).GetChild(0).GetChild(0).GetComponent<SpriteMeshAnimation>().frame = 1;
                break;

            default:
                break;
        }
        _attackStart = true;
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
                anim.SetBool("isSordAttackLeft", false);
                anim.SetBool("isSordAttackRight", false);
                anim.SetBool("isLadderUp", false);
                anim.SetBool("isLeftGetItem", false);
                anim.SetBool("isRightGetItem", false);
                pot_anim.SetBool("isIdle", true);
                break;

            case AnimState.AnimType.LEFTJUMP:
                animstate.animtype = AnimState.AnimType.LEFTJUMP;
                anim.SetBool("isRightJump", false);
                anim.SetBool("isLeftJump", true);
                anim.SetBool("isLeftWalk", false);
                anim.SetBool("isRightWalk", false);
                anim.SetBool("isLeftIdle", false);
                anim.SetBool("isRightIdle", false);
                anim.SetBool("isSordAttackLeft", false);
                anim.SetBool("isSordAttackRight", false);
                anim.SetBool("isLadderUp", false);
                anim.SetBool("isLeftGetItem", false);
                anim.SetBool("isRightGetItem", false);
                pot_anim.SetBool("isIdle", true);
                break;

            case AnimState.AnimType.LEFTIDLE:
                animstate.animtype = AnimState.AnimType.LEFTIDLE;
                anim.SetBool("isRightJump", false);
                anim.SetBool("isLeftJump", false);
                anim.SetBool("isLeftWalk", false);
                anim.SetBool("isRightWalk", false);
                anim.SetBool("isLeftIdle", true);
                anim.SetBool("isRightIdle", false);
                anim.SetBool("isSordAttackLeft", false);
                anim.SetBool("isSordAttackRight", false);
                anim.SetBool("isLadderUp", false);
                anim.SetBool("isLeftGetItem", false);
                anim.SetBool("isRightGetItem", false);
                pot_anim.SetBool("isIdle", true);
                break;

            case AnimState.AnimType.RIGHTIDLE:
                animstate.animtype = AnimState.AnimType.RIGHTIDLE;
                anim.SetBool("isRightJump", false);
                anim.SetBool("isLeftJump", false);
                anim.SetBool("isLeftWalk", false);
                anim.SetBool("isRightWalk", false);
                anim.SetBool("isLeftIdle", false);
                anim.SetBool("isRightIdle", true);
                anim.SetBool("isSordAttackLeft", false);
                anim.SetBool("isSordAttackRight", false);
                anim.SetBool("isLadderUp", false);
                anim.SetBool("isLeftGetItem", false);
                anim.SetBool("isRightGetItem", false);
                pot_anim.SetBool("isIdle", true);
                break;

            case AnimState.AnimType.LEFT_WALK:
                animstate.animtype = AnimState.AnimType.LEFT_WALK;
                anim.SetBool("isRightJump", false);
                anim.SetBool("isLeftJump", false);
                anim.SetBool("isLeftWalk", true);
                anim.SetBool("isRightWalk", false);
                anim.SetBool("isLeftIdle", false);
                anim.SetBool("isRightIdle", false);
                anim.SetBool("isSordAttackLeft", false);
                anim.SetBool("isSordAttackRight", false);
                anim.SetBool("isLadderUp", false);
                anim.SetBool("isLeftGetItem", false);
                anim.SetBool("isRightGetItem", false);
                pot_anim.SetBool("isMove", true);
                break;

            case AnimState.AnimType.RIGHT_WALK:
                animstate.animtype = AnimState.AnimType.RIGHT_WALK;
                anim.SetBool("isRightJump", false);
                anim.SetBool("isLeftJump", false);
                anim.SetBool("isLeftWalk", false);
                anim.SetBool("isRightWalk", true);
                anim.SetBool("isLeftIdle", false);
                anim.SetBool("isRightIdle", false);
                anim.SetBool("isSordAttackLeft", false);
                anim.SetBool("isSordAttackRight", false);
                anim.SetBool("isLadderUp", false);
                anim.SetBool("isLeftGetItem", false);
                anim.SetBool("isRightGetItem", false);
                pot_anim.SetBool("isMove", true);
                break;

            case AnimState.AnimType.SORDATTACK_LEFT:
                animstate.animtype = AnimState.AnimType.SORDATTACK_LEFT;
                anim.SetBool("isRightJump", false);
                anim.SetBool("isLeftJump", false);
                anim.SetBool("isLeftWalk", false);
                anim.SetBool("isRightWalk", false);
                anim.SetBool("isLeftIdle", false);
                anim.SetBool("isRightIdle", false);
                anim.SetBool("isSordAttackLeft", true);
                anim.SetBool("isSordAttackRight", false);
                anim.SetBool("isLadderUp", false);
                anim.SetBool("isLeftGetItem", false);
                anim.SetBool("isRightGetItem", false);
                pot_anim.SetBool("isIdle", true);
                break;

            case AnimState.AnimType.SORDATTACK_RIGHT:
                animstate.animtype = AnimState.AnimType.SORDATTACK_RIGHT;
                anim.SetBool("isRightJump", false);
                anim.SetBool("isLeftJump", false);
                anim.SetBool("isLeftWalk", false);
                anim.SetBool("isRightWalk", false);
                anim.SetBool("isLeftIdle", false);
                anim.SetBool("isRightIdle", false);
                anim.SetBool("isSordAttackLeft", false);
                anim.SetBool("isSordAttackRight", true);
                anim.SetBool("isLadderUp", false);
                anim.SetBool("isLeftGetItem", false);
                anim.SetBool("isRightGetItem", false);
                pot_anim.SetBool("isIdle", true);
                break;

            case AnimState.AnimType.LADDER_UP:
                animstate.animtype = AnimState.AnimType.LADDER_UP;
                anim.SetBool("isRightJump", false);
                anim.SetBool("isLeftJump", false);
                anim.SetBool("isLeftWalk", false);
                anim.SetBool("isRightWalk", false);
                anim.SetBool("isLeftIdle", false);
                anim.SetBool("isRightIdle", false);
                anim.SetBool("isSordAttackLeft", false);
                anim.SetBool("isSordAttackRight", false);
                anim.SetBool("isLadderUp", true);
                anim.SetBool("isLeftGetItem", false);
                anim.SetBool("isRightGetItem", false);
                pot_anim.SetBool("isIdle", true);
                break;

            case AnimState.AnimType.LADDER_DOWN:
                animstate.animtype = AnimState.AnimType.LADDER_DOWN;
                anim.SetBool("isRightJump", false);
                anim.SetBool("isLeftJump", false);
                anim.SetBool("isLeftWalk", false);
                anim.SetBool("isRightWalk", false);
                anim.SetBool("isLeftIdle", false);
                anim.SetBool("isRightIdle", false);
                anim.SetBool("isSordAttackLeft", false);
                anim.SetBool("isSordAttackRight", false);
                anim.SetBool("isLadderUp", false);
                anim.SetBool("isLeftGetItem", false);
                anim.SetBool("isRightGetItem", false);
                pot_anim.SetBool("isIdle", true);
                break;

            case AnimState.AnimType.LEFT_GETITEM:
                animstate.animtype = AnimState.AnimType.LEFT_GETITEM;
                anim.SetBool("isRightJump", false);
                anim.SetBool("isLeftJump", false);
                anim.SetBool("isLeftWalk", false);
                anim.SetBool("isRightWalk", false);
                anim.SetBool("isLeftIdle", false);
                anim.SetBool("isRightIdle", false);
                anim.SetBool("isSordAttackLeft", false);
                anim.SetBool("isSordAttackRight", false);
                anim.SetBool("isLadderUp", false);
                anim.SetBool("isLeftGetItem", true);
                anim.SetBool("isRightGetItem", false);
                pot_anim.SetBool("isIdle", true);
                StartCoroutine(GetItemEffectWaitTime());
                break;

            case AnimState.AnimType.RIGHT_GETITEM:
                animstate.animtype = AnimState.AnimType.RIGHT_GETITEM;
                anim.SetBool("isRightJump", false);
                anim.SetBool("isLeftJump", false);
                anim.SetBool("isLeftWalk", false);
                anim.SetBool("isRightWalk", false);
                anim.SetBool("isLeftIdle", false);
                anim.SetBool("isRightIdle", false);
                anim.SetBool("isSordAttackLeft", false);
                anim.SetBool("isSordAttackRight", false);
                anim.SetBool("isLadderUp", false);
                anim.SetBool("isLeftGetItem", false);
                anim.SetBool("isRightGetItem", true);
                pot_anim.SetBool("isIdle", true);
                StartCoroutine(GetItemEffectWaitTime());
                break;
        }
    }

    /// <summary>
    /// 剣を振りおろした時にダメージ処理
    /// </summary>
    public void AttackDamage()
    {
        attack_ctr.Attack();
    }

    /// <summary>
    /// 拾うアニメーション中にでるエフェクトと連動させるための処理
    /// </summary>
    /// <returns></returns>
    public IEnumerator GetItemEffectWaitTime()
    {
        pot_anim.SetBool("isPotDown", true);
        yield return new WaitForSeconds(0.35f);
        //アイテムのある位置にエフェクトの位置を合わせて呼ぶ
        pot_anim.SetBool("isPotDown", false);
        GameObject EffectObj = effect_mng.PlayEffect(0, Itemtarget.transform.position, 10, Itemtarget, false).gameObject;
        EffectObj.transform.DOScale(new Vector3(0, 0, 0), 0.4f);
        //Effectのスケールとアイテムのスケールをだんだん小さくしていく処理
        Itemtarget.transform.DOScale(new Vector3(0, 0, 0), 0.4f);
        yield return new WaitForSeconds(0.4f);
        EffectObj.SetActive(false);
        Itemtarget.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        //ツボの上に移動させてツボにはいってるように移動させる
        Itemtarget.transform.position = new Vector2(pot_anim.transform.position.x, pot_anim.transform.position.y + 3f);
        EffectObj.transform.position = Itemtarget.transform.position;
        EffectObj.SetActive(true);
        Itemtarget.SetActive(true);
        EffectObj.transform.DOScale(new Vector3(6, 6, 6), 0.4f);
        Itemtarget.transform.DOScale(new Vector3(1, 1, 1), 0.4f);
        yield return new WaitForSeconds(0.4f);
        EffectObj.transform.DOScale(new Vector3(0, 0, 0), 0.4f);
        yield return new WaitForSeconds(0.4f);
        //ツボの中に入る瞬間にどんどん小さくなってく
        Itemtarget.transform.DOScale(new Vector3(0, 0, 0), 0.4f);
        Itemtarget.transform.DOMoveY(pot_anim.transform.position.y + 1, 0.4f);
        PotAnimSetBool();
        yield return new WaitForSeconds(1f);
        //エフェクトとアイテムのオブジェクトはもう使わないので削除、フラグをfalseにする
        Destroy(EffectObj.gameObject);
        Destroy(Itemtarget.gameObject);
        PotAnimSetBool();
        anim.SetBool("isRightGetItem", false);
        anim.SetBool("isLeftGetItem", false);
        player_ctr.AllCommandActive = true;
    }


    /// <summary>
    /// PotアニメーションのisGetItemを変更
    /// </summary>
    private void PotAnimSetBool()
    {
        if (!pot_anim.GetBool("isGetItem"))
        {
            pot_anim.SetBool("isGetItem", true);
        }
        else
        {
            pot_anim.SetBool("isGetItem", false);
        }
    }
}
