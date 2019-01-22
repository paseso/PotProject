﻿using System.Collections;
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

    private bool keyFlag = false;

    private MoveController.Direction dir;

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
        Debug.Log("KeyFlag=" + move_ctr.keyDoorFlag);
    }

    /// <summary>
    /// 持つ範囲コライダーと剣を左右に合わせて移動
    /// </summary>
    private void DirecControl()
    {
        if (dir == move_ctr.direc)
            return;
        if (move_ctr.direc == MoveController.Direction.LEFT)
        {
            gameObject.transform.localPosition = new Vector2(0, gameObject.transform.localPosition.y);
            dir = MoveController.Direction.LEFT;
        }
        else
        {
            gameObject.transform.localPosition = new Vector2(2.6f, gameObject.transform.localPosition.y);
            dir = MoveController.Direction.RIGHT;
        }
    }

    /// <summary>
    /// 攻撃
    /// </summary>
    public void Attack()
    {
        Debug.Log(status.PlayerAttack);
        Debug.Log("AttackTarget = " + Attack_Target.name);
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
            Debug.Log("はいったよ");
            return;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        _attackMonster = false;
        if(col.gameObject.tag == "Monster")
        {
            Attack_Target = null;
            return;
        }
    }
}
