using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlyingBoss : MonoBehaviour {
    int count = 3;
    Vector2 size;
    float moveTime = 4f;
    private CameraController cController;

    void Start()
    {
        cController = FindObjectOfType<CameraController>();
        GameObject player = FindObjectOfType<MoveController>().gameObject;
        size = GetComponent<SpriteRenderer>().bounds.size;
    }


    void DeleteTest(GameObject player)
    {
        cController.target = gameObject;
        var sr = GetComponent<SpriteRenderer>();
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<BoxCollider2D>().enabled = false;
        
        Vector2 pos = player.transform.position;

        sr.sortingOrder = 1000;

        transform.DOLocalMoveY(transform.localPosition.y - 0.5f, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            Sequence s = DOTween.Sequence();
            s.Append(transform.DORotate(new Vector3(0, 0, 365 * 2), 1f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(count));
            transform.DOMove(pos, count).SetEase(Ease.Linear);
            transform.DOScale(size, count).SetEase(Ease.InSine).OnComplete(() =>
            {
                s.Complete();
                cController.target = player;
                transform.DOMoveY(transform.localPosition.y - (size.y * 5), moveTime).SetEase(Ease.Linear).SetDelay(1f);
            });

        });
        
        
    }
}
