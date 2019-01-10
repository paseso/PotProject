using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallBlock : MonoBehaviour {
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
        if(createTime < fallTime) {
            createTime = fallTime * 2;
        }
    }

    void Update() {
        if (!fallFlag) { return; }

        timer += Time.deltaTime;

        if(timer > fallTime / 2 && state == fallState.normal) {
            state = fallState.reach;
        }else if(timer > fallTime && state == fallState.reach) {
            state = fallState.fall;
            transform.GetComponent<Rigidbody2D>().simulated = true;
        }else if(timer > createTime && state == fallState.fall) {
            GameObject fallBlock = Instantiate(Resources.Load<GameObject>("Prefabs/GimmickTiles/fallBlockPrefab"));
            fallBlock.transform.SetParent(transform.root.transform);
            fallBlock.transform.localPosition = defaultPos;
            Destroy(gameObject);
        }
        
    }
}
