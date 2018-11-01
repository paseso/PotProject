using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

    private Rigidbody2D rig;
    public float speed = 0;

	// Use this for initialization
	void Start () {
        rig = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rig.AddForce(Vector2.up * 1f * speed, ForceMode2D.Force);
        }
	}
}
