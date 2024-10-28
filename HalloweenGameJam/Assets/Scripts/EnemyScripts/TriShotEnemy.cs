using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriShotEnemy : Enemy
{
    [SerializeField] private float weaponRange;
    [SerializeField] protected GameObject bullet;

    private float angle = 60;
    private float angleStart = -60;
    // Start is called before the first frame update
    protected override void Start()
    {
        //calls the Enemy start() method
        base.Start();
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
                //Fires 3 shots, with 60 degrees apart
                for (int i = 0; i < 3; i++)
                {
                    GameObject instance = Instantiate(bullet);
                    var be = instance.GetComponent<BulletEnemy>();
                    be.Fire(transform.position, playerTransform.position, angleStart + angle * i);
                }
            }
        }
        attackTimer += Time.deltaTime;
    }
}
