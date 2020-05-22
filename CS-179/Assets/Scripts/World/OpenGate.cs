using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGate : MonoBehaviour
{

    private bool complete = false;
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
            complete = true;
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

    public bool GetSecondCheckpoint()
    {
        return complete;
    }

    //wait are we really having the gate count the kills?
    public int GetAlienKills()
    {
        //TODO implement way to count and store enemy deaths
        //placeholder kill count return 10
        return 10;
    }

}
