using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonpress : MonoBehaviour
{
    public GameObject button;
    public Transform player;
    public Transform playerCam;
    RaycastHit _hit;
    bool canBePressed = true;
    public Transform destination;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    IEnumerator Close()
    {
        canBePressed = false;

        float moveTime = (destination.position - transform.position).magnitude;
        for (float t = 0f; t < moveTime; t += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(transform.position, destination.position, t / moveTime * speed);
            yield return 0;
        }
        transform.position = destination.position;

        yield return null;
    }

    // Update is called once per frame
    void Update()
    {

        float dist = Vector3.Distance(button.transform.position, playerCam.position);


        if (dist <= 3f && Physics.Raycast(playerCam.position, playerCam.forward, out _hit, 10))
            if (_hit.transform.gameObject == button.gameObject && canBePressed && Input.GetButtonDown("Use"))
                StartCoroutine("Close");






    }


}