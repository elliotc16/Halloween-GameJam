using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Melee))]
public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private int maxHealth;
    [SerializeField] private float fireRate; // bullets that can be fired per second
    [SerializeField] private float meleeRate; // melee attacks per second
    [SerializeField] private float invicibilityLength;  //length of invicibility when hit
    [SerializeField] private float invicibilityFlickerCD;
    

    private int health;
    private float timer;
    private float meleeTimer;
    private bool invincible;
    //timer to determine length of invicibility
    private float invincibleTimer;
    private float invincibleFlickerTimer;
    //determines whether player is hidden due to invicibility flicker
    private bool hidden;

    private Melee Melee;
    private SpriteRenderer sr;
    void Start()
    {
        timer = 1 / fireRate;
        meleeTimer = 1 / meleeRate;
        invincible = false;
        health = maxHealth;
        sr = GetComponent<SpriteRenderer>();
        Melee = GetComponent<Melee>();
    }

    void Update()
    {
        // fire every 1/fireRate seconds
        if (Input.GetMouseButton(0) && timer > (1/fireRate))
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            timer = 0;
        }
        else if(Input.GetMouseButton(1) && meleeTimer > (1/meleeRate))
        {
            // take mouse position using camera as reference
            var mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            var mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            // direction vector = difference between mouse position and player position
            var direction = mousePos - transform.position;
            direction = (Vector2)direction.normalized;

            Melee.MakeMeleeAttack(direction);
            meleeTimer = 0;
        }
        timer += Time.deltaTime;
        meleeTimer += Time.deltaTime;

        //determines whether to end invicibility and if to hide/show player for flicker effect
        if (invincible)
        {
            if (invincibleTimer >= invicibilityLength)
            {
                invincible = false;
                sr.enabled = true;
                hidden = false;
            }
            else if (invincibleFlickerTimer > invicibilityFlickerCD)
            {
                //shows player again
                if (hidden)
                {
                    sr.enabled = true;
                    hidden = false;
                }
                //hides player
                else
                {
                    sr.enabled = false;
                    hidden = true;
                }
                invincibleFlickerTimer = 0;
            }
            invincibleTimer += Time.deltaTime;
            invincibleFlickerTimer += Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyBullet")
        {
            Destroy(collision.gameObject);
            TakeDamage(1);
        }
    }
    public void TakeDamage(int damage)
    {
        //No damage if invicible
        if (invincible)
        {
            return;
        }

        health -= damage;
        if(health <= 0)
        {
            playerDies();
            return;
        }
        TurnInvincible();
    }

    private void playerDies()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    private void TurnInvincible()
    {
        invincible = true;
        invincibleTimer = 0;
        invincibleFlickerTimer = 0;
    }
}
