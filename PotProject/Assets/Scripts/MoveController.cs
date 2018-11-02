using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour {

    [SerializeField]
    private float speed = 0f;

    private Rigidbody2D rig;
    private bool _bring = false;
    private bool _isJump = false;
    private bool _wait = false;
    private bool _hitOtoto = false;
    //-------左右ボタンを押してるかどうか----------
    [HideInInspector]
    public bool _onRight = false;
    [HideInInspector]
    public bool _onLeft = false;
    //---------------------------------------------
    [SerializeField,Header("弟")]
    private GameObject Ototo;

    [SerializeField]
    private GameObject managerGameObject;
    private PlayerManager manager;
    private BringCollider bringctr;
    
    private enum ButtonType
    {
        JUMP = 0,
        RIGTH,
        LEFT,
        CIRCLE,
        SQUERE,
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
        rig = gameObject.GetComponent<Rigidbody2D>();
        manager = managerGameObject.GetComponent<PlayerManager>();
        bringctr = gameObject.transform.GetChild(0).GetComponent<BringCollider>();
        _bring = false;
        _isJump = false;
        _wait = false;
        _onRight = false;
        _onLeft = false;
	}

    //private void FixedUpdate()
    //{
    //    rig.AddForce(Vector2.down * 10, ForceMode2D.Force);
    //}

    // Update is called once per frame
    void Update () {

        //if (_wait)
        //{
        //    CoolTime(1.0f);
        //}
        BtnCheck();
	}

    private void CoolTime(float cooltime)
    {
        cooltime -= Time.deltaTime;
        if (cooltime <= 0f)
        {
            cooltime = 1.0f;
            _wait = false;
        }
    }

    /// <summary>
    /// ボタンが押された時の処理
    /// </summary>
    private void Move(ButtonType btn)
    {
        switch (btn)
        {
            case ButtonType.JUMP:
                Debug.Log("×");
                rig.velocity = new Vector2(rig.velocity.x, 1f * speed);
                _wait = true;
                break;

            case ButtonType.LEFT:
                Debug.Log("LEFT");
                //rig.velocity = new Vector2(-5f, rig.velocity.y);
                if (!_hitOtoto)
                {

                    rig.velocity = new Vector2(-5f, rig.velocity.y);
                }
                else
                {
                    rig.AddForce(Vector2.right * 8f, ForceMode2D.Impulse);
                }
                _onLeft = true;
                break;

            case ButtonType.RIGTH:
                Debug.Log("RIGHT");
                //rig.velocity = new Vector2(5f, rig.velocity.y);
                if (!_hitOtoto)
                {
                    rig.velocity = new Vector2(5f, rig.velocity.y);
                }
                else
                {
                    rig.AddForce(Vector2.left * 8f, ForceMode2D.Impulse);
                }
                _onRight = true;
                break;

            case ButtonType.CIRCLE:
                Debug.Log("〇");
                break;

            case ButtonType.SQUERE:
                Debug.Log("□");
                if (!bringctr._Brotherhit)
                    return;

                if (!_bring)
                {
                    Debug.Log("持つ");
                    Ototo.gameObject.transform.parent = gameObject.transform;
                    Ototo.transform.position = new Vector3(gameObject.transform.position.x, 4f, 0);
                    _bring = true;
                }
                else
                {
                    Debug.Log("離す");
                    Ototo.gameObject.transform.position = new Vector2(10f, 0);
                    _bring = false;
                }
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
    }

    /// <summary>
    /// どのボタンを押されたかの処理
    /// </summary>
    private void BtnCheck()
    {
        if (Input.GetButton("Jump") && !_isJump)
        {//×ボタン or キーボードの「W」
            Move(ButtonType.JUMP);
        }
        else if (Input.GetAxis("Vertical_ps4") >= 0.15f || Input.GetKey(KeyCode.A))
        {//左ジョイスティックを左にたおす or キーボードの「A」
            Move(ButtonType.LEFT);
        }
        else if (Input.GetAxis("Vertical_ps4") <= -0.15f || Input.GetKey(KeyCode.D))
        {//左ジョイスティックを右にたおす or キーボードの「D」
            Move(ButtonType.RIGTH);
        }
        else if (Input.GetAxis("Vertical_ps4") <= 0.15f && Input.GetAxis("Vertical_ps4") >= -0.15f)
        {
            rig.velocity = new Vector2(0, rig.velocity.y);    //gameObject.transform.position.y
            Debug.Log("移動してない");
        }
        if (Input.GetButton("Squere") || Input.GetKeyDown(KeyCode.Q))
        {//□ボタン　or キーボードの「Q」
            Move(ButtonType.SQUERE);
        }
        if (Input.GetButton("Circle") || Input.GetKey(KeyCode.E))
        {//〇ボタン　or キーボードの「E」
            Move(ButtonType.CIRCLE);
        }
        if (Input.GetButton("Triangle") || Input.GetKeyDown(KeyCode.F))
        {
            Move(ButtonType.TRIANGLE);
        }
        if (Input.GetButton("L1") || Input.GetKeyDown(KeyCode.L))
        {
            Move(ButtonType.L1);
        }
        if (Input.GetButton("R1") || Input.GetKeyDown(KeyCode.K))
        {
            Move(ButtonType.R1);
        }
        if (Input.GetButton("L2") || Input.GetKeyDown(KeyCode.P))
        {
            Move(ButtonType.L2);
        }
        if (Input.GetButton("R2") || Input.GetKeyDown(KeyCode.O))
        {
            Move(ButtonType.R2);
        }
        if (Input.GetButton("Option") || Input.GetKeyDown(KeyCode.U))
        {
            Move(ButtonType.OPTION);
        }
        if (Input.GetButton("PSbtn") || Input.GetKeyDown(KeyCode.H))
        {
            Move(ButtonType.PSBTN);
        }
        if (Input.GetButton("PSpad") || Input.GetKeyDown(KeyCode.Y))
        {
            Move(ButtonType.PSPAD);
        }
        if (Input.GetAxis("CrossX") >= 0.15f || Input.GetKey(KeyCode.C))
        {
            Move(ButtonType.CROSSX_LEFT);
        }
        if (Input.GetAxis("CrossX") <= -0.15f || Input.GetKey(KeyCode.X))
        {
            Move(ButtonType.CROSSX_RIGTH);
        }
        if (Input.GetAxis("CrossY") >= 0.15f || Input.GetKey(KeyCode.Z))
        {
            Move(ButtonType.CROSSY_DOWN);
        }
        if (Input.GetAxis("CrossY") <= -0.15f || Input.GetKey(KeyCode.V))
        {
            Move(ButtonType.CROSSY_UP);
        }
        _onRight = false;
        _onLeft = false;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ototo")
        {
            Debug.Log("弟にあたった");
            rig.velocity = new Vector2(0, rig.velocity.y);
            _hitOtoto = true;
            manager.HpMinus();
        }
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "floor")
        {
            Debug.Log("floorに当たってるよ");
            _isJump = false;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if(col.gameObject.tag == "floor")
        {
            Debug.Log("floorに当たってないよ");
            _isJump = true;
        }
        if(col.gameObject.tag == "Ototo")
        {
            _hitOtoto = false;
        }
    }
}