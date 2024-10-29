using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField] private float meleeRange;

    [SerializeField] private float chargeSpeed;
    [SerializeField] private float chargeCD;
    [SerializeField] private float chargeRange;

    [SerializeField] private float chargeFirstPhaseTime;    //percent of time spent of accelerating phase
    private float chargeSecondPhaseTime;   //percent of time spent on constant speed phase
    [SerializeField] private float chargeThirdPhaseTime;    //percent of time spent on decceleration phase

    [SerializeField] protected GameObject bullet;
    [SerializeField] private GameObject spawnOne;
    [SerializeField] private GameObject spawnTwo;
    [SerializeField] private GameObject spawnThree;
    [SerializeField] private GameObject spawnFour;
    [SerializeField] private GameObject triShotPrefab;

    private bool firstSpawnDone = false;
    private bool secondSpawnDone = false;
    private bool thirdSpawnDone = false;

    private float chargeTime = 0;
    private float chargeTimer = 0;
    //Phase charge is in, 0 if not charging
    private int chargePhase = 0;
    private float chargeCDTimer = 0;
    private bool isActive = false;

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
        if(isActive)
        {
            if(!firstSpawnDone && health < 40)
            {
                Instantiate(triShotPrefab, spawnOne.transform.position, Quaternion.identity);
                Instantiate(triShotPrefab, spawnTwo.transform.position, Quaternion.identity);
                Instantiate(triShotPrefab, spawnThree.transform.position, Quaternion.identity);
                Instantiate(triShotPrefab, spawnFour.transform.position, Quaternion.identity);
                firstSpawnDone = true;
            }
        }


        if(!isActive)
        {
            if(PlayerDistance() < sightRange)
            {
                isActive = true;
            }
        }
        else if (chargePhase == 1)
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

        else if (chargePhase == 3)
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
                ShootEightWays();
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

    public void ShootEightWays()
    {
        var startingAngle = 0;
        var angleIncrement = 45;
        for(int i = 0; i < 8; i++)
        {
            GameObject instance = Instantiate(bullet);
            instance.GetComponent<BulletEnemy>().Fire(transform.position, playerTransform.position, startingAngle + angleIncrement * i);

        }
                
    }
}
