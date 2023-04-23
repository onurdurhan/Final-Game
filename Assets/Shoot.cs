using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyclone.Core;
using Cyclone.Rigid;
using Cyclone.Rigid.Constraints;
using Cyclone.Rigid.Collisions;
using Cyclone.Particles;
using QUATERNION = Cyclone.Core.Quaternion;
public class Shoot : MonoBehaviour
{
    public double mass = 1;
    private double damping = 1.0;
    private  RigidBody b;
    public GameObject[] bullets;
    public GameObject bullet;
    //    public Particle[] m_particles;
    private GameObject clone;
    List<RigidBody> b_list;
    List<GameObject> b_unity;
    public GameObject sphere;
    bool isCreated;


   // void Awake() { bullets = GameObject.FindGameObjectsWithTag("bullet"); }
    // Start is called before the first frame update
    void Start()

    {
        bullets = GameObject.FindGameObjectsWithTag("bullet");
        b_list =  new List<RigidBody>();
        b_unity = new List<GameObject>();
        sphere = GameObject.Find("Sphere");
        
        Bam();

    }


    void CreateBullet()
    {
        //Vector3d sphere_vel = GameObject.Find("Sphere").GetComponent<CentralForce>().public_vel;
        //Vector3d sphere_pos = GameObject.Find("Sphere").GetComponent<CentralForce>().public_pos;
        //bullets = GameObject.FindGameObjectsWithTag("bullet");
        for (int i = 0; i < 1; i++)
        {
            if (!isCreated)
            {
                sphere = GameObject.Find("Sphere");
                var clone = Instantiate(gameObject, sphere.transform.position, transform.rotation);
                var pos = transform.position.ToVector3d();
                var scale = transform.localScale.y * 0.5;
                var rot = transform.rotation.ToQuaternion();
                var b = new RigidBody();
                b.Position = pos;
                b.Orientation = rot;
                b.SetMass(1.0);
                b.LinearDamping = 1.0;
                b.SetAwake(true);
                b.SetCanSleep(true);
                var shape = new CollisionSphere(scale);
                shape.Body = b;
                Vector3 direction = 1.0f*(GameObject.Find("planet").transform.position-sphere.transform.position);
                b.Velocity = 50*direction.ToVector3d().Normalized;
                RigidPhysicsEngine.Instance.Bodies.Add(b);
                RigidPhysicsEngine.Instance.Collisions.Primatives.Add(shape);
                QUATERNION q = new QUATERNION(1.0, 0.0, 0.0, 0.0);
                
               // clone.transform.position = GameObject.Find("Sphere").transform.position;
                //var clone = Instantiate(gameObject, pos, rot);
                b_list.Add(b);
                b_unity.Add(clone);
                isCreated = true;
            }
        }
    }


    void Bam()
    {

        if (Input.GetMouseButtonDown(0))
        {
            CreateBullet();
        }

    }
    // Update is called once per frame
    void Update()
    {
        
        Bam();
        for (int i = 0; i < b_unity.Count; i++)
        {  
            b_list[i].LinearDamping = 1.0;
            b_list[i].Integrate(Time.deltaTime);
           if (b_unity[i] != null) { b_unity[i].transform.position = b_list[i].Position.ToVector3(); }
           if (b_unity[i] != null) { b_unity[i].transform.rotation = b_list[i].Orientation.ToQuaternion();}
           if (b_unity.Count>20 && b_list.Count > 20)
            {
                
                Destroy(b_unity[i]);
                b_list[i] = null;
            }
        }
        



    }

}
