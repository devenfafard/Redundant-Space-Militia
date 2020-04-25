using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flashlightAttach : MonoBehaviour
{
    public Transform player;
    public Transform playerCam;
    public GameObject temp1;
    public GameObject temp2;
    public float strength = 10f;
    bool canBeGrabbed = false;
    bool beingCarried = false;
    private bool colliding = false;

    private RaycastHit _hit = new RaycastHit();


    // Start is called before the first frame update
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {

        float dist = Vector3.Distance(gameObject.transform.position, player.position);

        if (dist <= 3.5f && Physics.Raycast(playerCam.position, playerCam.forward, out _hit, 500))
            if (_hit.transform.gameObject == gameObject)
                canBeGrabbed = true;

            else
                canBeGrabbed = false;


        if (canBeGrabbed && Input.GetButtonDown("Use"))
        {

            GetComponent<Rigidbody>().isKinematic = true;

            //disable flashlight colliders (naive solution)
            temp1.GetComponent<Collider>().enabled = false;
            temp2.GetComponent<Collider>().enabled = false;
            GetComponent<Collider>().enabled = false;

            //map flashlight to player coordinates and rotation, appropriately
            Vector3 temp = new Vector3(0.157f, -0.136f, 0.281f);
            Vector3 rotation = new Vector3(-43.82f, 263.508f, .776f);
            transform.parent = playerCam;
            transform.localPosition = new Vector3(0.157f,-0.136f,0.281f);
            transform.localRotation = Quaternion.Euler(new Vector3(0, -90f, 8f));
            
        }

    }

}
