using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 生成時のポップアップ
/// </summary>
public class PopUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 100));
	}
}
