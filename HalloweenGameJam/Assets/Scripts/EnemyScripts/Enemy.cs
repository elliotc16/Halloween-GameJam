using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int damage;  //damage enemy deals
    [SerializeField] protected int health;  //no. of player shots untill enemy deleted
    [SerializeField] protected float sightRange; //determines range enemy will attack player
    [SerializeField] protected float attackRate; // bullets that can be fired per second
    [SerializeField] protected float flickerLength;  //How long enemy is invisible when hit

    protected Transform playerTransform;
    protected float attackTimer;

    protected Rigidbody2D rb;
    protected SpriteRenderer sr;

    // Start is called before the first frame update
    virtual protected void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>(); // store rigidbody2d component in rb
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")   //Triggers when hit by player bullet
        {
            Destroy(collision.gameObject);  //removes bullet
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        Flicker();
    }

    protected float PlayerDistance()
    {
        Vector2 distance = playerTransform.position - transform.position;
        return (distance).magnitude;
    }

    //Causes enemy to dissapear then briefly reappear, used when enemy takes damage
    public void Flicker()
    {
        sr.enabled = false;
        StartCoroutine(UnFlicker());
    }
    private IEnumerator UnFlicker()
    {
        yield return new WaitForSeconds(flickerLength);
        sr.enabled = true;
    }
}
