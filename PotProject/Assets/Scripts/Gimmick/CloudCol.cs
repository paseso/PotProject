using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudCol : MonoBehaviour {

    private void OnTriggerStay2D(Collider2D col) {
        if (col.gameObject.name == "Leg") {
            GameObject player = col.transform.parent.transform.parent.gameObject;
            player.transform.SetParent(transform);
            IsMovePosSet(player);
        }
    }

    private void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.name == "Leg") {
            GameObject player = col.transform.parent.transform.parent.gameObject;
            player.transform.SetParent(transform.root.transform);
        }
    }

    void IsMovePosSet(GameObject obj) {
        obj.transform.localPosition = new Vector2(transform.localPosition.x, obj.transform.localPosition.y);
    }
}
