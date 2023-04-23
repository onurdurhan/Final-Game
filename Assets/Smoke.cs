using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyclone.Core;
using Cyclone.Particles;
using Cyclone.Particles.Forces;
using Cyclone.Particles.Constraints;



public class Smoke : MonoBehaviour
{
    List<Particle> m_particles;
    List<GameObject> m_unity;
    GameObject s;
    GameObject clone;
    void Start()
    {
        m_particles = new List<Particle>();
        m_unity = new List<GameObject>();
        
        CreateParticles();

    }

    private void CreateParticles()
    {
        double mass = 1;
        double damping = 0.5;
        GameObject s = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Vector3 scaleChange = new Vector3(0.1f, 0.1f, 0.1f);
        s.transform.localScale = scaleChange;
        for (int i = 1; i <= 5; i++)
        {
            var p = new Particle();
            clone = Instantiate(s);
            clone.name = "smoky" + i.ToString();
            p.SetMass(0);
            p.Damping = damping;
            p.Velocity = new Vector3d((double)Random.Range(-1.0f, 1.0f), (double)Random.Range(5.0f, 10.0f), (double)Random.Range(-1.0f, 1.0f));
            m_particles.Add(p);
            m_unity.Add(clone);
        }




    }

    private void DestroyParticle(Particle p, GameObject g, float age)
    {
           
            if (p.Position.Magnitude / p.Velocity.Magnitude > age)
            {
               m_particles.Remove(p);
               m_unity.Remove(g);
            }
    }




    void Update()
    {
        CreateParticles();
        float age = 2.0f;
        print(m_particles.Count);
        print(m_unity.Count);
        for (int i = 0; i < m_unity.Count; i++)
        {
            m_particles[i].Position += m_particles[i].Velocity * Time.deltaTime;
            if (m_unity[i] != null){ m_unity[i].transform.position = m_particles[i].Position.ToVector3(); }
            if (m_particles.Count > 2000 && m_unity.Count > 2000) {
                DestroyParticle(m_particles[i], m_unity[i], age);
                Destroy(m_unity[i], 1);
            }
            

        }

    }

}

