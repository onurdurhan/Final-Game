using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyclone.Rigid;
using Cyclone.Rigid.Forces;
using UnityEngine.SceneManagement;
public class RigidPhysicsEngine : MonoBehaviour
{
    public int iterations = 0;

    public int maxContacts = 100;

    public double epsilon = 0.002;

    public int n_colliders = 0;

    public static RigidBodyEngine Instance { get; private set; }
    private void Awake()
    {
        
        Instance = new RigidBodyEngine(maxContacts);
        Instance.Resolver.PositionIterations = iterations;
        Instance.Resolver.VelocityIterations = iterations;
        Instance.Resolver.PositionEpsilon = epsilon;
        Instance.Resolver.VelocityEpsilon = epsilon;
        Instance.Collisions.Restitution = 0.001;
        Instance.Collisions.Friction = 0.1;

        Scene scene = SceneManager.GetActiveScene();
        if(scene.name == "SampleScene")
        {
            Instance.ForceAreas.Add(new RigidGravityForce(-9.81));
        }
           



    }

    private void flush()
    {

        Instance.Bodies.Clear();
        //Instance.Forces.Clear();
        //Instance.ForceAreas.Clear();
        Instance.Constraints.Clear();
    //    Instance.Resolver = null;
    //    Instance.Collisions = null;

    }


        private void FixedUpdate()
        {
           double dt = Time.fixedDeltaTime;
    
           Instance.StartFrame();
           Instance.RunPhysics(dt);
        }
       

}
