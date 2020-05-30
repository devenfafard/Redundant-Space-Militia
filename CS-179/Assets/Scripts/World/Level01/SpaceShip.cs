using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceShip : Subject
{
    [SerializeField]
    private Transform CameraPosition;
    public Camera mainCamera;

    Animator TakeOff;

    private bool terminal1_complete = false;
    private bool terminal2_complete = false;
    private bool player_entered = false;

    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        TakeOff = this.GetComponent<Animator>();
    }

    public void OnNotify(NotificationType type)
    {
        switch(type)
        {
            case NotificationType.TERMINAL_1_DONE:
                terminal1_complete = true;
                break;

            case NotificationType.TERMINAL_2_DONE:
                terminal2_complete = true;
                break;
        }
    }

    private void OnTriggerEnter(Collider player)
    {
        if(player.gameObject.tag == "Player")
        {
            player_entered = true;
            takeOff(false);
            CheckTakeOff();
        }
    }

    private void CheckTakeOff()
    {
        if(player_entered == true && terminal1_complete == true && terminal2_complete == true)
        {
            print("Player Entered");
            mainCamera.transform.position = CameraPosition.position;

            takeOff(true);

            Notify(NotificationType.LEVEL1_COMPLETE);
        }
    }

    void takeOff(bool state)
    {
        TakeOff.SetBool("Take Off", state);
    }
}