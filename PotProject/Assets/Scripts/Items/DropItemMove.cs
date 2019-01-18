using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 生成時のポップアップ
/// </summary>
public class DropItemMove : MonoBehaviour {
    float distance = 0.5f;
    float time = 1f;

	// Use this for initialization
	void Start () {
        StartCoroutine(MoveItem());
	}

    IEnumerator MoveItem()
    {
        while (true)
        {
            transform.DOMoveY(transform.position.y + distance, time).SetEase(Ease.Linear);
            yield return new WaitForSeconds(time);
            transform.DOMoveY(transform.position.y - distance, time).SetEase(Ease.Linear);
            yield return new WaitForSeconds(time);
        }
    }
}
