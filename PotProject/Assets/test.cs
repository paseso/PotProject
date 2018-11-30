using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

    public GameObject obj;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.T))
        {
            AddComp(obj);
        }
	}

    void AddComp(GameObject obj)
    {
        obj.AddComponent<SpriteRenderer>();
    }
}
