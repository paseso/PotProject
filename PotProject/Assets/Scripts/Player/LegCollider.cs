using UnityEngine;

public class LegCollider : MonoBehaviour {

    private MoveController move_ctr;
    //足の部分にfloorがあったってるかどうか
    [HideInInspector]
    public bool _legFloor = false;

    

	// Use this for initialization
	void Start () {
        move_ctr = transform.parent.GetComponentInChildren<MoveController>();
        _legFloor = false;
	}

    private void OnTriggerStay2D(Collider2D col)
    {
        GimmickInfo info = col.GetComponent<GimmickInfo>();
        if (!info || info.type != GimmickInfo.GimmickType.LADDER)
        {
            move_ctr._isJump = true;
        }else if(info.type == GimmickInfo.GimmickType.LADDER)
        {
            move_ctr.IsLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        GimmickInfo info = col.GetComponent<GimmickInfo>();
        move_ctr._isJump = false;
        if (info && info.type == GimmickInfo.GimmickType.LADDER)
        {
            move_ctr.IsLadder = false;
            move_ctr.ChangeLayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "floor")
        {
            _legFloor = true;
        }
    }
}