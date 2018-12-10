using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// バリアスクリプト
/// </summary>
public class PlayerBarrier : MonoBehaviour {
    // バリアの制限時間
    private float timeLimit;
    private Status status;

	// Use this for initialization
	void Awake () {
        timeLimit = status.GetBarrierTime;
	}

    void Update()
    {
        if (timeLimit > 0)
        {
            timeLimit -= Time.deltaTime;
        }else if(timeLimit < 0)
        {
            Destroy(gameObject);
        }

    }

}
