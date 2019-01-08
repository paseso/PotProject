using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public struct Status
{
    //兄のHP
    [SerializeField]
    private int playerHP;

    // バリアの制限時間
    [SerializeField]
    private float barrierTime;

    public float GetBarrierTime
    {
        get { return barrierTime; }
    }

    public int PlayerHP
    {
        get { return playerHP; }
        set { playerHP = value; }
    }

    //剣のタイプ
    public enum SWORDTYPE
    {
        FIRE = 0,
        WATER,
        EARTH,
        KEY,
    }

    // 兄がどの状態か
    public enum GimmickState
    {
        NORMAL,
        ONLADDER,
        ONTREE,
    }

    //今のイベント状態
    public enum EventState
    {
        NORMAL = 0,
        CAMERA,
        ALCHEMYUI,
        MINIMAP,
    }

    //アイテム
    public SWORDTYPE swordtype;

    // 状態
    public GimmickState state;

    public EventState event_state;

    //持ち物に入ってるアイテム
    public List<ItemStatus.ITEM> ItemList;
}

public class PlayerController : MonoBehaviour {

    [SerializeField, Header("剣Sprite")]
    private SpriteRenderer sword;

    [SerializeField]
    private List<Sprite> swordSpriteList;

    private GameObject HeartObject;
    private GameObject[] HeartChild = new GameObject[maxHP];
    private const int maxHP = 3;
    private const int maxItemBox = 3;

    public Status status;
    private AlchemyController alchemy_ctr;
    private AlchemyUIController alchemyUI_ctr;
    private MoveController move_ctr;
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

    // Use this for initialization
    void Start () {
        status.PlayerHP = maxHP;
        status.ItemList = new List<ItemStatus.ITEM>();
        alchemy_ctr = FindObjectOfType<AlchemyController>();
        alchemyUI_ctr = GameObject.Find("Canvas/Alchemy_UI").GetComponent<AlchemyUIController>();
        BrotherObj = FindObjectOfType<AnimController>().gameObject;
        move_ctr = BrotherObj.transform.GetChild(3).GetComponent<MoveController>();
        PotObject = FindObjectOfType<PotController>().gameObject;
        HeartObject = GameObject.Find("Canvas/Heart");
        getHeartChildren();
        _itemMax = false;
        _alchemyUi = false;
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
    private void setEventState(Status.EventState st)
    {
        switch (st)
        {
            case Status.EventState.NORMAL:

                break;

            case Status.EventState.CAMERA:

                break;

            case Status.EventState.ALCHEMYUI:

                break;
        }
    }
	
    /// <summary>
    /// 持ち物リストの情報
    /// </summary>
    /// <returns></returns>
    public List<ItemStatus.ITEM> getItemList()
    {
        return status.ItemList;
    }

    /// <summary>
    /// 持ち物リストにアイテムを追加
    /// </summary>
    /// <param name="name"></param>
    public void setItemList(ItemStatus.ITEM Item_Id)
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
    public void deleteItemList(List<ItemStatus.ITEM> items)
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
    public void SwordTypeChange(Status.SWORDTYPE s_type)
    {
        switch (s_type)
        {
            case Status.SWORDTYPE.FIRE:
                Debug.Log("火属性");
                sword.sprite = swordSpriteList[(int)Status.SWORDTYPE.FIRE];
                break;
            case Status.SWORDTYPE.WATER:
                Debug.Log("水属性");
                sword.sprite = swordSpriteList[(int)Status.SWORDTYPE.WATER];
                break;
            case Status.SWORDTYPE.EARTH:
                Debug.Log("土属性");
                sword.sprite = swordSpriteList[(int)Status.SWORDTYPE.EARTH];
                break;
            case Status.SWORDTYPE.KEY:
                Debug.Log("鍵");
                break;
        }
    }

    /// <summary>
    /// アイテムを錬金する処理
    /// </summary>
    /// <param name="item"></param>
    public void ItemAlchemy(List<ItemStatus.ITEM> item)
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
    /// プレイヤーのHP減らす処理
    /// </summary>
    public void DownHp(int point)
    {
        if((status.PlayerHP - point) <= 0)
        {
            DownHpUI(point);
            status.PlayerHP = 0;
            BrotherObj.transform.localPosition = GameObject.Find(BrotherObj.transform.root.name + "/RespornPoint(Clone)").transform.localPosition;
            UpHp(maxHP);
            Debug.Log("HPが0になりました");
            return;
        }
        DownHpUI(point);
        status.PlayerHP -= point;
        Debug.Log("HP: " + status.PlayerHP);
    }

    /// <summary>
    /// プレイヤーのHP増やす処理
    /// </summary>
    public void UpHp(int point)
    {
        if ((status.PlayerHP + point) >= 3)
        {
            status.PlayerHP = 3;
            UpHpUI(3);
            Debug.Log("HPがmaxになりました");
            return;
        }
        UpHpUI(point);
        status.PlayerHP += point;
    }

    /// <summary>
    /// ハートの数を減らす処理
    /// </summary>
    /// <param name="point"></param>
    private void DownHpUI(int point)
    {
        if (status.PlayerHP <= 0)
            return;
        for (int i = status.PlayerHP; i < status.PlayerHP + point; i++)
        {
            HeartChild[maxHP - i].SetActive(false);
            if (status.PlayerHP == maxHP)
                break;
        }
    }

    /// <summary>
    /// ハートの数増やす処理
    /// </summary>
    /// <param name="point"></param>
    private void UpHpUI(int point)
    {
        for (int i = 1; i < maxHP + 1; i++)
        {
            HeartChild[maxHP - i].SetActive(true);
        }
    }

    /// <summary>
    /// レイヤー変更
    /// </summary>
    public void ChangeLayer()
    {
        move_ctr.OnLadder = false;
        if (gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            PotObject.layer = LayerMask.NameToLayer("Pot");
            PotObject.GetComponent<Rigidbody2D>().simulated = true;
            var children = BrotherObj.transform;
            foreach (Transform child in children)
            {
                if (child.GetComponent<Collider2D>())
                {
                    child.gameObject.layer = LayerMask.NameToLayer("Player");
                }
            }
            BrotherObj.layer = LayerMask.NameToLayer("Player");
        }
    }
}
