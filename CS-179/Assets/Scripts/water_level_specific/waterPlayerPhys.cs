using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterPlayerPhys : MonoBehaviour
{

    float gravMultiplier = .2f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay(Collider collider)
    {

        if (collider.tag == "Environment")
        {
            if (Input.GetKey("space"))
            {

                GetComponent<CharacterController>().Move(-Physics.gravity * .0135f);
                //GetComponent<RigidBody>().velocity = (-Physics.gravity * gravMultiplier);
                //GetComponent<Rigidbody>().AddForce(-Physics.gravity * gravMultiplier * .9f);
            }
            else if (Input.GetKey("left ctrl"))
            {
                GetComponent<CharacterController>().Move(Physics.gravity * .01675f);
                //GetComponent<Rigidbody>().velocity = (Physics.gravity * gravMultiplier *1.1f);
                //GetComponent<Rigidbody>().AddForce(Physics.gravity * gravMultiplier *.9f);
            }

            GetComponent<CharacterController>().Move(-Physics.gravity * .0205f);
            //GetComponent<Rigidbody>().AddForce(-Physics.gravity * .505f);

        }

    }
}
