using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleController : MonoBehaviour
{

    private FadeManager fade_m;

    // Use this for initialization
    void Start()
    {
        fade_m = GameObject.Find("FadeManager").GetComponent<FadeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        TapTitle();
    }

    /// <summary>
    ///　ボタンを押してシーン移動
    /// </summary>
    private void TapTitle()
    {
        if (Input.GetButtonDown("Circle") || Input.GetKeyDown(KeyCode.E))
        {
            fade_m.LoadScene(1, 0.3f);
        }
    }
}
