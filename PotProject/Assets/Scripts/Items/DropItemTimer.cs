using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 自動削除
/// </summary>
public class DropItemTimer : MonoBehaviour {
    float DestroyTime = 30f;
    float time = 0;
    bool reachFlag = false;
    float duration = 0.3f;
    private PlayerController pCon;

    void Start() {
        pCon = GameObject.Find("Controller").GetComponent<PlayerController>();
    }

    // Use this for initialization
    void Update () {
        time += Time.deltaTime;
        if(time > DestroyTime * 0.8f && !reachFlag)
        {
            reachFlag = true;
            StartCoroutine(Flashing());
        }
        if(time > DestroyTime && !pCon.pickUpFlag)
        {
            Destroy(transform.parent.gameObject);
        }
	}

    /// <summary>
    /// アイテム点滅コルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator Flashing()
    {
        while (true)
        {
            GetComponent<SpriteRenderer>().DOFade(0, duration).SetEase(Ease.Linear);
            yield return new WaitForSeconds(duration);
            GetComponent<SpriteRenderer>().DOFade(1, duration).SetEase(Ease.Linear);
            yield return new WaitForSeconds(duration);  

        }
    }
}
