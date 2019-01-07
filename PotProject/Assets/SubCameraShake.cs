using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubCameraShake : MonoBehaviour {

    private Vector3 pos;
    private MoveController mController;

    public void OnEnable() {
        mController = FindObjectOfType<MoveController>();
        if (!mController.getIsMiniMap) {
            pos = transform.position;
            StartCoroutine(Shake());
        }
    }

    public IEnumerator Shake() {
        while (true) {
            transform.position = new Vector3(pos.x, pos.y + 1,pos.z);
            yield return new WaitForSeconds(0.05f);
            transform.position = pos;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
