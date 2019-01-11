using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseManager : SingletonMonoBehaviour<PauseManager> {

    public GameObject pauseCanvasPrefab;
    private GameObject pauseCanvasObj;
    private EventSystem eventSystem;

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
            pauseCanvasObj = transform.GetChild(0).gameObject;
            pauseCanvasObj.SetActive(false);
        }
        isPause = false;
        Button[] btns = pauseCanvasObj.GetComponentsInChildren<Button>();
        //  ポーズキャンバスのボタンにイベントを設定
        btns[0].onClick.AddListener(ReturnTitle);
        btns[1].onClick.AddListener(ReturnStageSelect);
        btns[2].onClick.AddListener(EscapeGame);
        //  EventSystemの設定
        eventSystem = EventSystem.current;
        eventSystem.firstSelectedGameObject = btns[0].gameObject;
        //  シーン遷移時に破棄されないように 
        DontDestroyOnLoad(this.gameObject);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Option"))
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
