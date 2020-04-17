using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lowPassFilter : MonoBehaviour
{



    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {

        if (collider.tag == ("Environment"))
        {
            GetComponent<AudioLowPassFilter>().cutoffFrequency = 250;
            GetComponent<AudioLowPassFilter>().lowpassResonanceQ = 2;
        }
    }

    void OnTriggerExit(Collider collider)
    {

        if (collider.tag == ("Environment"))
        {
            GetComponent<AudioLowPassFilter>().cutoffFrequency = 22000;
            gameObject.GetComponent<AudioLowPassFilter>().lowpassResonanceQ = 1;
        }

        }

}
