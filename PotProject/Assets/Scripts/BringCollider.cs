using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringCollider : MonoBehaviour {

    [HideInInspector]
    public bool _Brotherhit = false;
    [HideInInspector]
    public bool _Tubohit = false;
    private bool _bring = false;
    private MoveController move_controll;

    // Use this for initialization
    void Start ()
    {
        _Brotherhit = false;
        _Tubohit = false;
        _bring = false;
        move_controll = gameObject.transform.parent.GetComponent<MoveController>();
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    /// <summary>
    /// 持てる範囲で□ボタンを押した時の処理
    /// </summary>
    private void SquereButton(Transform pos)
    {
        if (_Brotherhit && !_bring)
        {
            Debug.Log("いける！");
            if (move_controll._onSquere)
            {
                Debug.Log("On Squere!");
                pos.localPosition = gameObject.transform.transform.position;
                pos.parent = gameObject.transform.parent;
                _bring = true;
            }
        }
        else
        {
            pos.transform.parent = null;
            pos.transform.position = new Vector2(gameObject.transform.parent.transform.position.x, gameObject.transform.parent.transform.position.y + 5);
            _bring = false;
            _Brotherhit = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ototo")
        {
            Debug.Log("Collider内にOtotoが入ってます");
            _Brotherhit = true;
            SquereButton(col.transform);
        }
        else if(col.gameObject.tag == "Tube")
        {
            _Tubohit = true;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if(col.gameObject.tag == "Ototo" && !_Brotherhit)
        {
            gameObject.transform.position = col.transform.position;

            _Brotherhit = true;
        }
    }
}
