using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gate_Open_Close : MonoBehaviour
{
    public GameObject button;
    public GameObject cubeObstruction; //very naive and inefficient solution
    public Transform player;
    public Transform playerCam;
    RaycastHit _hit;
    bool canBePressed = true;
    public Transform closeDestination, openDestination;
    public float downSpeed, upSpeed;
    private float tempDownSpeed;
    Coroutine lastRoutine;

    public AudioSource audio;

    private bool _open = true;

    // Start is called before the first frame update
    void Start()
    {
        tempDownSpeed = downSpeed;
    }

    IEnumerator Close()
    {
        //canBePressed = false;

        float moveTime = (closeDestination.position - openDestination.position).magnitude;
        for (float t = 0f; transform.position != closeDestination.position; t += Time.deltaTime)
        {


            transform.position = Vector3.Lerp(transform.position, closeDestination.position, t / moveTime * downSpeed);
            yield return 0;
        }
        transform.position = closeDestination.position;

        yield return null;
    }

    IEnumerator Open()
    {

        downSpeed = tempDownSpeed;

        float moveTime = (openDestination.position - closeDestination.position).magnitude;
        for (float t = 0f; transform.position != openDestination.position; t += Time.deltaTime)
        {
           
            transform.position = Vector3.Lerp(transform.position, openDestination.position, t / moveTime * upSpeed);
            yield return 0;
        }
        transform.position = openDestination.position;

        yield return null;
    }

    // Update is called once per frame
    void Update()
    {

        float dist = Vector3.Distance(button.transform.position, playerCam.position);


        if (dist <= 3f && Physics.Raycast(playerCam.position, playerCam.forward, out _hit, 10))
            if (_hit.transform.gameObject == button.gameObject && Input.GetButtonDown("Use") && _open)
            {
                _open = false;
                StopCoroutine("Open");
                lastRoutine = StartCoroutine("Close");

            }
            else if (_hit.transform.gameObject == button.gameObject && Input.GetButtonDown("Use") && !_open)
            {
                _open = true;
                StopCoroutine("Close");
                lastRoutine = StartCoroutine("Open");

            }




    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject == cubeObstruction)
            StopCoroutine("Close");

    }


}
