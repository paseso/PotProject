using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraManager : MonoBehaviour {

    private Camera mainCamera;
    private Camera subCamera;
    private FadeImage fade;

    private bool isSwitchingSub = false;
    private bool isSwitchingMain = false;

    void Awake()
    {
        mainCamera = Camera.main;
        subCamera = Camera.allCameras[0];
        subCamera.gameObject.SetActive(false);
        fade = FindObjectOfType<FadeImage>();
    }

    void Start () {
		
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            FadeInOutCamera(new Vector2(16, 0));
        }
        if (Input.GetKeyDown(KeyCode.N)){
            SwitchingCameraSub(new Vector2(16, 0), 7);
        }
        if (Input.GetKeyDown(KeyCode.H)){
            SwitchingCameraSub(new Vector2(16, 0), 30);
        }
        if (Input.GetKeyDown(KeyCode.M)){
            SwitchingCameraMain();
        }
	}

    /// <summary>
    /// 暗転して指定座標を写すカメラ
    /// </summary>
    /// <param name="cameraPos">SubCameraの移動するposition</param>
    /// <param name="waitTime">サブカメラで表示している時間 デフォルト2秒</param>
    /// <param name="cameraSize">サブCameraのサイズ デフォルト7</param>
    public void FadeInOutCamera(Vector2 cameraPos, float waitTime = 2, float cameraSize = 7)
    {
        StartCoroutine(FadeInOut(cameraPos, waitTime, cameraSize));
    }

    /// <summary>
    /// 暗転してメインカメラに戻る
    /// </summary>
    public void SwitchingCameraMain()
    {
        if (isSwitchingMain)
        {
            return;
        }
        StartCoroutine(SwitchMain());
    }

    /// <summary>
    /// 暗転して指定座標にサブカメラを移動して表示する
    /// </summary>
    /// <param name="cameraPos">サブカメラを移動したいポジション</param>
    /// <param name="cameraSize">サブカメラのsize</param>
    public void SwitchingCameraSub(Vector2 cameraPos, float cameraSize)
    {
        if (isSwitchingSub){
            return;
        }
        StartCoroutine(SwitchSub(cameraPos, cameraSize));
    }

    IEnumerator SwitchSub(Vector2 cameraPos, float cameraSize){
        //  黒いテクスチャーで画面を隠す
        isSwitchingSub = true;
        isSwitchingMain = false;
        float time = 0;
        float interval = 0.5f;
        while (time <= interval)
        {
            fade.Range = Mathf.Lerp(0f, 1f, time / interval);
            time += Time.unscaledDeltaTime;
            yield return 0;
        }
        fade.Range = 1;

        //  カメラの変更処理
        subCamera.transform.position = new Vector3(cameraPos.x, cameraPos.y, -10);
        subCamera.orthographicSize = cameraSize;
        subCamera.gameObject.SetActive(true);
        mainCamera.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);

        //  黒のテクスチャを取りのぞく
        time = 0;
        while (time <= interval)
        {
            fade.Range = Mathf.Lerp(1f, 0f, time / interval);
            time += Time.unscaledDeltaTime;
            yield return 0;
        }
        fade.Range = 0;
        yield return null;
    }

    IEnumerator SwitchMain(){
        //  黒いテクスチャーで画面を隠す
        isSwitchingMain = true;
        isSwitchingSub = false;
        float time = 0;
        float interval = 0.5f;
        while (time <= interval)
        {
            fade.Range = Mathf.Lerp(0f, 1f, time / interval);
            time += Time.unscaledDeltaTime;
            yield return 0;
        }
        fade.Range = 1;

        //  カメラの変更処理
        mainCamera.gameObject.SetActive(true);
        subCamera.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);

        //  黒のテクスチャを取りのぞく
        time = 0;
        while (time <= interval)
        {
            fade.Range = Mathf.Lerp(1f, 0f, time / interval);
            time += Time.unscaledDeltaTime;
            yield return 0;
        }
        fade.Range = 0;
        yield return null;
    }

    IEnumerator FadeInOut(Vector2 cameraPos, float waitTime = 2, float cameraSize = 7)
    {
        //  黒いテクスチャーで画面を隠す
        float time = 0;
        float interval = 0.5f;
        while (time <= interval)
        {
            fade.Range = Mathf.Lerp(0f, 1f, time / interval);
            time += Time.unscaledDeltaTime;
            yield return 0;
        }
        fade.Range = 1;

        //  カメラの変更処理
        subCamera.transform.position = new Vector3(cameraPos.x, cameraPos.y, -10);
        subCamera.orthographicSize = cameraSize;
        subCamera.gameObject.SetActive(true);
        mainCamera.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);

        //  黒のテクスチャを取りのぞく
        time = 0;
        while (time <= interval)
        {
            fade.Range = Mathf.Lerp(1f, 0f, time / interval);
            time += Time.unscaledDeltaTime;
            yield return 0;
        }
        fade.Range = 0;

        yield return new WaitForSeconds(waitTime);

        //  黒いテクスチャーで画面を隠す
        time = 0;
        while (time <= interval)
        {
            fade.Range = Mathf.Lerp(0f, 1f, time / interval);
            time += Time.unscaledDeltaTime;
            yield return 0;
        }
        fade.Range = 1;

        //  カメラの変更処理
        mainCamera.gameObject.SetActive(true);
        subCamera.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        //  黒のテクスチャを取りのぞく
        time = 0;
        while (time <= interval)
        {
            fade.Range = Mathf.Lerp(1f, 0f, time / interval);
            time += Time.unscaledDeltaTime;
            yield return 0;
        }
        fade.Range = 0;
        yield return null;
    }
}
