using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
//錬金したアイテムの種類
public struct CreateItemStatus
{
    public enum Type
    {
        Ladder = 0,
        Barrier,
        Key,
        HPPotion,
        FlyCloud,
        Tornado,
        Torch,
        ATKPotion,
        Lasso,
        SmokeScreen,
        SmokeBall,
        Explosive,
        RainCloud,
        Vajura,
        Lamp,
        Watter,
        Magnet,
        Boomerang,
        Drill,
        Inclubator,
        Speaker,
        Venom,
        TreePotion,
        Dast,
    };
    public CreateItemStatus.Type createItem;
}


public class AlchemyController : MonoBehaviour {

    //レシピ
    /* 
     * へび　 + 粘土 = はしご
     * トカゲ + 粘土 = バリア
     * 王冠 　+ かぎ = 鍵
     * 花　 + 鱗粉 = 回復ポーション
     * 雲　　 + 鱗粉 = 飛べる雲
     * 煙玉　 + 雲 　= 竜巻
     * 木　　 + 粘土 = 攻撃ポーション
     * トカゲ + ヘビ = 投げ縄
     * 煙玉　 + 鱗粉 = 煙玉
     * コウモリ + 粘土 = 拡声器
     * 雲 　　+ クリスタル = 雨雲
     * ヴァジュラ先端 + 粘土 = バジュラ（電撃系の武器）
     * 
     */

    //画面右下にでる画像 CreateItemStatus.Typeと同じ順番
    //CreateItemImage
    /*  
     * 0.はしご
     * 1.バリア
     * 2.鍵
     * 3.HPポーション
     * 4.飛べる雲
     * 5.竜巻
     * 6.たいまつ
     * 7.攻撃ポーション
     * 8.投げ縄
     * 9.煙幕
     * 10.煙玉
     * 11.火薬
     * 12.雨雲
     * 13.ヴァジュラ
     * 14.ランプ
     * 15.水
     * 16.磁石
     * 18.ブーメラン
     * 19.ドリル
     * 20.培養器
     * 21.拡声器
     * 22.毒液
     * 23.木を成長させるポーション
     * 24.ゴミ
     */


    //生成アイテム
    private Sprite[] CreateItem;
    private TextAsset csvFile;
    private MapInfo mInfo;
    //フレームの右下のImage
    private Image GeneratedImg;
    private PlayerController player_ctr;
    private ItemController item_ctr;
    private PlayerManager player_mng;

    private Sprite AlphaSprite;

    public Sprite[] getCreateItem
    {
        get { return CreateItem; }
    }

    // Use this for initialization
    void Start () {
        player_ctr = GameObject.Find("Controller").GetComponent<PlayerController>();
        GeneratedImg = GameObject.Find("Canvas/Panel/Image").GetComponent<Image>();
        item_ctr = GameObject.Find("Controller").GetComponent<ItemController>();
        player_mng = GameObject.Find("PlayerStatus").GetComponent<PlayerManager>();
        AlphaSprite = Resources.Load<Sprite>("Textures/UI/AlphaImage");
        ReSetGeneratedImg();
        setCreateItem();
        mInfo = transform.root.GetComponent<MapInfo>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// 生成できるアイテム配列CreateItemにセット
    /// </summary>
    private void setCreateItem()
    {
        CreateItem = new Sprite[25];
        for(int i = 0; i < CreateItem.Length; i++)
        {
            CreateItem[i] = Resources.Load<Sprite>("Textures/CreateItems/CreateItem_" + i);
        }
    }

    /// <summary>
    /// 錬成csv読み込み
    /// </summary>
    private void ReadText()
    {
        csvFile = Resources.Load("CSV/alchemyList")as TextAsset;
        System.IO.StringReader reader = new System.IO.StringReader(csvFile.text);
        while(reader.Peek() >= -1)
        {

        }
    }

    /// <summary>
    /// 右下のフレームにある生成したアイテム画像を消す処理
    /// </summary>
    public void ReSetGeneratedImg()
    {
        GeneratedImg.sprite = AlphaSprite;
    }

    /// <summary>
    /// 右下のフレームにある生成したアイテム画像をセットする処理
    /// </summary>
    public void setGeneratedImg(CreateItemStatus.Type type)
    {
        GeneratedImg.sprite = CreateItem[(int)type];
    }

    /// <summary>
    /// アイテム錬金
    /// </summary>
    /// <param name="item">錬金したいアイテム</param>
    public void MadeItem(ItemStatus.Type item)
    {
        switch (item)
        {
            case ItemStatus.Type.WOOD:
                //木が成長
                player_ctr.setCreateItemList(CreateItemStatus.Type.TreePotion);
                break;
            case ItemStatus.Type.LAMP:
                //ランプ
                player_ctr.setCreateItemList(CreateItemStatus.Type.Lamp);
                break;
            case ItemStatus.Type.SMOKE:
                //爆薬
                player_ctr.setCreateItemList(CreateItemStatus.Type.Explosive);
                break;
            case ItemStatus.Type.CRYSTAL:
                //水
                player_ctr.setCreateItemList(CreateItemStatus.Type.Watter);
                break;
            default:
                //ゴミができる
                player_ctr.setCreateItemList(CreateItemStatus.Type.Dast);
                break;
        }
    }
    
    /// <summary>
    /// アイテム錬金
    /// </summary>
    /// <param name="item_0">素材_0</param>
    /// <param name="item_1">素材_1</param>
    public void MadeItem(ItemStatus.Type item_0, ItemStatus.Type item_1)
    {
        switch (item_0)
        {
            case ItemStatus.Type.SNAKE:
                switch (item_1)
                {
                    case ItemStatus.Type.CLAY_N:
                        //はしご
                        player_ctr.setCreateItemList(CreateItemStatus.Type.Ladder);
                        break;
                    case ItemStatus.Type.POWDER:
                        //投げ縄
                        player_ctr.setCreateItemList(CreateItemStatus.Type.Lasso);
                        break;
                    default:
                        //ゴミ
                        player_ctr.setCreateItemList(CreateItemStatus.Type.Dast);
                        break;
                }
                break;

            case ItemStatus.Type.CROWN:
                switch (item_1)
                {
                    case ItemStatus.Type.KEYROD:
                        //鍵
                        player_ctr.setCreateItemList(CreateItemStatus.Type.Key);
                        break;
                    case ItemStatus.Type.CLAY_N:
                        //磁石
                        player_ctr.setCreateItemList(CreateItemStatus.Type.Magnet);
                        break;
                    default:
                        //ゴミ
                        player_ctr.setCreateItemList(CreateItemStatus.Type.Dast);
                        break;
                }
                break;

            case ItemStatus.Type.KEYROD:
                switch (item_1)
                {
                    case ItemStatus.Type.CROWN:
                        //鍵
                        player_ctr.setSwordList(PlayerStatus.SWORDTYPE.KEY);
                        break;
                    case ItemStatus.Type.VAJURA:
                        //槍
                        player_ctr.setSwordList(PlayerStatus.SWORDTYPE.SPEAR);
                        break;
                    case ItemStatus.Type.LIZARD:
                        //斧
                        player_ctr.setSwordList(PlayerStatus.SWORDTYPE.AXE);
                        break;
                    default:
                        //ゴミ
                        player_ctr.setCreateItemList(CreateItemStatus.Type.Dast);
                        break;
                }
                break;

            case ItemStatus.Type.FLOWER:
                switch (item_1)
                {
                    case ItemStatus.Type.POWDER:
                        //HPポーション
                        player_ctr.setCreateItemList(CreateItemStatus.Type.HPPotion);
                        break;
                    case ItemStatus.Type.CLAY_N:
                        //攻撃ポーション
                        player_ctr.setCreateItemList(CreateItemStatus.Type.ATKPotion);
                        break;
                    default:
                        //ゴミ
                        player_ctr.setCreateItemList(CreateItemStatus.Type.Dast);
                        break;
                }
                break;

            case ItemStatus.Type.LIZARD:
                switch (item_0)
                {
                    case ItemStatus.Type.CLAY_N:
                        //バリア
                        player_ctr.setCreateItemList(CreateItemStatus.Type.Barrier);
                        break;
                    case ItemStatus.Type.POWDER:
                        //ブーメラン
                        player_ctr.setCreateItemList(CreateItemStatus.Type.Boomerang);
                        break;
                    case ItemStatus.Type.KEYROD:
                        //斧
                        player_ctr.setSwordList(PlayerStatus.SWORDTYPE.AXE);
                        break;
                    case ItemStatus.Type.VAJURA:
                        //ドリル
                        player_ctr.setCreateItemList(CreateItemStatus.Type.Drill);
                        break;
                    default:
                        //ゴミ
                        player_ctr.setCreateItemList(CreateItemStatus.Type.Dast);
                        break;
                }
                break;

            case ItemStatus.Type.WOOD:
                switch (item_1)
                {
                    case ItemStatus.Type.CRYSTAL:
                        //培養液
                        player_ctr.setCreateItemList(CreateItemStatus.Type.Inclubator);
                        break;
                    default:
                        //ゴミ
                        player_ctr.setCreateItemList(CreateItemStatus.Type.Dast);
                        break;
                }
                break;

            case ItemStatus.Type.VAJURA:
                switch (item_1)
                {
                    case ItemStatus.Type.CLAY_N:
                        //バジュラ（電撃武器）
                        player_ctr.setCreateItemList(CreateItemStatus.Type.Vajura);
                        break;
                    case ItemStatus.Type.KEYROD:
                        //槍
                        player_ctr.setSwordList(PlayerStatus.SWORDTYPE.SPEAR);
                        break;
                    case ItemStatus.Type.LIZARD:
                        //ドリル
                        player_ctr.setCreateItemList(CreateItemStatus.Type.Drill);
                        break;
                    default:
                        //ゴミ
                        player_ctr.setCreateItemList(CreateItemStatus.Type.Dast);
                        break;
                }
                break;

            case ItemStatus.Type.MIC:
                switch (item_1)
                {
                    case ItemStatus.Type.CLAY_N:
                        //拡声器
                        player_ctr.setCreateItemList(CreateItemStatus.Type.Speaker);
                        break;
                    default:
                        //ゴミ
                        player_ctr.setCreateItemList(CreateItemStatus.Type.Dast);
                        break;
                }
                break;

            case ItemStatus.Type.CLOUD:
                switch (item_1)
                {
                    case ItemStatus.Type.POWDER:
                        //飛べる雲
                        player_ctr.setCreateItemList(CreateItemStatus.Type.FlyCloud);
                        break;
                    case ItemStatus.Type.SMOKE:
                        //竜巻
                        player_ctr.setCreateItemList(CreateItemStatus.Type.Tornado);
                        break;
                    case ItemStatus.Type.CRYSTAL:
                        //雨雲
                        player_ctr.setCreateItemList(CreateItemStatus.Type.RainCloud);
                        break;
                    default:
                        //ゴミ
                        player_ctr.setCreateItemList(CreateItemStatus.Type.Dast);
                        break;
                }
                break;

            case ItemStatus.Type.CRYSTAL:
                switch (item_1)
                {
                    case ItemStatus.Type.CLOUD:
                        //雨雲
                        player_ctr.setCreateItemList(CreateItemStatus.Type.RainCloud);
                        break;
                    case ItemStatus.Type.WOOD:
                        //培養液
                        player_ctr.setCreateItemList(CreateItemStatus.Type.Inclubator);
                        break;
                    case ItemStatus.Type.POWDER:
                        //毒液
                        player_ctr.setCreateItemList(CreateItemStatus.Type.Venom);
                        break;
                    case ItemStatus.Type.CLAY_N:
                        //氷の剣
                        player_ctr.setSwordList(PlayerStatus.SWORDTYPE.FROZEN);
                        break;
                    default:
                        //ゴミ
                        player_ctr.setCreateItemList(CreateItemStatus.Type.Dast);
                        break;
                }
                break;

            case ItemStatus.Type.SMOKE:
                switch (item_1)
                {
                    case ItemStatus.Type.CLOUD:
                        //竜巻
                        player_ctr.setCreateItemList(CreateItemStatus.Type.Tornado);
                        break;
                    case ItemStatus.Type.POWDER:
                        //煙幕
                        player_ctr.setItemList(ItemStatus.Type.SMOKESCREEN);
                        break;
                    case ItemStatus.Type.CLAY_N:
                        //闇の剣
                        player_ctr.setSwordList(PlayerStatus.SWORDTYPE.DARK);
                        break;
                    default:
                        //ゴミ
                        player_ctr.setCreateItemList(CreateItemStatus.Type.Dast);
                        break;
                }
                break;

            case ItemStatus.Type.POWDER:
                switch (item_1)
                {
                    case ItemStatus.Type.SNAKE:
                        //投げ縄
                        player_ctr.setCreateItemList(CreateItemStatus.Type.Lasso);
                        break;
                    case ItemStatus.Type.CLOUD:
                        //飛べる雲
                        player_ctr.setCreateItemList(CreateItemStatus.Type.FlyCloud);
                        break;
                    case ItemStatus.Type.FLOWER:
                        //HPポーション
                        player_ctr.setCreateItemList(CreateItemStatus.Type.HPPotion);
                        break;
                    case ItemStatus.Type.SMOKE:
                        //煙幕
                        player_ctr.setItemList(ItemStatus.Type.SMOKESCREEN);
                        break;
                    case ItemStatus.Type.LIZARD:
                        //ブーメラン
                        player_ctr.setCreateItemList(CreateItemStatus.Type.Boomerang);
                        break;
                    case ItemStatus.Type.CRYSTAL:
                        //毒液
                        player_ctr.setCreateItemList(CreateItemStatus.Type.Venom);
                        break;
                    default:
                        //ゴミ
                        player_ctr.setCreateItemList(CreateItemStatus.Type.Dast);
                        break;
                }
                break;

            case ItemStatus.Type.CLAY_N:
                switch (item_1)
                {
                    case ItemStatus.Type.FLOWER:
                        //攻撃ポーション
                        player_ctr.setCreateItemList(CreateItemStatus.Type.ATKPotion);
                        break;
                    case ItemStatus.Type.MIC:
                        //拡声器
                        player_ctr.setCreateItemList(CreateItemStatus.Type.Speaker);
                        break;
                    case ItemStatus.Type.VAJURA:
                        //バジュラ（電撃武器）
                        player_ctr.setCreateItemList(CreateItemStatus.Type.Vajura);
                        break;
                    case ItemStatus.Type.LIZARD:
                        //バリア
                        player_ctr.setCreateItemList(CreateItemStatus.Type.Barrier);
                        break;
                    case ItemStatus.Type.SNAKE:
                        //はしご
                        player_ctr.setCreateItemList(CreateItemStatus.Type.Ladder);
                        break;
                    case ItemStatus.Type.CROWN:
                        //磁石
                        player_ctr.setCreateItemList(CreateItemStatus.Type.Magnet);
                        break;
                    case ItemStatus.Type.CRYSTAL:
                        //氷の剣
                        player_ctr.setSwordList(PlayerStatus.SWORDTYPE.FROZEN);
                        break;
                    case ItemStatus.Type.SMOKE:
                        //闇の剣
                        player_ctr.setSwordList(PlayerStatus.SWORDTYPE.DARK);
                        break;
                    default:
                        //ゴミ
                        player_ctr.setCreateItemList(CreateItemStatus.Type.Dast);
                        break;
                }
                break;
        }
    }

    /// <summary>
    /// アイテム錬金
    /// </summary>
    /// <param name="item_0">ItemStatus.Type型のアイテム</param>
    /// <param name="item_1">CreateItem型のアイテム</param>
    public void MadeItem(ItemStatus.Type item_0, CreateItemStatus.Type item_1)
    {
        switch (item_0)
        {
            case ItemStatus.Type.CLAY_N:
                switch (item_1)
                {
                    case CreateItemStatus.Type.Explosive:
                        //火の剣
                        player_ctr.setSwordList(PlayerStatus.SWORDTYPE.FIRE);
                        break;
                    default:
                        player_ctr.setCreateItemList(CreateItemStatus.Type.Dast);
                        break;
                }
            break;
        }
    }

    /// <summary>
    /// アイテム錬金
    /// </summary>
    /// <param name="item_0"></param>
    /// <param name="item_1"></param>
    public void MadeItem(CreateItemStatus.Type item_0, ItemStatus.Type item_1)
    {
        switch (item_0)
        {
            case CreateItemStatus.Type.Explosive:
                switch (item_1)
                {
                    case ItemStatus.Type.CLAY_N:
                        //火の剣
                        player_ctr.setSwordList(PlayerStatus.SWORDTYPE.FIRE);
                        break;
                    default:
                        player_ctr.setCreateItemList(CreateItemStatus.Type.Dast);
                        Debug.Log("ゴミ");
                        break;
                }
                break;
        }
    }

    /// <summary>
    /// 錬金アイテムの効果
    /// </summary>
    public void AlchemyItem(CreateItemStatus.Type type)
    {
        switch (type)
        {
            case CreateItemStatus.Type.ATKPotion:
                item_ctr.ATKPortion();
                break;
            case CreateItemStatus.Type.Barrier:
                item_ctr.CreateBarrier();
                break;
            case CreateItemStatus.Type.Boomerang:

                break;
            case CreateItemStatus.Type.Drill:

                break;
            case CreateItemStatus.Type.Explosive:

                break;
            case CreateItemStatus.Type.FlyCloud:

                break;
            case CreateItemStatus.Type.HPPotion:
                item_ctr.HPPortion();
                break;
            case CreateItemStatus.Type.Inclubator:

                break;
            case CreateItemStatus.Type.Ladder:
                item_ctr.LadderCreate();
                break;
            case CreateItemStatus.Type.Lamp:

                break;
            case CreateItemStatus.Type.Lasso:

                break;
            case CreateItemStatus.Type.Magnet:

                break;
            case CreateItemStatus.Type.RainCloud:

                break;
            case CreateItemStatus.Type.SmokeBall:

                break;
            case CreateItemStatus.Type.SmokeScreen:

                break;
            case CreateItemStatus.Type.Torch:

                break;
            case CreateItemStatus.Type.Tornado:

                break;
            case CreateItemStatus.Type.Vajura:

                break;
            case CreateItemStatus.Type.Venom:

                break;
            case CreateItemStatus.Type.Watter:

                break;
            case CreateItemStatus.Type.TreePotion:
                item_ctr.TreePortion();
                break;
        }
    }
}
