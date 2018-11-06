using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour {

    [SerializeField]
    private GameObject AniObject;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        CameraMove();
	}

    /// <summary>
    /// カメラ移動処理（追従）
    /// </summary>
    private void CameraMove()
    {
        gameObject.transform.position = new Vector3(AniObject.transform.position.x, AniObject.transform.position.y + 2.13f, -10);
        if (gameObject.transform.position.x < 0)
        {
            gameObject.transform.position = new Vector3(0, AniObject.transform.position.y + 2.13f, -10);
        }
        if (gameObject.transform.position.x >= 12.9f)
        {
            gameObject.transform.position = new Vector3(12.9f, AniObject.transform.position.y + 2.13f, -10);
        }
    }

    /// <summary>
    /// カメラが移動する処理（範囲で移動）
    /// </summary>
    //private void CameraMove()
    //{
    //    gameObject.transform.position = new Vector3(0, 0, -10);
    //    //-------------一段目----------------------------------------------------------------------
    //    if (AniObject.transform.position.x >= 6.5f)
    //    {
    //        gameObject.transform.DOLocalMove(new Vector3(12.9f, 0, -10), 0.3f);
    //    }
    //    else
    //    {
    //        gameObject.transform.DOLocalMove(new Vector3(0, 0, -10), 0.3f);
    //    }
    //    //--------------二段目-----------------------------------------------------------------------
    //    if (AniObject.transform.position.y >= 3.5f && AniObject.transform.position.x >= 6.5f)
    //    {
    //        gameObject.transform.DOLocalMove(new Vector3(12.9f, 15, -10), 0.3f);
    //    }
    //    else if (AniObject.transform.position.y >= 3.5f && AniObject.transform.position.x < 6.5f)
    //    {
    //        gameObject.transform.DOLocalMove(new Vector3(0, 15, -10), 0.3f);
    //    }
    //    //---------------三段目------------------------------------------------------------------------
    //    if (AniObject.transform.position.y >= 13.5f && AniObject.transform.position.x >= 6.5f)
    //    {
    //        gameObject.transform.DOLocalMove(new Vector3(12.9f, 30, -10), 0.3f);
    //    }
    //    else if (AniObject.transform.position.y >= 13.5f && AniObject.transform.position.x < 6.5f)
    //    {
    //        gameObject.transform.DOLocalMove(new Vector3(0, 30, -10), 0.3f);
    //    }
    //}

}
