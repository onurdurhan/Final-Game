using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyclone.Core;
using Cyclone.Rigid;
using Cyclone.Rigid.Constraints;
using Cyclone.Rigid.Collisions;

public class RigidBox : MonoBehaviour
{
    public double mass = 1.0;
    public float speed = 5.0f;
    public double damping = 0.9;

    private RigidBody m_body;
    // Start is called before the first frame update
    void Start()
    {
        var pos = transform.position.ToVector3d();
        var scale = transform.localScale.ToVector3d()*0.8;
        var rot = transform.rotation.ToQuaternion();

        m_body = new RigidBody();
        m_body.Position = pos;
        m_body.Orientation = rot;
        m_body.LinearDamping = damping;
        m_body.AngularDamping = damping;
        m_body.SetMass(mass);
        m_body.SetAwake(true);
        m_body.SetCanSleep(true);

        var shape = new CollisionBox(scale);
        shape.Body = m_body;

        RigidPhysicsEngine.Instance.Bodies.Add(m_body);
        RigidPhysicsEngine.Instance.Collisions.Primatives.Add(shape);
    }

    // Update is called once per frame
    void Update()
    {
        m_body.LinearDamping = damping;
        m_body.AngularDamping = damping;
        m_body.Integrate(Time.deltaTime);

        if (Input.GetKey(KeyCode.W))
        {
            m_body.Position.z += speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            m_body.Position.z -= speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            m_body.Position.x += speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            m_body.Position.x -= speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.E))
        {
           m_body.Orientation.j += speed/3*Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            m_body.Orientation.j -= speed/3* Time.deltaTime;
        }





        transform.position = m_body.Position.ToVector3();
        transform.rotation = m_body.Orientation.ToQuaternion();

  


    }



}
