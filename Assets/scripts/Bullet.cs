using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float damage;



    public void SetDamage(float value) => damage = value;

    public float GetDamage() { return damage; }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Creature"))
    //    {
    //        Dino enemy = collision.gameObject.GetComponent<Dino>();
    //        if(!enemy.isDead())
    //            enemy.GetDamage(damage);
    //        Destroy(gameObject);
    //    }
    //}
}
