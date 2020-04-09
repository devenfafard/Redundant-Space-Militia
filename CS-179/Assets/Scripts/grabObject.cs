using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabObject : MonoBehaviour
{
    public Transform player;
    public Transform playerCam;
    public float strength = 10f;
    bool canBeGrabbed = false;
    bool beingCarried = false;
    private bool colliding = false;


    // Start is called before the first frame update
    void Start()
    {


        
    }

    // Update is called once per frame
    void Update()
    {

        float dist = Vector3.Distance(gameObject.transform.position, player.position);

        if (dist <= 2.5f)
            canBeGrabbed = true;

        else
            canBeGrabbed = false;


        if (canBeGrabbed && Input.GetButtonDown("Use"))
        {

            GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = playerCam;
            beingCarried = true;

        }

        if (beingCarried)
        {

            if (Input.GetMouseButtonDown(0))
            {

                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
                GetComponent<Rigidbody>().AddForce(playerCam.forward * strength);

            }
            else if (Input.GetMouseButtonDown(1))
            {

                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;

            }


        }
    }

}
