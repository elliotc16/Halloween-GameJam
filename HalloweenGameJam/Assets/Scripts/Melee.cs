using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    [SerializeField] private GameObject MeleeAttack;
    [SerializeField] private float meleeLength;
    [SerializeField] private float angleToSweep;
    [SerializeField] private float meleeRange;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeMeleeAttack(Vector2 direction)
    {
        Vector2 offset = meleeRange * direction.normalized;
        Vector2 startingPos = offset + (Vector2)transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        var instance = Instantiate(MeleeAttack, startingPos, Quaternion.Euler(0f, 0f, angle - 90), gameObject.transform);
        instance.GetComponent<MeleeAttack>().MeleeRotation(meleeLength, gameObject, angleToSweep);

    }
}
