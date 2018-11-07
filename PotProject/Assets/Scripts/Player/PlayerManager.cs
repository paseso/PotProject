using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Status
{
    //兄のHP
    public int PlayerHP;
    //剣のタイプ
    public enum SWORDTYPE
    {
        FIRE = 0,
        WATER,
        EARTH
    }
    //アイテム
    public SWORDTYPE swordtype;
    //持ち物に入ってるアイテム
    public List<ItemStatus.ITEM> ItemList;
}

public class PlayerManager : MonoBehaviour {

    [SerializeField, Header("剣Sprite")]
    private SpriteRenderer sword;

    [SerializeField]
    private List<Sprite> swordSpriteList;

    public Status status;
    [SerializeField]
    private AlchemyController alchemy_ctr;

	// Use this for initialization
	void Start () {
        status.PlayerHP = 5;
        status.ItemList = null;
	}
	
	// Update is called once per frame
	void Update () {
		
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
        status.ItemList.Add(Item_Id);
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
        }
    }

    /// <summary>
    /// アイテムを錬金する処理
    /// </summary>
    /// <param name="item"></param>
    public void ItemAlchemy(ItemStatus.ITEM item)
    {
        alchemy_ctr.MadeItem(item);
    }

    /// <summary>
    /// プレイヤーのHPが1減る処理
    /// </summary>
    public void HpMinus()
    {
        if(status.PlayerHP <= 0)
        {
            status.PlayerHP = 0;
            Debug.Log("もう体力がありません");
        }
        else
        {
            status.PlayerHP--;
        }
        Debug.Log("Player HP: " + status.PlayerHP);
    }
}
