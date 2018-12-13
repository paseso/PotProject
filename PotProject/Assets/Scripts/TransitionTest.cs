using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionTest : MonoBehaviour {

    [SerializeField]
    Material mat;
    [SerializeField]
    Texture tex;
    [SerializeField]
    Color color = new Color(0,0,0,0);
    [SerializeField, Range(0,1)]
    float range;

	// Use this for initialization
	void Start () {
		
	}

    private void OnGUI()
    {
        mat.SetFloat("_Range", range);
        mat.SetColor("_Color", color);
        Graphics.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), tex, mat);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(testFade()); 
        }
	}

    IEnumerator testFade()
    {
        float time = 0;
        while(time <= 1)
        {
            range = Mathf.Lerp(0, 1, time / 1);
            time += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        time = 0;
        while (time <= 1)
        {
            range = Mathf.Lerp(1, 0, time / 1);
            time += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }
}
