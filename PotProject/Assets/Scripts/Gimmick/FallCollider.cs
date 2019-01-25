using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallCollider : MonoBehaviour {

    public void OnTriggerStay2D(Collider2D col) {
        
        if (col.gameObject.name == "Leg") {
            transform.parent.GetComponent<FallBlock>().SetFallFlag = true;
        }
    }

    public void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.name == "Leg") {
            transform.parent.GetComponent<FallBlock>().SetFallFlag = false;
            transform.parent.GetComponent<FallBlock>().SetTime = 0;
            transform.parent.GetComponent<FallBlock>().SetShakeFlag = false;
        }
    }
}
