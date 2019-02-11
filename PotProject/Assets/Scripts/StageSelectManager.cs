using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StageSelectManager : MonoBehaviour {

    [SerializeField]
    private GameObject[] buttons;
    [SerializeField]
    private GameObject checkObject;

	[SerializeField]
	private GameObject[] checkObjs;

    private bool isCheck = false;
    private int stageSelectNum = 0;

    private StandaloneInputModule[] inputModules;


	void Start () {
        EventSystem.current.firstSelectedGameObject = buttons[0];
        EventSystem.current.gameObject.GetComponent<StandaloneInputModule>().horizontalAxis = "Horizontal_ps4";
        EventSystem.current.gameObject.GetComponent<StandaloneInputModule>().horizontalAxis = "Horizontal_ps4";
        EventSystem.current.gameObject.GetComponent<StandaloneInputModule>().submitButton = "Circle";
        EventSystem.current.SetSelectedGameObject(buttons[0]);
    }
	
	void Update () {
		if (isCheck)
        {
            if (Input.GetButtonDown("Circle") || Input.GetKeyDown(KeyCode.E))
                FadeManager.Instance.LoadScene(stageSelectNum, 0.5f);
            else if (Input.GetButton("Jump") || Input.GetKeyDown(KeyCode.Space))
            {
                isCheck = false;
                checkObject.SetActive(false);
				for (int i = 0; i < checkObjs.Length; i++)
				{
					checkObjs [i].SetActive (false);
				}					
            }
        }
	}

    public void TapStageButton_1(int sceneNum)
    {
		checkObjs[0].SetActive (true);
        StartCoroutine(CheckWindow(sceneNum));
    }

	public void TapStageButton_2(int sceneNum)
	{
		checkObjs[1].SetActive (true);
		StartCoroutine(CheckWindow(sceneNum));
	}

	public void TapStageButton_3(int sceneNum)
	{
		checkObjs[2].SetActive (true);
		StartCoroutine(CheckWindow(sceneNum));
	}


    public void TapNextButton()
    {
        Debug.Log("ステージ" + stageSelectNum + "にシーン遷移(いまは全部チュートリアルに移動)");
        FadeManager fade_m = GameObject.Find("FadeManager").GetComponent<FadeManager>();
        fade_m.LoadScene(2, 0.5f);
    }

    IEnumerator CheckWindow(int num)
    {
        yield return null;
        stageSelectNum = num;
        yield return null;
        isCheck = true;
        checkObject.SetActive(true);
        SoundManager.Instance.PlaySe((int)SoundManager.SENAME.SE_SELECT);
        yield break;
    }

}
