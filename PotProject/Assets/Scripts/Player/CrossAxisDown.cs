using UnityEngine;

/*
 * 前回の入力の値と今回の入力の値
 * 前回と今回を比較して差分があった場合にフラグを立てる
 * フラグ: ダウンフラグ、ずっと押してるフラグ、
 * ダウンフラグ -> 一回フラグがたったら次のフレームでフラグを下す
 * ずっと押してるフラグ -> 入力がなくならない限りはフラグをたてる。入力がなくなったらフラグを下す
 * それを４方向分つくる
 */

public class CrossAxisDown : MonoBehaviour {

    //前回のAxis値
    private float beforeValue = 0f;
    //今回のAxis値
    private float afterValue = 0f;
    //ダウンフラグ
    private bool _inputDown = false;
    //押してるかどうか
    private bool _keepDown = false;
    //CrossRightの上下左右のフラグ
    private bool[] _crossFlag;

    private MoveController move_ctr;
    //  up  down right left
    //(0000 0000 0000  0000)
    private int Bit_flag_cross  = 0x0000;
    //-----それぞれのビットフラグ----------
    private int CrossUpInput    = 0x1000;
    private int CrossUpKeep     = 0x2000;
    private int CrossDownInput  = 0x0100;
    private int CrossDownKeep   = 0x0200;
    private int CrossRightInput = 0x0010;
    private int CrossRightKeep  = 0x0020;
    private int CrossLeftInput  = 0x0001;
    private int CrossLeftKeep   = 0x0002;
    //-------------------------------------

    public bool getKeepDown
    {
        get { return _keepDown; }
    }

    // Use this for initialization
    void Start () {
        move_ctr = GetComponent<MoveController>();
        _crossFlag = new bool[4] { false, false, false, false };
        Bit_flag_cross = 0x0000;
        _keepDown = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (move_ctr.OnCrossUp || move_ctr.OnCrossDown || move_ctr.OnCrossRight || move_ctr.OnCrossLeft)
        {
            afterValue = 1;
        }
        else
        {
            afterValue = 0;
            beforeValue = 0;
        }
        CrossDown();
    }

    /// <summary>
    /// CrossRightのInput.GetDownを取得する処理
    /// </summary>
    private void CrossDown()
    {
        //今回の値を取得

        //前回の値と今回の値が異なっていて、かつ押されたフラグがtrueだったら
        if (afterValue == beforeValue)
        {
            _keepDown = true;
            return;
        }

        _keepDown = false;

        if (move_ctr.OnCrossUp)
        {
            Bit_flag_cross = CrossUpInput;
        }
        else if (move_ctr.OnCrossDown)
        {
            Bit_flag_cross = CrossDownInput;
        }
        else if (move_ctr.OnCrossRight)
        {
            Bit_flag_cross = CrossRightInput;
        }
        else if (move_ctr.OnCrossLeft)
        {
            Bit_flag_cross = CrossLeftInput;
        }

        //今回の値を前回の値変数に入れる
        beforeValue = afterValue;
        CrossKeep();
    }

    /// <summary>
    /// CrossRightDownを押し続けた時の処理
    /// </summary>
    private void CrossKeep()
    {
        //前回の値と今回の値が異なっていたら押され続けてるフラグをfalseにする
        if (afterValue != beforeValue)
        {
            Bit_flag_cross = 0x0000;
            beforeValue = 0;
            _keepDown = false;
        }
        else
        {
            if ((Bit_flag_cross & CrossUpInput) != 0)
            {
                Bit_flag_cross -= CrossUpInput;
                Bit_flag_cross = CrossUpKeep;
            }
            else if ((Bit_flag_cross & CrossDownInput) != 0)
            {
                Bit_flag_cross -= CrossDownInput;
                Bit_flag_cross = CrossDownKeep;
            }
            else if ((Bit_flag_cross & CrossRightInput) != 0)
            {
                Bit_flag_cross -= CrossRightInput;
                Bit_flag_cross = CrossRightKeep;
            }
            else if ((Bit_flag_cross & CrossLeftInput) != 0)
            {
                Bit_flag_cross -= CrossLeftInput;
                Bit_flag_cross = CrossLeftKeep;
            }
        }
    }
}