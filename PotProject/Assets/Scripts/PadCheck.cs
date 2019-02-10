using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PadCheck : MonoBehaviour {

    private void Start()
    {
        CheckInputModule();
    }

    public void CheckInputModule()
    {
        Debug.Log("Check");
        //  ステージセレクト画面の操作を判定
        StandaloneInputModule[] inputModule = GameObject.FindObjectsOfType<StandaloneInputModule>();
        string[] controllerName = Input.GetJoystickNames();
        //  キーボード
        if (controllerName.Length == 0 || controllerName[0] == "")
        {
            Debug.Log("キーボード");
            for (int i = 0; i < inputModule.Length; i++)
            {
                inputModule[i].horizontalAxis = "Horizontal";
                inputModule[i].verticalAxis = "Vertical";            
            }            
        }
        //  コントローラー
        else if (controllerName[0] != "")
        {
            Debug.Log("コントローラー");
            for (int i = 0; i < inputModule.Length; i++)
            {
                inputModule[i].horizontalAxis = "Horizontal_ps4";
                inputModule[i].verticalAxis = "Vertical_ps4";
                inputModule[i].submitButton = "Circle";
            }
        }
    }
}
