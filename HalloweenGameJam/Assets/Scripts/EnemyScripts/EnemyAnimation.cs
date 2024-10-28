using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator anim;
    private Transform player;

    // boolean animation variable
    public string animState;
    public float rotationZ;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        anim.SetBool("up", false);
        anim.SetBool("down", false);
        anim.SetBool("left", false);
        anim.SetBool("right", false);

        Vector3 direction = player.position - transform.position;
        rotationZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (rotationZ < 0)
        {
            rotationZ += 360;
        }

        if (rotationZ >= 45 && rotationZ < 135)
        {
            animState = "up";
        }
        else if (rotationZ >= 135 && rotationZ < 225)
        {
            animState = "left";
        }
        else if (rotationZ >= 225 && rotationZ < 315)
        {
            animState = "down";
        }
        else
        {
            animState = "right";
        }

        anim.SetBool(animState, true);
    }
}
