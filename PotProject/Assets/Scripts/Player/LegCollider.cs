using UnityEngine;

public class LegCollider : MonoBehaviour
{

    private MoveController move_ctr;
    private PlayerController player_ctr;
    private GameObject PotObj;
    private AnimController anim_ctr;

    private bool landingFlag = false;
    //雲に乗ってるかどうか
    private bool _onLandding = false;
    //ちくわブロックに乗ってるかどうか
    private bool _onFallBlock = false;
    private bool _onMoveCloud = false;

    private MoveController.Direction nowDirec;

    float jumpPos;
    int onGroundCount;
    //タイル一個分の大きさ
    private const float TILESIZE = 2;
    //落下で即死する高さ
    private int deadFallHeight = 3;
    //落下でダメージを受ける高さ
    private int damageFallHeight = 2;

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
                //ちくわブロックと動く雲の時
                if (_onFallBlock || _onMoveCloud)
                {
                    jumpPos = transform.position.y;
                    landingFlag = value;
                    return;
                }
                if (jumpPos - transform.position.y >= TILESIZE * deadFallHeight)
                {
                    player_ctr.HPDown(6);
                    jumpPos = transform.position.y;
                }
                else if (jumpPos - transform.position.y >= TILESIZE * damageFallHeight)
                {
                    player_ctr.HPDown(1);
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

    // Use this for initialization
    void Start()
    {
        move_ctr = transform.parent.GetComponentInChildren<MoveController>();
        player_ctr = GameObject.Find("Controller").GetComponent<PlayerController>();
        PotObj = GameObject.FindObjectOfType<PotController>().gameObject;
        anim_ctr = move_ctr.gameObject.transform.parent.GetComponent<AnimController>();
        _onLandding = false;
        _onFallBlock = false;
        _onMoveCloud = false;
        nowDirec = move_ctr.direc;
    }

    private void Update()
    {
        //ギミック(雲、木)に乗ってる時は弟の場所をお兄ちゃんの場所と同じにする
        if (_onLandding)
        {
            PotObj.transform.position = gameObject.transform.parent.transform.position;
            PotObj.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            
            nowDirec = move_ctr.direc;
        }
        if (gameObject.layer == LayerMask.NameToLayer("LadderPlayer") && move_ctr.InLadderCount <= 0)
        {
            player_ctr.ChangeLayer();
        }
    }

    /// <summary>
    /// ジャンプ判定
    /// </summary>
    /// <param name="col">足元のObject</param>
    /// <returns></returns>
    bool JumpCheck(GameObject col)
    {
        if (col.GetComponent<ItemManager>()) { return false; }// Item
        if (col.gameObject.layer == 2) { return false; }// 背景
        if (col.gameObject.layer == LayerMask.NameToLayer("BackGround")) { return true; }
        if (col.GetComponent<KeyBlockCol>()) { return false; } // 鍵ActiveCollider
        if (col.GetComponent<GimmickInfo>()) {
            GimmickInfo info = col.GetComponent<GimmickInfo>();
            if (info.type == GimmickInfo.GimmickType.LADDER) { return false; } // はしご
            if (info.type == GimmickInfo.GimmickType.FIREFIELD) { return false; } // 敵攻撃範囲
            if (info.type == GimmickInfo.GimmickType.THUNDERFIELD) { return false; } // 敵攻撃範囲
        }
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

    private void OnTriggerEnter2D(Collider2D col)
    {
        // switchの上にいるか判定
        if (SwitchCheck(col.gameObject))
        {
            move_ctr.switchGimmick = col.gameObject;
            col.GetComponent<GimmickController>().OnPlayerFlag = true;
        }
        //動く雲に乗った後に普通のブロックに乗ったら弟位置解除
        if(col.gameObject.layer == LayerMask.NameToLayer("Block"))
        {
            if(_onLandding || _onMoveCloud)
            {
                _onMoveCloud = false;
                _onLandding = false;
            }
        }
        //ちくわブロックに乗っかった時
        if (col.gameObject.name == "FallCol")
        {
            _onFallBlock = false;
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
        if (col.gameObject.layer == LayerMask.NameToLayer("Block"))
        {
            Rigidbody2D rb = transform.parent.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(rb.velocity.x, 0);
            player_ctr.OnBlock = col.gameObject;

            if (gameObject.layer == LayerMask.NameToLayer("LadderPlayer"))
            {
                player_ctr.ChangeLayer();
                move_ctr.ladderDownFlag = true;
                move_ctr.InLadderCount = 1;
                return;
            }
        }

        // 壁ブロックなら
        if (col.gameObject.layer == 2 && !col.GetComponent<MapChange>())
        {
            move_ctr.InLadderCount++;
            move_ctr.ladderDownFlag = true;
        }

        if (!col.GetComponent<GimmickInfo>()) { return; }
        GimmickInfo info = col.GetComponent<GimmickInfo>();
        player_ctr.OnGimmick = col.gameObject;

        switch (info.type)
        {
            case GimmickInfo.GimmickType.GROWTREE:
                player_ctr.rideTreeFlag = true;
                _onLandding = true;
                break;
            case GimmickInfo.GimmickType.LADDER:
                move_ctr.InLadderCount++;
                break;
        }
        player_ctr.OnBlock = null;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        //雲のスクリプトに当たったらツボをプレイヤーの場所に移動させる
        if (col.gameObject.GetComponent<CloudCol>())
        {
            if (col.gameObject.GetComponent<CloudCol>().getLandingCloud)
            {
                _onMoveCloud = true;
                _onLandding = true;
                //if (move_ctr.direc != nowDirec)
                //{
                //    if (move_ctr.direc == MoveController.Direction.LEFT)
                //        anim_ctr.ChangeAnimatorState(AnimController.AnimState.AnimType.LEFTBRINGPOT);
                //    else
                //        anim_ctr.ChangeAnimatorState(AnimController.AnimState.AnimType.RIGHTBRINGPOT);
                //}
            }
        }
        //ちくわブロックに乗っかってる時
        if (col.gameObject.name == "FallCol")
        {
            _onFallBlock = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        //スイッチから降りた時
        if (SwitchCheck(col.gameObject))
        {
            move_ctr.switchGimmick = null;
            col.GetComponent<GimmickController>().OnPlayerFlag = false;
        }
        if (col.gameObject.layer != 2 && JumpCheck(col.gameObject))
        {
            onGroundCount--;
        }

        if (col.gameObject.name == "FallCol")
        {
            _onFallBlock = false;
        }

        if (col.GetComponent<GimmickInfo>())
        {
            if (col.GetComponent<GimmickInfo>().type == GimmickInfo.GimmickType.FIREFIELD && onGroundCount <= 0){
                return;
            }
            if (col.GetComponent<GimmickInfo>().type == GimmickInfo.GimmickType.THUNDERFIELD && onGroundCount <= 0) {
                return;
            }
        }

        if (onGroundCount <= 0)
        {
            onGroundCount = 0;
            isLanding = false;
        }

        if (col.gameObject.layer == 2 && !col.GetComponent<MapChange>())
        {
            move_ctr.InLadderCount--;
            move_ctr.ladderDownFlag = false;
        }

        if (!col.GetComponent<GimmickInfo>()) { return; }
        player_ctr.OnGimmick = null;
        GimmickInfo info = col.GetComponent<GimmickInfo>();

        switch (col.GetComponent<GimmickInfo>().type)
        {
            case GimmickInfo.GimmickType.GROWTREE:
                _onLandding = false;
                player_ctr.rideTreeFlag = false;
                break;
            case GimmickInfo.GimmickType.LADDER:
                move_ctr.ladderDownFlag = false;
                move_ctr.InLadderCount--;
                if (move_ctr.InLadderCount <= 0)
                {
                    move_ctr.InLadderCount = 0;
                }
                break;
        }
    }
}