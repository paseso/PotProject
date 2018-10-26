using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringController : MonoBehaviour {

    [HideInInspector]
    public bool _hit = false;

    // Use this for initialization
    void Start ()
    {
        _hit = false;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ototo")
        {
            Debug.Log("Collider内にOtotoが入ってます");
            _hit = true;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if(col.gameObject.tag == "Ototo" && _hit)
        {
            _hit = false;
        }
    }
}
