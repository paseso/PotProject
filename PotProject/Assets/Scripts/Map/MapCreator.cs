using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour
{

    [SerializeField]
    private Tile[] tiles;
    //[SerializeField]
    //private Gimmick[] gimmicks;
    //[SerializeField]
    //private Enemy[] enemys;
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

