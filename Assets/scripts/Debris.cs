using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    [SerializeField] private float _force, _forceY, forceRight;
    [SerializeField] private Rigidbody rd;
    [SerializeField] private float timeAlive;
    private bool trig;
    void Start()
    {
        transform.rotation = Quaternion.Euler(new Vector3(Random.Range(0, 360f), Random.Range(0, 360f), Random.Range(0, 360f)));
        Invoke("Disable", timeAlive);
    }

    public void Force(Vector3 vec)
    {
         rd.AddForce(vec * _force,ForceMode.VelocityChange);
        rd.velocity = vec * _forceY;
        trig = false;
       // Invoke("Disable", timeAlive);



    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground") && !trig)
        {
            trig = true;
            rd.AddForce(new Vector3(0f, 0f, Random.Range(-1f,1f)) * forceRight,ForceMode.Impulse);
        }
    }

    private void Disable()
    {
        Destroy(gameObject);
        transform.position = new Vector3(1000f, 0f, 1000f);
     
    }

}
