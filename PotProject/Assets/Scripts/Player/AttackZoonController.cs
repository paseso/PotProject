using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AttackZoonController : MonoBehaviour {

    private MoveController move_ctr;
    [HideInInspector]
    public GameObject Attack_Target;
    [SerializeField, Header("殴れるオブジェクトがx方向に飛ぶ距離")]
    private float Impalce_x = 50;
    [SerializeField, Header("殴れるオブジェクトがy方向に飛ぶ距離")]
    private float Impalce_y = 50;

	// Use this for initialization
	void Start () {
        move_ctr = gameObject.transform.parent.GetComponent<MoveController>();
        Attack_Target = null;
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
        if (move_ctr._onLeft)
        {
            gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        if (move_ctr._onRight)
        {
            gameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
        }
    }

    /// <summary>
    /// オブジェクトを殴った時の処理
    /// </summary>
    /// <param name="obj">殴るオブジェクト</param>
    public void AttackObject()
    {
        if (!move_ctr._onCircle || Attack_Target == null)
            return;

        Rigidbody2D target_rig = Attack_Target.GetComponent<Rigidbody2D>();
        target_rig.AddForce(new Vector2(-Impalce_x, target_rig.velocity.y + Impalce_y), ForceMode2D.Impulse);
        Attack_Target = null;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.tag == "Monster")
        {
            move_ctr._onCircle = true;
            Debug.Log("殴れるよ");
            Attack_Target = col.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        move_ctr._onCircle = false;
    }
}
