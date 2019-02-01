using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour {

    private SpriteRenderer sword;

    private List<Sprite> swordSpriteList = new List<Sprite>();

    private GameObject[] hearts;

    private int maxHP;
    private const int maxItemBox = 3;

    private PlayerStatus status;
    private PlayerManager pManager;
    private AlchemyController alchemy_ctr;
    private AlchemyUIController alchemyUI_ctr;
    private GameObject BrotherObj;
    private GameObject PotObject;
    private AnimController anim_ctr;
    private MoveController move_ctr;

    //錬金したアイテムのボックス　最大3つ
    private List<CreateItemStatus.Type> createItemBox;

    private PlayerStatus.SWORDTYPE[] swordList;

    public PlayerStatus.SWORDTYPE[] GetSwordList
    {
        get { return swordList; }
    }

    [SerializeField]
    private RectTransform Pot_UI;
    //錬金UIが開いてるかどうか
    private bool alchemyUIFlag = false;
    //アイテムボックスがMaxかどうか
    private bool _itemMax = false;

    public bool ItemMax
    {
        get { return _itemMax; }
    }

    public bool GetAlchemyUIFlag
    {
        get { return alchemyUIFlag; }
    }

    // 操作可能か
    private bool isCommandActive = true;

    public bool IsCommandActive {
        get { return isCommandActive; }
        set { isCommandActive = value; }
    }

    private bool allCommandActive = true;

    public bool AllCommandActive {
        get { return allCommandActive; }
        set { allCommandActive = value; }
    }
    
    private bool isMiniMap = false;

    public bool IsMiniMap {
        get { return isMiniMap; }
        set { isMiniMap = value; }
    }

    public GameObject OnBlock { get; set; }

    private GameObject lifePoint;

    // Use this for initialization
    void Start ()
    {
        try
        {
            pManager = FindObjectOfType<PlayerManager>();
            status = pManager.Status;
            alchemy_ctr = FindObjectOfType<AlchemyController>();
            alchemyUI_ctr = GameObject.Find("Canvas/Alchemy_UI").GetComponent<AlchemyUIController>();
            BrotherObj = FindObjectOfType<MoveController>().gameObject;
            move_ctr = BrotherObj.GetComponent<MoveController>();
            PotObject = FindObjectOfType<PotController>().gameObject;
            createItemBox = new List<CreateItemStatus.Type>();
            anim_ctr = BrotherObj.transform.parent.GetComponent<AnimController>();
            lifePoint = GameObject.FindGameObjectWithTag("LifePoint");
        }
        catch (UnityException e)
        {
            Debug.Log(e + "がないんご");
        }
        StartHeart();
        setStartSwordList();
        _itemMax = false;
        alchemyUIFlag = false;
	}

    private void Update()
    {
        //デバッグ用　HP減らす処理
        if (Input.GetKeyDown(KeyCode.T))
        {
            HPDown(3);
        }
    }

    /// <summary>
    /// ハートの初期化
    /// </summary>
    private void StartHeart()
    {
        status.PlayerHP = status.GetMaxHP;
        hearts = new GameObject[status.GetMaxHP];
        for (int i = 0; i < status.GetMaxHP; i++)
        {
            hearts[i] = lifePoint.transform.GetChild(i).gameObject;
        }

        for (int i = 0; i < status.PlayerHP; i++)
        {
            hearts[i].SetActive(true);
        }
    }

    /// <summary>
    /// スタート時に剣のセットをする処理
    /// </summary>
    private void setStartSwordList()
    {
        //剣は最大４つまで持てる
        swordList = new PlayerStatus.SWORDTYPE[4];
        swordList[0] = PlayerStatus.SWORDTYPE.NORMAL;
    }

    /// <summary>
    /// 剣をリストにセットする処理
    /// </summary>
    /// <param name="type"></param>
    public void setSwordList(PlayerStatus.SWORDTYPE type)
    {
        //0番目以上のNORMALタイプのところに錬金した剣をいれる
        for(int i = 1; i < 4; i++)
        {
            if (swordList[i] != PlayerStatus.SWORDTYPE.NORMAL)
            {
                continue;
            }
            swordList[i] = type;
            break;
        }
    }

    /// <summary>
    /// EventStateの設定
    /// </summary>
    private void setEventState(PlayerStatus.EventState st)
    {
        switch (st)
        {
            case PlayerStatus.EventState.NORMAL:

                break;

            case PlayerStatus.EventState.CAMERA:

                break;

            case PlayerStatus.EventState.ALCHEMYUI:

                break;
        }
    }

    /// <summary>
    /// 持ち物リストの情報
    /// </summary>
    /// <returns></returns>
    public List<ItemStatus.Type> getItemList()
    {
        return status.ItemList;
    }

    /// <summary>
    /// 持ち物リストにアイテムを追加
    /// </summary>
    /// <param name="name"></param>
    public void setItemList(ItemStatus.Type Item_Id)
    {
        if(status.ItemList.Count == maxItemBox)
        {
            _itemMax = true;
            Debug.Log("アイテムボックスは最大です！");
            return;
        }
        else
        {
            _itemMax = false;
            status.ItemList.Add(Item_Id);
        }
    }

    /// <summary>
    /// 持ち物リストのアイテムを削除
    /// </summary>
    public void deleteItemList(List<ItemStatus.Type> items)
    {
        status.ItemList.Remove(items[0]);
        if (items.Count == 2)
        {
            status.ItemList.Remove(items[1]);
        }
    }

    /// <summary>
    /// 持ち物リストのアイテムを一つ削除
    /// </summary>
    /// <param name="items"></param>
    public void deleteItemList(ItemStatus.Type items)
    {
        status.ItemList.Remove(items);
    }

    /// <summary>
    /// 錬金したアイテムボックスにアイテムを入れる処理
    /// </summary>
    public void setCreateItemList(CreateItemStatus.Type type)
    {
        if (createItemBox.Count >= 3)
        {
            Debug.Log("アイテムがマックスです");
            alchemyUI_ctr.ActiveThrowItemUI();
            return;
        }

        createItemBox.Add(type);
        alchemy_ctr.setGeneratedImg(type);
    }

    /// <summary>
    /// 錬金したアイテムボックスのアイテムを一つ消す処理
    /// </summary>
    /// <param name="type">消したいアイテムのタイプ</param>
    public void deleteCreateItemList(CreateItemStatus.Type type)
    {
        if (createItemBox.Count <= 0)
            return;
        createItemBox.Remove(type);
    }

    /// <summary>
    /// 錬金したアイテムボックスの中身を取得する処理
    /// </summary>
    /// <returns></returns>
    public List<CreateItemStatus.Type> getCreateItemList()
    {
        return createItemBox;
    }

    /// <summary>
    /// 錬金したアイテムを1個消す処理
    /// </summary>
    public void deleteCreateItemList(int num)
    {
        alchemy_ctr.ReSetGeneratedImg();
        createItemBox.RemoveAt(num);
    }

    /// <summary>
    /// 錬金したアイテムを使う処理
    /// </summary>
    /// <param name="num"></param>
    public void UseAlchemyItem(int num)
    {
        alchemy_ctr.AlchemyItem(getCreateItemList()[alchemyUI_ctr.getNowAlchemyItem]);
        deleteCreateItemList(num);
        alchemyUI_ctr.setNowAlchemyItem();
        alchemyUI_ctr.setCreateItemUI();
    }

    /// <summary>
    /// swordSpriteListに画像をセットする処理
    /// </summary>
    private void setSwordSpriteList()
    {
        if(sword == null)
            sword = FindObjectOfType<AnimController>().transform.transform.Find("Sword").GetComponent<SpriteRenderer>();

        swordSpriteList = new List<Sprite>();
        for (int i = 0; i < 7; i++)
        {
            Sprite img = Resources.Load<Sprite>("Textures/SwordImage_" + i);
            swordSpriteList.Add(img);
        }
    }

    /// <summary>
    /// 剣の属性を変える処理
    /// </summary>
    /// <param name="s_type"></param>
    public void SwordTypeChange(PlayerStatus.SWORDTYPE s_type)
    {
        if (swordSpriteList.Count == 0)
        {
            setSwordSpriteList();
        }
        sword.sprite = swordSpriteList[(int)s_type];
        switch (s_type)
        {
            case PlayerStatus.SWORDTYPE.NORMAL:
            case PlayerStatus.SWORDTYPE.FIRE:
            case PlayerStatus.SWORDTYPE.FROZEN:
            case PlayerStatus.SWORDTYPE.DARK:
                status.PlayerAttack = 1;
                break;
            case PlayerStatus.SWORDTYPE.SPEAR:
            case PlayerStatus.SWORDTYPE.AXE:
            case PlayerStatus.SWORDTYPE.TORCH:
                status.PlayerAttack = 2;
                break;
        }
    }

    /// <summary>
    /// アイテムを錬金する処理
    /// </summary>
    /// <param name="item"></param>
    public void ItemAlchemy(List<ItemStatus.Type> item)
    {
        if (item == null)
            return;

        if(item.Count == 1)
        {
            alchemy_ctr.MadeItem(item[0]);
        }
        else if(item.Count == 2)
        {
            alchemy_ctr.MadeItem(item[0], item[1]);
        }
    }

    /// <summary>
    /// 錬金UIを開く
    /// </summary>
    public void OpenAlchemy()
    {
        if (!alchemyUIFlag)
        {
            alchemyUI_ctr.setItemboxImage();
            alchemyUI_ctr.ItemFrameReSet();
            alchemyUI_ctr.setCreateItemUI();
            alchemyUI_ctr.ReSetMaterialsBox(status.ItemList);
            Pot_UI.DOLocalMoveX(0, 0.3f).SetEase(Ease.Linear);
            alchemyUIFlag = true;
        }
        else
        {
            Pot_UI.DOLocalMoveX(1920, 0.3f).SetEase(Ease.Linear);
            alchemyUIFlag = false;
        }
    }

    /// <summary>
    /// プレイヤーのHP増やす処理
    /// </summary>
    /// <param name="point">上昇値</param>
    public void HPUp(int point)
    {
        if (status.PlayerHP + point > status.GetMaxHP) {
            status.PlayerHP = status.GetMaxHP;
            for (int i = 0; i < status.PlayerHP; i++)
            {
                hearts[i].SetActive(true);
            }
            return;
        }
        status.PlayerHP += point;
        for(int i = 0; i < status.PlayerHP; i++)
        {
            hearts[i].SetActive(true);
        }
    }

    /// <summary>
    /// プレイヤーのHP減らす処理
    /// </summary>
    /// <param name="point">減少値</param>
    public void HPDown(int point)
    {
        // HPが0になる攻撃を受けたら
        if (status.PlayerHP - point <= 0)
        {
            Resporn();
            return;
        }
        //  HPの減算
        status.PlayerHP -= point;
        //  ゲージ部分のハートにエフェクトを生成
        EffectManager.Instance.PlayEffect((int)EffectManager.EffectName.Effect_HeartBurst, hearts[status.PlayerHP].transform.position + new Vector3(0,-0.5f,0), 0.05f, hearts[0].transform.parent.gameObject, true);
        // ダメージエフェクトの生成
        EffectManager.Instance.PlayEffect((int)EffectManager.EffectName.Effect_Damage, BrotherObj.transform.position, 5, BrotherObj, true);
        PotObject.GetComponent<PotController>().ChangePotFace(PotStatus.PotFace.Sad);
        //ダメージを受けるアニメーション
        if (move_ctr.direc == MoveController.Direction.LEFT)
        {
            anim_ctr.ChangeAnimatorState(AnimController.AnimState.AnimType.LEFT_SUFFERDAMAGE);
        }
        else
        {
            anim_ctr.ChangeAnimatorState(AnimController.AnimState.AnimType.RIGHT_SUFFERDAMAGE);
        }

        for(int i = status.GetMaxHP - 1; i > status.PlayerHP - 1; i--)
        {
            hearts[i].SetActive(false);
        }
    }

    /// <summary>
    /// 攻撃力変更
    /// </summary>
    /// <param name="point">可変値(下がるなら「-」をつける)</param>
    public void ATKChange(int point) {
        status.PlayerAttack += point;
    }

    /// <summary>
    /// 再生
    /// </summary>
    public void Resporn()
    {
        status.PlayerHP = status.GetMaxHP;
        for (int i = 0; i < status.GetMaxHP; i++)
        {
            hearts[i].SetActive(true);
        }
        BrotherObj.transform.parent.position = GameObject.Find(BrotherObj.transform.root.name + "/OtherObject/RespornPoint(Clone)").transform.position + new Vector3(0, 1.5f, 0);
        PotObject.transform.position = GameObject.Find(BrotherObj.transform.root.name + "/OtherObject/RespornPoint(Clone)").transform.position;
        // リスポーンエフェクトの生成
        EffectManager.Instance.PlayEffect((int)EffectManager.EffectName.Effect_Respawn, BrotherObj.transform.position, 5, BrotherObj, true);
    }

    /// <summary>
    /// レイヤー変更
    /// </summary>
    public void ChangeLayer()
    {
        if (gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            status.gimmick_state = PlayerStatus.GimmickState.NORMAL;
            PotObject.layer = LayerMask.NameToLayer("Pot");
            
            var children = BrotherObj.transform.parent.transform;
            foreach (Transform child in children)
            {
                if (child.GetComponent<Collider2D>())
                {
                    child.gameObject.layer = LayerMask.NameToLayer("Player");
                }
            }
            BrotherObj.transform.parent.gameObject.layer = LayerMask.NameToLayer("Player");
            BrotherObj.transform.parent.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            BrotherObj.transform.parent.GetComponent<Rigidbody2D>().gravityScale = 1;
            PotObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            PotObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }
}
