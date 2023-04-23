using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyclone.Core;

public class Spawn : MonoBehaviour
{
   public GameObject robot ;
   public GameObject[] meteors;
   public GameObject meteor;
   public int destroyedinstances = 1;
   bool isCreated;
   float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        robot = GameObject.Find("robot");
    }

    public void DestroySpawny()
    {
        meteors = GameObject.FindGameObjectsWithTag("Respawn");
        for (int i = 0; i < destroyedinstances; i++)
        {
            Destroy(meteors[i].gameObject);
            // meteors[i].gameObject.GetComponent<RigidPhysicsEngine>().Instance;
           // meteors[i].gameObject.GetComponent<SmokySphere>().m_body = null;
        }

    }

    public void InstantiateSpawny()
    {
        meteors = GameObject.FindGameObjectsWithTag("Respawn");
        int n_colliders  = GameObject.Find("PhysicsEngine").GetComponent<RigidPhysicsEngine>().n_colliders;
        
        if (!isCreated )
        {
            
            robot = GameObject.Find("Robot");
            Vector3 robot_pos = robot.transform.position;
            Vector3 spawn_pos = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(30f, 40.0f), Random.Range(200.0f, 250.0f));
            spawn_pos = robot_pos+spawn_pos;
            Instantiate(gameObject, spawn_pos, transform.rotation);
   //         Instantiate(gameObject, new Vector3(Random.Range(-50.0f, 50.0f), Random.Range(50.0f, 50.0f), Random.Range(-50.0f, 50.0f)), transform.rotation);
            isCreated = true;
        }
        

    }


    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;
        if (timer > 0.9f) 
        {
            InstantiateSpawny();
//            timer = 0.0f;
        }

        if (timer > 4.0f)
        {
           DestroySpawny();
           timer = 0.0f;
        }

  //      StartCoroutine(ExampleCoroutine());


    }



}
