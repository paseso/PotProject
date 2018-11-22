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

    private MoveController move_ctr;

    //---ジョイスティックを回す時に使う変数-----
    private bool _one = false;
    private bool _two = false;
    private bool _three = false;
    private int RotationCount = 0;
    //------------------------------------------


    // Use this for initialization
    void Start () {
        //デバッグ用
        player_ctr.setItemList(ItemStatus.ITEM.SNAKE);
        player_ctr.setItemList(ItemStatus.ITEM.SLIME);
        //player_ctr.setItemList(ItemStatus.ITEM.GOLEM);
        move_ctr = GameObject.Find("Brother").GetComponent<MoveController>();
        _one = false;
        _two = false;
        _three = false;
        RotationCount = 0;
    }
	
	// Update is called once per frame
	void Update () {
        RightJoyStickRotation();
    }

    /// <summary>
    /// 右ジョイスティックを回転させる処理
    /// </summary>
    private void RightJoyStickRotation()
    {
        if (!move_ctr._onRJoystickUp && !move_ctr._onRJoystickDown && !move_ctr._onRJoystickRight && !move_ctr._onRJoystickLeft)
        {
            _one = false;
            _two = false;
            _three = false;
            RotationCount = 0;
        }

        if (move_ctr._onRJoystickUp)
            _one = true;
        if (move_ctr._onRJoystickRight)
            _two = true;
        if (move_ctr._onRJoystickDown)
            _three = true;

        if (move_ctr._onRJoystickLeft)
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
                _one = false;
                _two = false;
                _three = false;
                RotationCount = 0;
            }
        }
    }

    /// <summary>
    /// アイテム欄の画像を変える処理
    /// </summary>
    public void setItemboxImage()
    {
        List<ItemStatus.ITEM> items = player_ctr.getItemList();
        Debug.Log("items: " + items);
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
