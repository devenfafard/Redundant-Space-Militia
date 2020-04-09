using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flashlightAttach : MonoBehaviour
{
    public Transform player;
    public Transform playerCam;



    // Start is called before the first frame update
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {


            GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = playerCam;


    }

}
