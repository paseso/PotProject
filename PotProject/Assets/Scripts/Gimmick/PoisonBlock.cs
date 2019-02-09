using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonBlock : MonoBehaviour {
    PlayerController pCon;
	// Use this for initialization
	void Start () {
        pCon = GameObject.Find("Controller").GetComponent<PlayerController>();
	}
	
    public void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player")) {
            pCon.HPDown(6);
        }
    }
}
