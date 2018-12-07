using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlchemyController : MonoBehaviour {

    [SerializeField, Header("生成アイテム")]
    private GameObject[] CreateItem;
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
                GameObject obj = Instantiate(CreateItem[1], null);
                obj.transform.position = new Vector2(gameObject.transform.position.x + 2, gameObject.transform.position.y + 1);
                break;
            case ItemStatus.ITEM.SNAKE:

                break;
        }
        Debug.Log(item.GetType());
    }
    
    /// <summary>
    /// アイテム錬金
    /// </summary>
    /// <param name="item_0">素材_0</param>
    /// <param name="item_1">素材_1</param>
    public void MadeItem(ItemStatus.ITEM item_0, ItemStatus.ITEM item_1)
    {
        GameObject obj;
        switch (item_0)
        {
            case ItemStatus.ITEM.SLIME:
                switch (item_1)
                {
                    case ItemStatus.ITEM.SLIME:
                        obj = Instantiate(CreateItem[0], null);
                        obj.transform.position = new Vector2(gameObject.transform.position.x + 2, gameObject.transform.position.y + 1);
                        break;
                    case ItemStatus.ITEM.GOLEM:
                        obj = Instantiate(CreateItem[0], null);
                        obj.transform.position = new Vector2(gameObject.transform.position.x + 2, gameObject.transform.position.y + 1);
                        break;
                    case ItemStatus.ITEM.SNAKE:
                        obj = Instantiate(CreateItem[0], null);
                        obj.transform.position = new Vector2(gameObject.transform.position.x + 2, gameObject.transform.position.y + 1);
                        break;
                }
                break;
            case ItemStatus.ITEM.GOLEM:
                switch (item_1)
                {
                    case ItemStatus.ITEM.SLIME:
                        obj = Instantiate(CreateItem[1], null);
                        obj.transform.position = new Vector2(gameObject.transform.position.x + 2, gameObject.transform.position.y + 1);
                        break;
                    case ItemStatus.ITEM.GOLEM:
                        obj = Instantiate(CreateItem[1], null);
                        obj.transform.position = new Vector2(gameObject.transform.position.x + 2, gameObject.transform.position.y + 1);
                        break;
                    case ItemStatus.ITEM.SNAKE:
                        obj = Instantiate(CreateItem[1], null);
                        obj.transform.position = new Vector2(gameObject.transform.position.x + 2, gameObject.transform.position.y + 1);
                        break;
                }
                break;

            case ItemStatus.ITEM.SNAKE:
                switch (item_1)
                {
                    case ItemStatus.ITEM.SLIME:
                        obj = Instantiate(CreateItem[2], null);
                        obj.transform.position = new Vector2(gameObject.transform.position.x + 2, gameObject.transform.position.y + 1);
                        break;
                    case ItemStatus.ITEM.GOLEM:
                        obj = Instantiate(CreateItem[2], null);
                        obj.transform.position = new Vector2(gameObject.transform.position.x + 2, gameObject.transform.position.y + 1);
                        break;
                    case ItemStatus.ITEM.SNAKE:
                        obj = Instantiate(CreateItem[2], null);
                        obj.transform.position = new Vector2(gameObject.transform.position.x + 2, gameObject.transform.position.y + 1);
                        break;
                }
                break;
        }
        Debug.Log(item_0.GetType() + "×" + item_1.GetType());
    }

}
