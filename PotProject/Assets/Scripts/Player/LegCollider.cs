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
            if (value)
            {
                if (jumpPos - transform.position.y >= 2.5f)
                {
                    player_ctr.HPDown(2);
                    jumpPos = transform.position.y;
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
        status = FindObjectOfType<PlayerManager>().Status;
        falldistance = gameObject.transform.position.y;
        move_ctr = transform.parent.GetComponentInChildren<MoveController>();
        player_ctr = GameObject.Find("Controller").GetComponent<PlayerController>();
        _legFloor = false;
	}

    /// <summary>
    /// ジャンプ判定
    /// </summary>
    /// <param name="col">足元のObject</param>
    /// <returns></returns>
    bool JumpCheck(GameObject col)
    {
        if (col.GetComponent<DropItemManager>()) { return false; }// Item
        if (col.gameObject.layer == 2) { return false; }// BackGround
        if (col.GetComponent<KeyBlockCol>()) { return false; } // 鍵ActiveCollider
        if (!col.GetComponent<GimmickInfo>()) { return true; } // Gimmick
        GimmickInfo info = col.GetComponent<GimmickInfo>();
        if (info.type == GimmickInfo.GimmickType.LADDER) { return false; } // はしご
        if (info.type == GimmickInfo.GimmickType.FIREFIELD) { return false; } // 敵攻撃範囲
        return true;
    }

    bool SwitchCheck(GameObject col)
    {
        if (!col.GetComponent<GimmickInfo>()) { return false; }
        if (col.GetComponent<GimmickInfo>().type == GimmickInfo.GimmickType.UP) { return true; }
        if (col.GetComponent<GimmickInfo>().type == GimmickInfo.GimmickType.DOWN) { return true; }
        if (col.GetComponent<GimmickInfo>().type == GimmickInfo.GimmickType.LEFT) { return true; }
        if (col.GetComponent<GimmickInfo>().type == GimmickInfo.GimmickType.RIGHT) { return true; }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D col) {

        if (SwitchCheck(col.gameObject))
        {
            move_ctr.switchGimmick = col.gameObject;
        }

        if (col.gameObject.layer != 2 && JumpCheck(col.gameObject))
        {
            onGroundCount++;
        }

        if (onGroundCount > 0)
        {
            isLanding = true;
        }
        
        if (move_ctr.Jumping)
        {
            move_ctr.setJumping = false;
        }

        if (col.gameObject.tag == "floor") {
            _legFloor = true;
        }

        if(col.gameObject.layer == LayerMask.NameToLayer("Block"))
        {
            Rigidbody2D rb = transform.parent.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(rb.velocity.x,0);
        }

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

    private void OnTriggerExit2D(Collider2D col)
    {
        if (SwitchCheck(col.gameObject))
        {
            move_ctr.switchGimmick = null;
        }

        if (col.gameObject.layer != 2 && JumpCheck(col.gameObject))
        {
            onGroundCount--;
        }

        if (col.GetComponent<GimmickInfo>())
            if(col.GetComponent<GimmickInfo>().type == GimmickInfo.GimmickType.FIREFIELD && onGroundCount <= 0)
            {
                return;
            }

        if (onGroundCount <= 0)
        {
            onGroundCount = 0;
            isLanding = false;
        }

        falldistance = gameObject.transform.position.y;
        if (!col.GetComponent<GimmickInfo>()) { return; }
        GimmickInfo info = col.GetComponent<GimmickInfo>();
        if (info.type == GimmickInfo.GimmickType.LADDER) {
            
            move_ctr.InLadderCount--;
            if (move_ctr.InLadderCount <= 0) {
                move_ctr.InLadderCount = 0;
                if(gameObject.layer == LayerMask.NameToLayer("LadderPlayer"))
                    player_ctr.ChangeLayer();
            }
        }
    }
}