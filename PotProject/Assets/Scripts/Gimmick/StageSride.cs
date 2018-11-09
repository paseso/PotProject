using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSride : MonoBehaviour {
    StageManager sManager = new StageManager();
    MapInfo mInfo = new MapInfo();
	// Use this for initialization
	void Start () {
        sManager = GameObject.Find("Controller").GetComponent<StageManager>();
        mInfo = transform.root.gameObject.GetComponent<MapInfo>();
    }

    public void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Player") {
            
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
                    Debug.Log("a");
                    Destroy(mInfo.rock);
                    break;
                
            }
            Destroy(gameObject);
        }
    }
}
