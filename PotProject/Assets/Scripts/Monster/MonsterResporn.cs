using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterResporn : MonoBehaviour {

    public Dictionary<MonsterStatus.MonsterType, string> pass = new Dictionary<MonsterStatus.MonsterType, string>() {
        {MonsterStatus.MonsterType.BAT,"BAT"},
        {MonsterStatus.MonsterType.CLAY_D,"Slime_D"},
        {MonsterStatus.MonsterType.CLAY_F,"Slime_F"},
        {MonsterStatus.MonsterType.CLAY_N,"Slime_N"},
        {MonsterStatus.MonsterType.CLAY_T,"Slime_T"},
        {MonsterStatus.MonsterType.CLAY_W,"Slime_W"},
        {MonsterStatus.MonsterType.CLOUD,"Cloud"},
        {MonsterStatus.MonsterType.FAIRY,"Fairy"},
        {MonsterStatus.MonsterType.HAMSTAR,"Hamstar"},
        {MonsterStatus.MonsterType.HARB,"Grass"},
        {MonsterStatus.MonsterType.LION,"Lion"},
        {MonsterStatus.MonsterType.LUKEWARM,"Lukewarm"},
        {MonsterStatus.MonsterType.ROBOT,"Robot"},
        {MonsterStatus.MonsterType.SHADOW,"Shadow"},
        {MonsterStatus.MonsterType.SNAKE,"Snake"},
        {MonsterStatus.MonsterType.TURTLE,"Turtle"},
        {MonsterStatus.MonsterType.WOOD,"Wood"},
    };

    private MonsterStatus.MonsterType mType;

    private string folderPass = "Prefabs/Monsters/";

    [SerializeField]
    private float createTime = 60;
    private float time;
    private bool countFlag = false;
    public bool CountFlag
    {
        get { return countFlag; }
        set {
            time = 0;
            countFlag = value;
        }
    }

    /// <summary>
    /// 生成するモンスターの種類
    /// </summary>
    public MonsterStatus.MonsterType MType {
        get { return mType; }
        set { mType = value; }
    }
	
	// Update is called once per frame
	void Update () {
        if (countFlag)
        {
            time += Time.deltaTime;
            if (time > createTime)
            {
                Resporn();
            }
        }
	}

    /// <summary>
    /// モンスター生成
    /// </summary>
    void Resporn() {
        GameObject monster = Instantiate(Resources.Load<GameObject>(folderPass + pass[mType]));
        monster.transform.SetParent(transform.parent.gameObject.transform);
        monster.transform.localPosition = transform.localPosition;
        CountFlag = false;
        Destroy(gameObject);
    }
}
