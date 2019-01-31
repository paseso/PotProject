using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour {

    /// <summary>
    /// カメラが追うオブジェクト
    /// </summary>
    public GameObject target { get; set; }

    public Transform map { get; set; }

    private float minSide;
    private float maxSide;
    private float minHeight;
    private float maxHeight;

    // Use this for initialization
    void Start () {
        target = FindObjectOfType<MoveController>().gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        CameraMove(target);
	}

    /// <summary>
    /// カメラ移動処理（追従）
    /// </summary>
    private void CameraMove(GameObject obj)
    {
        gameObject.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y + 4f, -100);
        Debug.Log("Pos = " + transform.position);
        /*
         画面中心より右(左)に来たらx座標追従
         画面中心より上？に来たらy座標追従
         今いるマップのカメラがいける最小最大座標をGlobalで取得(x,y)←どっかに保存したい
         その座標まで来たらカメラのx(y)座標を固定
         マップが切り替わったらその都度座標更新
         はしごで下に降りるときは？？(仕様確認)
         */




        //if (gameObject.transform.position.x < 0)
        //{
        //    gameObject.transform.position = new Vector3(0, AniObject.transform.position.y + 2.13f, -10);
        //}
        //if (gameObject.transform.position.x >= 12.9f)
        //{
        //    gameObject.transform.position = new Vector3(12.9f, AniObject.transform.position.y + 2.13f, -10);
        //}
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
