using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringController : MonoBehaviour {

    [HideInInspector]
    public bool _Brotherhit = false;
    [HideInInspector]
    public bool _Tubohit = false;


    // Use this for initialization
    void Start ()
    {
        _Brotherhit = false;
        _Tubohit = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D col)
    {
        
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ototo")
        {
            Debug.Log("Collider内にOtotoが入ってます");
            _Brotherhit = true;
        }else if(col.gameObject.tag == "Tube")
        {
            _Tubohit = true;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if(col.gameObject.tag == "Ototo" && _Brotherhit)
        {
            _Brotherhit = false;
        }
    }
}
