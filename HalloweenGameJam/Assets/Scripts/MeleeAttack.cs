using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] public float damage;

    GameObject disappearingMeleeWeapon;

    float angleToSweep;
    float timer;
    float deltaAngle;

    float meleeLength;
    GameObject RotatePoint;

    bool active = false;
    Vector2 PrevPosition;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            disappearingMeleeWeapon.GetComponent<Renderer>().enabled = false;
            transform.position += RotatePoint.transform.position - (Vector3)PrevPosition;
            PrevPosition = RotatePoint.transform.position;

            deltaAngle = -angleToSweep * (Time.deltaTime / meleeLength);
            transform.RotateAround(RotatePoint.transform.position, Vector3.forward, deltaAngle);
            timer += Time.deltaTime;
            if (timer > meleeLength)
            {
                disappearingMeleeWeapon.GetComponent<Renderer>().enabled = true;
                Destroy(gameObject);
            }
        }
    }
    public void MeleeRotation(float meleeLength, GameObject RotatePoint, float angleToSweep, GameObject MeleeWeapon)
    {
        this.meleeLength = meleeLength;
        this.RotatePoint = RotatePoint;
        this.angleToSweep = angleToSweep;
        this.disappearingMeleeWeapon = MeleeWeapon;
        active = true;
        timer = 0;

        PrevPosition = RotatePoint.transform.position;
        transform.RotateAround(RotatePoint.transform.position, Vector3.forward, angleToSweep/2);

    }
}
