using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// バリアスクリプト
/// </summary>
public class PlayerBarrier : MonoBehaviour {
    // バリアの制限時間
    private float timeLimit;
    private PlayerStatus status;

	// Use this for initialization
	void Awake () {
        timeLimit = status.GetBarrierTime;
        status.ActiveBarrier = true;
	}

    void Update()
    {
        if (timeLimit > 0)
        {
            timeLimit -= Time.deltaTime;
        }else if(timeLimit < 0)
        {
            status.ActiveBarrier = false;
            Destroy(gameObject);
        }

    }

}
