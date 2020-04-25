using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadLevel : MonoBehaviour
{

    public string levelName;
    public Transform playerCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {

            if(playerCamera.transform.childCount > 1)
            {
                for (int i = 1; i < playerCamera.transform.childCount; i++)
                    if(playerCamera.gameObject.GetComponent<Transform>().GetChild(i).gameObject.tag != "Equipment")
                    Destroy(playerCamera.gameObject.GetComponent<Transform>().GetChild(i).gameObject);
            }

            SceneManager.LoadScene(levelName);


        }

    }

}
