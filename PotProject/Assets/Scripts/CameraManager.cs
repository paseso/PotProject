using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    private Camera mainCamera;
    private Camera subCamera;

    void Awake()
    {
        mainCamera = Camera.main;
        subCamera = Camera.allCameras[1];
    }

    void Start () {
		
	}
	
	void Update () {
		
	}
}
