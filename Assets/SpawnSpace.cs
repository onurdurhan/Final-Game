using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpace : MonoBehaviour
{
    private GameObject robot;
    private GameObject[] meteors;
    private GameObject meteor;
    private int destroyedinstances = 1;
    private bool isCreated;
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        robot = GameObject.Find("planet");
    }

    public void DestroySpawny()
    {
        meteors = GameObject.FindGameObjectsWithTag("Respawn");
        for (int i = 0; i < destroyedinstances; i++)
        {
            Destroy(meteors[i].gameObject);
        }

    }

    public void InstantiateSpawny()
    {
        meteors = GameObject.FindGameObjectsWithTag("Respawn");
        int n_colliders = GameObject.Find("PhysicsEngine").GetComponent<RigidPhysicsEngine>().n_colliders;

        if (!isCreated)
        {

            robot = GameObject.Find("planet");
            Vector3 robot_pos = robot.transform.position;
            Vector3 spawn_pos = new Vector3(Random.Range(-200.0f, 200.0f), Random.Range(-200.0f, 200.0f), Random.Range(-200.0f, 200.0f));
            spawn_pos = robot_pos + spawn_pos;
            Instantiate(gameObject, spawn_pos, transform.rotation);
            //         Instantiate(gameObject, new Vector3(Random.Range(-50.0f, 50.0f), Random.Range(50.0f, 50.0f), Random.Range(-50.0f, 50.0f)), transform.rotation);
            isCreated = true;
        }

    }
    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;
        if (timer > 1.0f)
        {
            InstantiateSpawny();
            //            timer = 0.0f;
        }

        if (timer > 3.0f)
        {
            DestroySpawny();
            timer = 0.0f;
        }
    }
}