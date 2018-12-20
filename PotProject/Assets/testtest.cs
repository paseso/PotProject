using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testtest : MonoBehaviour {

    [SerializeField]
    GameObject player;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setParent(GameObject parent)
    {
        GameObject broOld = Instantiate(player);
        broOld.transform.SetParent(parent.transform);

    }
}
