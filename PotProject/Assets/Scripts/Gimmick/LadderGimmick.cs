using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderGimmick : MonoBehaviour {

    private bool ladderDown = false;
    private bool ladderUp = false;
    [SerializeField]
    private int ladderNum;
    private MapInfo info;


    public bool LadderDown
    {
        get { return ladderDown; }
        private set { ladderDown = value; }
    }

    public bool LadderUp
    {
        get { return ladderUp; }
        private set { ladderUp = value; }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        info = transform.root.GetComponent<MapInfo>();
        switch (gameObject.name)
        {
            case "Up":
                LadderUp = true;
                info.UpLaddersFlag[ladderNum] = true;
                break;
            case "Down":
                LadderDown = true;
                info.DownLaddersFlag[ladderNum] = true;
                break;
            default:
                break;
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        info = transform.root.GetComponent<MapInfo>();
        switch (gameObject.name)
        {
            case "Up":
                LadderUp = false;
                info.UpLaddersFlag[ladderNum] = false;
                break;
            case "Down":
                LadderDown = false;
                info.DownLaddersFlag[ladderNum] = false;
                break;
            default:
                break;
        }
        
    }
}
