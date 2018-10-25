using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージ配置クラス
/// </summary>
public class StageSetting : MonoBehaviour {
    [SerializeField]
    private SpriteRenderer map;

    [SerializeField]
    private Transform firstPos;

    [SerializeField]
    private List<SpriteRenderer> maps = new List<SpriteRenderer>();

    [SerializeField]
    private List<Vector2[]> StageGrid = new List<Vector2[]>();

    private float sizeX;
    private float sizeY;

    [SerializeField]
    private int mapValue;

	// Use this for initialization
	void Start () {
        sizeX = map.size.x;
        sizeY = map.size.y;
        //Debug.Log(StageGrid.Count);
        CreateStageGrid();
    }
	
    /// <summary>
    /// マス作成
    /// </summary>
    public void CreateStageGrid()
    {
        Vector2[] valueX = new Vector2[mapValue];

        for(int i = 0; i < mapValue; i++)
        {
            for(int j = 0; j < mapValue; j++)
            {
                valueX[j] = new Vector2(firstPos.position.x + (sizeX * j),
                                        firstPos.position.y + (sizeY * i));

                valueX[j] = new Vector2(0, 0);
            }
            StageGrid.Add(valueX);
        }
    }
}
