using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D rig;
    private bool _bring = false;
    private bool _hit = false;
    [SerializeField, Header("Canvas")]
    private Transform canvasTransform;
    [SerializeField,Header("弟")]
    private GameObject Ototo;


    // Use this for initialization
    void Start()
    {
        rig = gameObject.GetComponent<Rigidbody2D>();
        _bring = false;
        _hit = false;
	}

    private void FixedUpdate()
    {
        rig.AddForce(Vector2.down * 200, ForceMode2D.Force);
    }

    // Update is called once per frame
    void Update () {
        Move();
	}

    /// <summary>
    /// プレイヤーの操作
    /// </summary>
    private void Move()
    {
        if (Input.GetButton("Jump"))
        {//×ボタン or キーボードの「W」
            Debug.Log("JUMP");
            rig.velocity = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 20f);
            //gameObject.transform.up = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 0.2f); gameObject.transform.position.x
            //gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 0.2f);
        }
        else if (Input.GetAxis("Vertical_ps4") >= 0.15f || Input.GetKey(KeyCode.A))
        {//左ジョイスティックを左にたおす or キーボードの「A」
            Debug.Log("LEFT");
            rig.velocity = new Vector2(-8f, 0);
            //gameObject.transform.forward = new Vector2(gameObject.transform.position.x - 0.1f, gameObject.transform.position.y);
            //gameObject.transform.position = new Vector2(gameObject.transform.position.x - 0.15f, gameObject.transform.position.y);
        }
        else if (Input.GetAxis("Vertical_ps4") <= -0.15f || Input.GetKey(KeyCode.D))
        {//左ジョイスティックを右にたおす or キーボードの「D」
            Debug.Log("RIGHT");
            rig.velocity = new Vector2(8f, 0);
            //gameObject.transform.forward = new Vector2(gameObject.transform.position.x + 0.1f, gameObject.transform.position.y);
            //gameObject.transform.position = new Vector2(gameObject.transform.position.x + 0.15f, gameObject.transform.position.y);
        }
        else if (Input.GetAxis("Vertical_ps4") <= 0.15f && Input.GetAxis("Vertical_ps4") >= -0.15f)
        {
            rig.velocity = new Vector2(0, 0);
        }
        else if (Input.GetButton("Squere") && _hit)
        {//□ボタン　or キーボードの「Q」
            if (!_bring)
            {
                _bring = true;
                Ototo.transform.position = canvasTransform.position;
                Debug.Log("_bring = true");
            }
            else
            {
                _hit = false;
                _bring = false;

            }
            
        }
        else if (Input.GetButton("Circle") || Input.GetKey(KeyCode.E))
        {//〇ボタン　or キーボードの「E」
            Debug.Log("まる");
        }
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if(col.gameObject.tag == "Ototo")
        {
            Debug.Log("Ototoに当たってるよ");
            if (Input.GetButton("Squere") && !_hit)
            {
                Debug.Log("GET");
                col.gameObject.transform.parent = gameObject.transform;
                col.transform.position = new Vector3(gameObject.transform.position.x, 5f, 0);
                _hit = true;
            }
        }
    }
}