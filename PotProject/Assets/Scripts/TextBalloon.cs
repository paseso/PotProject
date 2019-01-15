using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TextBalloon : MonoBehaviour {

    private PlayerController playerController;

    [SerializeField]
    private string[] massages;
    [SerializeField]
    private GameObject balloonPrefab;
    [SerializeField,Range(0, 0.3f)]
    float intervalForCharDisplay = 0.2f;           // 1文字の表示にかける時間

    private GameObject canvas;
    private GameObject massageUI;

    private bool isTalk = false;
    private int currentSentenceNum = 0;             //現在表示している文章番号
    private string currentSentence = string.Empty;  // 現在の文字列
    private float timeBeginTalk = 0;                // 会話が始まってからの時間

    // イベント開始のフラグがたったら会話が始まる（その間はキャラの操作は無効）
    // 1文章を1文字ずつ表示する
    // 何らかのボタンが押されたら次の文章に進む
    // 全部話し終わったら会話を終了して戦闘に戻る


    void Start () {
        canvas = GameObject.Find("Canvas");
        playerController = FindObjectOfType<PlayerController>();
        StartCoroutine(InstanceBalloon());
    }
	
	void Update () {
        // デバッグ用
        if (Input.GetKey(KeyCode.Space)) isTalk = !isTalk;
        
        // 会話が開始したら
        if (isTalk)
        {
            // 文章が最後まで表示していなかったら
            if (massages[currentSentenceNum].Length >= timeBeginTalk / intervalForCharDisplay)
            {
                timeBeginTalk += Time.deltaTime;
                currentSentence = massages[currentSentenceNum].Substring(0, (int)(timeBeginTalk / intervalForCharDisplay));
                massageUI.transform.GetComponentInChildren<Text>().text = currentSentence;
            }
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetButton("Circle"))
            {
                NextWard();
            }
        }
    }

    private void NextWard()
    {
        if (massages.Length > currentSentenceNum + 1)
        {
            currentSentenceNum++;
            timeBeginTalk = 0;
        }
        else
        {
            StartCoroutine(DisableBalloon());
        }
    }

    // 吹き出しの生成・会話の開始
    IEnumerator InstanceBalloon()
    {
        yield return new WaitForSeconds(0.1f);
        massageUI = Instantiate(balloonPrefab) as GameObject;
        massageUI.transform.SetParent(canvas.transform, false);
        massageUI.transform.localScale = new Vector3(0.75f, 0.75f, 1);
        massageUI.transform.DOScale(new Vector2(1, 1), 0.1f);
        yield return new WaitForSeconds(0.15f);
        isTalk = true;
        yield break;
    }
    // 会話の終了
    IEnumerator DisableBalloon()
    {
        isTalk = false;
        yield return null;
        massageUI.transform.DOScale(new Vector2(0.75f, 0.75f), 0.1f);
        massageUI.GetComponentInChildren<Text>().text = "";
        yield return new WaitForSeconds(0.1f);
        massageUI.SetActive(false);
        yield break;
    }
}
