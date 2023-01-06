using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoomCC : MonoBehaviour
{
    private Vector3 TargetVelocity = new Vector3(0f,0f,0f);
    private Vector3 CurrVelocity = new Vector3(0f,0f,0f);
    private float Acceleration = 9f; // in (meters/second)/second
    private float MoveSpeed = 9f;
    private float LookSpeed = 3f;
    private Vector2 CurrLookRotation = new Vector2(0f,0f);
    private Rigidbody rb;
    [SerializeField] private GameObject camroot;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        CurrLookRotation.x += Input.GetAxis("Mouse X") * LookSpeed;
        CurrLookRotation.y = Mathf.Clamp(CurrLookRotation.y + (Input.GetAxis("Mouse Y") * LookSpeed),-80f,80f);
        transform.localRotation = Quaternion.Euler(0f,CurrLookRotation.x,0f);
        camroot.transform.localRotation = Quaternion.Euler(-1f * CurrLookRotation.y, 0f, 0f);

        CurrVelocity = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")) * MoveSpeed;
        rb.velocity = (transform.right * CurrVelocity.x) + (transform.forward * CurrVelocity.z);
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if(hasFocus) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else {
            // pause game, etc..
        }
    }
}
