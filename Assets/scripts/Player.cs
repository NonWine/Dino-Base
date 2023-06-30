using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private Animator _anim;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Transform psWinFx;
    private PlayerController playerController;
    private enemyHealthUI healthUI;
    private bool dead;
    private int starthealth;
    private void Start()
    {
      // Bank.Instance.AddCoins(1000);

        starthealth = _health;
        healthUI = gameObject.GetComponent<enemyHealthUI>();
        playerController = GetComponent<PlayerController>();
        healthUI.SetHealth(_health);
    }
    public void GetDamage(int damag)
    {
        if (SpawnEnemyManager.Instance.isWave())
        {
            _health -= damag;
            healthUI.GetDamageUI(damag);
            if (healthUI.GetfillAmout() <= 0f && !dead)
                Dead();
        }
     
    }

    private void Dead()
    {
        Debug.Log("DEAD");
        dead = true;
        _weapon.enabled = false;
        healthUI.enabled = false;
        _anim.SetLayerWeight(1, 0f);
        playerController.enabled = false;
        _anim.SetBool("Dead", true);
        Invoke(nameof(DisableMe), 2f);
    }

    private void DisableMe()
    {
        gameObject.SetActive(false);
        for (int i = 0; i < DinoList.GetAmount(); i++)
        {
            Destroy(DinoList.GetDino(i));
        }
        GameManager.Instance.GameLose();

    }

    private void EnableMe()
    {
        dead = false;
        gameObject.SetActive(true);
        healthUI.ResetUIHealth();
        SpawnEnemyManager.Instance.CheckStateFrame();
        Debug.Log(1);
    }

    public void EnaableToLive()
    {
        Debug.Log(2);
        _health = 100;
        healthUI.enabled = true;
       
        transform.rotation = Quaternion.Euler(Vector3.zero);
        _anim.SetBool("Dead", false);
        playerController.enabled = true;
        _weapon.enabled = true;
        Invoke(nameof(EnableMe), 0.5f);
    }

    public bool isAlive() { return dead; }

    public void ToHeal()
    {
        _health = starthealth;
        healthUI.ResetUIHealth();
    }
}
