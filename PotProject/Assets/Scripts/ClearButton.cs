using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearButton : MonoBehaviour {
    FadeManager fade_m;
    Button button_c;
    // Use this for initialization
    void Start () {
        fade_m = GameObject.Find("FadeManager").GetComponent<FadeManager>();
        button_c = GetComponent<Button>();
        button_c.onClick.AddListener(ClearCall);
    }

    void ClearCall()
    {
        fade_m.LoadScene(0, 0.3f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
