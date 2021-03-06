﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FallBlock : MonoBehaviour {
    private string blockPrefab = ("Prefabs/GimmickTiles/FallBlockPrefab");
    //[HideInInspector]
    public enum fallState {
        normal,
        reach,
        fall
    }

    private fallState state;

    [SerializeField]
    private float fallTime;
    [SerializeField]
    private float createTime;

    private float timer;
    private bool fallFlag = false;
    private bool randingFlag = false;

    public float SetTime {
        set { timer = value; }
    }

    public fallState State {
        get { return state; }
        set { state = value; }
    }

    public bool SetRandingFlag {
        set { randingFlag = value; }
    }

    private bool shakeFlag = false;

    public bool SetShakeFlag {
        set { shakeFlag = value; }
    }
    
    private Vector2 defaultPos;

    void Start() {
        defaultPos = transform.localPosition;
        state = fallState.normal;
        transform.GetComponent<Rigidbody2D>().isKinematic = true;

        // バグ回避
        if (createTime < fallTime) {
            createTime = fallTime * 2;
        }

        StartCoroutine(Floating());
    }

    void Update() {
        if (randingFlag || state == fallState.fall) {
            timer += Time.deltaTime;

            if (timer > fallTime / 2 && state == fallState.normal)
            {
                state = fallState.reach;
                if (!shakeFlag) {
                    StartCoroutine(Shake());
                    shakeFlag = true;
                }
            }
            else if (timer > fallTime && state == fallState.reach)
            {
                timer = 0;
                state = fallState.fall;
                fallFlag = true;
                transform.GetComponent<Rigidbody2D>().isKinematic = false;
                
                gameObject.layer = LayerMask.NameToLayer("FallBlock");
            }
        }

        if (timer > createTime && state == fallState.fall)
        {
            timer = 0;
            GameObject fallBlock = Instantiate(Resources.Load<GameObject>(blockPrefab));
            fallBlock.transform.SetParent(transform.root.transform);
            fallBlock.transform.localPosition = defaultPos;
            Destroy(gameObject);
        }
    }

    IEnumerator Shake() {
        while (state != fallState.fall && randingFlag) {
            transform.localPosition = new Vector2(transform.localPosition.x + 0.05f, transform.localPosition.y);
            yield return new WaitForSeconds(0.05f);
            transform.localPosition = new Vector2(transform.localPosition.x - 0.05f, transform.localPosition.y);
            yield return new WaitForSeconds(0.05f);
        }
    }

    public IEnumerator Floating()
    {
        while (state == fallState.normal)
        {
            transform.DOMoveY(transform.position.y + 0.1f, 0.5f).SetEase(Ease.Linear);
            yield return new WaitForSeconds(0.5f);
            transform.DOMoveY(transform.position.y - 0.1f, 0.5f).SetEase(Ease.Linear);
            yield return new WaitForSeconds(0.5f);
            yield return null;
        }
    }
}
