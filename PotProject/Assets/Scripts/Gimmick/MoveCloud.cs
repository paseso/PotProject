using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveCloud : MonoBehaviour {
    private float distance;
    private int direction = 1;
    private int speed = 5;
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void LateUpdate () {
        Move(direction);
    }

    public void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.layer == LayerMask.NameToLayer("Block")){
            direction *= -1;
        }
    }

    public void Move(int dir) {
        transform.localPosition = new Vector2(transform.localPosition.x + (Time.deltaTime * speed * dir), transform.localPosition.y);
    }
}
