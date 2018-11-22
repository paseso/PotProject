using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlchemyController : MonoBehaviour {

    [SerializeField, Header("ポーション")]
    private GameObject Potion;
    private TextAsset csvFile;
    private GimmickController gimmick_ctr;
    private MapInfo mInfo;

    // Use this for initialization
    void Start () {
        mInfo = transform.root.GetComponent<MapInfo>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// 錬成csv読み込み
    /// </summary>
    private void ReadText()
    {
        csvFile = Resources.Load("CSV/alchemyList")as TextAsset;
        System.IO.StringReader reader = new System.IO.StringReader(csvFile.text);
        while(reader.Peek() >= -1)
        {

        }
    }

    /// <summary>
    /// アイテム錬金
    /// </summary>
    /// <param name="item">錬金したいアイテム</param>
    public void MadeItem(ItemStatus.ITEM item)
    {
        switch (item)
        {
            case ItemStatus.ITEM.SLIME:
                gimmick_ctr = FindObjectOfType<GimmickController>();
                mInfo = transform.root.GetComponent<MapInfo>();
                Debug.Log("mInfo.GrowTreeFlag: " + mInfo.GrowTreeFlag);
                Debug.Log("mInfo.name: " + mInfo.gameObject.name);
                gimmick_ctr.Grow();
                
                break;
            case ItemStatus.ITEM.GOLEM:
                GameObject obj = Instantiate(Potion, null);
                obj.transform.position = new Vector2(gameObject.transform.position.x + 2, gameObject.transform.position.y + 1);
                break;
            case ItemStatus.ITEM.SNAKE:

                break;
        }
    }

}
