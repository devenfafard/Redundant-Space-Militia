using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGate : MonoBehaviour
{
    public GameObject hinge1;

    public GameObject hinge2;

    public GameObject trigger;

    public GameObject player;

    private int alien_Deaths;

    bool complete_Kills;

    public GameObject player;

    private int alien_Deaths;

    bool complete_Kills;

    public GameObject player;

    private int alien_Deaths;

    bool complete_Kills;

    Animator left_door;
    Animator right_door;

    void Start()
    {
        left_door = hinge1.GetComponent<Animator>();
        right_door = hinge2.GetComponent<Animator>();
    }

    void Update()

    {
        CheckKills();
    }


    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player" && complete_Kills)
        {
            OpenDoor(true);
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "Player" && complete_Kills)
        {
            OpenDoor(false);
        }
    }

    void OpenDoor(bool state)
    {
        left_door.SetBool("open", state);
        right_door.SetBool("open", state);
    }

    private void CheckKills()
    {
        if(alien_Deaths == 3)
        {
            complete_Kills = true;
        }

    }

    public void UpdateKills()
    {
        ++alien_Deaths;
        print(alien_Deaths);
    }

}
    