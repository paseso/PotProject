using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearButton : MonoBehaviour {
    Button button_c;
    // Use this for initialization
    void Start () {
        button_c = GetComponent<Button>();
        button_c.onClick.AddListener(ClearCall);
    }

    void ClearCall()
    {
        FadeManager.Instance.LoadScene(1, 0.3f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
