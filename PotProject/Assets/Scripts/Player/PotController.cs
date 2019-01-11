using UnityEngine;

public class PotController : MonoBehaviour {

    private PlayerController player_ctr;
    private BringCollider bring_col;
    private MoveController move_ctr;

    private Rigidbody2D rig;

    private Sprite[] PotImages;

    private enum Direction
    {
        RIGHT = 0,
        LEFT
    }
    private Direction direction;

    // Use this for initialization
    void Start () {
        try
        {
            player_ctr = GameObject.FindObjectOfType<PlayerController>();
            bring_col = GameObject.FindObjectOfType<BringCollider>();
            move_ctr = GameObject.FindObjectOfType<MoveController>();
            rig = gameObject.GetComponent<Rigidbody2D>();
        }catch(UnityException e)
        {
            Debug.Log("お兄ちゃんが見当たらない");
        }
        direction = Direction.LEFT;
    }
	
	// Update is called once per frame
	void Update () {
        PotJump();
	}

    /// <summary>
    /// 壺の画像変更処理
    /// </summary>
    private void ChangePotImage()
    {
        if (move_ctr.OnLeft || direction == Direction.RIGHT)
        {

            direction = Direction.LEFT;
        }
        else if (move_ctr.OnLeft || direction == Direction.LEFT)
        {

            direction = Direction.RIGHT;
        }
    }

    /// <summary>
    /// プレイヤーがジャンプした時壺も一緒にジャンプする処理
    /// </summary>
    private void PotJump()
    {
        if (move_ctr.Jumping)
        {
            rig.velocity = new Vector2(0, 1f * move_ctr.speed);
        }
    }

    /// <summary>
    /// お兄ちゃんが物を持っている時レイヤーを変更する処理
    /// </summary>
    public void ChangeLayer()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
        if (bring_col._bring)
        {
            gameObject.GetComponent<DistanceJoint2D>().enabled = false;
            move_ctr.transform.parent.GetComponent<DistanceJoint2D>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<DistanceJoint2D>().enabled = true;
            move_ctr.transform.parent.GetComponent<DistanceJoint2D>().enabled = false;
        }
    }

    /// <summary>
    /// ツボにアイテムを受け渡す処理
    /// </summary>
    public void AddItem(ItemStatus.ITEM type)
    {
        switch (type)
        {
            case ItemStatus.ITEM.SLIME:
                player_ctr.setItemList(ItemStatus.ITEM.SLIME);
                break;

            case ItemStatus.ITEM.GOLEM:
                player_ctr.setItemList(ItemStatus.ITEM.GOLEM);
                break;

            case ItemStatus.ITEM.SNAKE:
                player_ctr.setItemList(ItemStatus.ITEM.SNAKE);
                break;

            default:
                Debug.Log("ItemType: " + type);
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<MonsterController>())
        {
            //if (!bring_col._Tubohit || !move_ctr._itemFall)
            //    return;
            if (!bring_col._Tubohit)
                return;

            Destroy(col.gameObject);
            //gameObject.layer = LayerMask.NameToLayer("Pot");
            MonsterController mInfo = col.gameObject.GetComponent<MonsterController>();
            switch (mInfo.GetMStatus.type)
            {
                case MonsterStatus.MonsterType.WATER:
                    player_ctr.setItemList(ItemStatus.ITEM.SLIME);
                    break;

                case MonsterStatus.MonsterType.SNAKE:
                    player_ctr.setItemList(ItemStatus.ITEM.SNAKE);
                    break;

                default:
                    Debug.Log("Type: " + mInfo.GetMStatus.type);
                    break;
            }
            Debug.Log("Type: " + mInfo.GetMStatus.type);
        }
    }
}
