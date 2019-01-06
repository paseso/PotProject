using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointSetting : MonoBehaviour
{

    private GameObject Brother;
    //private GameObject[] Joints;
    private GameObject Pot;

    private DistanceJoint2D Brother_Distance;
    private DistanceJoint2D Pot_Distance;

    private bool _reSetJoint = false;

    public bool ResetJoint
    {
        set { _reSetJoint = value; }
        get { return _reSetJoint; }
    }

    // Use this for initialization
    void Start()
    {
        Brother = GameObject.FindObjectOfType<MoveController>().gameObject.transform.parent.gameObject;
        Pot = GameObject.FindObjectOfType<PotController>().gameObject;
        AddPlayerJoint();
        AddJointsJoint();
        Brother_Distance = Brother.GetComponent<DistanceJoint2D>();
        Pot_Distance = Pot.GetComponent<DistanceJoint2D>();
        setPlayerJoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (_reSetJoint)
        {
            setPlayerJoint();
        }
    }

    /// <summary>
    /// お兄ちゃんと弟にHingeJoint2DとDistanceJoint2Dを追加
    /// </summary>
    private void AddPlayerJoint()
    {
        //Brother.AddComponent<HingeJoint2D>();
        //Pot.AddComponent<HingeJoint2D>();
        Brother.AddComponent<DistanceJoint2D>();
        Pot.AddComponent<DistanceJoint2D>();
    }

    /// <summary>
    /// Jointsオブジェクトの子供にHingeJoint2DとDistanceJoint2Dを追加
    /// </summary>
    private void AddJointsJoint()
    {
        //for (int i = 0; i < Joints.Length; i++)
        //{
        //    Joints[i].AddComponent<HingeJoint2D>();
        //    Joints[i].AddComponent<DistanceJoint2D>();
        //}
    }

    /// <summary>
    /// お兄ちゃんと弟のDistanceJointのセット
    /// </summary>
    private void setPlayerJoint()
    {
        Brother_Distance.connectedBody = Pot.GetComponent<Rigidbody2D>();
        Pot_Distance.connectedBody = Brother.GetComponent<Rigidbody2D>();
        Brother_Distance.autoConfigureDistance = false;
        Pot_Distance.autoConfigureDistance = false;
        //Distance Joint2DのMax Distance Onlyにチェックをいれる
        Brother_Distance.maxDistanceOnly = true;
        Pot_Distance.maxDistanceOnly = true;
    }
}