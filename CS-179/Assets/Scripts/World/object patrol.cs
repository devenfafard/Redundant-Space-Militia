using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy1behavior : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 5f;

    Animator def_animator;
    void Start()
    {
        def_animator = gameObject.GetComponent<Animator>();

        StartCoroutine(Patrol());
    }
    IEnumerator Patrol()
    {
        while (true)
        {
            for (int w = 0; w < waypoints.Length; w++)
            {
                Transform thisWaypoint = waypoints[w];
                Transform nextWaypoint;
                if (w + 1 < waypoints.Length) nextWaypoint = waypoints[w + 1];
                else nextWaypoint = waypoints[0];

                float moveTime = (nextWaypoint.position - thisWaypoint.position).magnitude;
                for (float t = 0f; t < moveTime; t += Time.deltaTime)
                {
                    transform.position = Vector3.Lerp(thisWaypoint.position, nextWaypoint.position, t/ moveTime * speed
                        );
                    yield return 0;
                }
                transform.position = nextWaypoint.position;
                transform.LookAt(nextWaypoint.position);
                yield return new WaitForSeconds(1f); //pause when you reach the target
            }
        }
    }


    // Update is called once per frame
    void Update()
    {


           



        
    }
}
