using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 生成時のポップアップ
/// </summary>
public class DropItemTimer : MonoBehaviour {
    float distance = 0.5f;
    float DestroyTime = 30f;
    float time = 0;

	// Use this for initialization
	void Update () {
        time += Time.deltaTime;
        if(time > DestroyTime)
        {
            Destroy(gameObject);
        }
	}
}
