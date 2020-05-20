using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceShip : MonoBehaviour
{
    private GameObject terminal1;
    private GameObject terminal2;
    private GameObject player;
    private GameObject kills;
    private GameObject Space_Ship;
    private int killCount;
    private bool takeOffDone;
    private float time = 0f;
    private float timer = 5f;

    public Canvas LevelComplete;

    [SerializeField]
    private Transform CameraPosition;

    public Camera mainCamera;
    public Camera firstPerson;

    Animator TakeOff;

    private bool terminal1_complete = false;
    private bool terminal2_complete = false;

    private bool player_entered = false;

    public string levelName; public GameObject playerController;

    void Start()
    {
        terminal1 = GameObject.FindGameObjectWithTag("Terminal1");
        terminal2 = GameObject.FindGameObjectWithTag("Terminal2");
        player = GameObject.FindGameObjectWithTag("Player");
        kills = GameObject.FindGameObjectWithTag("Canyon Gate");
        Space_Ship = GameObject.FindGameObjectWithTag("Space Ship");
        TakeOff = Space_Ship.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        terminal1_complete = terminal1.GetComponent<TerminalBase1>().checkTerminalUsed();
        terminal2_complete = terminal2.GetComponent<TerminalBase2>().checkTerminalUsed();
        killCount = kills.GetComponent<OpenGate>().GetAlienKills();

        CheckTakeOff();

    }

    private void OnTriggerEnter(Collider player)
    {
        if(player.gameObject.tag == "Player")
        {
            player_entered = true;
            takeOff(false);

        }
    }

    private void CheckTakeOff()
    {
        if(player_entered && terminal1 && terminal2 && killCount >= 9)

        {
            print("Player Entered");
            mainCamera.transform.position = CameraPosition.position;
            firstPerson.enabled = false;

            takeOff(true);

            if (takeOffDone == false)
            {
                if (time < timer)
                {
                    time += Time.deltaTime;
                    LevelComplete.gameObject.SetActive(false);
                }
                else
                {
                    LevelComplete.gameObject.SetActive(true);
                    time = 0;
                    takeOffDone = true;
                }
            }
            else {

                SceneManager.LoadScene(levelName);

            }
        }
    }

    void takeOff(bool state)
    {
        TakeOff.SetBool("Take Off", state);
    }


}
