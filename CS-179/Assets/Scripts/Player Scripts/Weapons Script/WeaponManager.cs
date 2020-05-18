using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    private WeaponHandler[] weapons;

    private int current_weapon_index;
    public static bool reset = false;


    // Start is called before the first frame update
    void Start()
    {
        current_weapon_index = 0;
        weapons[current_weapon_index].gameObject.SetActive(true);

        globalVars.disarmed = false;
        globalVars.disarmedMiddleMan = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (!globalVars.disarmed)
        {
            if (reset)
            {
                //turnOnSelectedWeapon(1);
                weapons[current_weapon_index].gameObject.SetActive(true);
                reset = false;

            }
              
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                turnOnSelectedWeapon(0);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                turnOnSelectedWeapon(1);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3) )
            {
                turnOnSelectedWeapon(2);
            }

            if (Input.GetKeyDown(KeyCode.Alpha4) )
            {
                turnOnSelectedWeapon(3);
            }

            if (Input.GetKeyDown(KeyCode.Alpha5) )
            {
                turnOnSelectedWeapon(4);
            }

            if (Input.GetKeyDown(KeyCode.Alpha6))
            {

                turnOnSelectedWeapon(5);
            }
        }
        else
        { 
            disarmPlayer();
        }
    }

    void turnOnSelectedWeapon(int weaponIndex)
    {
        if(current_weapon_index == weaponIndex)
        {
            return;
        }

        weapons[current_weapon_index].gameObject.SetActive(false);
        weapons[weaponIndex].gameObject.SetActive(true);
        current_weapon_index = weaponIndex;
    }

    void disarmPlayer()
    {
        weapons[current_weapon_index].gameObject.SetActive(false);
    }
    public WeaponHandler getCurrentSelectedWeapon() //Needed for each individual weapon
    {
        return weapons[current_weapon_index];
    }
}
