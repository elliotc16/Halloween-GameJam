using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float fireRate; // bullets that can be fired per second
    private float timer;
    void Start()
    {
        timer = 1 / fireRate;
    }

    void Update()
    {
        // fire every 1/fireRate seconds
        if (Input.GetMouseButton(0) && timer > (1/fireRate))
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            timer = 0;
        }
        timer += Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyBullet")
        {
            Destroy(collision.gameObject);
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        
    }
}
