using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalBase2 : MonoBehaviour
{
    private GameObject terminal2;

    private bool checkPower;
    private bool checkIfTerminalUsed = false;
    private bool activDisplayDone = false;
    private bool displayDirections = false;

    private float timer = 7f;
    private float time = 0f;

    public Canvas terminalDirections;
    public Canvas terminalActivated;

    // Start is called before the first frame update
    void Start()
    {
        terminal2 = GameObject.FindGameObjectWithTag("PowerStation2");
    }

    // Update is called once per frame
    void Update()
    {
        checkPower = terminal2.GetComponent<PowerBase2>().checkPowerCell2Status();
        TerminalActive();
        DisplayActive();

        if (Input.GetKeyDown(KeyCode.E) && displayDirections)
        {
            checkIfTerminalUsed = true;
            displayDirections = false;

        }

    }

    private void OnTriggerEnter(Collider Player)
    {
        if (Player.gameObject.tag == "Player" && checkPower && !checkIfTerminalUsed)
        {
            displayDirections = true;
        }

        
    }

    private void TerminalActive()
    {
        if (activDisplayDone == false && checkIfTerminalUsed)
        {
            if (time < timer)
            {
                terminalActivated.gameObject.SetActive(true);
                time += Time.deltaTime;
            }
            else
            {
                terminalActivated.gameObject.SetActive(false);
                time = 0;
                activDisplayDone = true;
            }
        }
    }

    public bool checkTerminalUsed()
    {
        return checkIfTerminalUsed;
    }

    private void DisplayActive()
    {
        if (displayDirections)
        {
            terminalDirections.gameObject.SetActive(true);
        }
        else
        {
            terminalDirections.gameObject.SetActive(false);
        }
    }

}
