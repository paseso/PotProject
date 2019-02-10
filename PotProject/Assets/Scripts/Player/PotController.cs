using UnityEngine;
using DG.Tweening;
using System.Collections;

[System.Serializable]
public struct PotStatus
{
    //ツボの属性-------------------------------------
    public enum PotType
    {
        Normal = 0,
        Fire,
        Ice,
        Thunder,
        Dark,
    }
    public PotType pottype;

    public PotType getPotType
    {
        get { return pottype; }
    }
    public PotType setPotType
    {
        set { pottype = value; }
    }
    //-------------------------------------------------

    //ツボの顔種類-------------------------------------
    public enum PotFace
    {
        Normal = 0,
        Angry,
        Sad,
        Smile
    }
    public PotFace potface;

    public PotFace getPotFace
    {
        get { return potface; }
    }
    public PotFace setPotFace
    {
        set { potface = value; }
    }
    //----------------------------------------------------
}

public class PotController : MonoBehaviour
{

    private PlayerController player_ctr;
    private MoveController move_ctr;
    private PotStatus pot_status;
    //ツボの顔の画像が入る配列
    private Sprite[] potFaceSprites;
    //ツボの顔のSpriteObject
    private SpriteRenderer PotFaceSpriteObj;

    private Rigidbody2D rig;

    private GameObject OtotoHead;

    private GameObject BrotherObj;

    private float distance = 0f;

    private bool _potMoving = false;
    //ツボが移動アニメーションしてるかどうか
    private bool _movePot = false;

    private MoveController.Direction direction;

    // Use this for initialization
    void Start()
    {
        try
        {
            OtotoHead = gameObject.transform.GetChild(0).gameObject;
            player_ctr = GameObject.FindObjectOfType<PlayerController>();
            move_ctr = GameObject.FindObjectOfType<MoveController>();
            rig = gameObject.GetComponent<Rigidbody2D>();
            BrotherObj = move_ctr.transform.parent.gameObject;
            PotFaceSpriteObj = gameObject.transform.GetChild(gameObject.transform.childCount - 1).GetComponent<SpriteRenderer>();
        }
        catch (UnityException e)
        {
            Debug.Log(e + "が見当たらない");
        }
        direction = move_ctr.direc;
        _potMoving = false;
        pot_status.setPotType = PotStatus.PotType.Normal;
        pot_status.setPotFace = PotStatus.PotFace.Normal;
        setStartPotFaceSprite();
        _movePot = false;
        distance = BrotherObj.transform.position.x - gameObject.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();
        PotDirection();
    }

    /// <summary>
    /// potFaceSprites配列に画像を入れる処理
    /// </summary>
    private void setStartPotFaceSprite()
    {
        potFaceSprites = new Sprite[4];
        for(int i = 0; i < potFaceSprites.Length; i++)
        {
            potFaceSprites[i] = Resources.Load<Sprite>("Textures/PotTextures/potface_" + i);
        }
    }

    /// <summary>
    /// お兄ちゃんの向きに合わせてツボをお兄ちゃんの後ろに移動させる
    /// </summary>
    private void PotDirection()
    {
        if (_potMoving)
            return;

        //お兄ちゃんよりも前にいたら
        if (direction == MoveController.Direction.RIGHT && distance <= 1f)
        {
            _potMoving = true;
            rig.velocity = new Vector2(-7, rig.velocity.y);
        }
        else if (direction == MoveController.Direction.LEFT && distance >= -1f)
        {
            _potMoving = true;
            rig.velocity = new Vector2(7, rig.velocity.y);
        }
        _potMoving = false;
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
        distance = BrotherObj.transform.position.x - gameObject.transform.position.x;

        //ツボが遠すぎたらワープしてプレイヤーの近くに来る
        if ((BrotherObj.transform.position.y - gameObject.transform.position.y) >= 5f)
        {
            if (_movePot || move_ctr._laddernow)
                return;
            StartCoroutine(PotWarpAnimation());
        }

        //ツボが近かった時の処理
        if (Mathf.Abs(distance) >= 0f && Mathf.Abs(distance) <= 2.5f)
        {
            rig.velocity = new Vector2(0, rig.velocity.y);
        }
        //ツボが少し離れた時の処理
        if (distance >= 7f)
        {
            if (_movePot || move_ctr._laddernow)
                return;
            StartCoroutine(PotWarpAnimation());
        }
    }

    /// <summary>
    /// ツボが離れすぎている時にワープする処理
    /// </summary>
    /// <returns></returns>
    private IEnumerator PotWarpAnimation()
    {
        _movePot = true;
        //エフェクトを出してツボを小さくしていく
        GameObject effectObj = EffectManager.Instance.PlayEffect(11, gameObject.transform.position, 10, gameObject, false);
        effectObj.transform.DOScale(new Vector3(1, 1, 1), 0.5f);
        yield return new WaitForSeconds(0.3f);
        effectObj.transform.DOScale(new Vector3(0, 0, 0), 0.5f);
        gameObject.transform.DOScale(new Vector3(0, 0, 0), 0.4f);
        gameObject.transform.position = new Vector2(BrotherObj.transform.position.x - 1f, BrotherObj.transform.position.y);
        effectObj.transform.position = gameObject.transform.position;
        yield return new WaitForSeconds(0.5f);
        //ツボを大きくしながらエフェクトを出す
        effectObj.transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        gameObject.transform.DOScale(new Vector3(0.7f, 0.7f, 1), 0.3f);
        yield return new WaitForSeconds(0.2f);
        Destroy(effectObj.gameObject);
        _movePot = false;
    }

    /// <summary>
    /// ツボの属性を変更する処理
    /// </summary>
    public void ChangePotType(PotStatus.PotType pot_type)
    {
        switch (pot_type)
        {
            case PotStatus.PotType.Normal:
                gameObject.GetComponent<Anima2D.SpriteMeshAnimation>().frame = 0;
                break;
            case PotStatus.PotType.Fire:
                gameObject.GetComponent<Anima2D.SpriteMeshAnimation>().frame = 1;
                break;
            case PotStatus.PotType.Ice:
                gameObject.GetComponent<Anima2D.SpriteMeshAnimation>().frame = 2;
                break;
            case PotStatus.PotType.Thunder:
                gameObject.GetComponent<Anima2D.SpriteMeshAnimation>().frame = 3;
                break;
            case PotStatus.PotType.Dark:
                gameObject.GetComponent<Anima2D.SpriteMeshAnimation>().frame = 4;
                break;
        }
    }

    /// <summary>
    /// ツボの顔を変更する処理
    /// </summary>
    public void ChangePotFace(PotStatus.PotFace faceType)
    {
        switch (faceType)
        {
            case PotStatus.PotFace.Normal:
                pot_status.setPotFace = PotStatus.PotFace.Normal;
                break;
            case PotStatus.PotFace.Angry:
                pot_status.setPotFace = PotStatus.PotFace.Angry;
                break;
            case PotStatus.PotFace.Sad:
                pot_status.setPotFace = PotStatus.PotFace.Sad;
                break;
            case PotStatus.PotFace.Smile:
                pot_status.setPotFace = PotStatus.PotFace.Smile;
                break;
        }
        PotFaceSpriteObj.sprite = potFaceSprites[(int)faceType];
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

    private void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.layer != LayerMask.NameToLayer("Block"))
        {
            gameObject.transform.position = new Vector2(rig.velocity.x, BrotherObj.transform.position.y - 2);
        }
    }
}