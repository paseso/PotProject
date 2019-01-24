using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudCol : MonoBehaviour {
    private GameObject player;

    void SetColParent(GameObject player) {
        player.transform.SetParent(transform);
    }

    private void OnTriggerStay2D(Collider2D col) {
        if (col.gameObject.name == "Leg") {
            player = col.transform.parent.transform.parent.gameObject;
            player.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.name == "Leg") {
            GameObject player = col.transform.parent.transform.parent.gameObject;
            player.transform.SetParent(transform.root.transform);
        }
    }
}
