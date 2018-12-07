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
    private GameObject Joints;

    // Use this for initialization
    void Start ()
    {
        _Brotherhit = false;
        _Tubohit = false;
        _bring = false;
        move_controll = gameObject.transform.parent.GetComponentInChildren<MoveController>();
        Joints = GameObject.Find("Joint/joint_0");
    }
	
	// Update is called once per frame
	void Update () {
        MoveCollider();
	}

    /// <summary>
    /// 持つ範囲コライダーを左右に合わせて移動
    /// </summary>
    private void MoveCollider()
    {
        if (move_controll.OnLeft)
        {
            gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            //Joints.transform.position = new Vector2(gameObject.transform.position.x + 2, gameObject.transform.position.y);
        }
        if (move_controll.OnRight)
        {
            gameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
            //Joints.transform.position = new Vector2(gameObject.transform.position.x - 2, gameObject.transform.position.y);
        }
        
    }

    /// <summary>
    /// 持てる範囲で□ボタンを押した時の処理
    /// </summary>
    private void SquereButton(Transform pos)
    {
        if (_Brotherhit && !_bring)
        {
            pos.position = new Vector2(gameObject.transform.transform.position.x, gameObject.transform.transform.position.y + 5f);
            _bring = true;
        }
        else
        {
            pos.transform.parent = null;
            if(move_controll.OnRight)
                pos.transform.position = new Vector2(gameObject.transform.position.x + 2, gameObject.transform.position.y);
            else
                pos.transform.position = new Vector2(gameObject.transform.position.x + 2, gameObject.transform.position.y);

            _bring = false;
            _Brotherhit = false;
        }
    }
    
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Monster")
        {
            if (!_bring)
            {
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
