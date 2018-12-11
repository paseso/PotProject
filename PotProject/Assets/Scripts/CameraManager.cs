using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    private FadeImage fade;

    private Camera mainCamera;
    private Camera subCamera;

    void Awake()
    {
        mainCamera = Camera.main;
        subCamera = Camera.allCameras[0];
        fade = FindObjectOfType<FadeImage>();
    }

    void Start () {
		
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            FadeInOutCamera(new Vector2(16, 0));
        }
	}

    private void FadeInOutCamera(Vector2 cameraPos)
    {
        StartCoroutine(FadeInOut(cameraPos, 2));
    }

    private void FadeInOutCamera(Vector3 cameraPos, float waitTime)
    {
        StartCoroutine(FadeInOut(cameraPos, waitTime));
    }

    IEnumerator FadeInOut(Vector2 cameraPos, float waitTime)
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
        SwitchingCameraSub(cameraPos);
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

        //  サブカメラで見ている時間
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
        SwitchingCameraMain();
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

    //  メインカメラのアクティブ化
    private void SwitchingCameraMain()
    {
        mainCamera.gameObject.SetActive(true);
        subCamera.gameObject.SetActive(false);
    }
    //  サブカメラのアクティブ化
    private void SwitchingCameraSub(Vector2 cameraPos)
    {
        subCamera.transform.position = new Vector3(cameraPos.x, cameraPos.y, -10);
        subCamera.gameObject.SetActive(true);
        mainCamera.gameObject.SetActive(false);
    }
}
