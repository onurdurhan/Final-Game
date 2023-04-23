using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyclone.Core;
using Cyclone.Rigid;
using Cyclone.Rigid.Constraints;
using Cyclone.Rigid.Collisions;
using Cyclone.Particles;
using QUATERNION = Cyclone.Core.Quaternion;
public class MeteorRain : MonoBehaviour
{
    private double mass = 1;

    private double damping = 0.9;

    private RigidBody m_body;
    bool isCreated;

    List<Particle> m_particles;
    List<GameObject> m_unity;
    public GameObject[] s;
    GameObject clone;

    void Awake() { s = GameObject.FindGameObjectsWithTag("dusty"); }
    // Start is called before the first frame update
    void Start()
    {
        var pos = transform.position.ToVector3d();
        var scale = transform.localScale.y * 0.5;
        var rot = transform.rotation.ToQuaternion();

        m_body = new RigidBody();
        m_body.Position = pos;
        m_body.Orientation = rot;
        m_body.LinearDamping = damping;
        m_body.AngularDamping = damping;
        m_body.SetMass(mass);
        m_body.SetAwake(true);
        m_body.SetCanSleep(true);
        m_body.Velocity = new Vector3d((double)Random.Range(-50, 50), (double)Random.Range(-50, 50), (double)Random.Range(-50, 50));

        var shape = new CollisionSphere(scale);

        shape.Body = m_body;

        RigidPhysicsEngine.Instance.Bodies.Add(m_body);
        RigidPhysicsEngine.Instance.Collisions.Primatives.Add(shape);

        s = GameObject.FindGameObjectsWithTag("dusty");

        m_particles = new List<Particle>();

        m_unity = new List<GameObject>();

        CreateParticles();

    }


    private void CreateParticles()
    {
        s = GameObject.FindGameObjectsWithTag("dusty");
        Vector3 scaleChange = new Vector3(0.1f, 0.1f, 0.1f);
        for (int i = 0; i < 50; i++)
        {
            var p = new Particle();
            clone = Instantiate(s[i]);
            p.SetMass(0);
            p.Damping = damping;
            Vector3d dir = -1 * m_body.Velocity;
            float theta = Mathf.Atan(Mathf.Sqrt(((float)dir.x * (float)dir.x + (float)dir.y * (float)dir.y + (float)dir.z * (float)dir.z) / (float)dir.z));
            theta = 3.14f / 8.0f;
            theta = Random.Range(0.0f, theta);
            float phi = Random.Range(0.0f, 2.0f * 3.14f);
            Vector3d temp = new Vector3d(Mathf.Cos(phi) * Mathf.Sin(theta), Mathf.Sin(phi) * Mathf.Sin(theta), Mathf.Cos(theta));
            temp = temp.Normalized;
            QUATERNION q = new QUATERNION(0.0f, temp.x * Mathf.Sin(theta / 2.0f), temp.y * Mathf.Sin(theta / 2.0f), temp.z * Mathf.Sin(theta / 2.0f));
            Matrix3 to_world_space_mat3 = new Matrix3();
            to_world_space_mat3.SetOrientation(q);
            temp = to_world_space_mat3 * dir;
            p.Velocity = temp;
            m_particles.Add(p);
            m_unity.Add(clone);
        }

    }

    // Update is called once per frame
    void Update()
    {

        m_body.Integrate(Time.deltaTime);
        m_body.LinearDamping = 1.0;
        transform.position = m_body.Position.ToVector3();
        transform.rotation = m_body.Orientation.ToQuaternion();
        CreateParticles();
        float age = 1.0f;
        for (int i = 0; i < m_unity.Count && i < m_particles.Count; i++)
        {

            m_particles[i].Position += m_particles[i].Velocity * Time.deltaTime;
            if (m_unity[i] != null) { m_unity[i].transform.position = m_body.Position.ToVector3() + m_particles[i].Position.ToVector3(); }
            if (m_particles.Count > 300 && m_unity.Count > 300)
            {
                Destroy(m_unity[i], 1.0f);
            }

        }

    }

    void OnDestroy()
    {
        m_body = null;
    }

}
