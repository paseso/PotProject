using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZoonController : MonoBehaviour {

    private MoveController move_ctr;
    private GameObject Attack_Target;

	// Use this for initialization
	void Start () {
        move_ctr = gameObject.transform.parent.GetComponent<MoveController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionStay2D(Collision2D col)
    {
        if(col.gameObject.tag == "Monster")
        {
            move_ctr._onCircle = true;
            Debug.Log("殴れるよ");
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        move_ctr._onCircle = false;
    }
}
