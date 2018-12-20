using UnityEngine;

public class LegCollider : MonoBehaviour {

    private MoveController move_ctr;
    private PlayerController player_ctr;
    //足の部分にfloorがあったってるかどうか
    [HideInInspector]
    public bool _legFloor = false;
    private float now = 0f;
    private float falldistance = 0f;

	// Use this for initialization
	void Start () {
        falldistance = gameObject.transform.position.y;
        move_ctr = transform.parent.GetComponentInChildren<MoveController>();
        player_ctr = GameObject.Find("Controller").GetComponent<PlayerController>();
        _legFloor = false;
	}
    
    /// <summary>
    /// 落ちた時にHPを減らすかどうかの処理
    /// </summary>
    private void FallCheck()
    {
        now = gameObject.transform.position.y;
        if ((falldistance - now) >= 2f)
        {
            Debug.Log("dir = " + (falldistance - now));
            player_ctr.ApplyHp(1);
            falldistance = gameObject.transform.position.y;
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
        falldistance = gameObject.transform.position.y;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "floor")
        {
            _legFloor = true;
        }
        
        if (gameObject.transform.localPosition.y != falldistance)
        {
            if(col.gameObject.tag == "floor" || col.gameObject.tag == "Untagged")
            {
                FallCheck();
            }
        }
    }
}