using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderGimmick : MonoBehaviour {

    private bool ladderDown = false;
    private bool ladderUp = false;


    public bool LadderDown
    {
        get { return ladderDown; }
        set { ladderDown = value; }
    }

    public bool LadderUp
    {
        get { return ladderUp; }
        set { ladderUp = value; }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        switch (gameObject.name)
        {
            case "Up":
                break;
            case "Down":
                break;
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {

    }
}
