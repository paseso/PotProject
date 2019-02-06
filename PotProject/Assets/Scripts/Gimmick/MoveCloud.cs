using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCloud : MonoBehaviour {
    private float distance;
    private int direction = 1;
    private Vector3 firstPos;

    private int speed = 5;
    private CloudCol cloudCol;
	// Use this for initialization
	void Start () {
        cloudCol = transform.parent.transform.GetChild(0).GetComponent<CloudCol>();
    }
	
	// Update is called once per frame
	void LateUpdate () {
        //MoveUpDown();
        MoveSide();
        //MoveDiagonal();
        
    }

    public void OnTriggerEnter2D(Collider2D col) {

        if (col.gameObject.layer == LayerMask.NameToLayer("Block")) {
            Debug.Log("call");
            direction *= -1;
        }


    }

    public void MoveSide() {
        transform.parent.localPosition = new Vector2(transform.parent.localPosition.x + (Time.deltaTime * speed * direction), transform.parent.localPosition.y);
    }

    public void MoveUpDown() {
        transform.parent.localPosition = new Vector2(transform.parent.localPosition.x, transform.parent.localPosition.y - (Time.deltaTime * speed * direction));
    }

    public void MoveDiagonal() {
        transform.parent.localPosition = new Vector2(transform.parent.localPosition.x + (Time.deltaTime * speed * direction), transform.parent.localPosition.y - (Time.deltaTime * speed * direction));
    }
}
