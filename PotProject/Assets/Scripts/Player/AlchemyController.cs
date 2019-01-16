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
     * 23.ゴミ
     */


    //生成アイテム
    private Sprite[] CreateItem;
    private TextAsset csvFile;
    private GimmickController gimmick_ctr;
    private MapInfo mInfo;
    //フレームの右下のImage
    private Image GeneratedImg;
    private PlayerController player_ctr;

    // Use this for initialization
    void Start () {
        player_ctr = GameObject.Find("Controller").GetComponent<PlayerController>();
        GeneratedImg = GameObject.Find("Canvas/Panel/Image").GetComponent<Image>();
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
        CreateItem = new Sprite[3];
        for(int i = 0; i < CreateItem.Length; i++)
        {
            CreateItem[i] = Resources.Load<Sprite>("Textures/CreateItem_" + i);
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
    /// 右下のフレームにある生成したアイテム画像をnullにする処理
    /// </summary>
    public void ReSetGeneratedImg()
    {
        GeneratedImg.sprite = null;
    }

    /// <summary>
    /// 右下のフレームにある生成したアイテム画像をセットする処理
    /// </summary>
    private void setGeneratedImg(CreateItemStatus.Type type)
    {
        if (player_ctr.getCreateItemList().Count >= 3)
            return;
        player_ctr.setCreateItemList(type);
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
            case ItemStatus.Type.CLAY:
                //木が成長
                gimmick_ctr = FindObjectOfType<GimmickController>();
                mInfo = transform.root.GetComponent<MapInfo>();
                gimmick_ctr.Grow();
                break;
            case ItemStatus.Type.WOOD:
                //たいまつ
                setGeneratedImg(CreateItemStatus.Type.Torch);
                break;
            case ItemStatus.Type.LAMP:
                //ランプ
                setGeneratedImg(CreateItemStatus.Type.Lamp);
                break;
            case ItemStatus.Type.SMOKE:
                //爆薬
                setGeneratedImg(CreateItemStatus.Type.Explosive);
                break;
            case ItemStatus.Type.CRYSTAL:
                //水
                setGeneratedImg(CreateItemStatus.Type.Watter);
                break;
            default:
                //ゴミができる
                setGeneratedImg(CreateItemStatus.Type.Dast);
                break;
        }
        Debug.Log(item.GetType());
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
                    case ItemStatus.Type.CLAY:
                        //はしご
                        setGeneratedImg(CreateItemStatus.Type.Ladder);
                        break;
                    case ItemStatus.Type.POWDER:
                        //投げ縄
                        setGeneratedImg(CreateItemStatus.Type.Lasso);
                        break;
                    default:
                        //ゴミ
                        setGeneratedImg(CreateItemStatus.Type.Dast);
                        break;
                }
                break;

            case ItemStatus.Type.CROWN:
                switch (item_1)
                {
                    case ItemStatus.Type.KEYROD:
                        //鍵
                        setGeneratedImg(CreateItemStatus.Type.Key);
                        break;
                    case ItemStatus.Type.CLAY:
                        //磁石
                        setGeneratedImg(CreateItemStatus.Type.Magnet);
                        break;
                    default:
                        //ゴミ
                        setGeneratedImg(CreateItemStatus.Type.Dast);
                        break;
                }
                break;

            case ItemStatus.Type.KEYROD:
                switch (item_1)
                {
                    case ItemStatus.Type.CROWN:
                        //鍵
                        setGeneratedImg(CreateItemStatus.Type.Key);
                        break;
                    case ItemStatus.Type.VAJURA:
                        //槍
                        break;
                    case ItemStatus.Type.LIZARD:
                        //斧
                        break;
                    default:
                        //ゴミ
                        setGeneratedImg(CreateItemStatus.Type.Dast);
                        break;
                }
                break;

            case ItemStatus.Type.FLOWER:
                switch (item_1)
                {
                    case ItemStatus.Type.POWDER:
                        //HPポーション
                        setGeneratedImg(CreateItemStatus.Type.HPPotion);
                        break;
                    case ItemStatus.Type.CLAY:
                        //攻撃ポーション
                        setGeneratedImg(CreateItemStatus.Type.ATKPotion);
                        break;
                    default:
                        //ゴミ
                        setGeneratedImg(CreateItemStatus.Type.Dast);
                        break;
                }
                break;

            case ItemStatus.Type.LIZARD:
                switch (item_0)
                {
                    case ItemStatus.Type.CLAY:
                        //バリア
                        setGeneratedImg(CreateItemStatus.Type.Barrier);
                        break;
                    case ItemStatus.Type.POWDER:
                        //ブーメラン
                        setGeneratedImg(CreateItemStatus.Type.Boomerang);
                        break;
                    case ItemStatus.Type.KEYROD:
                        //斧
                        break;
                    case ItemStatus.Type.VAJURA:
                        //ドリル
                        setGeneratedImg(CreateItemStatus.Type.Drill);
                        break;
                    default:
                        //ゴミ
                        setGeneratedImg(CreateItemStatus.Type.Dast);
                        break;
                }
                break;

            case ItemStatus.Type.WOOD:
                switch (item_1)
                {
                    case ItemStatus.Type.CRYSTAL:
                        //培養液
                        setGeneratedImg(CreateItemStatus.Type.Inclubator);
                        break;
                    default:
                        //ゴミ
                        setGeneratedImg(CreateItemStatus.Type.Dast);
                        break;
                }
                break;

            case ItemStatus.Type.VAJURA:
                switch (item_1)
                {
                    case ItemStatus.Type.CLAY:
                        //バジュラ（電撃武器）
                        setGeneratedImg(CreateItemStatus.Type.Vajura);
                        break;
                    case ItemStatus.Type.KEYROD:
                        //槍
                        break;
                    case ItemStatus.Type.LIZARD:
                        //ドリル
                        setGeneratedImg(CreateItemStatus.Type.Drill);
                        break;
                    default:
                        //ゴミ
                        setGeneratedImg(CreateItemStatus.Type.Dast);
                        break;
                }
                break;

            case ItemStatus.Type.MIC:
                switch (item_1)
                {
                    case ItemStatus.Type.CLAY:
                        //拡声器
                        setGeneratedImg(CreateItemStatus.Type.Speaker);
                        break;
                    default:
                        //ゴミ
                        setGeneratedImg(CreateItemStatus.Type.Dast);
                        break;
                }
                break;

            case ItemStatus.Type.CLOUD:
                switch (item_1)
                {
                    case ItemStatus.Type.POWDER:
                        //飛べる雲
                        setGeneratedImg(CreateItemStatus.Type.FlyCloud);
                        break;
                    case ItemStatus.Type.SMOKE:
                        //竜巻
                        setGeneratedImg(CreateItemStatus.Type.Tornado);
                        break;
                    case ItemStatus.Type.CRYSTAL:
                        //雨雲
                        setGeneratedImg(CreateItemStatus.Type.RainCloud);
                        break;
                    default:
                        //ゴミ
                        setGeneratedImg(CreateItemStatus.Type.Dast);
                        break;
                }
                break;

            case ItemStatus.Type.CRYSTAL:
                switch (item_1)
                {
                    case ItemStatus.Type.CLOUD:
                        //雨雲
                        setGeneratedImg(CreateItemStatus.Type.RainCloud);
                        break;
                    case ItemStatus.Type.WOOD:
                        //培養液
                        setGeneratedImg(CreateItemStatus.Type.Inclubator);
                        break;
                    case ItemStatus.Type.POWDER:
                        //毒液
                        setGeneratedImg(CreateItemStatus.Type.Venom);
                        break;
                    case ItemStatus.Type.CLAY:
                        //氷の剣
                        break;
                    default:
                        //ゴミ
                        setGeneratedImg(CreateItemStatus.Type.Dast);
                        break;
                }
                break;

            case ItemStatus.Type.SMOKE:
                switch (item_1)
                {
                    case ItemStatus.Type.CLOUD:
                        //竜巻
                        setGeneratedImg(CreateItemStatus.Type.Tornado);
                        break;
                    case ItemStatus.Type.POWDER:
                        //煙幕
                        setGeneratedImg(CreateItemStatus.Type.SmokeScreen);
                        break;
                    case ItemStatus.Type.CLAY:
                        //闇の剣
                        break;
                    default:
                        //ゴミ
                        setGeneratedImg(CreateItemStatus.Type.Dast);
                        break;
                }
                break;

            case ItemStatus.Type.POWDER:
                switch (item_1)
                {
                    case ItemStatus.Type.SNAKE:
                        //投げ縄
                        setGeneratedImg(CreateItemStatus.Type.Lasso);
                        break;
                    case ItemStatus.Type.CLOUD:
                        //飛べる雲
                        setGeneratedImg(CreateItemStatus.Type.FlyCloud);
                        break;
                    case ItemStatus.Type.FLOWER:
                        //HPポーション
                        setGeneratedImg(CreateItemStatus.Type.HPPotion);
                        break;
                    case ItemStatus.Type.SMOKE:
                        //煙幕
                        setGeneratedImg(CreateItemStatus.Type.SmokeScreen);
                        break;
                    case ItemStatus.Type.LIZARD:
                        //ブーメラン
                        setGeneratedImg(CreateItemStatus.Type.Boomerang);
                        break;
                    case ItemStatus.Type.CRYSTAL:
                        //毒液
                        setGeneratedImg(CreateItemStatus.Type.Venom);
                        break;
                    default:
                        //ゴミ
                        setGeneratedImg(CreateItemStatus.Type.Dast);
                        break;
                }
                break;

            case ItemStatus.Type.CLAY:
                switch (item_1)
                {
                    case ItemStatus.Type.FLOWER:
                        //攻撃ポーション
                        setGeneratedImg(CreateItemStatus.Type.ATKPotion);
                        break;
                    case ItemStatus.Type.MIC:
                        //拡声器
                        setGeneratedImg(CreateItemStatus.Type.Speaker);
                        break;
                    case ItemStatus.Type.VAJURA:
                        //バジュラ（電撃武器）
                        setGeneratedImg(CreateItemStatus.Type.Vajura);
                        break;
                    case ItemStatus.Type.LIZARD:
                        //バリア
                        setGeneratedImg(CreateItemStatus.Type.Barrier);
                        break;
                    case ItemStatus.Type.SNAKE:
                        //はしご
                        setGeneratedImg(CreateItemStatus.Type.Ladder);
                        break;
                    case ItemStatus.Type.CROWN:
                        //磁石
                        setGeneratedImg(CreateItemStatus.Type.Magnet);
                        break;
                    case ItemStatus.Type.CRYSTAL:
                        //氷の剣
                        break;
                    case ItemStatus.Type.SMOKE:
                        //闇の剣
                        break;
                    default:
                        //ゴミ
                        setGeneratedImg(CreateItemStatus.Type.Dast);
                        break;
                }
                break;
        }
        Debug.Log(item_0.GetType() + "×" + item_1.GetType());
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
            case ItemStatus.Type.CLAY:
                switch (item_1)
                {
                    case CreateItemStatus.Type.Explosive:
                        //火の剣
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
                    case ItemStatus.Type.CLAY:
                        //火の剣
                        break;
                }
                break;
        }
    }

}
