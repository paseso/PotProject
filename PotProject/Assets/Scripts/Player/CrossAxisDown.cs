using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 前回の入力の値と今回の入力の値
 * 前回と今回を比較して差分があった場合にフラグを立てる
 * フラグ: ダウンフラグ、ずっと押してるフラグ、
 * ダウンフラグー＞一回フラグがたったら次のフレームでフラグを下す
 * ずっと押してるフラグー＞入力がなくならない限りはフラグをたてる。入力がなくなったらフラグを下す
 * それを４方向分つくる
 */

public class CrossAxisDown : MonoBehaviour {

    //前回のAxis値
    private float beforeValue = 0f;
    //今回のAxis値
    private float afterValue = 0f;
    //比較してちがうかどうか
    private bool _comparison = false;
    //押してるかどうか
    private bool _keepDown = false;
    //CrossRightの上下左右のフラグ
    private bool[] _crossFlag = { false, false, false, false };

    private MoveController move_ctr;

    // Use this for initialization
    void Start () {
        move_ctr = GetComponent<MoveController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// CrossRightのInput.GetDownを取得する処理
    /// </summary>
    private void CrossDown()
    {
        //今回の値を取得
        afterValue = move_ctr.CrossAxisValue;


        //今回の値を前回の値変数に入れる
        beforeValue = afterValue;
    }
}
