﻿using UnityEngine;
using Anima2D;
using System.Collections;
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
            SWORDATTACK_LEFT,
            SWORDATTACK_RIGHT,
            LADDER_UP,
            LADDER_DOWN,
            LEFT_GETITEM,
            RIGHT_GETITEM,
            LEFT_SUFFERDAMAGE,
            RIGHT_SUFFERDAMAGE,
            RIGHT_VAJURA,
            LEFT_VAJURA,
            RIGHTBRINGPOT,
            LEFTBRINGPOT,
        }
        public AnimType animtype;
    }

    public AnimState animstate;

    private Animator anim;
    private AttackZoneController attack_ctr;
    private Animator pot_anim;
    private EffectManager effect_mng;
    private PlayerController player_ctr;
    private PlayerManager player_mng;
    private MoveController move_ctr;

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
        effect_mng = GameObject.Find("EffectManager").GetComponent<EffectManager>();
        move_ctr = gameObject.GetComponentInChildren<MoveController>();
        player_mng = GameObject.Find("PlayerStatus").GetComponent<PlayerManager>();
        _attackStart = false;
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
            case AnimState.AnimType.SWORDATTACK_LEFT:
            case AnimState.AnimType.LEFT_SUFFERDAMAGE:
            case AnimState.AnimType.LEFT_VAJURA:
            case AnimState.AnimType.LEFTBRINGPOT:
                //剣
                gameObject.transform.GetChild(4).GetComponent<SpriteRenderer>().sortingOrder = 10;
                //頭
                gameObject.transform.GetChild(5).GetComponent<SpriteMeshInstance>().sortingOrder = 6;
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
            case AnimState.AnimType.SWORDATTACK_RIGHT:
            case AnimState.AnimType.RIGHT_SUFFERDAMAGE:
            case AnimState.AnimType.RIGHT_VAJURA:
            case AnimState.AnimType.RIGHTBRINGPOT:
                //剣
                gameObject.transform.GetChild(4).GetComponent<SpriteRenderer>().sortingOrder = 1;
                //頭
                gameObject.transform.GetChild(5).GetComponent<SpriteMeshInstance>().sortingOrder = 6;
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
            case AnimState.AnimType.SWORDATTACK_LEFT:
                //RightArm
                gameObject.transform.GetChild(9).GetChild(0).GetComponent<SpriteMeshInstance>().sortingOrder = 10;
                gameObject.transform.GetChild(9).GetChild(0).GetChild(0).GetComponent<SpriteMeshInstance>().sortingOrder = 10;
                gameObject.transform.GetChild(9).GetChild(0).GetComponent<SpriteMeshAnimation>().frame = 0;
                gameObject.transform.GetChild(9).GetChild(0).GetChild(0).GetComponent<SpriteMeshAnimation>().frame = 0;
                break;

            case AnimState.AnimType.SWORDATTACK_RIGHT:
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
                anim.SetBool("isLeftSufferDamage", false);
                anim.SetBool("isRightSufferDamage", false);
                anim.SetBool("isRightVajura", false);
                anim.SetBool("isLeftVajura", false);
                anim.SetBool("isLeftBringPot", false);
                anim.SetBool("isRightBringPot", false);
                pot_anim.SetBool("isIdle", true);
                pot_anim.SetBool("isRightMove", false);
                pot_anim.SetBool("isLeftMove", false);
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
                anim.SetBool("isLeftSufferDamage", false);
                anim.SetBool("isRightSufferDamage", false);
                anim.SetBool("isRightVajura", false);
                anim.SetBool("isLeftVajura", false);
                anim.SetBool("isLeftBringPot", false);
                anim.SetBool("isRightBringPot", false);
                pot_anim.SetBool("isIdle", true);
                pot_anim.SetBool("isRightMove", false);
                pot_anim.SetBool("isLeftMove", false);
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
                anim.SetBool("isLeftSufferDamage", false);
                anim.SetBool("isRightSufferDamage", false);
                anim.SetBool("isRightVajura", false);
                anim.SetBool("isLeftVajura", false);
                anim.SetBool("isLeftBringPot", false);
                anim.SetBool("isRightBringPot", false);
                pot_anim.SetBool("isIdle", true);
                pot_anim.SetBool("isRightMove", false);
                pot_anim.SetBool("isLeftMove", false);
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
                anim.SetBool("isLeftSufferDamage", false);
                anim.SetBool("isRightSufferDamage", false);
                anim.SetBool("isRightVajura", false);
                anim.SetBool("isLeftVajura", false);
                anim.SetBool("isLeftBringPot", false);
                anim.SetBool("isRightBringPot", false);
                pot_anim.SetBool("isIdle", true);
                pot_anim.SetBool("isRightMove", false);
                pot_anim.SetBool("isLeftMove", false);
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
                anim.SetBool("isLeftSufferDamage", false);
                anim.SetBool("isRightSufferDamage", false);
                anim.SetBool("isRightVajura", false);
                anim.SetBool("isLeftVajura", false);
                anim.SetBool("isLeftBringPot", false);
                anim.SetBool("isRightBringPot", false);
                pot_anim.SetBool("isIdle", false);
                pot_anim.SetBool("isRightMove", false);
                pot_anim.SetBool("isLeftMove", true);
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
                anim.SetBool("isLeftSufferDamage", false);
                anim.SetBool("isRightSufferDamage", false);
                anim.SetBool("isRightVajura", false);
                anim.SetBool("isLeftVajura", false);
                anim.SetBool("isLeftBringPot", false);
                anim.SetBool("isRightBringPot", false);
                pot_anim.SetBool("isIdle", false);
                pot_anim.SetBool("isRightMove", true);
                pot_anim.SetBool("isLeftMove", false);
                break;

            case AnimState.AnimType.SWORDATTACK_LEFT:
                animstate.animtype = AnimState.AnimType.SWORDATTACK_LEFT;
                anim.SetBool("isRightJump", false);
                anim.SetBool("isLeftJump", false);
                anim.SetBool("isLeftWalk", false);
                anim.SetBool("isRightWalk", false);
                anim.SetBool("isLeftIdle", false);
                anim.SetBool("isRightIdle", false);
                anim.SetBool("isSordAttackRight", false);
                anim.SetBool("isSordAttackLeft", true);
                anim.SetBool("isLadderUp", false);
                anim.SetBool("isLeftGetItem", false);
                anim.SetBool("isRightGetItem", false);
                anim.SetBool("isLeftSufferDamage", false);
                anim.SetBool("isRightSufferDamage", false);
                anim.SetBool("isRightVajura", false);
                anim.SetBool("isLeftVajura", false);
                anim.SetBool("isLeftBringPot", false);
                anim.SetBool("isRightBringPot", false);
                pot_anim.SetBool("isIdle", true);
                pot_anim.SetBool("isRightMove", false);
                pot_anim.SetBool("isLeftMove", false);
                break;

            case AnimState.AnimType.SWORDATTACK_RIGHT:
                animstate.animtype = AnimState.AnimType.SWORDATTACK_RIGHT;
                anim.SetBool("isRightJump", false);
                anim.SetBool("isLeftJump", false);
                anim.SetBool("isLeftWalk", false);
                anim.SetBool("isRightWalk", false);
                anim.SetBool("isLeftIdle", false);
                anim.SetBool("isRightIdle", false);
                anim.SetBool("isSordAttackRight", true);
                anim.SetBool("isSordAttackLeft", false);
                anim.SetBool("isLadderUp", false);
                anim.SetBool("isLeftGetItem", false);
                anim.SetBool("isRightGetItem", false);
                anim.SetBool("isLeftSufferDamage", false);
                anim.SetBool("isRightSufferDamage", false);
                anim.SetBool("isRightVajura", false);
                anim.SetBool("isLeftVajura", false);
                anim.SetBool("isLeftBringPot", false);
                anim.SetBool("isRightBringPot", false);
                pot_anim.SetBool("isIdle", true);
                pot_anim.SetBool("isRightMove", false);
                pot_anim.SetBool("isLeftMove", false);
                break;

            case AnimState.AnimType.LEFT_VAJURA:
                animstate.animtype = AnimState.AnimType.LEFT_VAJURA;
                anim.SetBool("isRightJump", false);
                anim.SetBool("isLeftJump", false);
                anim.SetBool("isLeftWalk", false);
                anim.SetBool("isRightWalk", false);
                anim.SetBool("isLeftIdle", false);
                anim.SetBool("isRightIdle", false);
                anim.SetBool("isSordAttackRight", false);
                anim.SetBool("isSordAttackLeft", false);
                anim.SetBool("isLadderUp", false);
                anim.SetBool("isLeftGetItem", false);
                anim.SetBool("isRightGetItem", false);
                anim.SetBool("isLeftSufferDamage", false);
                anim.SetBool("isRightSufferDamage", false);
                anim.SetBool("isRightVajura", false);
                anim.SetBool("isLeftVajura", true);
                anim.SetBool("isLeftBringPot", false);
                anim.SetBool("isRightBringPot", false);
                pot_anim.SetBool("isIdle", true);
                pot_anim.SetBool("isRightMove", false);
                pot_anim.SetBool("isLeftMove", false);
                break;

            case AnimState.AnimType.RIGHT_VAJURA:
                animstate.animtype = AnimState.AnimType.RIGHT_VAJURA;
                anim.SetBool("isRightJump", false);
                anim.SetBool("isLeftJump", false);
                anim.SetBool("isLeftWalk", false);
                anim.SetBool("isRightWalk", false);
                anim.SetBool("isLeftIdle", false);
                anim.SetBool("isRightIdle", false);
                anim.SetBool("isSordAttackRight", false);
                anim.SetBool("isSordAttackLeft", false);
                anim.SetBool("isLadderUp", false);
                anim.SetBool("isLeftGetItem", false);
                anim.SetBool("isRightGetItem", false);
                anim.SetBool("isLeftSufferDamage", false);
                anim.SetBool("isRightSufferDamage", false);
                anim.SetBool("isRightVajura", true);
                anim.SetBool("isLeftVajura", false);
                anim.SetBool("isLeftBringPot", false);
                anim.SetBool("isRightBringPot", false);
                pot_anim.SetBool("isIdle", true);
                pot_anim.SetBool("isRightMove", false);
                pot_anim.SetBool("isLeftMove", false);
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
                anim.SetBool("isLeftSufferDamage", false);
                anim.SetBool("isRightSufferDamage", false);
                anim.SetBool("isRightVajura", false);
                anim.SetBool("isLeftVajura", false);
                anim.SetBool("isLeftBringPot", false);
                anim.SetBool("isRightBringPot", false);
                pot_anim.SetBool("isIdle", true);
                pot_anim.SetBool("isRightMove", false);
                pot_anim.SetBool("isLeftMove", false);
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
                anim.SetBool("isLeftSufferDamage", false);
                anim.SetBool("isRightSufferDamage", false);
                anim.SetBool("isRightVajura", false);
                anim.SetBool("isLeftVajura", false);
                anim.SetBool("isLeftBringPot", false);
                anim.SetBool("isRightBringPot", false);
                pot_anim.SetBool("isIdle", true);
                pot_anim.SetBool("isRightMove", false);
                pot_anim.SetBool("isLeftMove", false);
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
                anim.SetBool("isLeftSufferDamage", false);
                anim.SetBool("isRightSufferDamage", false);
                anim.SetBool("isRightVajura", false);
                anim.SetBool("isLeftVajura", false);
                anim.SetBool("isLeftBringPot", false);
                anim.SetBool("isRightBringPot", false);
                pot_anim.SetBool("isIdle", false);
                pot_anim.SetBool("isRightMove", false);
                pot_anim.SetBool("isLeftMove", false);
                pot_anim.SetBool("isPotDown", true);
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
                anim.SetBool("isLeftSufferDamage", false);
                anim.SetBool("isRightSufferDamage", false);
                anim.SetBool("isRightVajura", false);
                anim.SetBool("isLeftVajura", false);
                anim.SetBool("isLeftBringPot", false);
                anim.SetBool("isRightBringPot", false);
                pot_anim.SetBool("isIdle", false);
                pot_anim.SetBool("isRightMove", false);
                pot_anim.SetBool("isLeftMove", false);
                pot_anim.SetBool("isPotDown", true);
                StartCoroutine(GetItemEffectWaitTime());
                break;

            case AnimState.AnimType.LEFT_SUFFERDAMAGE:
                animstate.animtype = AnimState.AnimType.LEFT_SUFFERDAMAGE;
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
                anim.SetBool("isLeftSufferDamage", true);
                anim.SetBool("isRightSufferDamage", false);
                anim.SetBool("isRightVajura", false);
                anim.SetBool("isLeftVajura", false);
                anim.SetBool("isLeftBringPot", false);
                anim.SetBool("isRightBringPot", false);
                pot_anim.SetBool("isIdle", false);
                pot_anim.SetBool("isRightMove", false);
                pot_anim.SetBool("isLeftMove", false);
                pot_anim.SetBool("isPotDown", false);
                break;

            case AnimState.AnimType.RIGHT_SUFFERDAMAGE:
                animstate.animtype = AnimState.AnimType.RIGHT_SUFFERDAMAGE;
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
                anim.SetBool("isLeftSufferDamage", false);
                anim.SetBool("isRightSufferDamage", true);
                anim.SetBool("isRightVajura", false);
                anim.SetBool("isLeftVajura", false);
                anim.SetBool("isLeftBringPot", false);
                anim.SetBool("isRightBringPot", false);
                pot_anim.SetBool("isIdle", false);
                pot_anim.SetBool("isRightMove", false);
                pot_anim.SetBool("isLeftMove", false);
                pot_anim.SetBool("isPotDown", false);
                break;

            case AnimState.AnimType.LEFTBRINGPOT:
                animstate.animtype = AnimState.AnimType.LEFTBRINGPOT;
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
                anim.SetBool("isLeftSufferDamage", false);
                anim.SetBool("isRightSufferDamage", false);
                anim.SetBool("isRightVajura", false);
                anim.SetBool("isLeftVajura", false);
                anim.SetBool("isLeftBringPot", true);
                anim.SetBool("isRightBringPot", false);
                pot_anim.SetBool("isIdle", false);
                pot_anim.SetBool("isRightMove", false);
                pot_anim.SetBool("isLeftMove", false);
                pot_anim.SetBool("isPotDown", true);
                break;

            case AnimState.AnimType.RIGHTBRINGPOT:
                animstate.animtype = AnimState.AnimType.RIGHTBRINGPOT;
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
                anim.SetBool("isLeftSufferDamage", false);
                anim.SetBool("isRightSufferDamage", false);
                anim.SetBool("isRightVajura", false);
                anim.SetBool("isLeftVajura", false);
                anim.SetBool("isLeftBringPot", false);
                anim.SetBool("isRightBringPot", true);
                pot_anim.SetBool("isIdle", false);
                pot_anim.SetBool("isRightMove", false);
                pot_anim.SetBool("isLeftMove", false);
                pot_anim.SetBool("isPotDown", true);
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
        yield return new WaitForSeconds(0.35f);
        //アイテムのある位置にエフェクトの位置を合わせて呼ぶ
        GameObject EffectObj = effect_mng.PlayEffect((int)EffectManager.EffectName.Effect_GetItem, Itemtarget.transform.position, 10, Itemtarget, false).gameObject;
        EffectObj.transform.DOScale(new Vector3(0, 0, 0), 0.4f);
        //Effectのスケールとアイテムのスケールをだんだん小さくしていく処理
        Itemtarget.transform.DOScale(new Vector3(0, 0, 0), 0.4f);
        yield return new WaitForSeconds(0.4f);
        EffectObj.SetActive(false);
        Itemtarget.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        //ツボの上に移動させてツボにはいってるように移動させる
        PotOnMoveAnim(EffectObj);
        yield return new WaitForSeconds(0.4f);
        EffectObj.transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        yield return new WaitForSeconds(0.4f);
        //ツボの中に入る瞬間にどんどん小さくなってく
        Itemtarget.transform.DOScale(new Vector3(0, 0, 0), 0.4f);
        Itemtarget.transform.DOMoveY(pot_anim.transform.position.y + 0.5f, 0.4f);
        PotAnimSetBool();
        yield return new WaitForSeconds(1.2f);
        //エフェクトとアイテムのオブジェクトはもう使わないので削除、フラグをfalseにする
        GetItemEndAnim(EffectObj);
        player_ctr.EventFlag = false;
        pot_anim.SetBool("isPotDown", false);
    }

    /// <summary>
    /// PotアニメーションのisGetItemを変更
    /// </summary>
    private void PotAnimSetBool()
    {
        if (!pot_anim.GetBool("isGetItem"))
            pot_anim.SetBool("isGetItem", true);
        else
            pot_anim.SetBool("isGetItem", false);
    }

    /// <summary>
    /// アイテムを拾うアニメーション中にツボの上に移動する処理
    /// </summary>
    /// <param name="effect"></param>
    private void PotOnMoveAnim(GameObject effect)
    {
        Itemtarget.transform.position = new Vector3(pot_anim.transform.position.x, pot_anim.transform.position.y + 2.5f, Itemtarget.transform.position.z);
        effect.transform.position = Itemtarget.transform.position;
        effect.SetActive(true);
        Itemtarget.SetActive(true);
        effect.transform.DOScale(new Vector3(7, 7, 7), 0.3f);
        Itemtarget.transform.DOScale(new Vector3(1, 1, 1), 0.4f);
    }

    /// <summary>
    /// 拾うアニメーションが終わった時にする処理
    /// </summary>
    private void GetItemEndAnim(GameObject effect)
    {
        Destroy(effect.gameObject);
        Destroy(Itemtarget.transform.parent.gameObject);
        player_ctr.pickUpFlag = false;
        PotAnimSetBool();
        anim.SetBool("isRightGetItem", false);
        anim.SetBool("isLeftGetItem", false);
        if (move_ctr.direc == MoveController.Direction.LEFT)
            anim.SetBool("isLeftIdle", true);
        else
            anim.SetBool("isRightIdle", true);
    }

    /// <summary>
    /// どこをおしても動かないようにする
    /// </summary>
    public void CommandStop()
    {
        if (player_ctr.AllCommandActive)
            player_ctr.AllCommandActive = false;
        else
            player_ctr.AllCommandActive = true;
    }

    /// <summary>
    /// SEを鳴らす処理
    /// </summary>
    public void ChoiceSe(SoundManager.SENAME se_name)
    {
        switch (se_name)
        {
            case SoundManager.SENAME.SE_FOOTSTEPS:
                SoundManager.Instance.PlaySe((int)SoundManager.SENAME.SE_FOOTSTEPS, 0.2f);
                break;
            case SoundManager.SENAME.SE_JAMP:
                SoundManager.Instance.PlaySe((int)SoundManager.SENAME.SE_JAMP);
                break;
            case SoundManager.SENAME.SE_THUNDER:
                SoundManager.Instance.PlaySe((int)SoundManager.SENAME.SE_THUNDER);
                break;
            default:
                Debug.Log("セットされてません");
                break;
        }
    }

    /// <summary>
    /// 剣を振るSEの処理
    /// </summary>
    public void SwordSE()
    {
        //アックス、槍、たいまつは重い剣のSE
        if (player_mng.GetSwordType == PlayerStatus.SWORDTYPE.AXE || player_mng.GetSwordType == PlayerStatus.SWORDTYPE.TORCH)
        {
            SoundManager.Instance.PlaySe((int)SoundManager.SENAME.SE_SWORDHEAVY);
        }
        else if(player_mng.GetSwordType == PlayerStatus.SWORDTYPE.VAJURA)
        {
            SoundManager.Instance.PlaySe((int)SoundManager.SENAME.SE_THUNDER);
        }
        else
        {
            SoundManager.Instance.PlaySe((int)SoundManager.SENAME.SE_SWORD);
        }
    }

    /// <summary>
    /// ツボの顔変える処理
    /// </summary>
    public void ChagePotFace(PotStatus.PotFace face)
    {
        pot_anim.gameObject.GetComponent<PotController>().ChangePotFace(face);
    }
}