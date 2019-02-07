using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveCloud : MonoBehaviour {

    private bool createFlag = false;
    public bool CreateFlag { get; set; }

    public enum MoveType {
        UpDown,
        Side,
        Diagonal,
    }

    [SerializeField]
    private MoveType moveType;

    private float distance = 8;
    private int direction = 1;
    private Vector3 firstPos;

    private float speed;
    private CloudCol cloudCol;

    void Start() {
        firstPos = transform.parent.localPosition;
        speed = Random.Range(2f, 5f);
        switch (moveType) {
            case MoveType.UpDown:
                Rigidbody2D rb = transform.parent.GetComponent<Rigidbody2D>();
                rb.isKinematic = true;
                distance = 10;
                StartCoroutine(MoveUpDown());
                break;
            case MoveType.Side:
                distance = 5;
                StartCoroutine(MoveSide());
                break;
            case MoveType.Diagonal:
                distance = 5;
                StartCoroutine(MoveDiagonal());
                break;
            default:
                break;
        }
    }

    private bool changeDirCheck() {
        if (transform.position.x > firstPos.x + distance) { return true; }
        if (transform.position.x < firstPos.x) { return true; }
        if (transform.position.y < firstPos.y - distance) { return true; }
        if (transform.position.y > firstPos.y) { return true; }
        return false;
    }

    // Update is called once per frame
    void LateUpdate() {
        
    }

    public void OnTriggerEnter2D(Collider2D col) {

        //if (col.gameObject.layer == LayerMask.NameToLayer("Block")) {
        //    Debug.Log("call");
        //    direction *= -1;
        //}
    }

    public IEnumerator MoveSide() {
        while (true) {
            transform.parent.DOLocalMoveX(transform.parent.localPosition.x + (distance * direction), speed).SetEase(Ease.Linear);
            yield return new WaitForSeconds(speed);
            transform.parent.DOLocalMoveX(firstPos.x, speed).SetEase(Ease.Linear).SetEase(Ease.Linear);
            yield return new WaitForSeconds(speed);
        }
        //transform.parent.localPosition = new Vector2(transform.parent.localPosition.x + (Time.deltaTime * speed * direction), transform.parent.localPosition.y);
    }

    public IEnumerator MoveUpDown() {
        while (true) {
            transform.parent.DOLocalMoveY(transform.parent.localPosition.y - (distance * direction), speed).SetEase(Ease.Linear);
            yield return new WaitForSeconds(speed);
            transform.parent.DOLocalMoveY(firstPos.y, speed).SetEase(Ease.Linear).SetEase(Ease.Linear);
            yield return new WaitForSeconds(speed);
        }
        //transform.parent.localPosition = new Vector2(transform.parent.localPosition.x, transform.parent.localPosition.y + (Time.deltaTime * speed * direction));
    }

    public IEnumerator MoveDiagonal() {
        while (true) {
            transform.parent.DOLocalMove(new Vector3(transform.parent.localPosition.x + (distance * direction), transform.parent.localPosition.y - (distance * direction), firstPos.z), speed).SetEase(Ease.Linear);
            yield return new WaitForSeconds(speed);
            transform.parent.DOLocalMove(firstPos, speed).SetEase(Ease.Linear);
            yield return new WaitForSeconds(speed);
        }
        //transform.parent.localPosition = new Vector2(transform.parent.localPosition.x + (Time.deltaTime * speed * direction), transform.parent.localPosition.y - (Time.deltaTime * speed * direction));
    }
}
