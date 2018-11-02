using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField]
    private GameObject AniObject;
    private Vector2 vec;
    private Camera camera;

	// Use this for initialization
	void Start () {
        camera = gameObject.GetComponent<Camera>();
        vec = camera.ScreenToViewportPoint(new Vector2(0,0));
	}
	
	// Update is called once per frame
	void Update () {
        CameraMove();
	}

    /// <summary>
    /// カメラが移動する処理
    /// </summary>
    private void CameraMove()
    {
        if(AniObject.transform.position.x >= (vec.x - 5f) || AniObject.transform.position.x >= (vec.x - 10f))
        {
            Debug.Log("Move");
            gameObject.transform.position = new Vector3(AniObject.transform.position.x, AniObject.transform.position.y, -10);
        }
    }
    
}
