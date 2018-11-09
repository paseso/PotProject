using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapChangeTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("kitao");
        if (col.gameObject.tag == "Player")
        {
            col.transform.SetParent(transform.root.gameObject.transform);
        }
    }
}
