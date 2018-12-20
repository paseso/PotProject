using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//優先度アップ
[DefaultExecutionOrder(-1)]
public class MoveController : MonoBehaviour
{
    [HideInInspector]
    public float speed = 0f;

    private bool _jumping = false;

    public bool IsLadder
    {
        get; set;
    }

    [SerializeField]
    private float ladderSpeed;

    private float axisValue = 0f;

    private Rigidbody2D rig;
    //はしご中かどうか
    private bool _laddernow = false;

    private bool isLadderTop = false;
    // はしごの上に足がついているか
    public bool IsLadderTop {
        get { return isLadderTop; }
        set { value = isLadderTop; }
    }

    //ジャンプできるかどうか
    [HideInInspector]
    public bool _isJump = false;
    //左右動かしてもいいかどうか
    [HideInInspector]
    public bool _ActiveRightLeft = false;
    //アイテムを落としたかどうか
    [HideInInspector]
    public bool _itemFall = false;
    //CrossYが一回押されたかどうか
    private bool _onCrossYTrigger = false;
    //モンスターに当たったかどうか
    [HideInInspector]
    public bool _hitmonster = false;

    //-------アクションボタンを押してるかどうか----------
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
    private SpriteRenderer obj_sprite;
    private GameObject PotObject;

    private PlayerController player_ctr;
    private BringCollider bringctr;
    private AttackZoonController atc_ctr;
    private AlchemyUIController alchemyUI_ctr;
    private AnimController anim_ctr;
    private Status status;
    //----------ボタンFlagのget---------------------
    public bool Jumping
    {
        get { return _jumping; }
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

    public bool OnCrossRigtht
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
    //CrossX,YのAixsの値
    public float CrossAxisValue
    {
        get { return axisValue; }
    }

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
        L1,
        R1,
        L2,
        R2,
        OPTION,
        PSBTN,
        PSPAD,
        CROSSX_RIGTH,
        CROSSX_LEFT,
        CROSSY_UP,
        CROSSY_DOWN,
    };

    private enum Direction
    {
        RIGHT = 0,
        LEFT
    }
    private Direction direc;

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
        PotObject = GameObject.FindObjectOfType<PotController>().gameObject;
        rig = gameObject.transform.parent.GetComponent<Rigidbody2D>();
        player_ctr = GameObject.Find("Controller").GetComponent<PlayerController>();
        bringctr = gameObject.transform.parent.GetChild(0).GetComponent<BringCollider>();
        atc_ctr = GameObject.Find("AttackRange").GetComponentInChildren<AttackZoonController>();
        alchemyUI_ctr = GameObject.Find("Canvas/Alchemy_UI").GetComponent<AlchemyUIController>();
        obj_sprite = gameObject.transform.parent.GetComponent<SpriteRenderer>();
        anim_ctr = gameObject.transform.parent.GetComponent<AnimController>();
        _isJump = false;
        _onCrossYTrigger = false;
        axisValue = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!player_ctr.IsCommandActive) { return; }

        if (bringctr._bring)
        {
            target.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 2.5f);
        }
        if(_laddernow)
        {
            PotObject.transform.DOMove(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), 0.5f).SetEase(Ease.Linear);
        }

        ClearBtnFlg();
        EventStateCheck();
        _onCrossYTrigger = true;
    }

    /// <summary>
    /// 今のEventStateをチェックして各処理に移る
    /// </summary>
    private void EventStateCheck()
    {
        switch (player_ctr.status.event_state)
        {
            case Status.EventState.NORMAL:
                BtnCheck();
                break;

            case Status.EventState.CAMERA:
                break;

            case Status.EventState.ALCHEMYUI:
                UIControll();
                break;
        }

    }

    /// <summary>
    /// 錬金UIを開いている時の制御
    /// </summary>
    private void UIControll()
    {
        _isJump = false;
        _ActiveRightLeft = false;
        BtnCheck();

    }

    /// <summary>
    /// 移動フラグを全部falseにする処理
    /// </summary>
    private void ClearBtnFlg()
    {
        _jumping = false;
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
                if (!_isJump)
                    return;
                anim_ctr.ChangeAnimatorState(AnimController.AnimState.AnimType.JUMP);
                rig.velocity = new Vector2(0, 1f * speed);
                _jumping = true;
                break;

            case ButtonType.LEFTJOYSTICK_LEFT:
                _onLeft = true;
                _onRight = false;
                if (!_ActiveRightLeft)
                    return;

                anim_ctr.ChangeAnimatorState(AnimController.AnimState.AnimType.LEFT_WALK);
                rig.velocity = new Vector2(-5f, rig.velocity.y);
                direc = Direction.LEFT;
                break;

            case ButtonType.LEFTJOYSTICK_RIGHT:
                _onRight = true;
                _onLeft = false;
                if (!_ActiveRightLeft)
                    return;

                anim_ctr.ChangeAnimatorState(AnimController.AnimState.AnimType.RIGHT_WALK);
                rig.velocity = new Vector2(5f, rig.velocity.y);
                direc = Direction.RIGHT;
                break;

            case ButtonType.LEFTJOYSTICK_UP:

                _onUp = true;

                if (status.state == Status.GimmickState.ONLADDER) {
                    Ladder(ladderSpeed, 1);
                    anim_ctr.ChangeAnimatorState(AnimController.AnimState.AnimType.LADDER_UP);
                }
                _onUp = false;
                break;

            case ButtonType.LEFTJOYSTICK_DOWN:
                
                Debug.Log("DOWN");

                _onDown = true;
                if (status.state == Status.GimmickState.ONLADDER) {
                    Ladder(ladderSpeed, -1);
                    anim_ctr.ChangeAnimatorState(AnimController.AnimState.AnimType.LADDER_DOWN);
                }
                break;

            case ButtonType.RIGHTJOYSTICK_LEFT:
                Debug.Log("Right Joystick Left");
                if (!player_ctr.AlchemyWindow)
                    return;
                _onRJoystickLeft = true;

                break;

            case ButtonType.RIGHTJOYSTICK_RIGHT:
                Debug.Log("Right Joystick Right");
                if (!player_ctr.AlchemyWindow)
                    return;
                _onRJoystickRight = true;

                break;

            case ButtonType.RIGHTJOYSTICK_UP:
                Debug.Log("Right Joystick Up");
                if (!player_ctr.AlchemyWindow)
                    return;
                _onRJoystickUp = true;

                break;

            case ButtonType.RIGHTJOYSTICK_DOWN:
                Debug.Log("Right Joystick Down");
                if (!player_ctr.AlchemyWindow)
                    return;
                _onRJoystickDown = true;

                break;

            case ButtonType.CIRCLE:
                _onCircle = true;
                
                if (player_ctr.AlchemyWindow)
                {
                    alchemyUI_ctr.PickItem();
                }
                else
                {
                    atc_ctr.AttackObject();
                }
                break;

            case ButtonType.SQUARE:
                if (!bringctr._Brotherhit)
                    return;

                _onSquare = true;
                if (!bringctr._bring)
                {//アイテムを持つ
                    target.GetComponent<Rigidbody2D>().simulated = false;
                    bringctr._bring = true;
                }
                else
                {//アイテムを離す
                    if (direc == Direction.RIGHT)
                    {
                        target.gameObject.transform.position = new Vector2(bringctr.gameObject.transform.position.x + 2f, bringctr.gameObject.transform.position.y + 1.5f);
                    }
                    else
                    {
                        target.gameObject.transform.position = new Vector2(bringctr.gameObject.transform.position.x - 2f, bringctr.gameObject.transform.position.y + 1.5f);
                    }
                    target.gameObject.transform.parent = null;
                    target.GetComponent<Rigidbody2D>().simulated = true;
                    bringctr._bring = false;
                    _itemFall = true;
                }
                _onSquare = false;
                break;

            case ButtonType.TRIANGLE:
                Debug.Log("△");
                break;

            case ButtonType.L1:
                Debug.Log("L1");
                break;

            case ButtonType.R1:
                Debug.Log("R1");
                break;

            case ButtonType.L2:
                Debug.Log("L2");
                break;

            case ButtonType.R2:
                Debug.Log("R2");
                player_ctr.OpenAlchemy();
                break;

            case ButtonType.OPTION:
                Debug.Log("Option");
                break;

            case ButtonType.PSBTN:
                Debug.Log("PSbtn");
                break;

            case ButtonType.PSPAD:
                Debug.Log("PSpad");
                break;

            case ButtonType.CROSSX_RIGTH:
                if (!player_ctr.AlchemyWindow)
                    return;

                axisValue = Input.GetAxis("CrossX");
                _onCrossRight = true;
                break;

            case ButtonType.CROSSX_LEFT:
                if (!player_ctr.AlchemyWindow)
                    return;

                axisValue = Input.GetAxis("CrossX");
                _onCrossLeft = true;
                break;

            case ButtonType.CROSSY_UP:
                Debug.Log("Cross_Up");
                if (!player_ctr.AlchemyWindow)
                    return;

                axisValue = Input.GetAxis("CrossY");
                _onCrossUp = true;
                break;

            case ButtonType.CROSSY_DOWN:
                Debug.Log("Cross_Down");
                if (!player_ctr.AlchemyWindow)
                    return;

                axisValue = Input.GetAxis("CrossY");
                _onCrossDown = true;
                break;
        }
        _onUp = false;
        _onDown = false;
    }

    #endregion

    #region Input処理

    /// <summary>
    /// どのボタンを押されたかの処理
    /// </summary>
    private void BtnCheck()
    {
        // はしご内で昇降ボタンを離したとき
        //if(Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.W))
        //{
        //    if (status.state == Status.GimmickState.ONLADDER && !_isJump && IsLadder)
        //    {
        //        transform.parent.GetComponent<Rigidbody2D>().simulated = false;
        //    }
        //}

        if (Input.GetButton("Jump") || Input.GetKeyDown(KeyCode.Space))
        {//×ボタン or キーボードの「W」
            Move(ButtonType.JUMP);
        }
        if (Input.GetAxis("Vertical_ps4") >= 0.15f || Input.GetKey(KeyCode.A))
        {//左ジョイスティックを左にたおす or キーボードの「A」
            Move(ButtonType.LEFTJOYSTICK_LEFT);
        }
        else if (Input.GetAxis("Vertical_ps4") <= -0.15f || Input.GetKey(KeyCode.D))
        {//左ジョイスティックを右にたおす or キーボードの「D」
            Move(ButtonType.LEFTJOYSTICK_RIGHT);
        }
        else if (Input.GetAxis("Vertical_ps4") <= 0.15f && Input.GetAxis("Vertical_ps4") >= -0.15f)
        {
            if(direc == Direction.LEFT)
            {
                anim_ctr.ChangeAnimatorState(AnimController.AnimState.AnimType.LEFTIDLE);
            }
            else
            {
                anim_ctr.ChangeAnimatorState(AnimController.AnimState.AnimType.RIGHTIDLE);
            }
            if (status.state == Status.GimmickState.ONLADDER && !_isJump && IsLadder)
            {
                transform.parent.GetComponent<Rigidbody2D>().simulated = false;
                Debug.Log("name = " + PotObject.name);
                PotObject.GetComponent<Rigidbody2D>().simulated = false;
            }

            rig.velocity = new Vector2(0, rig.velocity.y);
        }
        if (Input.GetAxis("Horizontal_ps4") >= 0.15f || Input.GetKey(KeyCode.W))
        {
            Move(ButtonType.LEFTJOYSTICK_UP);
        }
        else if (Input.GetAxis("Horizontal_ps4") <= -0.15f || Input.GetKey(KeyCode.S))
        {
            Move(ButtonType.LEFTJOYSTICK_DOWN);
        }
        else if (Input.GetAxis("Horizontal_ps4") <= 0.15f && Input.GetAxis("Horizontal_ps4") >= -0.15f)
        {
            _onUp = false;
            _onDown = false;
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
        if (Input.GetButtonDown("L1") || Input.GetKeyDown(KeyCode.L))
        {// L1ボタン or キーボードの「L」
            Move(ButtonType.L1);
        }
        if (Input.GetButtonDown("R1") || Input.GetKeyDown(KeyCode.K))
        {// R1ボタン or キーボードの「K」
            Move(ButtonType.R1);
        }
        if (Input.GetButtonDown("L2") || Input.GetKeyDown(KeyCode.P))
        {// L2ボタン or キーボードの「P」
            Move(ButtonType.L2);
        }
        if (Input.GetButtonDown("R2") || Input.GetKeyDown(KeyCode.O))
        {// R2ボタン or キーボードの「O」英語のオーです「o」
            Move(ButtonType.R2);
        }
        if (Input.GetButtonDown("Option") || Input.GetKeyDown(KeyCode.U))
        {// Optionボタン or キーボードの「U」
            Move(ButtonType.OPTION);
        }
        if (Input.GetButtonDown("PSbtn") || Input.GetKeyDown(KeyCode.H))
        {// 真ん中のPSボタン or キーボードの「H」
            Move(ButtonType.PSBTN);
        }
        if (Input.GetButtonDown("PSpad") || Input.GetKeyDown(KeyCode.Y))
        {//PSパッドボタン or キーボードの「Y」
            Move(ButtonType.PSPAD);
        }
        if (Input.GetAxis("CrossX") >= 0.15f || Input.GetKey(KeyCode.C))
        {// 十字左ボタン or キーボードの「C」
            Move(ButtonType.CROSSX_LEFT);
        }
        else if (Input.GetAxis("CrossX") <= -0.15f || Input.GetKey(KeyCode.X))
        {//十字右ボタン or キーボードの「X」
            Move(ButtonType.CROSSX_RIGTH);
        }
        if (Input.GetAxis("CrossY") >= 0.15f || Input.GetKey(KeyCode.Z))
        {//十字下ボタン or キーボードの「Z」
            Move(ButtonType.CROSSY_DOWN);
        }
        else if (Input.GetAxis("CrossY") <= -0.15f || Input.GetKey(KeyCode.V))
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
    /// <param name="speed"></param>
    /// <param name="dir"></param>
    public void Ladder(float speed, float dir)
    {
        Debug.Log("valocity=" + speed * dir);
        if (status.state != Status.GimmickState.ONLADDER) { return; }
        transform.parent.GetComponent<Rigidbody2D>().simulated = true;
        
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

    private void HitRayWall()
    {
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, Vector2.right);
        Debug.DrawRay(gameObject.transform.position, hit.point * 2, Color.red);
        if (Physics2D.Raycast(gameObject.transform.position, hit.point * 2, 2))
        {
            if (hit.collider.tag == "floor")
            {
                _ActiveRightLeft = false;
            }
            else
            {
                _ActiveRightLeft = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<GimmickInfo>())
        {
            switch (col.GetComponent<GimmickInfo>().type)
            {
                case GimmickInfo.GimmickType.GROWTREE:
                    status.state = Status.GimmickState.ONTREE;
                    break;
                case GimmickInfo.GimmickType.LADDERTOP:
                    status.state = Status.GimmickState.ONLADDER;
                    break;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if(!col.GetComponent<GimmickInfo>()){ return; }

        switch (col.GetComponent<GimmickInfo>().type)
        {
            
            case GimmickInfo.GimmickType.LADDER:
                status.state = Status.GimmickState.ONLADDER;
                break;
            default:

                break;
        }

    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (!_isJump) return;
        PotObject.transform.position = new Vector2(PotObject.transform.position.x, PotObject.transform.position.y);
        if(!col.GetComponent<GimmickInfo>()) { return; }

        switch (col.GetComponent<GimmickInfo>().type) {
            case GimmickInfo.GimmickType.LADDER:
                status.state = Status.GimmickState.NORMAL;
                ChangeLayer();
                PotObject.layer = LayerMask.NameToLayer("Pot");
                _laddernow = false;
                break;
            case GimmickInfo.GimmickType.LADDERTOP:
                status.state = Status.GimmickState.NORMAL;
                ChangeLayer();
                PotObject.layer = LayerMask.NameToLayer("Pot");
                _laddernow = false;
                PotObject.transform.position = new Vector2(PotObject.transform.position.x, PotObject.transform.position.y);
                break;
        } 
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Monster")
        {
            _hitmonster = true;
            player_ctr.ApplyHp(1);
            atc_ctr.AttackObject();
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Monster")
        {
            _hitmonster = false;
        }
    }

    /// <summary>
    /// レイヤー変更
    /// </summary>
    public void ChangeLayer()
    {
        if (gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            PotObject.GetComponent<Rigidbody2D>().simulated = true;
            var children = transform.parent.transform;
            foreach (Transform child in children)
            {
                if (child.GetComponent<Collider2D>())
                {
                    child.gameObject.layer = LayerMask.NameToLayer("Player");
                }
            }
            gameObject.transform.parent.gameObject.layer = LayerMask.NameToLayer("Player");
        }
    }
}