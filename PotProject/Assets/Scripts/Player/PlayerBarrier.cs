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
    private PlayerController pController;

	// Use this for initialization
	void Awake () {
        status = GameObject.Find("PlayerStatus").GetComponent<PlayerManager>().Status;
        pController = GameObject.Find("Controller").GetComponent<PlayerController>();
        timeLimit = status.GetBarrierTime;
        status.ActiveBarrier = true;
	}

    void Update()
    {
        if (pController.IsCommandActive && pController.AllCommandActive){
            if (timeLimit > 0) {
                timeLimit -= Time.deltaTime;
            } else if (timeLimit < 0) {
                status.ActiveBarrier = false;
                Destroy(gameObject);
            }
        }
    }

}
