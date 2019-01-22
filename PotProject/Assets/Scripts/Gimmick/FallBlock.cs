using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallBlock : MonoBehaviour {
    private string blockPrefab = ("Prefabs/GimmickTiles/FallBlockPrefab");
    private enum fallState {
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

    public bool SetFallFlag {
        set { fallFlag = value; }
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
    }

    void Update() {
        if (fallFlag || state == fallState.fall) {
            timer += Time.deltaTime;

            if (timer > fallTime / 2 && state == fallState.normal)
            {
                state = fallState.reach;
            }
            else if (timer > fallTime && state == fallState.reach)
            {
                timer = 0;
                state = fallState.fall;
                transform.GetComponent<Rigidbody2D>().isKinematic = false;
                gameObject.layer = LayerMask.NameToLayer("FallBlock");
            }
        }

        if (timer > createTime && state == fallState.fall)
        {
            timer = 0;
            //GameObject fallBlock = Instantiate(Resources.Load<GameObject>(blockPrefab));
            //fallBlock.transform.SetParent(transform.root.transform);
            //fallBlock.transform.localPosition = defaultPos;
            Destroy(gameObject);
        }

    }
}
