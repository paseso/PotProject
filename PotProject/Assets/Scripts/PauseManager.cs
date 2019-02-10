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
    Button[] btns;

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
        btns = pauseCanvasObj.GetComponentsInChildren<Button>();
        //  ポーズキャンバスのボタンにイベントを設定
        btns[0].onClick.AddListener(Respawn);
        btns[1].onClick.AddListener(ReturnStageSelect);
        btns[2].onClick.AddListener(ReturnTitle);
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
                GameObject controller = GameObject.Find("Controller");
                if (controller != null)
                    controller.GetComponent<PlayerController>().AllCommandActive = false;
                Time.timeScale = 0;
                EventSystem.current.SetSelectedGameObject(btns[0].gameObject);
            }
            else
            {
                pauseCanvasObj.SetActive(false);
                Time.timeScale = 1;
                Button btn = FindObjectOfType<Button>();
                if (btn != null)
                    EventSystem.current.SetSelectedGameObject(btn.gameObject);
                GameObject controller = GameObject.Find("Controller");
                if (controller != null)
                    controller.GetComponent<PlayerController>().AllCommandActive = true;
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
            GameObject pManagarObject = GameObject.Find("PlayerStatus");
            if (pManagarObject != null)
            {
                pManager = pManagarObject.GetComponent<PlayerManager>();
                pManager.InitStatus();
            }            
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
            SoundManager.Instance.PlayBgm(0);
            Time.timeScale = 1;
            isPause = false;
            GameObject pManagarObject = GameObject.Find("PlayerStatus");
            if (pManagarObject != null)
            {
                pManager = pManagarObject.GetComponent<PlayerManager>();
                pManager.InitStatus();
            }
            pauseCanvasObj.SetActive(false);
        }
        else if (FadeManager.Instance == null)
        {
            Debug.Log("ステージ選択画面に移動しようとしましたがFadeManagerが見つかりません。");
        }
    }

    public void Respawn()
    {
        GameObject controller = GameObject.Find("Controller");
        if (controller != null)
        {
            controller.GetComponent<PlayerController>().Resporn();
        }
        if (GameObject.Find("Controller").gameObject != null)
            GameObject.Find("Controller").GetComponent<PlayerController>().AllCommandActive = true;
        Time.timeScale = 1;
        pauseCanvasObj.SetActive(false);
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
