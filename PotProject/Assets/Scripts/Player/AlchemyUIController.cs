using System.Collections;
using System.Collections.Generic;
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
    private int nowBox = 0;

    private MoveController move_ctr;

    //---ジョイスティックを回す時に使う変数-----
    private bool _one = false;
    private bool _two = false;
    private bool _three = false;
    private int RotationCount = 0;
    //------------------------------------------
    
    void Start () {
        //デバッグ用
        player_ctr.setItemList(ItemStatus.ITEM.SNAKE);
        player_ctr.setItemList(ItemStatus.ITEM.SLIME);
        //player_ctr.setItemList(ItemStatus.ITEM.GOLEM);
        nowBox = 0;
        move_ctr = GameObject.Find("Brother").GetComponent<MoveController>();
        ClearJoystickRotation();
    }
	
	// Update is called once per frame
	void Update () {
        RightJoyStickRotation();
        ItemFramemove();
    }

    /// <summary>
    /// アイテム選択フレームのリセット
    /// </summary>
    public void ItemFrameReSet()
    {
        nowBox = 0;
        ItemFrame.transform.position = Itembox[nowBox].transform.position;
    }

    /// <summary>
    /// アイテム選択フレームの移動処理
    /// </summary>
    private void ItemFramemove()
    {
        while (move_ctr.OnCrossUp || move_ctr.OnCrossDown)
        {
            if (!move_ctr.OnCrossUp && !move_ctr.OnCrossDown)
                break;

            if (move_ctr.OnCrossUp)
            {
                if (nowBox - 1 < 0)
                    nowBox = Itembox.Length - 1;
                else
                    nowBox--;
            }
            if (move_ctr.OnCrossDown)
            {
                if (Itembox.Length - 1 < nowBox + 1)
                    nowBox = 0;
                else
                    nowBox++;
            }
            ItemFrame.transform.position = Itembox[nowBox].transform.position;
            break;
        }
    }

    /// <summary>
    /// 右ジョイスティックを回転させる処理
    /// </summary>
    private void RightJoyStickRotation()
    {
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
            }
            else
            {
                player_ctr.ItemAlchemy(ItemStatus.ITEM.GOLEM);
                Debug.Log("生成");
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
