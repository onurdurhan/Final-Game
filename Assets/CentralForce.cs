using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyclone.Core;
using Cyclone.Rigid;
using Cyclone.Rigid.Constraints;
using Cyclone.Rigid.Collisions;
using Cyclone.Particles;
using QUATERNION = Cyclone.Core.Quaternion;

public class CentralForce : MonoBehaviour
{
    private double mass = 10.0;

    private double damping = 1.0;
    private RigidBody m_body;
    private double speed = 1.0;
    public Vector3d public_pos;
    public Vector3d public_vel;

    // Start is called before the first frame update
    void Start()
    {
        
        var rot = transform.rotation.ToQuaternion();
        m_body = new RigidBody();
        m_body.Position = new Vector3d(0.0, 0.0, 100.0);
        m_body.Orientation = rot;
        m_body.LinearDamping = damping;
        m_body.AngularDamping = damping;
        m_body.SetMass(mass);
     //   m_body.SetAwake(true);
     //   m_body.SetCanSleep(true);
        m_body.Velocity = new Vector3d(0.0, 0.0, 50.0);




    }

    // Update is called once per frame
    void Update()
    {
        m_body.LinearDamping = damping;
        
        m_body.Integrate((double)Time.deltaTime);
     


        /////////////
        Vector3d r = m_body.Position - new Vector3d(-200.0, 0.0, 200.0);
        Vector3d r_hat = r.Normalized;
        r_hat = -1*r_hat;
        double r_sqr = r.SqrMagnitude;
        double G = 5000000;
        r_hat = (G / r_sqr)*r_hat;
        m_body.AddForce(r_hat);

        ////////////////
        transform.position = m_body.Position.ToVector3();
        transform.rotation = m_body.Orientation.ToQuaternion();
        public_pos = m_body.Position;
        public_vel = m_body.Velocity;


    }
}
