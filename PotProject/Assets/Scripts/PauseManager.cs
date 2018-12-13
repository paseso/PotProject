using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class PauseManager : SingletonMonoBehaviour<PauseManager> {

    public GameObject pauseCanvasPrefab;
    private GameObject pauseCanvasObj;

    private bool isPause;

    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(this);
            return;
        }
        if (pauseCanvasObj == null)
        {
            pauseCanvasObj = Instantiate(pauseCanvasPrefab);
            pauseCanvasObj.SetActive(false);
        }
        isPause = false;
        Button[] btns = pauseCanvasObj.GetComponentsInChildren<Button>();
        //  ポーズキャンバスのボタンにイベントを設定
        btns[0].onClick.AddListener(ReturnTitle);
        btns[1].onClick.AddListener(ReturnStageSelect);
        btns[2].onClick.AddListener(EscapeGame);
        //  シーン遷移時に破棄されないように 
        DontDestroyOnLoad(pauseCanvasObj);
        DontDestroyOnLoad(this.gameObject);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Esc");
            if (!isPause)
            {
                pauseCanvasObj.SetActive(true);
                isPause = true;
            }
            else
            {
                pauseCanvasObj.SetActive(false);
                isPause = false;
            }
        }
    }

    public void ReturnTitle()
    {
        if (FadeManager.Instance != null)
        {
            FadeManager.Instance.LoadScene(0, 1f);
        }
        else if (FadeManager.Instance == null)
        {
            Debug.Log("タイトルに移動しようとしましたがFadeManagerが見つかりません。");
        }
    }

    public void ReturnStageSelect()
    {
        if (FadeManager.Instance != null)
        {
            FadeManager.Instance.LoadScene(1, 1f);
        }
        else if (FadeManager.Instance == null)
        {
            Debug.Log("ステージ選択画面に移動しようとしましたがFadeManagerが見つかりません。");
        }
    }

    public void EscapeGame()
    {
    #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
    #elif UNITY_STANDALONE
            Application.Quit();
    #endif
    }

}
