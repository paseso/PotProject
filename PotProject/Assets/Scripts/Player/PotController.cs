using UnityEngine;
using DG.Tweening;

public class PotController : MonoBehaviour
{

    private PlayerController player_ctr;
    private BringCollider bring_col;
    private MoveController move_ctr;

    private Rigidbody2D rig;

    private Sprite[] PotImages;

    private Sprite ototo_left;
    private Sprite ototo_right;
    private GameObject OtotoHead;

    private GameObject BrotherObj;
    private Vector2 beforePosion;

    private float distance = 0f;

    private bool _onece = false;

    private MoveController.Direction direction;

    // Use this for initialization
    void Start()
    {
        try
        {
            OtotoHead = gameObject.transform.GetChild(1).gameObject;
            ototo_left = Resources.Load("Textures/Charactor/Ototo_Head_Left") as Sprite;
            ototo_right = Resources.Load("Textures/Charactor/Ototo_Head_Right") as Sprite;
            player_ctr = GameObject.FindObjectOfType<PlayerController>();
            bring_col = GameObject.FindObjectOfType<BringCollider>();
            move_ctr = GameObject.FindObjectOfType<MoveController>();
            rig = gameObject.GetComponent<Rigidbody2D>();
            BrotherObj = move_ctr.transform.parent.gameObject;
        }
        catch (UnityException e)
        {
            Debug.Log("お兄ちゃんが見当たらない");
        }
        direction = move_ctr.direc;
        _onece = false;
        beforePosion = new Vector2(0, 0);
        distance = BrotherObj.transform.position.x - gameObject.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();
        //PotDirection();
    }

    /// <summary>
    /// お兄ちゃんの向きに合わせてツボをお兄ちゃんの後ろに移動させる
    /// </summary>
    public void PotDirection()
    {
        float dis = Mathf.Abs(BrotherObj.transform.position.x) - Mathf.Abs(gameObject.transform.position.x);
        if (dis >= 0)
            return;
        Debug.Log("逆");
        gameObject.transform.DOMoveX(gameObject.transform.position.x * 2, 0.2f).SetEase(Ease.Linear);
    }

    /// <summary>
    /// 右にツボが移動する
    /// </summary>
    public void RightMove()
    {
        direction = MoveController.Direction.RIGHT;
        rig.velocity = new Vector2(5f, rig.velocity.y);
    }

    /// <summary>
    /// 左にツボが移動する
    /// </summary>
    public void LeftMove()
    {
        direction = MoveController.Direction.LEFT;
        rig.velocity = new Vector2(-5f, rig.velocity.y);
    }

    /// <summary>
    /// ツボが止まる
    /// </summary>
    public void StopPot()
    {
        rig.velocity = new Vector2(0, rig.velocity.y);
    }

    /// <summary>
    /// ツボがジャンプする
    /// </summary>
    public void JumpPot()
    {
        rig.velocity = new Vector2(0, 1f * move_ctr.speed);
    }

    /// <summary>
    /// お兄ちゃんとツボの距離の処理
    /// </summary>
    private void CheckDistance()
    {
        //Debug.Log("distance = " + distance);
        if (distance <= 20f)
            return;
        player_ctr.HPDown(6);
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
            OtotoHead.GetComponent<Anima2D.SpriteMeshAnimation>().frame = 0;
        }
        else
        {
            OtotoHead.GetComponent<Anima2D.SpriteMeshAnimation>().frame = 1;
        }
    }
}
