using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StageSride : MonoBehaviour {
    StageManager sManager = new StageManager();
    MapInfo mInfo;
	// Use this for initialization
	void Start () {
        sManager = GameObject.Find("Controller").GetComponent<StageManager>();
        mInfo = transform.root.gameObject.GetComponent<MapInfo>();
    }

    public void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Player") {
            mInfo = transform.root.GetComponent<MapInfo>();
            Debug.Log("あにきたお");
            switch (gameObject.name) {
                case "Up":
                    break;
                case "Down":
                    sManager.SrideStage(1, StageManager.Direction.down);
                    break;
                case "Light":
                    break;
                case "Right":
                    sManager.SrideStage(0, StageManager.Direction.right);
                    break;
                case "RockUp":
                    mInfo.rock.transform.DOScaleY(0f, 1.0f).SetEase(Ease.Linear);
                    mInfo.UpRock = true;
                    break;  
            }
            Destroy(gameObject);
        }
    }
}
