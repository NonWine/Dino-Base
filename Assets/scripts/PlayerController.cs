using UnityEngine;
using UnityEngine.AI;
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }
    [SerializeField] private Joystick joystick;
    [SerializeField] private Animator _player;
    [SerializeField] private Transform _target;
    [SerializeField] private Transform bogy;
    private bool canRotate = true;
    private NavMeshAgent agent;
    private Vector3 direction;

    private void Awake()
    {
        Instance = this;
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
       // Bank.Instance.AddCoins(10000);
    }

    private void Update()
    {
        Move();
        Rotate();
        UpdateAnimator();
    }

    private void Move()
    {
         direction = new Vector3(joystick.Horizontal, 0, joystick.Vertical).normalized;
         agent.Move(direction * (agent.speed * Time.deltaTime));
    }

    private void Rotate()
    {
           if (direction != Vector3.zero && canRotate)
            bogy.rotation = Quaternion.Slerp(bogy.rotation, Quaternion.LookRotation(direction), agent.angularSpeed * Time.deltaTime);
    
    }

    private void UpdateAnimator() 
    {
        if (GetComponentInChildren<Detector>().isDetected())
            _player.SetLayerWeight(1, 0.5f);
        else
            _player.SetLayerWeight(1, 0f);
        _player.SetFloat("speed", Vector3.ClampMagnitude(direction, 1).magnitude);
    } 

    public float GetSpeedRotation() { return agent.angularSpeed; }

    public void ChangeAbilityToRotate(bool flag) => canRotate = flag;

    public Transform GetTargetPlayer() { return _target; } 
}
