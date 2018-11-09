using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodGimmick : MonoBehaviour {
    private bool glowFlag = false;
    [SerializeField]
    private GameObject wood;

    [SerializeField]
    private float growSpeed;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("oioioi");
            StartCoroutine(Grow());
        }
    }

    public bool GlowWoodFlag
    {
        get { return glowFlag; }
        private set { glowFlag = value; }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
            glowFlag = true;
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
            glowFlag = false;
    }

    public IEnumerator Grow()
    {
        while(wood.transform.localScale.y < 1f)
        {
            Vector3 growSize = wood.transform.localScale;
            Vector3 growPos = wood.transform.localPosition;
            growSize.y += growSpeed * Time.deltaTime;
            growPos.y += growSpeed * 2 * Time.deltaTime;
            wood.transform.localScale = growSize;
            wood.transform.localPosition = growPos;
            yield return null;
        }
    }
}
