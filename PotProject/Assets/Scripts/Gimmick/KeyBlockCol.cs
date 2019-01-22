using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBlockCol : MonoBehaviour {
    private MoveController mController;
	// Use this for initialization
	void Start () {
        mController = FindObjectOfType<MoveController>();
	}

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.GetComponent<MoveController>()) {
            mController.keyDoorFlag = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col) {
        if (col.GetComponent<MoveController>()) {
            mController.keyDoorFlag = false;
        }
    }
}
