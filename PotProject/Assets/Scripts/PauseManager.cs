using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

}
