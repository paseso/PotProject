using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveCloud : MonoBehaviour {
    private float distance;
    private int direction = 1;
    private int speed = 3;
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        Move(direction);
    }

    public void OnCollisionEnter2D(Collision2D col) {
        Debug.Log(col.gameObject.name);
        if(col.gameObject.layer == LayerMask.NameToLayer("Block")){
            direction *= -1;
        }
    }

    public void Move(int dir) {
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed * dir, 0);
    }
}
