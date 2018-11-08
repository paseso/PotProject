using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringCollider : MonoBehaviour {

    [HideInInspector]
    public bool _Brotherhit = false;
    [HideInInspector]
    public bool _Tubohit = false;
    [HideInInspector]
    public bool _bring = false;
    private MoveController move_controll;
    private GameObject target;

    // Use this for initialization
    void Start ()
    {
        target = null;
        _Brotherhit = false;
        _Tubohit = false;
        _bring = false;
        move_controll = gameObject.transform.parent.GetComponent<MoveController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (_Tubohit)
        {
            SquereTubo();
        }
        MoveCollider();
	}

    /// <summary>
    /// 持つ範囲コライダーを左右に合わせて移動
    /// </summary>
    private void MoveCollider()
    {
        if (move_controll._onLeft)
        {
            gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        if (move_controll._onRight)
        {
            gameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
        }
    }


    /// <summary>
    /// 持てる範囲で□ボタンを押した時の処理
    /// </summary>
    private void SquereButton(Transform pos)
    {
        Debug.Log("On Squere!");
        if (_Brotherhit && !_bring)
        {
            Debug.Log("持つる！");
            pos.position = new Vector2(gameObject.transform.transform.position.x, gameObject.transform.transform.position.y + 5f);
            _bring = true;
        }
        else
        {
            pos.transform.parent = null;
            pos.transform.position = new Vector2(gameObject.transform.parent.transform.position.x + 2, gameObject.transform.parent.transform.position.y);
            _bring = false;
            _Brotherhit = false;
        }
    }

    private void SquereTubo()
    {
        if (!move_controll._onSquere && !_bring)
            return;


    }

    private void OnTriggerStay2D(Collider2D col)
    {
        //gameObject.transform.parent.position = new Vector2();
        if (col.gameObject.tag == "Ototo" || col.gameObject.tag == "Monster")
        {
            if (!_bring)
            {
                Debug.Log("Collider内にOtotoが入ってます");
                move_controll.target = col.gameObject;
                _Brotherhit = true;
            }
        }
        if (col.gameObject.tag == "Tubo")
        {
            _Tubohit = true;
            col.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "Tubo")
        {
            _Tubohit = false;
            col.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
