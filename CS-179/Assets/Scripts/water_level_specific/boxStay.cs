using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxStay : MonoBehaviour
{
    GameObject player; 
    GameObject playerCam;
    public float strength = 10f;
    bool canBeGrabbed = false;
    bool beingCarried = false;
    private bool colliding = false;

    public GameObject randomThing;

    private RaycastHit _hit = new RaycastHit();


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("playerController");
        playerCam = GameObject.Find("playerCamera");

    }

    // Update is called once per frame
    void Update()
    {

        if (!colliding)
        {

            float dist = Vector3.Distance(gameObject.transform.position, playerCam.transform.position);

            if (dist <= 3f && Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out _hit, 10))
                if (_hit.transform.gameObject == gameObject)
                    canBeGrabbed = true;

                else
                    canBeGrabbed = false;


            if (canBeGrabbed && Input.GetButtonDown("Use"))
            {

                GetComponent<Rigidbody>().isKinematic = true;
               // GetComponent<Rigidbody>().useGravity = false;
                transform.parent = playerCam.transform;
                beingCarried = true;
                canBeGrabbed = false;

                

            }

            if (beingCarried)
            {

                if (Input.GetMouseButtonDown(0))
                {

                    GetComponent<Rigidbody>().isKinematic = false;
                 //   GetComponent<Rigidbody>().useGravity = true;
                    transform.parent = null;
                    beingCarried = false;
                    GetComponent<Rigidbody>().AddForce(playerCam.transform.forward * strength);

                    

                }
                else if (Input.GetMouseButtonDown(1))
                {

                    GetComponent<Rigidbody>().isKinematic = false;
                   // GetComponent<Rigidbody>().useGravity = true;
                    transform.parent = null;
                    beingCarried = false;

                   

                }


            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject == randomThing)
        {
            
            GetComponent<Rigidbody>().isKinematic = true;
            
        }

        if (collision.gameObject.tag == "Player" && beingCarried)
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());


    }

}
