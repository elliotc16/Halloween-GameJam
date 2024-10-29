using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enemy who charges at and past player
//Velocity time graph for charge is trapezium
public class ChargeEnemy : Enemy
{
    [SerializeField] private float meleeRange;
   
    [SerializeField] private float chargeSpeed;
    [SerializeField] private float chargeCD;
    [SerializeField] private float chargeRange;

    [SerializeField] private float chargeFirstPhaseTime;    //percent of time spent of accelerating phase
    private float chargeSecondPhaseTime;   //percent of time spent on constant speed phase
    [SerializeField] private float chargeThirdPhaseTime;    //percent of time spent on decceleration phase


    private float chargeTime = 0;
    private float chargeTimer = 0;
    //Phase charge is in, 0 if not charging
    private int chargePhase = 0;
    private float chargeCDTimer = 0;

    private Vector2 chargeVelocity;
    private Vector2 target;

    // Start is called before the first frame update
    protected override void Start()
    {
        //calls the Enemy start() method
        base.Start();
        //So the times for the different phases add to 1
        chargeSecondPhaseTime = 1 - chargeFirstPhaseTime - chargeThirdPhaseTime;
    }

    void FixedUpdate()
    {
        transform.rotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
        if (chargePhase == 1)
        {
            //velocity increases over time, lerp calculates it as a percentage of time passed
            //Time for first phase is total charge time(chargeTime) multiplied by percent of time to be spent in first phase(chargeFIrstPhaseTime)
            rb.velocity = Vector2.Lerp(Vector2.zero, chargeVelocity, chargeTimer / (chargeTime * chargeFirstPhaseTime));
            //moves onto next phase or advances timer
            if (chargeTimer > chargeTime * chargeFirstPhaseTime)
            {
                chargePhase = 2;
                chargeTimer = 0;
                rb.velocity = chargeVelocity;
            }
            else
            {
                chargeTimer += Time.deltaTime;
            }
        }

        else if (chargePhase == 2)
        {
            //velocity stays constant
            if (chargeTimer > chargeTime * chargeSecondPhaseTime)
            {
                chargePhase = 3;
                chargeTimer = 0;
            }
            else
            {
                chargeTimer += Time.deltaTime;
            }
        }

        else if(chargePhase == 3)
        {
            //velocity decreases over time, lerp calculates it as a percentage of time passed
            //Time for first phase is total charge time(chargeTime) multiplied by percent of time to be spent in first phase(chargeFIrstPhaseTime)
            rb.velocity = Vector2.Lerp(chargeVelocity, Vector2.zero, chargeTimer / (chargeTime * chargeThirdPhaseTime));
            //moves onto next phase or advances timer
            if (chargeTimer > chargeTime * chargeFirstPhaseTime)
            {
                //stops charge
                chargePhase = 0;
                chargeTimer = 0;
                rb.velocity = Vector2.zero;
            }
            else
            {
                chargeTimer += Time.deltaTime;
            }
        }

        //Lunges if player in range and lunge cooldown done
        else if (PlayerDistance() < chargeRange && chargeCDTimer > chargeCD)
        {
            charge();
        }
        else
        {
            chargeCDTimer += Time.deltaTime;
            attackTimer += Time.deltaTime;
        }
    }

    //Enemy jumps towards players before doing a regular melee attack
    void charge()
    {
        //resets timer for the lunge cooldown
        chargeCDTimer = 0;
        chargePhase = 1;
        chargeTimer = 0;

        //sets where the enemy is aiming to jump to
        target = (Vector2)playerTransform.position;

        var displacement = target - (Vector2)transform.position;
        var direction = displacement.normalized;
        //increases charge displacement so enemy ends up just behind player
        displacement = displacement + direction * 7;
          
        var distance = displacement.magnitude;  //calculates distance of lunge
        //calculates time charge will take, derived from velocity time graph for trapezium
        chargeTime = distance / (chargeSpeed * (0.5f * chargeFirstPhaseTime + chargeSecondPhaseTime + 0.5f * chargeThirdPhaseTime)); //calculates how long half of the lunge should take

        chargeVelocity = direction * chargeSpeed; //max velocity reached at mid point of lunge
    }
}
