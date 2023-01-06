using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupForce : MonoBehaviour
{
    public Rigidbody PullTarget;
    public Vector3 PullDestination;
    public float GrabPosOffset = 3f;

    public float GrabRange = 20f;
    public float InteractRange = 3f;

    [SerializeField] private Transform PlayerCam;
    private RaycastHit hit;

    [SerializeField] private HoverTextManager htman;

    // Start is called before the first frame update
    void Start()
    {
        //
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(PlayerCam.position, PlayerCam.forward, out hit, InteractRange)) {
            Hover(hit.collider.gameObject);
            if(Input.GetButtonDown("Fire2")) {
                Interact(hit.collider.gameObject);
            }         
        }
        if(Input.GetButtonDown("Fire1") && Physics.Raycast(PlayerCam.position, PlayerCam.forward, out hit, GrabRange)) {
            Grab(hit.rigidbody);
        }
        else if(Input.GetButton("Fire1") && PullTarget != null) {
            PullDestination = PlayerCam.position + (PlayerCam.forward * GrabPosOffset);
            PullTarget.velocity = Vector3.Normalize(PullDestination - PullTarget.transform.position) * Vector3.Distance(PullDestination, PullTarget.transform.position) * 10f;
        }
        else {
            LetGo();
        }
    }

    void Grab(Rigidbody grabbedObj) {
        if(grabbedObj == null || grabbedObj.GetComponent<ObjectData>() == null) {
            // what to do if object has no rigidbody (e.g. play "shwump" failed pickup sound)
            return;
        }
        PullTarget = grabbedObj;
        PullTarget.useGravity = false;
        grabbedObj.GetComponent<ObjectData>().Add("isHeld");
        // play "successful" sound
    }

    void LetGo() {
        if(PullTarget == null || PullTarget.GetComponent<ObjectData>() == null) {
            return;
        }
        float throwForce = GrabRange/4f;
        PullTarget.velocity = PlayerCam.forward * throwForce;
        PullTarget.GetComponent<ObjectData>().Remove("isHeld");
        PullTarget.useGravity = true;
        PullTarget = null;
    }

    void Interact(GameObject other) {
        if(other.GetComponent<Interactable>() != null) {
            other.GetComponent<Interactable>().OnInteract();
        }
        else {
            //
        }
    }

    void Hover(GameObject other) {
        if(other.GetComponent<Interactable>() != null) {
            htman.SetText(other.GetComponent<Interactable>().GetDescription());
        }
    }
}