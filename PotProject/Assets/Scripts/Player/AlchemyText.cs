using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


[System.Serializable]
public struct AlchemyItemText
{
    public string itemName;
    public string itemDesc;
}

[System.Serializable]
public struct AlchemyMaterialText
{
    public string materialName;
    public string materialDesc;
    public string madeableItem;
}


public class AlchemyText : MonoBehaviour {

    private const string PATH_ALCHEMYITEM = "TextData/AlchemyItem";
    private const string PATH_ALCHEMYMATERIAL = "TextData/AlchemyMaterial";

    [SerializeField]
    private List<AlchemyItemText> itemTexts = new List<AlchemyItemText>();
    [SerializeField]
    private List<AlchemyMaterialText> materialTexts = new List<AlchemyMaterialText>();

    public List<AlchemyItemText> ItemTexts { get { return itemTexts; } }
    public List<AlchemyMaterialText> MaterialTexts { get { return materialTexts; } }

    void Awake () {
        LoadAlchemyCSV();
	}
	
    private void LoadAlchemyCSV()
    {
        //  完成品のCSV読み込み
        TextAsset itemTextCSV = Resources.Load(PATH_ALCHEMYITEM) as TextAsset;
        StringReader readerItem = new StringReader(itemTextCSV.text);
        itemTexts.Clear();
        while (readerItem.Peek() != -1)
        {
            //  改行で区切る
            string line = readerItem.ReadLine();
            if (line.Contains("@"))
                break;
            //  カンマで区切る
            string[] block = line.Split(',');
            var itemText = new AlchemyItemText();
            //  特殊文字で区切る
            //  ---後で---
            //  inspectorで確認しやすい型に変換する
            itemText.itemName = block[0];
            itemText.itemDesc = block[1];
            //  追加
            itemTexts.Add(itemText);
        }

        //  素材のCSV読み込み
        TextAsset materialTextCSV = Resources.Load(PATH_ALCHEMYMATERIAL) as TextAsset;
        StringReader readerMaterial = new StringReader(materialTextCSV.text);
        MaterialTexts.Clear();
        while (readerMaterial.Peek() != -1)
        {
            //  改行で区切る
            string line = readerMaterial.ReadLine();
            if (line.Contains("@"))
                break;
            //  カンマで区切る
            string[] block = line.Split(',');
            var matText = new AlchemyMaterialText();
            //  特殊文字で区切る
            //  ---後で---
            //  inspectorで確認しやすい型に変換する
            matText.materialName = block[0];
            matText.madeableItem = block[1];
            matText.materialDesc = block[2];
            //  追加
            materialTexts.Add(matText);
        }
    }
}
