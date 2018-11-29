using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class AlchemyUIController : MonoBehaviour {

    [SerializeField,Header("アイテム欄のボックス")]
    private GameObject[] Itembox;

    [SerializeField, Header("アイテムの画像")]
    private Sprite[] ItemImage;

    [SerializeField]
    private PlayerController player_ctr;

    [SerializeField]
    private GameObject ItemFrame;
    [SerializeField]
    private GameObject IntoPot;
    //---IntoPotの子オブジェクト---------
    private GameObject mtr_0;
    private GameObject mtr_1;
    //-----------------------------------
    private int nowBox = 0;
    private GameObject[] Box_item;
    private bool _boxRight = true;

    private MoveController move_ctr;

    //---ジョイスティックを回す時に使う変数-----
    private bool _one = false;
    private bool _two = false;
    private bool _three = false;
    private int RotationCount = 0;
    //------------------------------------------
    
    void Start () {
        //デバッグ
        player_ctr.setItemList(ItemStatus.ITEM.SLIME);
        player_ctr.setItemList(ItemStatus.ITEM.SNAKE);
        nowBox = 0;
        move_ctr = GameObject.Find("Brother/Body").GetComponent<MoveController>();
        mtr_0 = IntoPot.transform.GetChild(0).GetChild(0).gameObject;
        mtr_1 = IntoPot.transform.GetChild(0).GetChild(1).gameObject;
        _boxRight = true;
        Box_item = new GameObject[Itembox.Length];
        ClearJoystickRotation();
    }
	
	// Update is called once per frame
	void Update () {
        RightJoyStickRotation();
        ItemFrameMove();
        PickItem();
    }
    
    /// <summary>
    /// アイテムの決定
    /// </summary>
    private void PickItem()
    {
        if (!move_ctr.OnCircle)
            return;

        if (_boxRight)
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
            else if (mtr_1_img.sprite == null)
            {
                mtr_1_img.sprite = img;
            }
        }
        else
        {
            Box_item[nowBox].GetComponent<Image>().sprite = null;
        }
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
                player_ctr.ItemAlchemy(ItemStatus.ITEM.GOLEM);
                ClearJoystickRotation();
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
    /// アイテム欄の画像を変える処理
    /// </summary>
    public void setItemboxImage()
    {
        List<ItemStatus.ITEM> items = player_ctr.getItemList();
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
