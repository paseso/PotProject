using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZoneController : MonoBehaviour {

    private PlayerStatus status;
    private MoveController move_ctr;
    [HideInInspector]
    public GameObject Attack_Target;
    [SerializeField, Header("殴れるオブジェクトがx方向に飛ぶ距離")]
    private float Impalce_x = 3;
    [SerializeField, Header("殴れるオブジェクトがy方向に飛ぶ距離")]
    private float Impalce_y = 6;
    private GameObject PlayerObject;

    private enum Direction
    {
        LEFT,
        RIGHT,
    }

    private Direction dir;

    //モンスターをアタックできるかどうか
    private bool _attackMonster = false;

    public bool AttackMonster
    {
        get { return _attackMonster; }
    }

	// Use this for initialization
	void Start () {
        move_ctr = gameObject.transform.parent.GetComponentInChildren<MoveController>();
        PlayerObject = gameObject.transform.parent.gameObject;
        Attack_Target = null;
        _attackMonster = false;
	}
	
	// Update is called once per frame
	void Update () {
        DirecControl();
    }

    /// <summary>
    /// 持つ範囲コライダーと剣を左右に合わせて移動
    /// </summary>
    private void DirecControl()
    {
        if (move_ctr.OnLeft)
        {
            dir = Direction.LEFT;
            gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        if (move_ctr.OnRight)
        {
            dir = Direction.RIGHT;
            gameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        //MoveCollider();
    }

    private void MoveCollider()
    {
        if(dir == Direction.LEFT)
        {
            gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        }else if(dir == Direction.RIGHT)
        {
            gameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
        }
    }

    /// <summary>
    /// 攻撃
    /// </summary>
    public void Attack()
    {
        if(Attack_Target == null) { return; }
        Attack_Target.GetComponent<MonsterController>().Damage(status.PlayerAttack);
    }

    /// <summary>
    /// オブジェクトが待つ処理
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitObject()
    {
        yield return new WaitForSeconds(0.5f);
        move_ctr._ActiveRightLeft = true;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.tag == "Monster")
        {
            _attackMonster = true;
            Attack_Target = col.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        _attackMonster = false;
        if(col.gameObject.tag == "Monster")
        {
            Attack_Target = null;
        }
    }
}
