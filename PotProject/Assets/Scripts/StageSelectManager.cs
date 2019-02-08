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
    private int stageSelectNum = 0;

    private StandaloneInputModule[] inputModules;


	void Start () {
        eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(buttons[0]);
        eventSystem.GetComponent<StandaloneInputModule>().horizontalAxis = "Horizontal_ps4";
        eventSystem.GetComponent<StandaloneInputModule>().verticalAxis = "Vertical_ps4";
    }
	
	void Update () {
		
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
