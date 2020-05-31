using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstDoor : MonoBehaviour
{
    public GameObject trigger;
    public GameObject topdoor;
    public GameObject bottomdoor;

    private bool complete = false;
    private GameObject console; 

    Animator bottom_door;
    Animator top_door;

    // Start is called before the first frame update
    void Start()
    {
        top_door = topdoor.GetComponent<Animator>();
        bottom_door = bottomdoor.GetComponent<Animator>();
        console = GameObject.FindGameObjectWithTag("Console_1");
    }

    // Update is called once per frame
    void Update()
    {
        complete = console.GetComponent<Console>().get_Console_Status();
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player" && complete)
        {
            print("Open Door");
            OpenDoor(true);
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "Player" && complete)
        {
            OpenDoor(false);
        }
    }

    void OpenDoor(bool state)
    {
        bottom_door.SetBool("open", state);
        top_door.SetBool("open", state);
    }

    public bool GetConsoleStatus()
    {
        return complete;
    }
}
