using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClear : MonoBehaviour {

    Button selectButton;

    void Start()
    {
        selectButton = transform.GetChild(0).GetComponent<Button>();
        selectButton.onClick.AddListener(ChangeSelect);
    }

    /// <summary>
    /// 画面遷移
    /// </summary>
    public void ChangeSelect()
    {
        FadeManager.Instance.LoadScene(1,1f);
    }
}
