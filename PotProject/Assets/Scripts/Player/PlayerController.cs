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

    public Status status;
    [SerializeField]
    private AlchemyController alchemy_ctr;
    [SerializeField]
    private AlchemyUIController alchemyUI_ctr;

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

	// Use this for initialization
	void Start () {
        status.PlayerHP = 5;
        status.ItemList = new List<ItemStatus.ITEM>();
        _itemMax = false;
        _alchemyUi = false;
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
        if(status.ItemList.Count == 3)
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
            Pot_UI.DOLocalMoveX(1745, 0.3f).SetEase(Ease.Linear);
            _alchemyUi = false;
        }
    }

    /// <summary>
    /// プレイヤーのHP処理
    /// </summary>
    public void ApplyHp(int point)
    {
        if(status.PlayerHP <= point || status.PlayerHP <= 0)
        {
            status.PlayerHP = 0;
            Debug.Log("HPが0になりました");
            return;
        }
        status.PlayerHP -= point;
        Debug.Log("HP: " + status.PlayerHP);
        // HPのUIに反映させる処理

    }
}
