using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StageSelectManager : MonoBehaviour {

    [SerializeField]
    private GameObject[] buttons;
    [SerializeField]
    private GameObject eventSystem;
    private int stageSelectNum;

    private StandaloneInputModule[] inputModules;


	void Start () {
        Init();
    }
	
	void Update () {
		
	}

    private void Init()
    {
        stageSelectNum = 0;
        inputModules = eventSystem.GetComponents<StandaloneInputModule>();
        string[] controllerName = Input.GetJoystickNames();
        if (controllerName[0] == "")
        {
            inputModules[0].enabled = true;
            inputModules[1].enabled = false;
        }
        else if (controllerName[0] != "")
        {
            inputModules[0].enabled = false;
            inputModules[1].enabled = true;
        }
    }
    
    public void TapStageButton(GameObject obj)
    {
        SoundManager.Instance.PlaySe((int)SoundManager.SENAME.SE_SELECT);
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].Equals(obj))
            {
                stageSelectNum = i;
                gameObject.GetComponent<Button>().interactable = true;
            }
        }
    }

    public void TapNextButton()
    {
        Debug.Log("ステージ" + stageSelectNum + "にシーン遷移(いまは全部チュートリアルに移動)");
        FadeManager fade_m = GameObject.Find("FadeManager").GetComponent<FadeManager>();
        fade_m.LoadScene(2, 0.5f);
    }

}
