using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MonsterWalk : MonoBehaviour {
    private Vector2 firstPos;
    private float randTime;
    private float distance = 2;
    private float time;
    private int direction;

	// Use this for initialization
	void Start () {
        firstPos = transform.localPosition;
        
        randTime = Random.Range(1, 5);
        direction = Random.Range(-1, 2);
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if(time > randTime) {
            Walk();
        }
	}

    void Walk() {
        if(direction == 0) {
            int dir = Random.Range(0, 2);
            if (dir == 0) {
                transform.DOLocalMoveX(firstPos.x + distance, 1f).SetEase(Ease.Linear);
                direction = 1;
            } else {
                transform.DOLocalMoveX(firstPos.x - distance, 1f).SetEase(Ease.Linear);
                direction = -1;
            }
        } else {
            direction = 0;
            transform.DOLocalMoveX(firstPos.x, 1f).SetEase(Ease.Linear);
        }
        time = 0;
        randTime = Random.Range(1, 5);
    }
}
