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
    private Enemy[] enemies;
    [SerializeField]
    private MapDate map;
    public MapDate ResourceMap;
    [HideInInspector]
    public GameObject tilePrefab;

    public Tile[] GetTiles() { return tiles; }
    public Gimmick[] GetGimmicks() { return gimmicks; }
    public Enemy[] GetEnemies() { return enemies; }

    public MapDate Map
    {
        get{ return map; }
        set{ map = value; }
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
    private Texture gimmickImage;
    [SerializeField]
    private Vector2 gimmickSize;
    [SerializeField]
    private GameObject gimmickObj;

    public Texture GimmickImage
    {
        get { return gimmickImage; }
        //set { gimmickImage = value; }
    }
    public Vector2 GimmickSize
    {
        get { return gimmickSize; }
        //set { gimmickSize = value; }
    }
    public GameObject GimmickObj
    {
        get { return gimmickObj; }
        //set { gimmickObj = value; }
    }
}

[System.Serializable]
public class Enemy
{
    [SerializeField]
    private Texture enemyImage;
    [SerializeField]
    private GameObject enemyObj;

    public Texture EnemyImage
    {
        get { return enemyImage; }
        //set { enemyImage = value; }
    }

    public GameObject EnemyObj
    {
        get { return enemyObj; }
        //set { enemyObj = value; }
    }
}

