using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float speed = 0f;

    private Rigidbody2D rig;
    private bool _bring = false;
    private bool _isJump = false;
    private bool _wait = false;
    [SerializeField,Header("弟")]
    private GameObject Ototo;
    private BringController Col;

    private float fallspeed = 0.0f;
    private Vector2 charaMove;
    private RectTransform rectgameObject;
    [SerializeField]
    private GameObject managerGameObject;
    private PlayerManager manager;
    private BringController bringctr;
    

    // Use this for initialization
    void Start()
    {
        rig = gameObject.GetComponent<Rigidbody2D>();
        Col = gameObject.transform.GetChild(0).GetComponent<BringController>();
        charaMove = Vector2.zero;
        rectgameObject = gameObject.GetComponent<RectTransform>();
        manager = managerGameObject.GetComponent<PlayerManager>();
        bringctr = gameObject.transform.GetChild(0).GetComponent<BringController>();
        _bring = false;
        _isJump = false;
        _wait = false;
	}

    private void FixedUpdate()
    {
        rig.AddForce(Vector2.down * 1000, ForceMode2D.Force);
    }

    // Update is called once per frame
    void Update () {

        if (_wait)
        {
            CoolTime(1.0f);
        }
        Move();
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
    /// プレイヤーの操作
    /// </summary>
    private void Move()
    {
        if (Input.GetButton("Jump") && !_isJump)
        {//×ボタン or キーボードの「W」
            Debug.Log("JUMP");
            rig.AddForce(Vector2.up * 1f * speed, ForceMode2D.Force);
            //rig.velocity += new Vector2(0, gameObject.transform.position.y + 1f * speed);
            //rectgameObject.DOJump(new Vector2(gameObject.transform.position.x + 2f, gameObject.transform.position.y + 2f), 5f, 1, 2f).SetEase(Ease.Linear);


            //gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 10f);
            //charaMove.y +=  2f;
            _wait = true;
        }
        else if (Input.GetAxis("Vertical_ps4") >= 0.15f || Input.GetKey(KeyCode.A))
        {//左ジョイスティックを左にたおす or キーボードの「A」
            Debug.Log("LEFT");
            rig.velocity = new Vector2(-20f, gameObject.transform.position.y);
            if (gameObject.transform.localRotation.y == -180)
            {
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else if (Input.GetAxis("Vertical_ps4") <= -0.15f || Input.GetKey(KeyCode.D))
        {//左ジョイスティックを右にたおす or キーボードの「D」
            Debug.Log("RIGHT");
            rig.velocity = new Vector2(20f, gameObject.transform.position.y);
            if(gameObject.transform.localRotation.y == 0f)
            {
                gameObject.transform.rotation = Quaternion.Euler(0, -180, 0);
            }
        }
        else if (Input.GetAxis("Vertical_ps4") <= 0.15f && Input.GetAxis("Vertical_ps4") >= -0.15f)
        {
            rig.velocity = new Vector2(0, 0);
        }
        if (Input.GetButton("Squere") || Input.GetKeyDown(KeyCode.Q)) //
        {//□ボタン　or キーボードの「Q」
            Debug.Log("Squere");
            if (bringctr._Brotherhit)
            {
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
                    //_hit = false;
                    _bring = false;
                }
            }
            
        }
        if (Input.GetButton("Circle") || Input.GetKey(KeyCode.E))
        {//〇ボタン　or キーボードの「E」
            Debug.Log("まる");
        }
        if (Input.GetKey(KeyCode.C))
        {
            manager.SwordTypeChange(Status.SWORDTYPE.EARTH);
        }
        if (Input.GetKey(KeyCode.X))
        {
            manager.SwordTypeChange(Status.SWORDTYPE.WATER);
        }
        if (Input.GetKey(KeyCode.Z))
        {
            manager.SwordTypeChange(Status.SWORDTYPE.FIRE);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ototo")
        {
            Debug.Log("弟にあたった");
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
    }
}