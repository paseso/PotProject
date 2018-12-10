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
        player_ctr = GameObject.FindObjectOfType<PlayerController>();
        bring_col = GameObject.Find("Brother/BringCollider").GetComponent<BringCollider>();
        move_ctr = GameObject.Find("Brother/Body").GetComponent<MoveController>();
        rig = gameObject.GetComponent<Rigidbody2D>();
        direction = Direction.LEFT;
    }
	
	// Update is called once per frame
	void Update () {
        PotJump();
        ChangeLayer();
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
    private void ChangeLayer()
    {
        if (bring_col._bring)
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
        else
        {
            if (gameObject.layer != LayerMask.NameToLayer("Default"))
                return;
            gameObject.layer = LayerMask.NameToLayer("Pot");
        }
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<MonsterInfo>())
        {
            Debug.Log("きてる！");
            if (!bring_col._Tubohit || !move_ctr._itemFall)
                return;

            Destroy(col.gameObject);
            gameObject.layer = LayerMask.NameToLayer("Pot");
            MonsterInfo mInfo = col.gameObject.GetComponent<MonsterInfo>();
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
