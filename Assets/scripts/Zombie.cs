using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField] private int health;
    private Animator _anim;
    private bool Alive;

    private void Start()
    {
        Alive = true;
        _anim = GetComponent<Animator>();
    }
    public bool isAlive() { return Alive; }
    public void ReduceHealth(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            GetComponent<BoxCollider>().enabled = false;
            Alive = false;
            _anim.SetBool("Death", true);
            Destroy(gameObject, 2f);
        }
    }
  
}
