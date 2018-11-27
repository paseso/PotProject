using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderTest : MonoBehaviour {
    private Status status;

	// Use this for initialization
	void Start () {
        status = GetComponent<Status>();
	}

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<GimmickInfo>())
        {
            switch (col.GetComponent<GimmickInfo>().type)
            {
                case GimmickInfo.GimmickType.LADDER:
                    status.state = Status.State.ONLADDER;
                    break;
                case GimmickInfo.GimmickType.GROWTREE:
                    status.state = Status.State.ONTREE;
                    break;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.GetComponent<GimmickInfo>())
        {
            if (status.state != Status.State.NORMAL)
            {
                status.state = Status.State.NORMAL;
            }
            if (gameObject.layer != LayerMask.NameToLayer("Player"))
            {
                gameObject.layer = LayerMask.NameToLayer("Player");
            }
        }
    }
}
