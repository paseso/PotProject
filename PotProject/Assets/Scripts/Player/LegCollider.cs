using UnityEngine;

public class LegCollider : MonoBehaviour {

    private MoveController move_ctr;
    private PlayerController player_ctr;
    private PlayerStatus status;
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
        if (status.gimmick_state == PlayerStatus.GimmickState.ONLADDER)
            return;
        now = gameObject.transform.position.y;
        if ((falldistance - now) >= 2.5f)
        {
            Debug.Log("dir = " + (falldistance - now));
            player_ctr.HPDown(2);
            falldistance = gameObject.transform.position.y;
        }
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
        if (col.gameObject.layer == LayerMask.NameToLayer("Background")) { return false; }
        if (!col.GetComponent<GimmickInfo>()) { return true; }
        GimmickInfo info = col.GetComponent<GimmickInfo>();
        if (info.type == GimmickInfo.GimmickType.LADDER) { return false; }
        if (info.type == GimmickInfo.GimmickType.FIREFIELD) { return false; }

        return true;
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (move_ctr.Jumping)
        {
            move_ctr.setJumping = false;
        }

        if (col.gameObject.tag == "floor") {
            _legFloor = true;
        }

        if (gameObject.transform.localPosition.y != falldistance) {
            if (col.gameObject.tag == "floor" || col.gameObject.tag == "Untagged") {
                FallCheck();
            }
        }

        if (gameObject.layer == LayerMask.NameToLayer("LadderPlayer") && col.gameObject.layer == LayerMask.NameToLayer("Block")) {
            
            player_ctr.ChangeLayer();
            move_ctr.InLadderCount = 0;
        }

        if (!col.GetComponent<GimmickInfo>()) { return; }
        GimmickInfo info = col.GetComponent<GimmickInfo>();
        if (info.type == GimmickInfo.GimmickType.LADDER)
        {
            move_ctr.InLadderCount++;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        GimmickInfo info = col.GetComponent<GimmickInfo>();
        
        if (JumpCheck(col.gameObject))
        {
            move_ctr._isJump = true;
        
        }else {
            return;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        move_ctr._isJump = false;
        if (!col.GetComponent<GimmickInfo>()) { return; }
        GimmickInfo info = col.GetComponent<GimmickInfo>();
        if (info.type == GimmickInfo.GimmickType.LADDER) {
            
            move_ctr.InLadderCount--;
            if (move_ctr.InLadderCount <= 0) {
                move_ctr.InLadderCount = 0;
                player_ctr.ChangeLayer();
            }
        }
        
        falldistance = gameObject.transform.position.y;
    }

   
}