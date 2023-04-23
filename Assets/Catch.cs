using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyclone.Core;

public class Catch : MonoBehaviour
{
    GameObject robot;
    GameObject coin;
    // Start is called before the first frame update
    void Start()
    {
        robot = GameObject.Find("Robot");
        coin = GameObject.Find("Coin");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3d robot_pos = new Vector3d((double)robot.transform.position.x, (double)robot.transform.position.y, (double)robot.transform.position.z);
        Vector3d coin_pos = new Vector3d((double)coin.transform.position.x, (double)coin.transform.position.y, (double)coin.transform.position.z);
        Vector3d r = robot_pos - coin_pos;
        double distance = r.Magnitude;
        if(distance<2) 
        {
            print("You collected me");
           // Destroy(coin, 1);
        }



    }
}
