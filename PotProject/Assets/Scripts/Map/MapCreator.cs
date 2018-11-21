using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class MapCreator : MonoBehaviour
{
    [SerializeField]
    private Tile[]  tiles;
    [SerializeField]
    private Gimmick[] gimmicks;
    [SerializeField]
    private Enemy[] enemys;
    private MapDate map;
    public MapDate ResourceMap;
    [HideInInspector]
    public GameObject tilePrefab;

    public MapDate Map
    {
        get{ return map; }
        set{ map = value; }
    }

    public Tile[] GetTileList()
    {
        return tiles;
    }
}

[System.Serializable]
public class Tile
{
    [SerializeField]
    private Texture tileImage;
    [SerializeField]
    private GameObject tileObj;

    public Texture TileImage
    {
        get { return tileImage; }
        //set { tileImage = value; }
    }

    public GameObject TileObj
    {
        get { return tileObj; }
        //set { tileObj = value; }
    }
}

[System.Serializable]
public class Gimmick
{
    [SerializeField]
    private Texture2D gimmickImage;
    [SerializeField]
    private Vector2 size;
    [SerializeField]
    private GameObject gimmickObj;
}

[System.Serializable]
public class Enemy
{
    [SerializeField]
    private Texture2D enemyImage;
    [SerializeField]
    private GameObject enemyObj;
}

