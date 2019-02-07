using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallCollider : MonoBehaviour {

    public void OnTriggerStay2D(Collider2D col) {
        
        if (col.gameObject.name == "Leg") {
            transform.parent.GetComponent<FallBlock>().SetRandingFlag = true;
        }
    }

    public void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.name == "Leg") {
            var block = transform.parent.GetComponent<FallBlock>();
            block.SetRandingFlag = false;
            block.SetTime = 0;
            block.SetShakeFlag = false;
            //if(block.State != FallBlock.fallState.fall) {
            //    block.State = FallBlock.fallState.normal;
            //    StartCoroutine(block.Floating());
            //}
        }
    }
}
