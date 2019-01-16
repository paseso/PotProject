using UnityEngine;
using UnityEngine.UI;

public class AlchemyController : MonoBehaviour {

    //生成アイテム
    private Sprite[] CreateItem;
    private TextAsset csvFile;
    private GimmickController gimmick_ctr;
    private MapInfo mInfo;
    //フレームの右下のImage
    private Image GeneratedImg;

    // Use this for initialization
    void Start () {
        GeneratedImg = GameObject.Find("Canvas/Panel/Image").GetComponent<Image>();
        ReSetGeneratedImg();
        setCreateItem();
        mInfo = transform.root.GetComponent<MapInfo>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// 生成できるアイテム配列CreateItemにセット
    /// </summary>
    private void setCreateItem()
    {
        CreateItem = new Sprite[3];
        for(int i = 0; i < CreateItem.Length; i++)
        {
            CreateItem[i] = Resources.Load<Sprite>("Textures/CreateItem_" + i);
        }
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
    /// 右下のフレームにある生成したアイテム画像をnullにする処理
    /// </summary>
    public void ReSetGeneratedImg()
    {
        GeneratedImg.sprite = null;
    }

    /// <summary>
    /// 右下のフレームにある生成したアイテム画像をセットする処理
    /// </summary>
    private void setGeneratedImg(int img_num)
    {
        if (GeneratedImg.sprite != null)
            return;
        GeneratedImg.sprite = CreateItem[img_num];
    }

    /// <summary>
    /// アイテム錬金
    /// </summary>
    /// <param name="item">錬金したいアイテム</param>
    public void MadeItem(ItemStatus.Type item)
    {
        switch (item)
        {
            case ItemStatus.Type.SLIME:
                gimmick_ctr = FindObjectOfType<GimmickController>();
                mInfo = transform.root.GetComponent<MapInfo>();
                Debug.Log("mInfo.GrowTreeFlag: " + mInfo.GrowTreeFlag);
                Debug.Log("mInfo.name: " + mInfo.gameObject.name);
                gimmick_ctr.Grow();
                
                break;
            case ItemStatus.Type.GOLEM:
                setGeneratedImg(1);
                //GameObject obj = Instantiate(CreateItem[1], null);
                //obj.transform.position = new Vector2(gameObject.transform.position.x + 2, gameObject.transform.position.y + 1);
                break;
            case ItemStatus.Type.SNAKE:
                setGeneratedImg(1);
                break;
        }
        Debug.Log(item.GetType());
    }
    
    /// <summary>
    /// アイテム錬金
    /// </summary>
    /// <param name="item_0">素材_0</param>
    /// <param name="item_1">素材_1</param>
    public void MadeItem(ItemStatus.Type item_0, ItemStatus.Type item_1)
    {
        switch (item_0)
        {
            case ItemStatus.Type.SLIME:
                switch (item_1)
                {
                    case ItemStatus.Type.SLIME:
                        setGeneratedImg(0);
                        break;
                    case ItemStatus.Type.GOLEM:
                        setGeneratedImg(0);
                        break;
                    case ItemStatus.Type.SNAKE:
                        setGeneratedImg(0);
                        break;
                }
                break;
            case ItemStatus.Type.GOLEM:
                switch (item_1)
                {
                    case ItemStatus.Type.SLIME:
                        setGeneratedImg(1);
                        break;
                    case ItemStatus.Type.GOLEM:
                        setGeneratedImg(1);
                        break;
                    case ItemStatus.Type.SNAKE:
                        setGeneratedImg(1);
                        break;
                }
                break;

            case ItemStatus.Type.SNAKE:
                switch (item_1)
                {
                    case ItemStatus.Type.SLIME:
                        setGeneratedImg(2);
                        break;
                    case ItemStatus.Type.GOLEM:
                        setGeneratedImg(2);
                        break;
                    case ItemStatus.Type.SNAKE:
                        setGeneratedImg(2);
                        break;
                }
                break;
        }
        Debug.Log(item_0.GetType() + "×" + item_1.GetType());
    }

}
