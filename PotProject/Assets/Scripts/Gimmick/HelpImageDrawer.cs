using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpImageDrawer : MonoBehaviour {

    [SerializeField]
    private Sprite[] helpSprites;

    private const float  nextSpriteTime = 3;    //  画像が変わる時間
    private bool onReady = false;               //  Spriteが存在するか
    private float rideTime = 0f;                //  乗られている時間

    private GimmickController gimmick;
    private SpriteRenderer spr;


	void Start ()
    {
        Init();
    }

    /// <summary>
    /// 初期化
    /// </summary>
    private void Init()
    {
        if (helpSprites.Length <= 0)
        {
            onReady = false;
            return;
        }
        gimmick = gameObject.GetComponentInChildren<GimmickController>();
        GameObject helpObject = new GameObject("Helpimage");
        helpObject.transform.parent = gameObject.transform;
        helpObject.transform.localPosition = new Vector3(0, 6, 0);
        helpObject.AddComponent<SpriteRenderer>();
        spr = helpObject.GetComponent<SpriteRenderer>();
        spr.enabled = false;
        spr.sprite = helpSprites[0];
        onReady = true;
    }


    void Update () {
        if (onReady)
            ImageDrawer();
	}

    /// <summary>
    /// スプライトを乗っている間はパラパラで表示する
    /// </summary>
    private void ImageDrawer()
    {
        //  プレイヤーが乗っていたら
        if (gimmick.OnPlayerFlag)
        {
            if (rideTime < 1)
            {
                spr.enabled = true;
                rideTime += 2 * Time.deltaTime;
                rideTime = Mathf.Min(rideTime, 1);
                spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, rideTime);
            }
            else
            {
                rideTime += Time.deltaTime;
                int spriteNum = 0;
                spriteNum = (int)(rideTime / nextSpriteTime) % helpSprites.Length;
                spr.sprite = helpSprites[spriteNum];
            }
        }
        //  プレイヤーが下りたら
        else
        {
            if (rideTime <= 0) return;
            if (rideTime >= 1) rideTime = 1;
            rideTime -= Time.deltaTime;
            rideTime = Mathf.Max(rideTime, 0);
            if (rideTime <= 0)
            {
                spr.enabled = false;
                return;
            }
            spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, rideTime);
        }
    }
}
