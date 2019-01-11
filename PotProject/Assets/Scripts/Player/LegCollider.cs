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
        if ((falldistance - now) >= 2.5f)
        {
            Debug.Log("dir = " + (falldistance - now));
            player_ctr.HPDown(2);
            falldistance = gameObject.transform.position.y;
        }
    }

    public bool JumpCheck(GameObject col)
    {
        if (!col.GetComponent<GimmickInfo>()) { return false; }
        GimmickInfo info = col.GetComponent<GimmickInfo>();
        if (col.gameObject.layer != LayerMask.NameToLayer("Background")) { return false; }
        if(info.type != GimmickInfo.GimmickType.LADDER) { return false; }
        if (info.type != GimmickInfo.GimmickType.LADDERTOP) { return false; }
        return true;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        GimmickInfo info = col.GetComponent<GimmickInfo>();
        
        if (JumpCheck(col.gameObject))
        {
            move_ctr._isJump = true;
        }else if(info.type == GimmickInfo.GimmickType.LADDERTOP)
        {
            move_ctr.OnLadder = true;
        }else {
            return;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        
        GimmickInfo info = col.GetComponent<GimmickInfo>();
        move_ctr._isJump = false;
        if (info && info.type == GimmickInfo.GimmickType.LADDER)
        {
            move_ctr.IsLadder = false;
        }

        if (info && info.type == GimmickInfo.GimmickType.LADDERTOP) {
            Debug.Log("This = " + transform.localPosition.y);
            Debug.Log("Col = " + col.transform.localPosition.y);
            
            player_ctr.ChangeLayer();
            move_ctr.OnLadder = false;
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