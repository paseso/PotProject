using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonBlock : MonoBehaviour {
    PlayerController pCon;
	// Use this for initialization
	void Start () {
        pCon = GameObject.Find("Controller").GetComponent<PlayerController>();
	}
	
    public void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player")) {
            pCon.HPDown(6);
        }
    }
}
