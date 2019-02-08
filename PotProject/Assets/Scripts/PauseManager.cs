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
    PlayerManager pManager;

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
        //  EventSystemの設定
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        eventSystem.firstSelectedGameObject = btns[0].gameObject;
        //  シーン遷移時に破棄されないように
        DontDestroyOnLoad(pauseCanvasObj);
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
            isPause = false;
            pauseCanvasObj.SetActive(false);
            pManager = GameObject.Find("PlayerStatus").GetComponent<PlayerManager>();
            pManager.InitStatus();
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
            isPause = false;
            pManager = GameObject.Find("PlayerStatus").GetComponent<PlayerManager>();
            pManager.InitStatus();
            pauseCanvasObj.SetActive(false);
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
