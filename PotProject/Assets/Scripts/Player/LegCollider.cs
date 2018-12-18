using UnityEngine;

public class LegCollider : MonoBehaviour {

    private MoveController move_ctr;
    private PlayerController player_ctr;
    //足の部分にfloorがあったってるかどうか
    [HideInInspector]
    public bool _legFloor = false;

    private float falldistance = 0f;

	// Use this for initialization
	void Start () {
        falldistance = gameObject.transform.localPosition.y;
        move_ctr = transform.parent.GetComponentInChildren<MoveController>();
        player_ctr = GameObject.Find("Controller").GetComponent<PlayerController>();
        _legFloor = false;
	}

    private void Update()
    {
        FallCheck();
    }
    
    /// <summary>
    /// 落ちた時にHPを減らすかどうかのチェック
    /// </summary>
    private void FallCheck()
    {
        Debug.Log("Jumping: " + move_ctr.Jumping);
        if (move_ctr.Jumping)
        {
            falldistance = gameObject.transform.localPosition.y;
        }
        else
        {

        }
        //if (player_ctr.status.state != Status.GimmickState.ONLADDER)
        //    return;
        if (gameObject.transform.localPosition.y == falldistance)
        {
            return;
        }
    }

    private void check()
    {
        float now = gameObject.transform.localPosition.y;
        Debug.Log("falldistance: " + falldistance);
        if ((now - falldistance) >= -2f)
        {
            Debug.Log("dir: " + (now - falldistance));
            
            Debug.Log("y = " + now);
            player_ctr.ApplyHp(1);
            falldistance = gameObject.transform.localPosition.y;
        }
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
        check();
    }
}