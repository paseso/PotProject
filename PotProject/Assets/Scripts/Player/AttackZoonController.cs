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
    [SerializeField]
    private PlayerController manager;
    [SerializeField]
    private GameObject Sword;
    //モンスターをアタックできるかどうか
    private bool _attackMonster = false;

    public bool AttackMonster
    {
        get { return _attackMonster; }
    }

	// Use this for initialization
	void Start () {
        move_ctr = gameObject.transform.parent.GetComponent<MoveController>();
        Attack_Target = null;
        _attackMonster = false;
	}
	
	// Update is called once per frame
	void Update () {
        MoveCollider();
    }

    /// <summary>
    /// 持つ範囲コライダーと剣を左右に合わせて移動
    /// </summary>
    private void MoveCollider()
    {
        if (move_ctr.OnLeft)
        {
            //Sword.transform.position = new Vector2(Sword.transform.position.x - 0.9f, Sword.transform.position.y);
            Sword.transform.rotation = Quaternion.Euler(0, 0, 51);
            gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        if (move_ctr.OnRight)
        {
            //Sword.transform.position = new Vector2(Sword.transform.position.x + 0.9f, Sword.transform.position.y);
            Sword.transform.rotation = Quaternion.Euler(0, 0, -51);
            gameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
        }
    }

    /// <summary>
    /// オブジェクトを殴った時の処理
    /// </summary>
    /// <param name="obj">殴るオブジェクト</param>
    public void AttackObject()
    {
        if (!move_ctr.OnCircle || Attack_Target == null)
            return;

        Rigidbody2D target_rig = Attack_Target.GetComponent<Rigidbody2D>();
        target_rig.AddForce(new Vector2(-Impalce_x, target_rig.velocity.y + Impalce_y), ForceMode2D.Impulse);
        Attack_Target = null;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.tag == "Monster")
        {
            _attackMonster = true;
            Debug.Log("殴れるよ");
            Attack_Target = col.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        _attackMonster = false;
    }
}
