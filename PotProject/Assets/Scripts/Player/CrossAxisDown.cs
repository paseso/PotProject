﻿using System.Collections;
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

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
