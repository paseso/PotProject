using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour {

    [SerializeField, Header("剣Sprite")]
    private SpriteRenderer sword;

    [SerializeField]
    private List<Sprite> swordSpriteList;

    private GameObject[] hearts;

    private GameObject HeartObject;
    private GameObject[] HeartChild;
    private int maxHP;
    private const int maxItemBox = 3;

    public PlayerStatus status;
    private AlchemyController alchemy_ctr;
    private AlchemyUIController alchemyUI_ctr;
    private GameObject BrotherObj;
    private GameObject PotObject;

    [SerializeField]
    private RectTransform Pot_UI;
    //錬金UIが開いてるかどうか
    private bool _alchemyUi = false;
    //アイテムボックスがMaxかどうか
    private bool _itemMax = false;

    public bool AlchemyWindow
    {
        get { return _alchemyUi; }
    }

    // 操作可能か
    private bool isCommandActive = true;

    public bool IsCommandActive {
        get { return isCommandActive; }
        set { isCommandActive = value; }
    }

    private bool isMiniMap = false;

    public bool IsMiniMap {
        get { return isMiniMap; }
        set { isMiniMap = value; }
    }

    private GameObject lifePoint;

    // Use this for initialization
    void Start ()
    {
        hearts = new GameObject[status.GetMaxHP];
        lifePoint = GameObject.FindGameObjectWithTag("LifePoint");
        for(int i = 0; i < status.GetMaxHP; i++)
        {
            hearts[i] = lifePoint.transform.GetChild(i).gameObject;
        }

        if (status.PlayerHP == 0)
        {
            status.PlayerHP = status.GetMaxHP;
        }

        status.ItemList = new List<ItemStatus.Type>();
        alchemy_ctr = FindObjectOfType<AlchemyController>();
        alchemyUI_ctr = GameObject.Find("Canvas/Alchemy_UI").GetComponent<AlchemyUIController>();
        BrotherObj = FindObjectOfType<MoveController>().gameObject;
        PotObject = FindObjectOfType<PotController>().gameObject;
        HeartObject = GameObject.Find("Canvas/Heart");
        getHeartChildren();
        _itemMax = false;
        _alchemyUi = false;

        for(int i = 0; i < status.PlayerHP; i++)
        {
            hearts[i].SetActive(true);
        }
	}

    /// <summary>
    /// HaertChildに子供を入れる処理
    /// </summary>
    private void getHeartChildren()
    {
       HeartChild = new GameObject[maxHP];
        int num = maxHP - 1;
       for(int i = 0; i < maxHP; i++)
        {
            HeartChild[num - i] = HeartObject.transform.GetChild(i).gameObject;
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
            status.ItemList.Add(Item_Id);
        }
    }

    /// <summary>
    /// 持ち物リストのアイテムを削除
    /// </summary>
    public void deleteItemList(List<ItemStatus.Type> items)
    {
        foreach (var _item in items)
        {
            status.ItemList.Remove(_item);
        }
        Debug.Log("ItemList: " + status.ItemList.GetType());
    }

    /// <summary>
    /// 剣の属性を変える処理
    /// </summary>
    /// <param name="s_type">FIRE=火、WATER=水、EARTH=土</param>
    public void SwordTypeChange(PlayerStatus.SWORDTYPE s_type)
    {
        switch (s_type)
        {
            case PlayerStatus.SWORDTYPE.NORMAL:
                sword.sprite = swordSpriteList[(int)PlayerStatus.SWORDTYPE.NORMAL];
                break;
            case PlayerStatus.SWORDTYPE.FIRE:
                Debug.Log("火属性");
                sword.sprite = swordSpriteList[(int)PlayerStatus.SWORDTYPE.FIRE];
                break;
            case PlayerStatus.SWORDTYPE.WATER:
                Debug.Log("水属性");
                sword.sprite = swordSpriteList[(int)PlayerStatus.SWORDTYPE.WATER];
                break;
            case PlayerStatus.SWORDTYPE.EARTH:
                Debug.Log("土属性");
                sword.sprite = swordSpriteList[(int)PlayerStatus.SWORDTYPE.EARTH];
                break;
            case PlayerStatus.SWORDTYPE.KEY:
                sword.sprite = swordSpriteList[(int)PlayerStatus.SWORDTYPE.KEY];
                Debug.Log("鍵");
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
        if (!_alchemyUi)
        {
            alchemyUI_ctr.setItemboxImage();
            alchemyUI_ctr.ItemFrameReSet();
            Pot_UI.DOLocalMoveX(0, 0.3f).SetEase(Ease.Linear);
            _alchemyUi = true;
        }
        else
        {
            Pot_UI.DOLocalMoveX(1000, 0.3f).SetEase(Ease.Linear);   //1745
            _alchemyUi = false;
        }
    }

    /// <summary>
    /// プレイヤーのHP増やす処理
    /// </summary>
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
    public void HPDown(int point)
    {
        // HPが0になる攻撃を受けたら
        if (status.PlayerHP - point <= 0)
        {
            Resporn();
            return;
        }
        status.PlayerHP -= point;
        for(int i = status.GetMaxHP - 1; i > status.PlayerHP - 1; i--)
        {
            hearts[i].SetActive(false);
        }
    }

    public void Resporn()
    {
        status.PlayerHP = status.GetMaxHP;
        for (int i = 0; i < status.GetMaxHP; i++)
        {
            hearts[i].SetActive(true);
        }
        BrotherObj.transform.parent.position = GameObject.Find(BrotherObj.transform.root.name + "/RespornPoint(Clone)").transform.position;
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
            BrotherObj.transform.parent.GetComponent<Rigidbody2D>().isKinematic = false;
            PotObject.GetComponent<Rigidbody2D>().isKinematic = false;
            BrotherObj.transform.parent.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            PotObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            var children = BrotherObj.transform.parent.transform;
            foreach (Transform child in children)
            {
                if (child.GetComponent<Collider2D>())
                {
                    child.gameObject.layer = LayerMask.NameToLayer("Player");
                }
            }
            BrotherObj.transform.parent.gameObject.layer = LayerMask.NameToLayer("Player");
        }
    }
}
