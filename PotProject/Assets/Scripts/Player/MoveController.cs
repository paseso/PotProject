﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//優先度アップ
[DefaultExecutionOrder(-1)]
public class MoveController : MonoBehaviour
{
    //[HideInInspector]
    public float speed = 0f;

    private bool _jumping = false;

    public bool IsLadder{ get; set; }

    public bool keyDoorFlag { get; set; }

    [SerializeField]
    private float ladderSpeed;

    private Rigidbody2D rig;
    //はしご中かどうか
    public bool _laddernow { get; set; }

    // はしご内にいるカウント
    public int InLadderCount { get; set; }

    // switchオブジェクト
    public GameObject switchGimmick { get; set; }

    public bool ladderDownFlag { get; set; }

    //左右動かしてもいいかどうか
    [HideInInspector]
    public bool _ActiveRightLeft = false;
    //アイテムを落としたかどうか
    [HideInInspector]
    public bool _itemFall = false;
    //モンスターに当たったかどうか
    [HideInInspector]
    public bool _hitmonster = false;

    public enum PlayerState {
        Action,
        Pause,
        MiniMap,
        MoveMap,
        Clear,
    }

    public PlayerState playerState;

    //-------アクションボタンを押してるかどうか----------
    private bool _onCrossCross = false;
    private bool _onRight = false;
    private bool _onLeft = false;
    private bool _onUp = false;
    private bool _onDown = false;
    private bool _onRJoystickRight = false;
    private bool _onRJoystickLeft = false;
    private bool _onRJoystickUp = false;
    private bool _onRJoystickDown = false;
    private bool _onSquare = false;
    private bool _onCircle = false;
    private bool _onCrossUp = false;
    private bool _onCrossDown = false;
    private bool _onCrossRight = false;
    private bool _onCrossLeft = false;
    private bool _onR2 = false;
    //---------------------------------------------

    [HideInInspector]
    public GameObject target;
    private List<Sprite> BrotherSprites;
    private GameObject PotObject;

    private PlayerController player_ctr;
    private BringCollider bringctr;
    private AlchemyUIController alchemyUI_ctr;
    private AnimController anim_ctr;
    private PlayerManager pManager;
    private MiniMapController miniMap_ctr;
    private LegCollider leg_col;
    private CrossAxisDown crossAxisDown;

    #region ボタンFlagのget

    //----------ボタンFlagのget---------------------
    public bool Jumping
    {
        get { return _jumping; }
    }

    public bool setJumping
    {
        set { _jumping = value; }
    }

    public bool CrossCross
    {
        get { return _onCrossCross; }
    }

    public bool OnRight
    {
        get { return _onRight; }
    }

    public bool OnLeft
    {
        get { return _onLeft; }
    }

    public bool OnUp
    {
        get { return _onUp; }
    }

    public bool OnDown
    {
        get { return _onDown; }
    }

    public bool OnRJoystickRight
    {
        get { return _onRJoystickRight; }
    }

    public bool OnRJoystickLeft
    {
        get { return _onRJoystickLeft; }
    }

    public bool OnRJoystickUp
    {
        get { return _onRJoystickUp; }
    }

    public bool OnRJoystickDown
    {
        get { return _onRJoystickDown; }
    }

    public bool OnSquere
    {
        get { return _onSquare; }
    }

    public bool OnCircle
    {
        get { return _onCircle; }
    }

    public bool OnCrossUp
    {
        get { return _onCrossUp; }
    }

    public bool OnCrossDown
    {
        get { return _onCrossDown; }
    }

    public bool OnCrossRight
    {
        get { return _onCrossRight; }
    }

    public bool OnCrossLeft
    {
        get { return _onCrossLeft; }
    }

    public bool OnR2
    {
        get { return _onR2; }
    }
    //------------------------------------------

    #endregion

    private enum ButtonType
    {
        JUMP = 0,
        LEFTJOYSTICK_RIGHT,
        LEFTJOYSTICK_LEFT,
        LEFTJOYSTICK_UP,
        LEFTJOYSTICK_DOWN,
        RIGHTJOYSTICK_RIGHT,
        RIGHTJOYSTICK_LEFT,
        RIGHTJOYSTICK_UP,
        RIGHTJOYSTICK_DOWN,
        CIRCLE,
        SQUARE,
        TRIANGLE,
        R1,
        L2,
        R2,
        OPTION,
        CROSSX_RIGTH,
        CROSSX_LEFT,
        CROSSY_UP,
        CROSSY_DOWN,
    };

    public enum LadderDirection
    {
        NONE,
        UP,
        DOWN,
    }

    public LadderDirection ladderDir;

    //プレイヤーの今向いてる方向
    public enum Direction
    {
        RIGHT = 0,
        LEFT
    }
    public Direction direc;

    private void Awake()
    {
        target = null;
        _itemFall = false;
        _ActiveRightLeft = true;
        _jumping = false;
        _hitmonster = false;
        _laddernow = false;
        ClearBtnFlg();
    }

    // Use this for initialization
    void Start()
    {
        direc = Direction.LEFT;
        pManager = GameObject.Find("PlayerStatus").GetComponent<PlayerManager>();
        PotObject = FindObjectOfType<PotController>().gameObject;
        rig = gameObject.transform.parent.GetComponent<Rigidbody2D>();
        player_ctr = GameObject.Find("Controller").GetComponent<PlayerController>();
        bringctr = gameObject.transform.parent.GetChild(0).GetComponent<BringCollider>();
        alchemyUI_ctr = GameObject.Find("Canvas/Alchemy_UI").GetComponent<AlchemyUIController>();
        miniMap_ctr = GameObject.Find("Canvas/MiniMap").GetComponent<MiniMapController>();
        anim_ctr = gameObject.transform.parent.GetComponent<AnimController>();
        leg_col = gameObject.transform.parent.GetComponentInChildren<LegCollider>();
        crossAxisDown = gameObject.GetComponent<CrossAxisDown>();
    }

    // Update is called once per frame
    void Update()
    {
        //はしご処理してる時、ツボのtransformをプレイヤーと同じ位置にする
        if (_laddernow)
        {
            PotObject.transform.DOMove(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), 0.5f).SetEase(Ease.Linear);
        }

        ClearBtnFlg();
        EventStateCheck();

    }

    /// <summary>
    /// 透明にしてから元に戻す処理
    /// </summary>
    /// <returns></returns>
    private IEnumerator TransWaitTime()
    {
        _hitmonster = true;
        player_ctr.PlayerAlphaDown();
        yield return new WaitForSeconds(1f);
        player_ctr.PlayerAlphaMax();
        _hitmonster = false;
    }

    /// <summary>
    /// 今のEventStateをチェックして各処理に移る
    /// </summary>
    private void EventStateCheck()
    {
        switch (pManager.Status.event_state)
        {
            case PlayerStatus.EventState.NORMAL:
                BtnCheck();
                break;

            case PlayerStatus.EventState.CAMERA:
                break;

            case PlayerStatus.EventState.ALCHEMYUI:
                UIControll();
                break;
        }
    }

    /// <summary>
    /// 錬金UIを開いている時の制御
    /// </summary>
    private void UIControll()
    {
        //leg_col.isLanding = false;
        _ActiveRightLeft = false;
        if(Input.GetAxis("CrossY") <= -0.15f || Input.GetAxis("CrossY") >= 0.15f ||
            Input.GetAxis("CrossX") <= -0.15f || Input.GetAxis("CrossX") >= 0.15f ||
            Input.GetButtonDown("Circle") || Input.GetKeyDown(KeyCode.E) ||
            Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.C) ||
            Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.V))
        {
            BtnCheck();
        }
    }

    /// <summary>
    /// 移動フラグを全部falseにする処理
    /// </summary>
    private void ClearBtnFlg()
    {
        _onCrossCross = false;
        _onUp = false;
        _onDown = false;
        _onRight = false;
        _onLeft = false;
        _onRJoystickLeft = false;
        _onRJoystickRight = false;
        _onRJoystickUp = false;
        _onRJoystickDown = false;
        _onSquare = false;
        _onCircle = false;
        _onCrossUp = false;
        _onCrossDown = false;
        _onCrossRight = false;
        _onCrossLeft = false;
        _onR2 = false;
    }

    #region Input内処理

    /// <summary>
    /// ボタンが押された時の処理
    /// </summary>
    private void Move(ButtonType btn)
    {
        switch (btn)
        {
            case ButtonType.JUMP:
                _onCrossCross = true;
                if (crossAxisDown.getKeepDown)
                    return;
                //アイテムを捨てる処理
                if (alchemyUI_ctr.ChooseWindow)
                {
                    alchemyUI_ctr.ChooseThrow(false);
                    return;
                }
                if (player_ctr.GetAlchemyUIFlag)
                {
                    alchemyUI_ctr.ActiveThrowItemUI();
                    return;
                }
                if (!leg_col.isLanding)
                    return;

                if (player_ctr.GetAlchemyUIFlag) { return; }

                //ジャンプの方向別アニメーション
                if (direc == Direction.RIGHT)
                    anim_ctr.ChangeAnimatorState(AnimController.AnimState.AnimType.RIGHTJUMP);
                else
                    anim_ctr.ChangeAnimatorState(AnimController.AnimState.AnimType.LEFTJUMP);

                rig.velocity = new Vector2(5, 1f * speed);
                //弟のジャンプ処理
                PotObject.GetComponent<PotController>().JumpPot();
                _jumping = true;
                break;

            case ButtonType.LEFTJOYSTICK_LEFT:
                if (direc != Direction.LEFT && Jumping)
                    return;
                else if (direc == Direction.LEFT && Jumping) { }
                //右ジョイスティックとジャンプ同時押し

                direc = Direction.LEFT;
                if (!_ActiveRightLeft)
                    return;

                if (player_ctr.GetAlchemyUIFlag) { return; }

                if (leg_col.isLanding && !Jumping)
                {
                    anim_ctr.ChangeAnimatorState(AnimController.AnimState.AnimType.LEFT_WALK);
                }
                if (CheckMoveable())
                    rig.velocity = new Vector2(-5f, rig.velocity.y);
                //弟の移動処理
                PotObject.GetComponent<PotController>().LeftMove();
                break;

            case ButtonType.LEFTJOYSTICK_RIGHT:
                if (direc != Direction.RIGHT && Jumping)
                    return;
                else if (direc == Direction.RIGHT && Jumping) { }
                //右ジョイスティックとジャンプ同時押し

                direc = Direction.RIGHT;

                if (!_ActiveRightLeft)
                    return;
                if (player_ctr.GetAlchemyUIFlag) { return; }

                if (leg_col.isLanding && !Jumping)
                {
                    anim_ctr.ChangeAnimatorState(AnimController.AnimState.AnimType.RIGHT_WALK);
                }

                if (CheckMoveable())
                    rig.velocity = new Vector2(5f, rig.velocity.y);
                //弟の移動処理
                PotObject.GetComponent<PotController>().RightMove();
                break;

            case ButtonType.LEFTJOYSTICK_UP:
                _onUp = true;
                ladderDownFlag = false;

                if (player_ctr.GetAlchemyUIFlag) { return; }

                if (InLadderCount > 0) {
                    Ladder(ladderSpeed, 1);
                    anim_ctr.ChangeAnimatorState(AnimController.AnimState.AnimType.LADDER_UP);
                }
                
                break;

            case ButtonType.LEFTJOYSTICK_DOWN:
                _onDown = true;

                if (player_ctr.GetAlchemyUIFlag) { return; }
                if (ladderDownFlag) { return; }

                if (InLadderCount > 0) {
                    Ladder(ladderSpeed, -1);
                    anim_ctr.ChangeAnimatorState(AnimController.AnimState.AnimType.LADDER_UP);
                }
                break;

            case ButtonType.RIGHTJOYSTICK_LEFT:
                //錬金UIが閉じてたらreturn
                if (!player_ctr.GetAlchemyUIFlag)
                    return;
                _onRJoystickLeft = true;
                break;

            case ButtonType.RIGHTJOYSTICK_RIGHT:
                //錬金UIが閉じてたらreturn
                if (!player_ctr.GetAlchemyUIFlag)
                    return;
                _onRJoystickRight = true;
                break;

            case ButtonType.RIGHTJOYSTICK_UP:
                //錬金UIが閉じてたらreturn
                if (!player_ctr.GetAlchemyUIFlag)
                    return;
                _onRJoystickUp = true;
                break;

            case ButtonType.RIGHTJOYSTICK_DOWN:
                //錬金UIが閉じてたらreturn
                if (!player_ctr.GetAlchemyUIFlag)
                    return;
                _onRJoystickDown = true;
                break;

            case ButtonType.CIRCLE:
                _onCircle = true;
                //錬金UI中の捨てるかどうかのウィンドウが出ている時に〇を押した時の処理
                if (alchemyUI_ctr.ChooseWindow)
                {
                    alchemyUI_ctr.ChooseThrow(true);
                    return;
                }
                //錬金UIが出ている時に〇を押した時の処理
                if (player_ctr.GetAlchemyUIFlag)
                {
                    alchemyUI_ctr.PickItem();
                }
                else
                {
                    if(gameObject.layer == LayerMask.NameToLayer("LadderPlayer")) { return; }
                    if (direc == Direction.LEFT)
                    {
                        if (pManager.GetSwordType == PlayerStatus.SWORDTYPE.VAJURA)
                            anim_ctr.ChangeAnimatorState(AnimController.AnimState.AnimType.LEFT_VAJURA);
                        else
                            anim_ctr.ChangeAnimatorState(AnimController.AnimState.AnimType.SWORDATTACK_LEFT);
                    }
                    else
                    {
                        if (pManager.GetSwordType == PlayerStatus.SWORDTYPE.VAJURA)
                            anim_ctr.ChangeAnimatorState(AnimController.AnimState.AnimType.RIGHT_VAJURA);
                        else
                            anim_ctr.ChangeAnimatorState(AnimController.AnimState.AnimType.SWORDATTACK_RIGHT);
                    }
                }
                break;

            case ButtonType.SQUARE:
                _onSquare = true;
                //スイッチを踏む処理
                if(switchGimmick != null)
                {
                    if (direc == Direction.LEFT)
                        anim_ctr.ChangeAnimatorState(AnimController.AnimState.AnimType.LEFTIDLE);
                    else
                        anim_ctr.ChangeAnimatorState(AnimController.AnimState.AnimType.RIGHTIDLE);
                    rig.velocity = new Vector2(0, 0);
                    GimmickController gCon = switchGimmick.GetComponent<GimmickController>();
                    gCon.StartSride();
                    return;
                }
                //アイテムを拾う処理
                bringctr.SquereButton();
                player_ctr.pickUpFlag = true;
                target = null;
                break;

            case ButtonType.TRIANGLE:
                //錬金したアイテム使用
                if (player_ctr.getCreateItemList().Count <= 0)
                    return;
                player_ctr.UseAlchemyItem(alchemyUI_ctr.getNowAlchemyItem);
                break;

            case ButtonType.R1:
                //錬金したアイテムの切り替え
                if (player_ctr.getCreateItemList().Count <= 1)
                    return;
                alchemyUI_ctr.setNowAlchemyItem();
                break;

            case ButtonType.L2:
                //全体のマップを表示する処理
                miniMap_ctr.ActiveMiniMap();
                break;

            case ButtonType.R2:
                //錬金UIを開いたり閉じたりする処理
                if (_jumping) { return; }
                player_ctr.OpenAlchemy();
                break;

            case ButtonType.OPTION:
                //PouseManagerにOption処理が書いてある（Escapeと同じ処理）
                break;

            case ButtonType.CROSSX_RIGTH:
                //剣の切り替え
                if (!player_ctr.GetAlchemyUIFlag)
                {
                    player_ctr.SwordNumList(1);
                    return;
                }
                _onCrossRight = true;
                break;

            case ButtonType.CROSSX_LEFT:
                //剣の切り替え
                if (!player_ctr.GetAlchemyUIFlag)
                {
                    player_ctr.SwordNumList(3);
                    return;
                }
                _onCrossLeft = true;
                break;

            case ButtonType.CROSSY_UP:
                //剣の切り替え
                if (!player_ctr.GetAlchemyUIFlag)
                {
                    player_ctr.SwordNumList(0);
                    return;
                }
                _onCrossUp = true;
                break;

            case ButtonType.CROSSY_DOWN:
                //剣の切り替え
                if (!player_ctr.GetAlchemyUIFlag)
                {
                    player_ctr.SwordNumList(2);
                    return;
                }
                _onCrossDown = true;
                break;
        }
    }

    #endregion

    #region Input処理
    /// <summary>
    /// どのボタンを押されたかの処理
    /// </summary>
    private void BtnCheck()
    {
        if (!player_ctr.AllCommandActive || player_ctr.EventFlag) { return; }

        if (Input.GetButtonDown("L2") || Input.GetKeyDown(KeyCode.P))
        {// L2ボタン or キーボードの「P」
            Move(ButtonType.L2);
        }

        if (!player_ctr.IsCommandActive) { return; }

        if (Input.GetButton("Jump") || Input.GetKeyDown(KeyCode.Space))
        {//×ボタン or キーボードの「W」
            Move(ButtonType.JUMP);
        }
        if (Input.GetAxis("Horizontal_ps4") <= -0.3f || Input.GetKey(KeyCode.A))
        {//左ジョイスティックを左にたおす or キーボードの「A」
            Move(ButtonType.LEFTJOYSTICK_LEFT);
        }
        else if (Input.GetAxis("Horizontal_ps4") >= 0.3f || Input.GetKey(KeyCode.D))
        {//左ジョイスティックを右にたおす or キーボードの「D」
            Move(ButtonType.LEFTJOYSTICK_RIGHT);
        }
        else if (Input.GetAxis("Horizontal_ps4") >= -0.3f && Input.GetAxis("Horizontal_ps4") <= 0.3f)
        {//左ジョイスティックを押してない時
            if (leg_col.isLanding) {
                _laddernow = false;
            }

            if (direc == Direction.LEFT && !Jumping && leg_col.isLanding)
            {
                anim_ctr.ChangeAnimatorState(AnimController.AnimState.AnimType.LEFTIDLE);
            }
            else if(direc == Direction.RIGHT && !Jumping && leg_col.isLanding)
            {
                anim_ctr.ChangeAnimatorState(AnimController.AnimState.AnimType.RIGHTIDLE);
            }
            if(InLadderCount > 0 && gameObject.layer == LayerMask.NameToLayer("LadderPlayer")) {
                transform.parent.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                PotObject.transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

                transform.parent.GetComponent<Rigidbody2D>().gravityScale = 0;
                PotObject.transform.GetComponent<Rigidbody2D>().gravityScale = 0;
            }

            rig.velocity = new Vector2(0, rig.velocity.y);
            PotObject.GetComponent<PotController>().StopPot();
        }
        if (Input.GetAxis("Vertical_ps4") <= -0.8f || Input.GetKey(KeyCode.W))
        {
            Move(ButtonType.LEFTJOYSTICK_UP);
        }
        else if (Input.GetAxis("Vertical_ps4") >= 0.8f || Input.GetKey(KeyCode.S))
        {
            Move(ButtonType.LEFTJOYSTICK_DOWN);
        }
        else if (Input.GetAxis("Vertical_ps4") >= -0.8f && Input.GetAxis("Vertical_ps4") <= 0.8f)
        {
            if (!_ActiveRightLeft)
            {
                rig.velocity = new Vector2(rig.velocity.x, rig.velocity.y * 0);
            }
            else
            {
                rig.velocity = new Vector2(rig.velocity.x, rig.velocity.y);
            }
        }
        if (Input.GetButtonDown("Squere") || Input.GetKeyDown(KeyCode.Q))
        {//□ボタン or キーボードの「Q」
            Move(ButtonType.SQUARE);
        }
        if (Input.GetButtonDown("Circle") || Input.GetKeyDown(KeyCode.E))
        {//〇ボタン or キーボードの「E」
            Move(ButtonType.CIRCLE);
        }
        if (Input.GetButtonDown("Triangle") || Input.GetKeyDown(KeyCode.F))
        {//△ボタン or キーボードの「F」
            Move(ButtonType.TRIANGLE);
        }
        if (Input.GetButtonDown("R1") || Input.GetKeyDown(KeyCode.K))
        {// R1ボタン or キーボードの「K」
            Move(ButtonType.R1);
        }
        if (Input.GetButtonDown("R2") || Input.GetKeyDown(KeyCode.O))
        {// R2ボタン or キーボードの「O」英語のオーです「o」
            Move(ButtonType.R2);
        }
        if (Input.GetButtonDown("Option") || Input.GetKeyDown(KeyCode.U))
        {// Optionボタン or キーボードの「U」
            Move(ButtonType.OPTION);
        }
        if (Input.GetAxis("CrossX") <= -0.15f || Input.GetKey(KeyCode.C))
        {// 十字左ボタン or キーボードの「C」
            Move(ButtonType.CROSSX_LEFT);
        }
        else if (Input.GetAxis("CrossX") >= 0.15f || Input.GetKey(KeyCode.X))
        {//十字右ボタン or キーボードの「X」
            Move(ButtonType.CROSSX_RIGTH);
        }
        if (Input.GetAxis("CrossY") <= -0.15f || Input.GetKey(KeyCode.Z))
        {//十字下ボタン or キーボードの「Z」
            Move(ButtonType.CROSSY_DOWN);
        }
        else if (Input.GetAxis("CrossY") >= 0.15f || Input.GetKey(KeyCode.V))
        {//十字上ボタン or キーボードの「V」
            Move(ButtonType.CROSSY_UP);
        }
        else if (Input.GetAxis("CrossY") >= -0.15f && Input.GetAxis("CrossY") <= 0.15f)
        {
            _onCrossDown = false;
            _onCrossUp = false;
        }
        if (Input.GetAxis("RightHorizontal_ps4") <= -0.15f || Input.GetKey(KeyCode.LeftArrow))
        {
            Move(ButtonType.RIGHTJOYSTICK_LEFT);
        }
        else if (Input.GetAxis("RightHorizontal_ps4") >= 0.15f || Input.GetKey(KeyCode.RightArrow))
        {
            Move(ButtonType.RIGHTJOYSTICK_RIGHT);
        }
        else if (Input.GetAxis("RightHorizontal_ps4") <= -0.15f && Input.GetAxis("RightHorizontal_ps4") >= 0.15f)
        {
            _onRJoystickLeft = false;
            _onRJoystickRight = false;
        }
        if (Input.GetAxis("RightVertical_ps4") <= -0.15f || Input.GetKey(KeyCode.UpArrow))
        {
            Move(ButtonType.RIGHTJOYSTICK_UP);
        }
        else if (Input.GetAxis("RightVertical_ps4") >= 0.15f || Input.GetKey(KeyCode.DownArrow))
        {
            Move(ButtonType.RIGHTJOYSTICK_DOWN);
        }
        else if (Input.GetAxis("RightVertical_ps4") <= -0.15f && Input.GetAxis("RightVertical_ps4") >= 0.15f)
        {
            _onRJoystickUp = false;
            _onRJoystickLeft = false;
        }
    }
    #endregion

    /// <summary>
    /// はしごの上下処理
    /// </summary>
    /// <param name="speed">はしご上り下りする速さ</param>
    /// <param name="dir"></param>
    public void Ladder(float speed, float dir)
    {
        PlayerStatus status = pManager.Status;
        status.gimmick_state = PlayerStatus.GimmickState.ONLADDER;
        IsLadder = true;
        //プレイヤーの子供全部のレイヤーを変更
        var children = transform.parent.transform;
        foreach(Transform child in children)
        {
            if (child.GetComponent<Collider2D>())
            {
                child.gameObject.layer = LayerMask.NameToLayer("LadderPlayer");
            }
        }

        gameObject.transform.parent.gameObject.layer = LayerMask.NameToLayer("LadderPlayer");
        PotObject.layer = LayerMask.NameToLayer("Trans");
        transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed * dir);
        _laddernow = true;
    }

    private bool CheckMoveable()
    {
        if (leg_col.isLanding)
            return true;
        if (gameObject.layer == LayerMask.NameToLayer("LadderPlayer"))
        {
            return true;
        }
        
        if (Jumping)
        {
            Vector3 startPos = gameObject.transform.position;
            Vector2 dir = direc == Direction.RIGHT ? Vector2.right : Vector2.left;
            // Rayを飛ばす
            RaycastHit2D[] hit = Physics2D.RaycastAll(startPos, dir, GetComponent<CapsuleCollider2D>().size.x * 0.5f);
            bool flag = true;
            foreach (RaycastHit2D r in hit)
            {
                if (r.collider.gameObject.layer == LayerMask.NameToLayer("Block"))
                {
                    flag = false;
                    break;
                }
            }
            return flag;
        }
        return false;
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        PlayerStatus status = pManager.Status;
        if (col.gameObject.tag == "Item")
        {
            target = col.gameObject;
        }
        //モンスターの投擲物に当たった時
        if(col.gameObject.tag == "Monster")
        {
            MonsterStatus mStatus = col.gameObject.GetComponent<MonsterController>().Status;
            if (mStatus.type == MonsterStatus.MonsterType.HARB) { return; }
            int atk = mStatus.GetAttack;
            StartCoroutine(TransWaitTime());
            player_ctr.HPDown(atk);
            StartCoroutine(PlayerNockBackWaitTime());
        }
        
        if (col.GetComponent<GimmickInfo>())
        {
            switch (col.GetComponent<GimmickInfo>().type)
            {
                case GimmickInfo.GimmickType.GROWTREE:
                   status.gimmick_state = PlayerStatus.GimmickState.ONTREE;
                    break;
                default:
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        PotObject.transform.position = new Vector2(PotObject.transform.position.x, PotObject.transform.position.y);
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        //モンスターにぶつかった時
        if (col.gameObject.tag == "Monster")
        {
            if (!col.gameObject.GetComponent<MonsterController>())
                return;
            MonsterStatus mStatus = col.gameObject.GetComponent<MonsterController>().Status;
            if (mStatus.type == MonsterStatus.MonsterType.HARB) { return; }
            if (_hitmonster) { return; }
            int atk = mStatus.GetAttack;
            StartCoroutine(TransWaitTime());
            player_ctr.HPDown(atk);
            StartCoroutine(PlayerNockBackWaitTime());
        }
    }

    /// <summary>
    /// モンスターに当たった時のプレイヤーのノックバック
    /// </summary>
    /// <returns></returns>
    private IEnumerator PlayerNockBackWaitTime()
    {
        //弟の顔変更
        PotObject.GetComponent<PotController>().ChangePotFace(PotStatus.PotFace.Sad);
        player_ctr.AllCommandActive = false;
        //プレイヤーをノックバック
        rig.AddForce(new Vector2(rig.velocity.x * -2f, 1.5f), ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.4f);
        player_ctr.AllCommandActive = true;
        //弟の顔変更
        PotObject.GetComponent<PotController>().ChangePotFace(PotStatus.PotFace.Normal);
    }
}