using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenkinController : MonoBehaviour {

    // レシピ----------------------------------------------------------------------------
    private DropItemStatus.Type[] recipe = {
        DropItemStatus.Type.collar,DropItemStatus.Type.clay,     // はしご
        DropItemStatus.Type.pothook,DropItemStatus.Type.ore,     // 鍵
        DropItemStatus.Type.weed,DropItemStatus.Type.scales,     // 回復ポーション
        DropItemStatus.Type.collar,DropItemStatus.Type.clay,     // バリア
        DropItemStatus.Type.cane,DropItemStatus.Type.clay,       // バジュラ
        DropItemStatus.Type.mic,DropItemStatus.Type.clay,        // 拡声器
        DropItemStatus.Type.cloud,DropItemStatus.Type.scales,    // 飛べる雲
        DropItemStatus.Type.smokeball,DropItemStatus.Type.cloud, // 竜巻
        DropItemStatus.Type.weed,DropItemStatus.Type.clay,       // 攻撃UPポーション
        DropItemStatus.Type.collar,DropItemStatus.Type.scales,   // 投げ縄
        DropItemStatus.Type.smokeball,DropItemStatus.Type.scales,// 煙玉
        DropItemStatus.Type.cloud,DropItemStatus.Type.crystals,  // 雨雲
    };
    
    private List<DropItemStatus.Type[]> recipes = new List<DropItemStatus.Type[]>();
    private Dictionary<DropItemStatus.Type[], string> createRecipes = new Dictionary<DropItemStatus.Type[], string>();
    
    private string[] ItemPass = {
        "Ladder",
        "Key",
        "HPPortion",
        "Barrier",
        "Vajura",
        "Transceiver",
        "FlyCloud",
        "Tornago",
        "ATKPortion",
        "Lasso",
        "SmokeScreen",
        "RainCloud",
    };
    // レシピEND-------------------------------------------------------------------------


    [SerializeField]
    private Image[] dropItemImages = new Image[3];

    [SerializeField]
    private Image[] createItemImages = new Image[3];

    [SerializeField]
    private Image[] InPotImages = new Image[2];

    private DropItemStatus.Type[] inPotData = new DropItemStatus.Type[2];

    private CreateItemManager cItemManager;
    private DropItemManager dItemManager;

	// Use this for initialization
	void Start () {
        SetRecipe();
	}

    public void InPot(DropItemStatus.Type type) {

    }

    /// <summary>
    /// レシピをセット
    /// </summary>
    public void SetRecipe() {
        for(int i = 0; i < recipe.Length; i+=2) {
            DropItemStatus.Type[] temp = new DropItemStatus.Type[2];
            int count = 0;
            for(int j = i; j < i + 2; j++) {
                temp[count] = recipe[j];
            }
            recipes.Add(temp);
        }
        
        for (int i = 0; i < recipes.Count; i++) {
            createRecipes.Add(recipes[i], ItemPass[i]);
        }
    }

    // 錬金してアイテムを所持アイテムリストに追加
    public void Renkin(DropItemStatus item1, DropItemStatus item2) {
        if(inPotData[0] == null || inPotData[1] == null) { return; }
    }
}
