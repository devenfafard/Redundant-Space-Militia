using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGate : MonoBehaviour
{
   public GameObject hinge1;

    public GameObject hinge2;

   public GameObject trigger;

    Animator left_door;
    Animator right_door;

    void Start()
    {
        left_door = hinge1.GetComponent<Animator>();
        right_door = hinge2.GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            OpenDoor(true);
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            OpenDoor(false);
        }
    }

    void OpenDoor(bool state)
    {
        left_door.SetBool("open", state);
        right_door.SetBool("open", state);
    }

}
