using UnityEngine;

public class PotController : MonoBehaviour {

    private PlayerController player_ctr;
    private BringCollider bring_col;
    private MoveController move_ctr;

    private Rigidbody2D rig;

    private Sprite[] PotImages;

    private Sprite ototo_left;
    private Sprite ototo_right;
    private GameObject OtotoHead;

    private bool _onece = false;

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
            OtotoHead = gameObject.transform.GetChild(1).gameObject;
            ototo_left = Resources.Load("Textures/Charactor/Ototo_Head_Left") as Sprite;
            ototo_right = Resources.Load("Textures/Charactor/Ototo_Head_Right") as Sprite;
            player_ctr = GameObject.FindObjectOfType<PlayerController>();
            bring_col = GameObject.FindObjectOfType<BringCollider>();
            move_ctr = GameObject.FindObjectOfType<MoveController>();
            rig = gameObject.GetComponent<Rigidbody2D>();
        }catch(UnityException e)
        {
            Debug.Log("お兄ちゃんが見当たらない");
        }
        direction = Direction.LEFT;
        _onece = false;
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
            if (!_onece)
            {
                rig.velocity = new Vector2(0, 1f * move_ctr.speed);
                _onece = true;
            }
        }
        else
        {
            _onece = false;
            //rig.velocity = new Vector2(0, 1f * move_ctr.speed);
        }
    }

    /// <summary>
    /// お兄ちゃんが物を持っている時レイヤーを変更する処理
    /// </summary>
    public void ChangeLayer()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    /// <summary>
    /// ツボにアイテムを受け渡す処理
    /// </summary>
    public void AddItem(ItemStatus.Type type)
    {
        switch (type)
        {
            case ItemStatus.Type.CLAY_N:
                player_ctr.setItemList(ItemStatus.Type.CLAY_N);
                break;
            case ItemStatus.Type.CLOUD:
                player_ctr.setItemList(ItemStatus.Type.CLOUD);
                break;
            case ItemStatus.Type.SNAKE:
                player_ctr.setItemList(ItemStatus.Type.SNAKE);
                break;
            case ItemStatus.Type.CROWN:
                player_ctr.setItemList(ItemStatus.Type.CROWN);
                break;
            case ItemStatus.Type.CRYSTAL:
                player_ctr.setItemList(ItemStatus.Type.CRYSTAL);
                break;
            case ItemStatus.Type.FLOWER:
                player_ctr.setItemList(ItemStatus.Type.FLOWER);
                break;
            case ItemStatus.Type.KEYROD:
                player_ctr.setItemList(ItemStatus.Type.KEYROD);
                break;
            case ItemStatus.Type.LAMP:
                player_ctr.setItemList(ItemStatus.Type.LAMP);
                break;
            case ItemStatus.Type.LIZARD:
                player_ctr.setItemList(ItemStatus.Type.LIZARD);
                break;
            case ItemStatus.Type.MIC:
                player_ctr.setItemList(ItemStatus.Type.MIC);
                break;
            case ItemStatus.Type.POWDER:
                player_ctr.setItemList(ItemStatus.Type.POWDER);
                break;
            case ItemStatus.Type.SMOKE:
                player_ctr.setItemList(ItemStatus.Type.SMOKE);
                break;
            case ItemStatus.Type.VAJURA:
                player_ctr.setItemList(ItemStatus.Type.VAJURA);
                break;
            case ItemStatus.Type.WOOD:
                player_ctr.setItemList(ItemStatus.Type.WOOD);
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

            Destroy(col.gameObject);
            MonsterController mInfo = col.gameObject.GetComponent<MonsterController>();
            switch (mInfo.Status.type)
            {
                case MonsterStatus.MonsterType.WATER:
                    player_ctr.setItemList(ItemStatus.Type.CLAY_N);
                    break;

                case MonsterStatus.MonsterType.SNAKE:
                    player_ctr.setItemList(ItemStatus.Type.SNAKE);
                    break;

                default:
                    Debug.Log("Type: " + mInfo.Status.type);
                    break;
            }
            Debug.Log("Type: " + mInfo.Status.type);
        }
    }

    /// <summary>
    /// 方向によって弟の画像切り替えする処理
    /// </summary>
    public void OtotoChangeSprite()
    {
        if (move_ctr.direc == MoveController.Direction.LEFT)
        {
            OtotoHead.GetComponent<SpriteRenderer>().sprite = ototo_left;
        }
        else
        {
            OtotoHead.GetComponent<SpriteRenderer>().sprite = ototo_right;
        }
    }
}
