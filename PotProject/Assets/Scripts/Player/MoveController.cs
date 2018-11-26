using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//優先度アップ
[DefaultExecutionOrder(-1)]
public class MoveController : MonoBehaviour
{

    [SerializeField]
    private float speed = 0f;

    [SerializeField]
    private float ladderSpeed;

    private Rigidbody2D rig;
    //ジャンプできるかどうか
    [HideInInspector]
    public bool _isJump = false;
    //左右動かしてもいいかどうか
    private bool _ActiveRightLeft = false;
    //ギミックの中いるかどうか
    [HideInInspector]
    public bool _InGimmick = false;
    //アイテムを落としたかどうか
    [HideInInspector]
    public bool _itemFall = false;
    //はしごの処理を行えるかどうか
    private bool _activeLadder = false;
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
    //---------------------------------------------
    [HideInInspector]
    public GameObject target;
    [SerializeField, Header("兄のSprite 0.左 1.右 2.後ろ")]
    private List<Sprite> BrotherSprites;

    private PlayerController manager;
    private BringCollider bringctr;
    private AttackZoonController atc_ctr;
    private MapInfo mInfo;
    private Status status;
    //----------ボタンFlagのget---------------------
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
    //------------------------------------------

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

    private void Awake()
    {
        target = null;
        _itemFall = false;
        _ActiveRightLeft = true;
        _InGimmick = false;
        _activeLadder = false;
        ClearBtnFlg();
    }

    // Use this for initialization
    void Start()
    {
        rig = gameObject.GetComponent<Rigidbody2D>();
        manager = GameObject.Find("Controller").GetComponent<PlayerController>();
        bringctr = gameObject.transform.GetChild(0).GetComponent<BringCollider>();
        mInfo = transform.root.GetComponent<MapInfo>();
        atc_ctr = gameObject.GetComponentInChildren<AttackZoonController>();

        _isJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (bringctr._bring)
        {
            target.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 2.5f);
        }
        BtnCheck();
    }

    /// <summary>
    /// 移動フラグを全部falseにする処理
    /// </summary>
    private void ClearBtnFlg()
    {
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
                //Debug.Log("×");
                if (!_isJump)
                    return;
                rig.velocity = new Vector2(0, 1f * speed);
                break;

            case ButtonType.LEFTJOYSTICK_LEFT:
                //Debug.Log("LEFT");
                _onLeft = true;
                _onRight = false;
                if (!_ActiveRightLeft)
                    return;

                gameObject.GetComponent<SpriteRenderer>().sprite = BrotherSprites[0];
                rig.velocity = new Vector2(-5f, rig.velocity.y);
                break;

            case ButtonType.LEFTJOYSTICK_RIGHT:
                //Debug.Log("RIGHT");
                _onRight = true;
                _onLeft = false;
                if (!_ActiveRightLeft)
                    return;

                gameObject.GetComponent<SpriteRenderer>().sprite = BrotherSprites[1];
                rig.velocity = new Vector2(5f, rig.velocity.y);
                break;

            case ButtonType.LEFTJOYSTICK_UP:

                _onUp = true;

                if (status.state == Status.State.ONLADDER) {
                    Ladder(ladderSpeed, 1);
                }

                //rig.bodyType = RigidbodyType2D.Kinematic;
                //rig.velocity = new Vector2(rig.velocity.x, 5f);
                _onUp = false;
                break;

            case ButtonType.LEFTJOYSTICK_DOWN:
                
                Debug.Log("DOWN");

                _onDown = true;
                if (status.state == Status.State.ONLADDER) {
                    Ladder(ladderSpeed, -1);
                }

                //_onDown = true;
                //if (_ActiveRightLeft)
                //    return;

                //rig.bodyType = RigidbodyType2D.Kinematic;
                //rig.velocity = new Vector2(rig.velocity.x, -5f);
                //_onDown = false;
                break;

            case ButtonType.RIGHTJOYSTICK_LEFT:
                Debug.Log("Right Joystick Left");
                if (!manager.AlchemyWindow)
                    return;
                _onRJoystickLeft = true;

                break;

            case ButtonType.RIGHTJOYSTICK_RIGHT:
                Debug.Log("Right Joystick Right");
                if (!manager.AlchemyWindow)
                    return;
                _onRJoystickRight = true;

                break;

            case ButtonType.RIGHTJOYSTICK_UP:
                Debug.Log("Right Joystick Up");
                if (!manager.AlchemyWindow)
                    return;
                _onRJoystickUp = true;

                break;

            case ButtonType.RIGHTJOYSTICK_DOWN:
                Debug.Log("Right Joystick Down");
                if (!manager.AlchemyWindow)
                    return;
                _onRJoystickDown = true;

                break;

            case ButtonType.CIRCLE:
                //Debug.Log("〇");
                if (!_onCircle)
                    return;

                atc_ctr.AttackObject();

                break;

            case ButtonType.SQUARE:
                //Debug.Log("□");
                if (!bringctr._Brotherhit)
                    return;

                _onSquare = true;
                if (!bringctr._bring)
                {//アイテムを持つ
                    target.GetComponent<Rigidbody2D>().simulated = false;
                    bringctr._bring = true;
                }
                else if (bringctr._bring)
                {//アイテムを離す
                    if (OnRight)
                    {
                        target.gameObject.transform.position = new Vector2(bringctr.gameObject.transform.position.x + 2f, bringctr.gameObject.transform.position.y + 1.5f);
                    }
                    else
                    {
                        target.gameObject.transform.position = new Vector2(bringctr.gameObject.transform.position.x - 2f, bringctr.gameObject.transform.position.y + 1.5f);
                    }
                    Debug.Log("Right：" + OnRight);
                    Debug.Log("Left：" + OnLeft);
                    //target.gameObject.transform.position = new Vector2(gameObject.transform.position.x + 2f, gameObject.transform.position.y + 1);
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
                manager.OpenAlchemy();
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
                manager.SwordTypeChange(Status.SWORDTYPE.EARTH);
                break;

            case ButtonType.CROSSX_LEFT:
                manager.SwordTypeChange(Status.SWORDTYPE.WATER);
                break;

            case ButtonType.CROSSY_UP:
                Debug.Log("Cross_Up");
                if (!manager.AlchemyWindow)
                    return;
                _onCrossUp = true;
                break;

            case ButtonType.CROSSY_DOWN:
                Debug.Log("Cross_Down");
                if (!manager.AlchemyWindow)
                    return;
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
        ClearBtnFlg();
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
            rig.velocity = new Vector2(0, rig.velocity.y);
            //_onLeft = false;
            //_onRight = false;
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
        if (Input.GetAxis("CrossX") <= -0.15f || Input.GetKey(KeyCode.X))
        {//十字右ボタン or キーボードの「X」
            Move(ButtonType.CROSSX_RIGTH);
        }
        if (Input.GetAxis("CrossY") >= 0.15f || Input.GetKey(KeyCode.Z))
        {//十字下ボタン or キーボードの「Z」
            Move(ButtonType.CROSSY_DOWN);
        }
        if (Input.GetAxis("CrossY") <= -0.15f || Input.GetKey(KeyCode.V))
        {//十字上ボタン or キーボードの「V」
            Move(ButtonType.CROSSY_UP);
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
    /// <param name="player"></param>
    /// <param name="speed"></param>
    /// <param name="dir"></param>
    public void Ladder(float speed, float dir)
    {
        Debug.Log("Ladder");
        gameObject.layer = LayerMask.NameToLayer("LadderPlayer");
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed * dir);
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
        if (col.GetComponent<GimmickInfo>()) {
            switch (col.GetComponent<GimmickInfo>().type) {
                case GimmickInfo.GimmickType.LADDER:
                    status.state = Status.State.ONLADDER;
                    break;
                case GimmickInfo.GimmickType.TREE:
                    status.state = Status.State.ONTREE;
                    break;
            }
        }

        //if (col.gameObject.GetComponent<GimmickInfo>())
        //{
        //    GimmickInfo gimInfo = col.gameObject.GetComponent<GimmickInfo>();
        //    if (gimInfo.type == GimmickInfo.GimmickType.LADDER)
        //    {
        //        _activeLadder = true;
        //    }
        //}
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (status.state != Status.State.NORMAL) {
            status.state = Status.State.NORMAL;
        }
        if(gameObject.layer != LayerMask.NameToLayer("Player")) {
            gameObject.layer = LayerMask.NameToLayer("Player");
        }
        
        if (col.gameObject.GetComponent<GimmickInfo>())
        {
            GimmickInfo gimInfo = col.gameObject.GetComponent<GimmickInfo>();
            if (gimInfo.type == GimmickInfo.GimmickType.LADDER)
            {
                _activeLadder = false;
            }
        }
    }
}