using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
        gameObject.transform.position = new Vector3(0, 0, -10);
        //-------------一段目----------------------------------------------------------------------
        if(AniObject.transform.position.x >= 6.5f)
        {
            gameObject.transform.DOLocalMove(new Vector3(12.9f,0,-10), 0.3f);
            //transform.position = new Vector3(12.9f, 0, -10);
        }
        else
        {
            gameObject.transform.DOLocalMove(new Vector3(0, 0, -10), 0.3f);
            //gameObject.transform.position = new Vector3(0, 0, -10);
        }
        //--------------二段目-----------------------------------------------------------------------
        if(AniObject.transform.position.y >= 3.5f && AniObject.transform.position.x >= 6.5f)
        {
            gameObject.transform.DOLocalMove(new Vector3(12.9f, 15, -10), 0.3f);
        }
        else if(AniObject.transform.position.y >= 3.5f && AniObject.transform.position.x < 6.5f)
        {
            gameObject.transform.DOLocalMove(new Vector3(0, 15, -10), 0.3f);
        }
        //---------------三段目------------------------------------------------------------------------
        if(AniObject.transform.position.y >= 13.5f && AniObject.transform.position.x >= 6.5f)
        {
            gameObject.transform.DOLocalMove(new Vector3(12.9f, 30, -10), 0.3f);
        }
        else if(AniObject.transform.position.y >= 13.5f && AniObject.transform.position.x < 6.5f)
        {
            gameObject.transform.DOLocalMove(new Vector3(0, 30, -10), 0.3f);
        }
    }
    
}
