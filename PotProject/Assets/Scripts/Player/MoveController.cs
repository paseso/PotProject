using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{

    [SerializeField]
    private float speed = 0f;

    private Rigidbody2D rig;
    //ジャンプできるかどうか
    [HideInInspector]
    public bool _isJump = false;
    //壁にあたったかどうか
    private bool _hitWall = false;
    //左右動かしてもいいかどうか
    private bool _ActiveRightLeft = false;
    //ギミックの中いるかどうか
    [HideInInspector]
    public bool _InGimmick = false;

    [HideInInspector]
    public bool _itemFall = false;
    //-------アクションボタンを押してるかどうか----------
    [HideInInspector]
    public bool _onRight = false;
    [HideInInspector]
    public bool _onLeft = false;
    [HideInInspector]
    public bool _onUp = false;
    [HideInInspector]
    public bool _onDown = false;
    [HideInInspector]
    public bool _onSquare = false;
    [HideInInspector]
    public bool _onCircle = false;
    //---------------------------------------------
    [HideInInspector]
    public GameObject target;
    private float gimmick_x = 0f;
    [SerializeField, Header("兄のSprite 0.左 1.右 2.後ろ")]
    private List<Sprite> BrotherSprites;


    private float ladder_y = 0;
    private float leg_y = 0;

    [SerializeField]
    private GameObject managerGameObject;
    private PlayerManager manager;
    private BringCollider bringctr;
    private AttackZoonController atc_ctr;
    private MapInfo mInfo;

    private bool _onece = false;

    private enum ButtonType
    {
        JUMP = 0,
        RIGHT,
        LEFT,
        UP,
        DOWN,
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

    // Use this for initialization
    void Start()
    {
        target = null;
        rig = gameObject.GetComponent<Rigidbody2D>();
        manager = managerGameObject.GetComponent<PlayerManager>();
        bringctr = gameObject.transform.GetChild(0).GetComponent<BringCollider>();
        mInfo = transform.root.GetComponent<MapInfo>();
        atc_ctr = gameObject.GetComponentInChildren<AttackZoonController>();
        _isJump = false;
        _onRight = false;
        _onLeft = false;
        _onSquare = false;
        _onCircle = false;
        _onece = false;
        _itemFall = false;
        _hitWall = false;
        _ActiveRightLeft = true;
        _onUp = false;
        _onDown = false;
        _InGimmick = false;
        gimmick_x = 0f;
        ladder_y = 0;
        leg_y = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!mInfo.LadderFlag)
        {
            GimmickLadderOut();
        }

        if (bringctr._bring)
        {
            target.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 2.5f);
        }
        BtnCheck();
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
                Debug.Log("×");
                if (!_isJump)
                    return;
                rig.velocity = new Vector2(0, 1f * speed);
                break;

            case ButtonType.LEFT:
                Debug.Log("LEFT");
                _onLeft = true;
                _onRight = false;
                if (!_ActiveRightLeft)
                    return;

                gameObject.GetComponent<SpriteRenderer>().sprite = BrotherSprites[0];
                rig.velocity = new Vector2(-5f, rig.velocity.y);
                break;

            case ButtonType.RIGHT:
                Debug.Log("RIGHT");
                _onRight = true;
                _onLeft = false;
                if (!_ActiveRightLeft)
                    return;

                gameObject.GetComponent<SpriteRenderer>().sprite = BrotherSprites[1];
                rig.velocity = new Vector2(5f, rig.velocity.y);
                break;

            case ButtonType.UP:
                Debug.Log("UP");
                _onUp = true;
                if (_ActiveRightLeft)
                    return;

                //gameObject.GetComponent<CapsuleCollider2D>().isTrigger = true;
                rig.bodyType = RigidbodyType2D.Kinematic;
                rig.velocity = new Vector2(rig.velocity.x, 5f);
                _onUp = false;
                break;

            case ButtonType.DOWN:
                Debug.Log("DOWN");
                _onDown = true;
                if (_ActiveRightLeft)
                    return;

                //gameObject.GetComponent<CapsuleCollider2D>().isTrigger = true;
                rig.bodyType = RigidbodyType2D.Kinematic;
                rig.velocity = new Vector2(rig.velocity.x, -5f);
                _onDown = false;
                break;

            case ButtonType.CIRCLE:
                Debug.Log("〇");
                if (!_onCircle)
                    return;

                atc_ctr.AttackObject();

                break;

            case ButtonType.SQUARE:
                Debug.Log("□");
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
                    target.gameObject.transform.position = new Vector2(gameObject.transform.position.x + 2f, gameObject.transform.position.y + 1);
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
                manager.SwordTypeChange(Status.SWORDTYPE.FIRE);
                break;

            case ButtonType.CROSSY_DOWN:
                manager.SwordTypeChange(Status.SWORDTYPE.FIRE);
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
        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Space))
        {//×ボタン or キーボードの「W」
            Move(ButtonType.JUMP);
        }
        if (Input.GetAxis("Vertical_ps4") >= 0.15f || Input.GetKey(KeyCode.A))
        {//左ジョイスティックを左にたおす or キーボードの「A」
            Move(ButtonType.LEFT);
        }
        else if (Input.GetAxis("Vertical_ps4") <= -0.15f || Input.GetKey(KeyCode.D))
        {//左ジョイスティックを右にたおす or キーボードの「D」
            Move(ButtonType.RIGHT);
        }
        else if (Input.GetAxis("Vertical_ps4") <= 0.15f && Input.GetAxis("Vertical_ps4") >= -0.15f)
        {
            rig.velocity = new Vector2(0, rig.velocity.y);
            _onLeft = false;
            _onRight = false;
        }
        if (Input.GetAxis("Horizontal_ps4") >= 0.15f || Input.GetKey(KeyCode.W))
        {
            Move(ButtonType.UP);
        }
        else if (Input.GetAxis("Horizontal_ps4") <= -0.15f || Input.GetKey(KeyCode.S))
        {
            Move(ButtonType.DOWN);
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
    }

    #endregion

    /// <summary>
    /// はしごのギミック処理
    /// </summary>
    public void GimmickLadderIn(float pos_x)
    {
        _ActiveRightLeft = false;
        //if (gameObject.transform.GetComponentInChildren<LegCollider>())
        //{
        //    gameObject.transform.GetComponentInChildren<LegCollider>().OnIsTrigger();
        //}
        gameObject.GetComponent<SpriteRenderer>().sprite = BrotherSprites[2];
        gameObject.transform.position = new Vector2(pos_x, gameObject.transform.position.y);
    }

    public void GimmickLadderOut()
    {
        Debug.Log("アウト");
        _ActiveRightLeft = true;
        //gameObject.GetComponent<CapsuleCollider2D>().isTrigger = false;
        //gameObject.GetComponentInChildren<LegCollider>().OffIsTrigger();
        rig.velocity = new Vector2(rig.velocity.x, rig.velocity.y);
        rig.bodyType = RigidbodyType2D.Dynamic;
        //gameObject.GetComponentInChildren<LegCollider>().ClearFloorCount();
    }

    private void LadderTrigger()
    {
        if (!mInfo.LadderFlag)
            return;
        if(_onUp || _onDown)
        {
            Debug.Log("イン");
            GimmickLadderIn(gimmick_x);
            Debug.Log("_InGimmick: " + _InGimmick);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "floor")
        {
            GimmickLadderOut();
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<GimmickInfo>())
        {
            Debug.Log("GimmickInfoあります！");
            GimmickInfo gInfo = col.gameObject.GetComponent<GimmickInfo>();
            Debug.Log("Gimmick Type: " + gInfo.type);
            if (gInfo.type == GimmickInfo.GimmickType.LADDER)
            {
                gimmick_x = col.gameObject.transform.position.x;
                LadderTrigger();
            }
        }
        //はしごに当たった時
        //if (mInfo.LadderFlag && !_InGimmick)
        //{
        //    _InGimmick = true;
        //    GimmickLadderIn(col.transform);
        //}

        //木がのびるcolliderに当たった時
        if (mInfo.GrowTreeFlag)
        {

        }
        
    }
}