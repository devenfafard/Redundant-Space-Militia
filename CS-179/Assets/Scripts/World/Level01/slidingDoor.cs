using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slidingDoor : MonoBehaviour
{
    public GameObject trigger;

    public GameObject player;

    public GameObject leftDoor;
    public GameObject rightDoor;

    Animator leftAnim;
    Animator rightAnim;

    // Start is called before the first frame update
    void Start()
    {
        leftAnim = leftDoor.GetComponent<Animator>();
        rightAnim = rightDoor.GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player" )
        {
            OpenDoor(true);
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "Player" )
        {
            OpenDoor(false);
        }
    }

    void OpenDoor(bool state)
    {
        leftAnim.SetBool("open", state);
        rightAnim.SetBool("open", state);
    }

}
