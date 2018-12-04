using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectManager : MonoBehaviour {

    [SerializeField]
    private GameObject[] buttons;
    private int stageSelectNum;


	void Start () {
        Init();
    }
	
	void Update () {
		
	}

    private void Init()
    {
        stageSelectNum = 0;
        //  ボタンにイベントの追加
        for (int i = 0; i < buttons.Length; i++)
        {
            Button btn = buttons[i].GetComponent<Button>();
            btn.onClick.AddListener(() => { TapStageButton(btn.gameObject); });
        }
        gameObject.GetComponent<Button>().onClick.AddListener(TapNextButton);
    }
    
    public void TapStageButton(GameObject obj)
    {
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
        Debug.Log("ステージ" + stageSelectNum + "にシーン遷移(いまはログだけ)");
    }

}
