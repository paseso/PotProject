using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderTest : MonoBehaviour {
    private Status status;

	// Use this for initialization
	void Start () {
        
	}

    void Update()
    {
        Debug.Log("LadderTest=" + status.state);
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<GimmickInfo>())
        {
            switch (col.GetComponent<GimmickInfo>().type)
            {
                case GimmickInfo.GimmickType.LADDER:
                    status.state = Status.GimmickState.ONLADDER;
                    break;
                case GimmickInfo.GimmickType.GROWTREE:
                    status.state = Status.GimmickState.ONTREE;
                    break;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.GetComponent<GimmickInfo>())
        {
            if (col.GetComponent<GimmickInfo>().type == GimmickInfo.GimmickType.LADDER)
            {
                status.state = Status.GimmickState.NORMAL;
            }
            if (gameObject.layer != LayerMask.NameToLayer("Player"))
            {
                gameObject.layer = LayerMask.NameToLayer("Player");
            }
        }
    }
}
