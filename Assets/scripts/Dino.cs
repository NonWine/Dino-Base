using UnityEngine;
using UnityEngine.AI;
using System.Threading.Tasks;

public class Dino : MonoBehaviour
{
    private MeshCollider colliderObject;
    private NavMeshAgent myAgent;
    private Animator myAnim;

    [Header("Stats: ")]
    public float health = 10f;
    [SerializeField] private float damage = 1f;
    [SerializeField] private int attackDelay;
    [SerializeField] private int _rewardMoney;
    [SerializeField] private int _distance;
    [SerializeField] private float _speedRotation = 3f;
    private enemyHealthUI healthUI;
    private Vector3 direction;
    private Transform playerPos;
    private Transform target;
    private bool dead,damaging;
    private float timerAnim;
    private float tdaamage;
    private bool canDamage;
    void Start()
    {
        tdaamage = damage;
        healthUI = gameObject.GetComponent<enemyHealthUI>();
        healthUI.SetHealth(health);
        playerPos = GameObject.FindWithTag("Player").transform;
        myAgent = GetComponent<NavMeshAgent>();
        myAnim = GetComponent<Animator>();
        colliderObject = GetComponentInChildren<MeshCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            
            if (!isDead())
                GetDamage(collision.gameObject.GetComponent<Bullet>().GetDamage());
            Destroy(collision.gameObject);
        }
    }

    void Update()
    {

    //    Debug.Log(Vector3.Distance(transform.position, target.position));
      //  Debug.Log(damaging);
        if (dead) return;
        if (damaging)
        if (Vector3.Distance(transform.position, target.position) > _distance)
        {
             
            timerAnim += Time.deltaTime;
            if(timerAnim > 1f)
            {
                 myAnim.SetInteger("state", 1);
                 damaging = false;
                 myAgent.isStopped = false;
            }
        }

        if (damaging) return;
        
        if (target != null)
        {
            Move();
        }
    }

    public void Move()
    {
        myAnim.SetInteger("state", 1);
        if (target.CompareTag("Frame"))
        {
         //   myAgent.SetDestination(target.position);
            direction = (target.position - transform.position).normalized;
            myAgent.Move(direction * (myAgent.speed * Time.deltaTime));
        }
        else if (target.CompareTag("Player"))
        {
            //direction = (target.position - transform.position).normalized;
            //myAgent.Move(direction * (myAgent.speed * Time.deltaTime));
            myAgent.SetDestination(target.position);
        }
          
     
        if (Vector3.Distance(transform.position, target.position) < _distance)
        {
            //    Debug.Log("AAAAA");
            Debug.Log(target.tag);
            if (target.CompareTag("Player"))
                canDamage = true;
            damaging = true;
            myAgent.isStopped = true;
          
            GiveDamageasync();
        }
    }

    private async void GiveDamageasync()
    {
        await Task.Delay(300);
       
        while (damaging && !dead)
        {
           
               
            myAnim.SetInteger("state", 3);
            if (target.transform.CompareTag("Frame"))
                target.GetComponent<Frame>().GetDamage(damage);
            else if (target.transform.CompareTag("Player") && canDamage)
            {
                if (!damaging)
                    break;
                target.GetComponent<Player>().GetDamage((int)damage);
                if (target.GetComponent<Player>().isAlive())
                    break;
            }
            if (!damaging)
                break;
            await Task.Delay(attackDelay * 1000);
        }
    }

    public void GetDamage(float dmg)
    {
        health -= dmg;
        ParticlePool.Instance.PlayBloodEnemy(transform.position);
        healthUI.GetDamageUI(dmg);
        if (healthUI.GetfillAmout() <= 0)
            Death();
    }

    public void Death()
    {
        dead = true;
        damaging = false;
        myAgent.enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        SpawnEnemyManager.Instance.AddDeathEnemy();
        Bank.Instance.AddCoins(_rewardMoney);
        myAnim.SetInteger("state", 5);
        Invoke("DisableMee", 3f);
    }


    void DisableMee()
    {
      
        gameObject.SetActive(false);
    }
    public bool isDead() { return dead; }

   

    public void setTarget(Transform _target)
    {
        target = _target;        
    }

}
