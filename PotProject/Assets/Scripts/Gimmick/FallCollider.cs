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
        }
    }

}
