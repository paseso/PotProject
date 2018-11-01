using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlchemyController : MonoBehaviour {

    private TextAsset csvFile;
    

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void ReadText()
    {
        csvFile = Resources.Load("CSV/alchemyList")as TextAsset;
        System.IO.StringReader reader = new System.IO.StringReader(csvFile.text);
        while(reader.Peek() >= -1)
        {

        }
    }

}
