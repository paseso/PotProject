using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class AlchemyUIController : MonoBehaviour {

    //錬金UIにある持っている素材一覧の欄オブジェクト
    private GameObject[] Itembox;

    //アイテムの画像
    private Sprite[] ItemImage;

    private PlayerController player_ctr;

    private GameObject ItemFrame;
    private GameObject IntoPot;
    //---IntoPotの子オブジェクト---------
    private GameObject mtr_0;
    private GameObject mtr_1;
    //-----------------------------------
    private int nowBox = 0;
    private GameObject[] Box_item;
    private List<ItemStatus.ITEM> items = new List<ItemStatus.ITEM>();
    private List<ItemStatus.ITEM> Materials_item = new List<ItemStatus.ITEM>();
    private bool _boxRight = true;

    private MoveController move_ctr;
    private CrossAxisDown crossAxisdown;

    //---ジョイスティックを回す時に使う変数-----
    private bool _one = false;
    private bool _two = false;
    private bool _three = false;
    private int RotationCount = 0;
    //------------------------------------------

    private void Awake()
    {
    }

    void Start ()
    {
        //デバッグ
        //player_ctr.setItemList(ItemStatus.ITEM.SLIME);
        //player_ctr.setItemList(ItemStatus.ITEM.SNAKE);
        try
        {
            move_ctr = GameObject.FindObjectOfType<MoveController>();
            setItembox();
            setItemImageList();
            ItemFrame = gameObject.transform.GetChild(gameObject.transform.childCount - 1).gameObject;
            IntoPot = gameObject.transform.GetChild(gameObject.transform.childCount - 2).gameObject;
            mtr_0 = IntoPot.transform.GetChild(0).GetChild(0).gameObject;
            mtr_1 = IntoPot.transform.GetChild(0).GetChild(1).gameObject;
            crossAxisdown = move_ctr.gameObject.GetComponent<CrossAxisDown>();
            player_ctr = GameObject.Find("Controller").GetComponent<PlayerController>();
        }
        catch (Exception e)
        {
            Debug.Log(e + "がないよ！");
        }


        nowBox = 0;

        _boxRight = true;
        Box_item = new GameObject[Itembox.Length];
        ClearJoystickRotation();
        Materials_item.Clear();
    }
	
	// Update is called once per frame
	void Update () {
        RightJoyStickRotation();
        ItemFrameMove();
    }
    
    /// <summary>
    /// アイテムの決定
    /// </summary>
    public void PickItem()
    {
        if (_boxRight)
            setMaterialsBox();
        else
            ReSetMaterials(nowBox);
    }

    /// <summary>
    /// 素材アイテムボックス欄のセット
    /// </summary>
    private void setMaterialsBox()
    {
        Sprite img = Box_item[nowBox].GetComponent<Image>().sprite;
        if (img == null)
            return;

        Image mtr_0_img = mtr_0.GetComponent<Image>();
        Image mtr_1_img = mtr_1.GetComponent<Image>();
        if (mtr_0_img.sprite == null)
        {
            mtr_0_img.sprite = img;
        }
        else if (mtr_0_img.sprite != img)
        {
            if(mtr_1_img.sprite == null)
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
    private void ReSetMaterials(int num)
    {
        //押したボックスの画像に何か入っていれば通る
        if (Box_item[num].GetComponent<Image>().sprite == null)
            return;

        Box_item[num].GetComponent<Image>().sprite = null;

        //もし素材ボックスに2個アイテムを入れてた場合2個目の画像を1個目のボックスに移す
        //Materials_itemがリストなので1個目をRemoveして2個目もRemoveしようとした場合エラーが起きるため
        if (Materials_item.Count == 2 || num == 0)
        {
            mtr_0.GetComponent<Image>().sprite = mtr_1.GetComponent<Image>().sprite;
            mtr_1.GetComponent<Image>().sprite = null;
        }
        Materials_item.RemoveAt(num);
    }

    /// <summary>
    /// 素材アイテム欄のリセット
    /// </summary>
    /// <param name="items">二つリセット</param>
    private void ReSetMaterialsBox(List<ItemStatus.ITEM> items)
    {
        mtr_0.GetComponent<Image>().sprite = null;
        mtr_1.GetComponent<Image>().sprite = null;
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
        Array.Copy(Itembox, Box_item, Itembox.Length);
        ItemFrame.transform.position = Box_item[0].transform.position;
        _boxRight = true;
    }

    /// <summary>
    /// アイテム選択フレームの移動処理
    /// </summary>
    private void ItemFrameMove()
    {
        if (crossAxisdown.getKeepDown)
        {
            return;
        }
            
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
            break;
        }
        //左右押した時
        while(move_ctr.OnCrossRigtht || move_ctr.OnCrossLeft)
        {
            if (!move_ctr.OnCrossRigtht && !move_ctr.OnCrossLeft)
                break;

            if (_boxRight)
            {
                Box_item = new GameObject[2];
                Box_item[0] = mtr_0;
                Box_item[1] = mtr_1;
                _boxRight = false;
            }
            else
            {
                Box_item = new GameObject[Itembox.Length];
                Array.Copy(Itembox, Box_item, Itembox.Length);
                _boxRight = true;
            }
            nowBox = 0;
            ItemFrame.transform.position = Box_item[0].transform.position;
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
        if (Materials_item == null)
            return;

        if (move_ctr.OnRJoystickUp)
            _one = true;
        if (move_ctr.OnRJoystickRight)
            _two = true;
        if (move_ctr.OnRJoystickDown)
            _three = true;

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
    /// Itemboxに子オブジェクトをセット
    /// </summary>
    private void setItembox()
    {
        Itembox = new GameObject[3];
        GameObject Items = gameObject.transform.GetChild(0).gameObject;
        for(int i = 0; i < Items.transform.childCount; i++)
        {
            Itembox[i] = Items.transform.GetChild(i).gameObject;
        }
    }

    /// <summary>
    /// アイテムの画像をセット
    /// </summary>
    private void setItemImageList()
    {
        ItemImage = new Sprite[3];
        for(int i = 0; i < ItemImage.Length; i++)
        {
            Sprite img = Resources.Load<Sprite>("Textures/background_normal" + i);
            ItemImage[i] = img;
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
            int num = 3 - items.Count;
            for(int j = 2; items.Count <= j; j--)
            {
                Image item_img = Itembox[j].GetComponent<Image>();
                item_img.sprite = null;
            }
        }

        for (int i = 0; i < items.Count; i++)
        {
            Image item_img = Itembox[i].GetComponent<Image>();

            switch (items[i])
            {
                case ItemStatus.ITEM.SLIME:
                    item_img.sprite = ItemImage[0];
                    break;
                case ItemStatus.ITEM.GOLEM:
                    item_img.sprite = ItemImage[1];
                    break;
                case ItemStatus.ITEM.SNAKE:
                    item_img.sprite = ItemImage[2];
                    break;
            }
        }
    }
}