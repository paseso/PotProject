using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AlchemyUIController : MonoBehaviour
{

    //Item_img
    /* 
     * 0.粘土
     * 1.ランプ
     * 2.ヘビ
     * 3.花
     * 4.雲
     * 5.王冠
     * 6.クリスタル
     * 7.鍵棒
     * 8.トカゲ
     * 9.コウモリ
     * 10.鱗粉
     * 11.バジュラ
     * 12.木材
     * 13.煙玉
     * 14.火薬
     * 
     */

    //錬金UIにある持っている素材一覧の欄オブジェクト
    private GameObject[] Itembox;
    //錬金してできたアイテムボックス欄のオブジェクト
    private GameObject[] createItembox;
    //アイテムの画像
    private Sprite[] ItemImage;

    private PlayerController player_ctr;
    private PlayerStatus status;

    private GameObject ItemFrame;
    private GameObject PotFrame;
    private GameObject ChooseObj;
    //---錬金に使う素材欄のオブジェクト---------
    private GameObject mtr_0;
    private GameObject mtr_1;
    //-----------------------------------
    private Sprite AlphaSprite;
    private int nowBox = 0;
    private GameObject[] Box_item;
    //
    private List<ItemStatus.Type> items = new List<ItemStatus.Type>();
    //
    private List<ItemStatus.Type> Materials_item = new List<ItemStatus.Type>();

    private MoveController move_ctr;
    private CrossAxisDown crossAxisdown;
    private AlchemyController alchemy_ctr;

    //---錬金UI中のフレームの縦ラインの位置-----------
    private int frameLine = 0000;
    private int frame_right = 0001;
    private int frame_center = 0010;
    private int frame_left = 0100;
    //-------------------------------------------------

    //---ジョイスティックを回す時に使う変数-----
    private bool _one = false;
    private bool _two = false;
    private bool _three = false;
    private int RotationCount = 0;
    //------------------------------------------

    private int beforeNowBox = -1;

    //捨てる捨てないのobjectがでてるかでてないかの判定
    private bool _chooseWindow = false;

    public bool ChooseWindow
    {
        get { return _chooseWindow; }
    }

    private int nowAlchemyItem;
    public int getNowAlchemyItem
    {
        get { return nowAlchemyItem; }
    }

    public int getNowBox
    {
        get { return nowBox; }
    }

    void Start()
    {
        try
        {
            status = FindObjectOfType<PlayerManager>().Status;
            player_ctr = GameObject.Find("Controller").GetComponent<PlayerController>();
            move_ctr = GameObject.FindObjectOfType<MoveController>();
            crossAxisdown = move_ctr.gameObject.GetComponent<CrossAxisDown>();
            alchemy_ctr = GameObject.FindObjectOfType<AlchemyController>();

            ChooseObj = gameObject.transform.Find("ThrowChoose").gameObject;
            ChooseObj.SetActive(false);
            ItemFrame = gameObject.transform.Find("SelectFrame").gameObject;
            PotFrame = gameObject.transform.Find("AlchemyPotFrame").gameObject;
            mtr_0 = PotFrame.transform.Find("AlchemyPotin/Material/material_0").gameObject.transform.GetChild(0).gameObject;
            mtr_1 = PotFrame.transform.Find("AlchemyPotin/Material/material_1").gameObject.transform.GetChild(0).gameObject;
            AlphaSprite = Resources.Load<Sprite>("Textures/UI/AlphaImage");
        }
        catch (Exception e)
        {
            Debug.LogWarning(e + "がないよ！");
        }
        nowAlchemyItem = 0;
        nowBox = 0;
        beforeNowBox = -1;
        _chooseWindow = false;
        setItembox();
        setItemImageList();
        StartCreateItemBox();
        Box_item = new GameObject[Itembox.Length];
        ClearJoystickRotation();
        Materials_item.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        RightJoyStickRotation();
        ItemFrameMove();
    }

    /// <summary>
    /// スタート時にCreateItemboxに画像を変える場所を指定
    /// </summary>
    private void StartCreateItemBox()
    {
        createItembox = new GameObject[3];
        GameObject Items = gameObject.transform.Find("CreateItems").gameObject;
        for (int i = 0; i < Items.transform.childCount; i++)
        {
            createItembox[i] = Items.transform.GetChild(i).GetChild(0).gameObject;
        }
    }

    /// <summary>
    /// アイテムの決定
    /// </summary>
    public void PickItem()
    {
        if (beforeNowBox == nowBox)
            return;
        if (frameLine == frame_right)
        {
            setMaterialsBox();
        }
        else if(frameLine == frame_center)
        {
            ReSetMaterialsBox(nowBox);
        }
        beforeNowBox = nowBox;
        SoundManager.Instance.PlaySe((int)SoundManager.SENAME.SE_CHOICE);
    }

    /// <summary>
    /// イメージ画像がなかった時に仮画像を入れる処理
    /// </summary>
    /// <returns></returns>
    private bool NullCheckImage(int num)
    {
        bool _null = false;

        if (ItemImage[num] != AlphaSprite)
            return _null;

        Sprite img = Resources.Load<Sprite>("TexturesAlchemyUI_items/AlchemyUI_item0");
        ItemImage[num] = img;

        return _null;
    }

    /// <summary>
    /// 素材アイテムボックス欄のセット
    /// </summary>
    private void setMaterialsBox()
    {
        Sprite img = Box_item[nowBox].GetComponent<Image>().sprite;
        if (img == AlphaSprite)
            return;

        Image mtr_0_img = mtr_0.GetComponent<Image>();
        Image mtr_1_img = mtr_1.GetComponent<Image>();
        if (mtr_0_img.sprite == AlphaSprite)
        {
            mtr_0_img.sprite = img;
        }
        else if (mtr_0_img.sprite != img)
        {
            if (mtr_1_img.sprite == AlphaSprite)
            {
                mtr_1_img.sprite = img;
            }
        }

        Materials_item.Add(items[nowBox]);
    }

    /// <summary>
    /// 素材アイテム欄のリセット
    /// </summary>
    /// <param name="num">一個だけリセット</param>
    public void ReSetMaterialsBox(int num)
    {
        //押したボックスの画像に何か入っていれば通る
        if (Box_item[num].GetComponent<Image>().sprite == AlphaSprite)
            return;

        Box_item[num].GetComponent<Image>().sprite = AlphaSprite;

        //もし素材ボックスに2個アイテムを入れてた場合2個目の画像を1個目のボックスに移す
        //Materials_itemがリストなので1個目をRemoveして2個目もRemoveしようとした場合エラーが起きるため
        if (Materials_item.Count == 2 || num == 0)
        {
            mtr_0.GetComponent<Image>().sprite = mtr_1.GetComponent<Image>().sprite;
            mtr_1.GetComponent<Image>().sprite = AlphaSprite;
        }
        Materials_item.RemoveAt(num);
    }

    /// <summary>
    /// 素材アイテム欄のリセット
    /// </summary>
    /// <param name="type"></param>
    public void ReSetMaterialsBox(ItemStatus.Type type)
    {
        //押したボックスの画像に何か入っていれば通る
        if (Box_item[nowBox].GetComponent<Image>().sprite == AlphaSprite)
            return;
        Box_item[nowBox].GetComponent<Image>().sprite = AlphaSprite;
        //もし素材ボックスに2個アイテムを入れてた場合2個目の画像を1個目のボックスに移す
        //Materials_itemがリストなので1個目をRemoveして2個目もRemoveしようとした場合エラーが起きるため
        if (Materials_item.Count == 2)
        {
            mtr_0.GetComponent<Image>().sprite = mtr_1.GetComponent<Image>().sprite;
            mtr_1.GetComponent<Image>().sprite = AlphaSprite;
        }
        Materials_item.Remove(type);
    }

    /// <summary>
    /// アイテムを捨てるかどうか選択するUIを表示処理
    /// </summary>
    public void ActiveThrowItemUI()
    {
        if (Box_item[nowBox].GetComponent<Image>().sprite == AlphaSprite)
            return;

        ChooseObj.transform.Find("ThrowItem").GetComponent<Image>().sprite = Box_item[nowBox].GetComponent<Image>().sprite;
        ChooseObj.SetActive(true);
        _chooseWindow = true;
    }

    /// <summary>
    /// 捨てるか捨てないかの選択処理
    /// </summary>
    /// <returns></returns>
    public void ChooseThrow(bool _choose)
    {
        if (_choose)
        {
            deleteItemBox(getNowBox);
        }
        ChooseObj.SetActive(false);
        _chooseWindow = false;
    }

    /// <summary>
    /// 取得した素材一覧にある素材を一つ削除
    /// </summary>
    /// <param name="num"></param>
    public void deleteItemBox(int num)
    {
        if (Itembox[num].GetComponent<Image>().sprite == AlphaSprite)
            return;

        if (mtr_0.GetComponent<Image>().sprite == Itembox[num].GetComponent<Image>().sprite)
        {
            ReSetMaterialsBox(0);
        }
        else if (mtr_1.GetComponent<Image>().sprite == Itembox[num].GetComponent<Image>().sprite)
        {
            ReSetMaterialsBox(1);
        }
        //画像を消す
        Itembox[num].GetComponent<Image>().sprite = AlphaSprite;

        //持ち物リストからも削除
        if (Materials_item.Count > 0)
            Materials_item.Remove(status.ItemList[num]);
        player_ctr.deleteItemList(status.ItemList[num]);
    }

    /// <summary>
    /// 素材アイテム欄のリセット
    /// </summary>
    /// <param name="items">二つリセット</param>
    public void ReSetMaterialsBox(List<ItemStatus.Type> items)
    {
        mtr_0.GetComponent<Image>().sprite = AlphaSprite;
        mtr_1.GetComponent<Image>().sprite = AlphaSprite;
        if (Materials_item.Count == 0)
            return;
        Materials_item.RemoveRange(0, Materials_item.Count - 1);
    }

    /// <summary>
    /// アイテム選択フレームのリセット
    /// </summary>
    public void ItemFrameReSet()
    {
        nowBox = 0;
        if(Box_item.Length == 2)
        {
            Box_item = new GameObject[Itembox.Length];
        }
        Array.Copy(Itembox, Box_item, Itembox.Length);
        ItemFrame.transform.position = Box_item[0].transform.position;
        frameLine = frame_right;
    }

    /// <summary>
    /// フレームの位置フラグのビットフラグの変更
    /// </summary>
    private int BitFrameLine(bool _right)
    {
        if((frameLine & frame_left) > 0)
        {
            if (_right)
            {
                frameLine = frame_center;
            }
            else
            {
                frameLine = frame_right;
            }
        }else if((frameLine & frame_center) > 0)
        {
            if (_right)
            {
                frameLine = frame_right;
            }
            else
            {
                frameLine = frame_left;
            }
        }
        else if((frameLine & frame_right) > 0)
        {
            if (_right)
            {
                frameLine = frame_left;
            }
            else
            {
                frameLine = frame_center;
            }
        }
        return frameLine;
    }

    /// <summary>
    /// フレームが左右に移動する時にBox_itemの中身も変更する処理
    /// </summary>
    /// <param name="nowLine">今の位置</param>
    private void BoxItemChange(int nowLine)
    {
        if ((nowLine & frame_left) > 0)
        {
            Box_item = new GameObject[createItembox.Length];
            Array.Copy(createItembox, Box_item, createItembox.Length);
        }
        else if ((nowLine & frame_center) > 0)
        {
            Box_item = new GameObject[2];
            Box_item[0] = mtr_0;
            Box_item[1] = mtr_1;
        }
        else if ((nowLine & frame_right) > 0)
        {
            Box_item = new GameObject[Itembox.Length];
            Array.Copy(Itembox, Box_item, Itembox.Length);
        }
    }

    /// <summary>
    /// アイテム選択フレームの移動処理
    /// </summary>
    private void ItemFrameMove()
    {
        if (crossAxisdown.getKeepDown || _chooseWindow)
            return;

        //上下押した時
        while (move_ctr.OnCrossUp || move_ctr.OnCrossDown)
        {
            if (!move_ctr.OnCrossUp && !move_ctr.OnCrossDown)
                break;
            if (move_ctr.OnCrossUp)
            {
                if (nowBox - 1 < 0)
                    nowBox = Box_item.Length - 1;
                else
                    nowBox--;
            }
            if (move_ctr.OnCrossDown)
            {
                if (Box_item.Length - 1 < nowBox + 1)
                    nowBox = 0;
                else
                    nowBox++;
            }
            ItemFrame.transform.position = Box_item[nowBox].transform.position;
            SoundManager.Instance.PlaySe((int)SoundManager.SENAME.SE_SELECT);
            break;
        }
        //左右押した時
        while (move_ctr.OnCrossRight || move_ctr.OnCrossLeft)
        {
            if (!move_ctr.OnCrossRight && !move_ctr.OnCrossLeft)
                break;

            if (move_ctr.OnCrossRight)
            {
                BoxItemChange(BitFrameLine(true));
            }
            else if(move_ctr.OnCrossLeft)
            {
                BoxItemChange(BitFrameLine(false));
            }

            nowBox = 0;
            ItemFrame.transform.position = Box_item[0].transform.position;
            SoundManager.Instance.PlaySe((int)SoundManager.SENAME.SE_SELECT);
            break;
        }
    }

    /// <summary>
    /// 右ジョイスティックを回転させる処理
    /// </summary>
    private void RightJoyStickRotation()
    {
        //該当のボタンが押されてない時
        if (!move_ctr.OnRJoystickUp && !move_ctr.OnRJoystickDown && !move_ctr.OnRJoystickRight && !move_ctr.OnRJoystickLeft)
        {
            ClearJoystickRotation();
        }

        if (Materials_item.Count == 0)
            return;

        if (move_ctr.OnRJoystickUp)
            _one = true;
        if (move_ctr.OnRJoystickRight)
            _two = true;
        if (move_ctr.OnRJoystickDown)
            _three = true;

        float h, v;

        var controllerNames = Input.GetJoystickNames();
        //if (controllerNames[0] == "")
        //{
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        //}
        //else
        //{
        //h = Input.GetAxis("RightHorizontal_ps4");
        //v = Input.GetAxis("RightVertical_ps4");
        //}

        float total = Mathf.Atan2(v, h);
        total *= Mathf.Rad2Deg;

        var direction = new Vector3(0, 0, total);
        gameObject.transform.GetChild(0).GetChild(0).DOLocalRotate(direction, 0.5f);

        if (move_ctr.OnRJoystickLeft)
        {
            if (!_one || !_two || !_three)
                return;
            if (RotationCount < 3)
            {
                RotationCount++;
                _one = false;
                _two = false;
                _three = false;
            }
            else
            {
                //アイテムを生成し、ジョイスティックの回した回数をリセット
                player_ctr.ItemAlchemy(Materials_item);
                ClearJoystickRotation();
                //錬金に使った素材を持ち物リストから削除
                player_ctr.deleteItemList(Materials_item);
                //それに伴って錬金UIの持ち物リストの更新
                setItemboxImage();
                //錬金に使いたいアイテム欄の画像とそのリストをリセット
                ReSetMaterialsBox(Materials_item);
                Materials_item.Clear();
                player_ctr.OpenAlchemy();
            }
        }
    }

    /// <summary>
    /// ジョイスティックを回転させる処理の初期化
    /// </summary>
    private void ClearJoystickRotation()
    {
        _one = false;
        _two = false;
        _three = false;
        RotationCount = 0;
    }

    /// <summary>
    /// 錬金UIにある錬金されたアイテムボックスに画像をセットする処理
    /// </summary>
    public void setCreateItemUI()
    {
        int item_num = player_ctr.getCreateItemList().Count;
        if (item_num < 3)
        {
            for (int i = item_num; 0 <= i; i--)
            {
                createItembox[i].GetComponent<Image>().sprite = AlphaSprite;
            }
        }
        if (item_num == 0)
            return;
        for (int i = 0; i < item_num; i++)
        {
            CreateItemStatus.Type type = player_ctr.getCreateItemList()[i];
            int num = (int)type;
            createItembox[i].GetComponent<Image>().sprite = alchemy_ctr.getCreateItem[num];
        }
    }

    /// <summary>
    /// 錬金UIにある錬金されたアイテムボックスの画像を1つ削除する処理
    /// </summary>
    public void deleteCreateItemUI(CreateItemStatus.Type type)
    {
        player_ctr.deleteCreateItemList(type);
        setCreateItemUI();
    }

    /// <summary>
    /// Itemboxに子オブジェクトをセット
    /// </summary>
    private void setItembox()
    {
        Itembox = new GameObject[3];
        GameObject Items = gameObject.transform.Find("MaterialItems").gameObject;
        for (int i = 0; i < Items.transform.childCount; i++)
        {
            Itembox[i] = Items.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject;
        }
    }

    /// <summary>
    /// アイテムの画像をセット
    /// </summary>
    private void setItemImageList()
    {
        ItemImage = new Sprite[14];
        Sprite img;
        for (int i = 0; i < ItemImage.Length; i++)
        {
            if (!NullCheckImage(i))
            {
                img = Resources.Load<Sprite>("Textures/AlchemyUI_items/AlchemyUI_item" + i);
                ItemImage[i] = img;
            }
            else
            {
                NullCheckImage(i);
            }
        }
    }

    /// <summary>
    /// アイテム欄の画像を変える処理
    /// </summary>
    public void setItemboxImage()
    {
        items = player_ctr.getItemList();

        if (items.Count < 3)
        {
            for (int j = 2; items.Count <= j; j--)
            {
                Image item_img = Itembox[j].GetComponent<Image>();
                item_img.sprite = AlphaSprite;
            }
        }

        for (int i = 0; i < items.Count; i++)
        {
            Image item_img = Itembox[i].GetComponent<Image>();

            switch (items[i])
            {
                case ItemStatus.Type.CLAY_N:
                    item_img.sprite = ItemImage[0];
                    break;
                case ItemStatus.Type.LAMP:
                    item_img.sprite = ItemImage[1];
                    break;
                case ItemStatus.Type.SNAKE:
                    item_img.sprite = ItemImage[2];
                    break;
                case ItemStatus.Type.FLOWER:
                    item_img.sprite = ItemImage[3];
                    break;
                case ItemStatus.Type.CLOUD:
                    item_img.sprite = ItemImage[4];
                    break;
                case ItemStatus.Type.CROWN:
                    item_img.sprite = ItemImage[5];
                    break;
                case ItemStatus.Type.CRYSTAL:
                    item_img.sprite = ItemImage[6];
                    break;
                case ItemStatus.Type.KEYROD:
                    item_img.sprite = ItemImage[7];
                    break;
                case ItemStatus.Type.LIZARD:
                    item_img.sprite = ItemImage[8];
                    break;
                case ItemStatus.Type.MIC:
                    item_img.sprite = ItemImage[9];
                    break;
                case ItemStatus.Type.POWDER:
                    item_img.sprite = ItemImage[10];
                    break;
                case ItemStatus.Type.VAJURA:
                    item_img.sprite = ItemImage[11];
                    break;
                case ItemStatus.Type.WOOD:
                    item_img.sprite = ItemImage[12];
                    break;
                case ItemStatus.Type.SMOKE:
                    item_img.sprite = ItemImage[13];
                    break;
            }
        }
    }

    /// <summary>
    /// 今セットされてる錬金アイテムの変更処理
    /// </summary>
    public void setNowAlchemyItem()
    {
        int num = player_ctr.getCreateItemList().Count;
        if (num == 0)
            return;

        if (nowAlchemyItem >= num - 1)
            nowAlchemyItem = 0;
        else
            nowAlchemyItem++;

        alchemy_ctr.setGeneratedImg(player_ctr.getCreateItemList()[nowAlchemyItem]);
    }
}