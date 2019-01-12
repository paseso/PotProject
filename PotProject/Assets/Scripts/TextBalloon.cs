using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBalloon : MonoBehaviour {

    [SerializeField]
    private string[] massages;
    private const string massage = "あああ";
    [SerializeField]
    private GameObject balloonPrefab;

    private GameObject canvas;
    private GameObject massageUI;

    [SerializeField,Range(0, 0.3f)]
    float intervalForCharDisplay = 0.2f;           // 1文字の表示にかける時間

    private bool isTalk = false;
    private int currentSentenceNum = 0;             //現在表示している文章番号
    private string currentSentence = string.Empty;  // 現在の文字列
    private float timeBeginTalk = 0;                // 会話が始まってからの時間
    private float timeUntilDisplay = 0;             // 表示にかかる時間
    private float timeBeganDisplay = 1;             // 文字列の表示を開始した時間
    private int lastUpdateCharCount = -1;           // 表示中の文字数

    // イベント開始のフラグがたったら会話が始まる（その間はキャラの操作は無効）
    // 1文章を1文字ずつ表示する
    // 何らかのボタンが押されたら次の文章に進む
    // 全部話し終わったら会話を終了して戦闘に戻る


    void Start () {
        canvas = GameObject.Find("Canvas");
        massageUI = Instantiate(balloonPrefab) as GameObject;
        massageUI.transform.SetParent(canvas.transform, false);
	}
	
	void Update () {

        if (isTalk) timeBeginTalk += Time.deltaTime;
        if (Input.GetKey(KeyCode.Space)) isTalk = !isTalk;
        if (Input.GetKeyDown(KeyCode.Return)) TapEnter();

        Debug.Log("Cast なし" + timeBeginTalk / intervalForCharDisplay);

        // 文章が最後まで表示していなかったら
        if (massages[currentSentenceNum].Length >= (timeBeginTalk / intervalForCharDisplay))
        {
            Debug.Log("bbb");
            Debug.Log("Cast あり" +(int)(timeBeginTalk / intervalForCharDisplay));
            Debug.Log("Cast なし" + timeBeginTalk / intervalForCharDisplay);
            currentSentence = massages[currentSentenceNum].Substring(0, (int)(timeBeginTalk / intervalForCharDisplay));
        }        

        if (massages[currentSentenceNum].Length > currentSentence.Length)
        {
            Debug.Log("aaa");
            DisplayText(currentSentence);
        }
    }

    private void TapEnter()
    {
        if (true)
        {
            currentSentenceNum++;
            timeBeginTalk = 0;
        }
    }

    //void StartTalk()
    //{
    //    string showText = "";
    //    DisplayText(massages[currentSentenceNum]);
    //}

    void DisplayText(string text)
    {
        massageUI.transform.GetChild(0).GetComponent<Text>().text = text;
    }
}
