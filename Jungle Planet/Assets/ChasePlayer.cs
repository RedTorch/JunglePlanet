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

    private bool isDashMode = true;
    private bool isDashingPlayer = false;
    private float toggleDashTime = 0f;
    private float durationOfDash = 1f;
    private float dashForce = 16f;
    private float durationOfIdle = 4f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // transform.Translate((player.transform.position - transform.position).normalized * force * Time.deltaTime, Space.World);
        if(isDashMode) {
            if(Time.time >= toggleDashTime) {
                isDashingPlayer = !isDashingPlayer;
                if(isDashingPlayer) {
                    targetPosition = player.transform.position;
                    rb.AddForce(((targetPosition - transform.position).normalized * 32f) - rb.velocity, ForceMode.Impulse);
                    toggleDashTime = Time.time + durationOfDash;
                }
                else {
                    toggleDashTime = Time.time + durationOfIdle;
                }
            }

            if(isDashingPlayer) {
                // rb.AddForce(((targetPosition - transform.position).normalized * dashForce) - rb.velocity);
            }
            // otherwise... just hover menacingly?
        }
        else {
            if(updatePlayerPosition) {
                if(transform.position.y < 4f) {
                    targetPosition = transform.position + new Vector3(0f,10f,0f);
                }
                else {
                    targetPosition = player.transform.position;
                }
            }
            rb.AddForce(((targetPosition - transform.position).normalized * force) - rb.velocity);
        }
    }
}
