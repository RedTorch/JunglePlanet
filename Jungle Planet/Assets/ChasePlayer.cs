using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    private GameObject player;
    private Rigidbody rb;
    [SerializeField] private float force = 3f;
    private bool updatePlayerPosition = true;
    private Vector3 targetPosition;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate((player.transform.position - transform.position).normalized * force * Time.deltaTime, Space.World);
    }
}
