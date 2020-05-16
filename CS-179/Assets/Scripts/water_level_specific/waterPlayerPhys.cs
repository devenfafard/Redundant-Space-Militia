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

        if (collider == GameObject.Find("Water4Simple").GetComponent<Collider>())
        {
            if (Input.GetKey("space"))
            {

                GameObject.Find("playerController").GetComponent<Rigidbody>().velocity = (-Physics.gravity * gravMultiplier);
                //GetComponent<Rigidbody>().AddForce(-Physics.gravity * gravMultiplier * .9f);
            }
            else if (Input.GetKey("left ctrl"))
            {
                GameObject.Find("playerController").GetComponent<Rigidbody>().velocity = (Physics.gravity * gravMultiplier * 1.1f);
                //GetComponent<Rigidbody>().AddForce(Physics.gravity * gravMultiplier *.9f);
            }

            GameObject.Find("playerController").GetComponent<Rigidbody>().AddForce(-Physics.gravity * .505f);

        }

    }
}
