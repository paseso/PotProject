using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

    public enum tsts
    {
        aaa,
        iii,
        uuu
    };

    //[SerializeField]private int num = 0;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("押した");
        }
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("押してる");
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("離した");
        }        
	}
}
