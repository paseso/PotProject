using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFlyingCloud : MonoBehaviour {
    private bool createFlag = false;
    public bool CreateFlag { get; set; }

    public enum MoveType {
        UpDown,
        Side,
        Diagonal,
    }

    private MoveType moveType;

    public MoveType setMoveType {
        set { moveType = value; }
    }
    private float distance;
    private int direction = 1;
    private Vector3 firstPos;

    private int speed = 5;
    private CloudCol cloudCol;
	// Use this for initialization
	void Start () {
        cloudCol = transform.parent.transform.GetChild(0).GetComponent<CloudCol>();
        if (!createFlag) {
            moveType = MoveType.Side;
        }
    }
	
	// Update is called once per frame
	void LateUpdate () {
        //if (!createFlag) {
        //    MoveSide();
        //    //MoveDiagonal();
        //    return;
        //}

        switch (moveType) {
            case MoveType.UpDown:
                MoveUpDown();
                break;
            case MoveType.Side:
                MoveSide();
                break;
            case MoveType.Diagonal:
                MoveDiagonal();
                break;
            default:
                break;
        }
    }

    public void OnTriggerEnter2D(Collider2D col) {

        if (col.gameObject.layer == LayerMask.NameToLayer("Block")) {
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
