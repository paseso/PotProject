using UnityEngine;

public class LegCollider : MonoBehaviour {

    private MoveController move_ctr;
    private PlayerController player_ctr;
    private PlayerStatus status;
    private bool landingFlag = false;
    float jumpPos;
    int onGroundCount;

    /// <summary>
    /// ジャンプフラグ(落下判定)
    /// </summary>
    public bool isLanding
    {
        get { return landingFlag; }
        set
        {
            Debug.Log("Call " + value);
            if (value)
            {
                if (jumpPos - transform.position.y >= 2.5f)
                {
                    player_ctr.HPDown(2);
                }
                landingFlag = value;
            }
            else
            {
                jumpPos = transform.position.y;
                landingFlag = value;
            }
        }
    }

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
        //if (gameObject.layer == LayerMask.NameToLayer("LadderPlayer"))
        //    return;
        //now = gameObject.transform.position.y;
        //if ((falldistance - now) >= 2.5f)
        //{
        //    Debug.Log("dir = " + (falldistance - now));
        //    player_ctr.HPDown(2);
        //    falldistance = gameObject.transform.position.y;
        //}
    }

    /// <summary>
    /// ジャンプ中の左右移動制限
    /// </summary>
    public void JumpingMove(MoveController.Direction dir)
    {
        if (!move_ctr.Jumping)
        {
            move_ctr._notLeft = false;
            move_ctr._notRight = false;
            return;
        }
        if (dir == MoveController.Direction.LEFT)
        {
            move_ctr._notLeft = true;
            move_ctr._notRight = false;
        }
        else
        {
            move_ctr._notLeft = false;
            move_ctr._notRight = true;
        }
    }

    public bool JumpCheck(GameObject col)
    {
        if (col.gameObject.layer == 2) { return false; }
        if (!col.GetComponent<GimmickInfo>()) { return true; }
        GimmickInfo info = col.GetComponent<GimmickInfo>();
        if (info.type == GimmickInfo.GimmickType.LADDER) { return false; }
        if (info.type == GimmickInfo.GimmickType.FIREFIELD) { return false; }

        return true;
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if(!isLanding) onGroundCount+=2;
        else onGroundCount++;

        isLanding = true;
        if (move_ctr.Jumping)
        {
            move_ctr.setJumping = false;
        }

        if (col.gameObject.tag == "floor") {
            _legFloor = true;
        }

        //if (gameObject.transform.localPosition.y != falldistance) {
        //    if(!col.GetComponent<GimmickInfo>() )
        //    if (col.gameObject.tag == "floor" || col.gameObject.tag == "Untagged") {
        //        FallCheck();
        //    }
        //}

        if (gameObject.layer == LayerMask.NameToLayer("LadderPlayer") && col.gameObject.layer == LayerMask.NameToLayer("Block")) {
            
            player_ctr.ChangeLayer();
            move_ctr.InLadderCount = 0;
        }
        if (col.gameObject.layer == LayerMask.NameToLayer("Block")) {
            player_ctr.OnBlock = col.gameObject;
        }
        

        if (!col.GetComponent<GimmickInfo>()) { return; }
        GimmickInfo info = col.GetComponent<GimmickInfo>();
        if (info.type == GimmickInfo.GimmickType.LADDER)
        {
            move_ctr.InLadderCount++;
        }
        player_ctr.OnBlock = null;
    }

    //private void OnTriggerStay2D(Collider2D col)
    //{
    //    GimmickInfo info = col.GetComponent<GimmickInfo>();

    //    if (JumpCheck(col.gameObject))
    //    {
    //        isLanding = true;

    //    }
    //    else
    //    {
    //        return;
    //    }
    //}

    private void OnTriggerExit2D(Collider2D col)
    {
        onGroundCount--;
        if(onGroundCount <= 0)
        {
            onGroundCount = 0;
            isLanding = false;
        }
        falldistance = gameObject.transform.position.y;
        isLanding = false;
        if (!col.GetComponent<GimmickInfo>()) { return; }
        GimmickInfo info = col.GetComponent<GimmickInfo>();
        if (info.type == GimmickInfo.GimmickType.LADDER) {
            
            move_ctr.InLadderCount--;
            if (move_ctr.InLadderCount <= 0) {
                move_ctr.InLadderCount = 0;
                player_ctr.ChangeLayer();
            }
        }
    }

   
}