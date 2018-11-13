using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StageSride : MonoBehaviour {
    StageManager sManager = new StageManager();
    MapInfo mInfo;

    [SerializeField]
    private enum direction
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }

	// Use this for initialization
	void Start () {
        sManager = GameObject.Find("Controller").GetComponent<StageManager>();
    }

    public void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Player") {
            mInfo = transform.root.GetComponent<MapInfo>();
            direction dir = new direction();
            switch (dir) {
                case direction.UP:
                    break;
                case direction.DOWN:
                    sManager.SrideStage(1, StageManager.Direction.DOWN);
                    break;
                case direction.LEFT:
                    break;
                case direction.RIGHT:
                    sManager.SrideStage(0, StageManager.Direction.RIGHT);
                    break;
                
            }
            Destroy(gameObject);
        }
    }
    /*
  case "RockUp":
                    mInfo.rock.transform.DOScaleY(0f, 1.0f).SetEase(Ease.Linear);
                    mInfo.UpRock = true;
                    break;  
 */
}


