using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    [SerializeField] private float meleeRange;
    [SerializeField] private float lungeRange;
    [SerializeField] private float initialLungeSpeed; //speeds lunge starts and ends at
    [SerializeField] private float maxLungeSpeed; //max speed lunge reaches
    [SerializeField] private float lungeCD;

    private float lungeCDTimer = 0;

    private float lungeProgessTimer;
    private bool lungingFirstPhase = false;
    private bool lungingSecondPhase = false;
    private float halfLungeTime;

    private Vector2 maxLerpVelocity;
    private Vector2 minLerpVelocity;
    private Vector2 target;
    // Start is called before the first frame update
    protected override void Start()
    {
        //calls the Enemy start() method
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        //Velocity increases as enemy jumps towards player then decreases and stops when they get closer
        //So velocity lerps between 0 and max untill half of the time to complete the lunge is done
        if (lungingFirstPhase)
        {
            //velocity increases over time, lerp calculates it as a percentage of time passed
            rb.velocity = Vector2.Lerp(minLerpVelocity, maxLerpVelocity, lungeProgessTimer / halfLungeTime);
            //moves onto next phase or advances timer
            if(lungeProgessTimer > halfLungeTime)
            {
                lungeProgessTimer = 0;
                lungingFirstPhase = false;
                lungingSecondPhase = true;
            }
            else
            {
                lungeProgessTimer += Time.deltaTime;
            }
        }
       
        else if (lungingSecondPhase) 
        {
            //velocity decreases over time, lerp calculates it as a percentage of time passed
            rb.velocity = Vector2.Lerp(maxLerpVelocity,minLerpVelocity, lungeProgessTimer / halfLungeTime);
            //finishes lunge or advances timer
            if (lungeProgessTimer > halfLungeTime)
            {
                lungeProgessTimer = 0;
                lungingSecondPhase = false;
                rb.velocity = Vector2.zero;
            }
            else
            {
                lungeProgessTimer += Time.deltaTime;
            }
        }
        else if(PlayerDistance() < meleeRange )
        {
          //something something attack
            
        }
        //Lunges if player in range and lunge cooldown done
        else if(PlayerDistance() < lungeRange && lungeCDTimer > lungeCD)
        {
            lungeAttack();
        }
        else
        {
            lungeCDTimer += Time.deltaTime;
            attackTimer += Time.deltaTime;
        }
    }

    //Enemy jumps towards players before doing a regular melee attack
    void lungeAttack()
    {
        //resets timer for the lunge cooldown
        lungeCDTimer = 0;

        //sets where the enemy is aiming to jump to
        target = (Vector2)playerTransform.position;


        lungingFirstPhase = true;
        lungeProgessTimer = 0;

        var displacement = target - (Vector2)transform.position;
        var direction = displacement.normalized;
        //reduces lunge distance so enemy stops just before the player
        displacement = displacement - direction * 2;
        var distance = displacement.magnitude;  //calculates distance of lunge
        halfLungeTime = distance / (initialLungeSpeed + maxLungeSpeed); //calculates how long half of the lunge should take

        minLerpVelocity = direction * initialLungeSpeed;
        maxLerpVelocity = direction * maxLungeSpeed; //max velocity reached at mid point of lunge
    }
}
