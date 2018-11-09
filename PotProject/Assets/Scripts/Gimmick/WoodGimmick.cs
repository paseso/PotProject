using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodGimmick : MonoBehaviour {
    private bool glowFlag = false;
    
    public bool GlowFlag
    {
        get { return glowFlag; }
        private set { glowFlag = value; }
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter2D(Collider2D col)
    {
        glowFlag = true;
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        glowFlag = false;
    }
}
