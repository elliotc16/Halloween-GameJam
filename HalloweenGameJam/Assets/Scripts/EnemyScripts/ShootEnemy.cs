using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemy : Enemy
{
    [SerializeField] private float weaponRange;
    [SerializeField] protected GameObject bullet;
    // Start is called before the first frame update
    protected override void Start()
    {
        //calls the Enemy start() method
        base.Start();
    }

    void FixedUpdate()
    {
        transform.rotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
        if (attackTimer > 1 / attackRate)
        {
            attackTimer = 0;
            //If within range fires bullet
            //Resets timer regardless so player gets small amount of time when entering enemy range (We may wanna change this idk)
            if (PlayerDistance() < weaponRange)
            {
                GameObject instance = Instantiate(bullet);
                instance.GetComponent<BulletEnemy>().Fire(transform.position, playerTransform.position);
            }
        }
        attackTimer += Time.deltaTime;
    }
}
