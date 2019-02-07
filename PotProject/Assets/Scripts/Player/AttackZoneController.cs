﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZoneController : MonoBehaviour {

    private MoveController move_ctr;
    [HideInInspector]
    public GameObject Attack_Target;

    private PlayerManager pManager;

    private MoveController.Direction dir;

    //モンスターをアタックできるかどうか
    private bool _attackMonster = false;

    public bool AttackMonster
    {
        get { return _attackMonster; }
    }

	// Use this for initialization
	void Start () {
        pManager = FindObjectOfType<PlayerManager>();
        move_ctr = gameObject.transform.parent.GetComponentInChildren<MoveController>();
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
        
        if (Attack_Target == null) { return; }

        if (Attack_Target.GetComponent<GimmickController>()) {
            Attack_Target.GetComponent<GimmickController>().BakeTree();
            return;
        }

        MonsterController MonsterController = Attack_Target.GetComponent<MonsterController>();
        MonsterController.Damage(pManager.Status.PlayerAttack);
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
            Debug.Log(Attack_Target);
            return;
        }

        if(col.GetComponent<GimmickInfo>() && col.GetComponent<GimmickInfo>().type == GimmickInfo.GimmickType.BAKETREE) {
            Attack_Target = col.gameObject;
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

        if (col.GetComponent<GimmickInfo>() && col.GetComponent<GimmickInfo>().type == GimmickInfo.GimmickType.BAKETREE) {
            Attack_Target = null;
        }
    }
}
