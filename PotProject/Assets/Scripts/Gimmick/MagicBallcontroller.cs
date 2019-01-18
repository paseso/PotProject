using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBallcontroller : MonoBehaviour {
    private float time;

    public Vector2 Pos {
        get;set;
    }

	// Update is called once per frame
	void Update () {
        GetComponent<Rigidbody2D>().velocity = Pos;
        time += Time.deltaTime;
        if(time > 5) {
            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.layer == LayerMask.NameToLayer("Player")) {
            PlayerController pcon = FindObjectOfType<PlayerController>();
            pcon.HPDown(transform.GetComponent<MonsterController>().Status.GetAttack);
            Destroy(gameObject);
        }
    }
}
