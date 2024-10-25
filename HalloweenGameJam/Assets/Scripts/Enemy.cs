using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private int health;
    [SerializeField] private int range;

    [SerializeField] private GameObject bullet;
    [SerializeField] private float fireRate; // bullets that can be fired per second

    private Transform playerTransform;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 1 / fireRate)
        {
            timer = 0;
            //If within range fires bullet
            //Resets timer regardless so player gets small amount of time when entering enemy range (We may wanna change this idk)
            if(Vector2.SqrMagnitude(transform.position - playerTransform.position) < range)
            {
                GameObject instance = Instantiate(bullet);
                instance.GetComponent<BulletEnemy>().Fire(transform.position, playerTransform.position);
            }
        }
        timer += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {

    }
}
