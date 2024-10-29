using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PikeRotator : MonoBehaviour
{
    private Transform player;
    [SerializeField] float rotationZ;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.position - transform.position;
        rotationZ = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, rotationZ);
    }
}
