using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private WeaponManager weapon_manager;
    public float fire_rate = 15f;
    private float next_time_to_fire;
    public float damage = 20f;
    private Animator zoomCameraAnimator;
    private bool zoomed;
    private Camera main_camera;
    private GameObject crosshair;
    private bool is_aiming;

    [SerializeField]
    private GameObject arrow_prefab;

    [SerializeField]
    private GameObject spear_prefab;

    [SerializeField]
    private Transform arrow_spear_start_position;

    void Awake()
    {
        weapon_manager = GetComponent<WeaponManager>();
        zoomCameraAnimator = transform.Find(Tags.LOOK_VIEW).transform.Find(Tags.ZOOM_CAMERA).GetComponent<Animator>();
        crosshair = GameObject.FindWithTag(Tags.CROSSHAIR);
        main_camera = Camera.main;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        weaponShoot();
        zoomInAndOut();
    }

    void weaponShoot()
    {
        if(weapon_manager.getCurrentSelectedWeapon().fire_type == WeaponFireType.MULTIPLE)
        {   
            //Assult Rifle
            if(Input.GetMouseButton(0) && Time.time > next_time_to_fire) //Time.time is how many seconds has passed since the beginning of the game
            {
                next_time_to_fire = Time.time + 1f / fire_rate;
                weapon_manager.getCurrentSelectedWeapon().ShootAnimation();
            }

        }

        else
        {
            if (Input.GetMouseButtonDown(0))
            {   
                //Axe
                if(weapon_manager.getCurrentSelectedWeapon().tag == Tags.AXE_TAG)
                {
                    weapon_manager.getCurrentSelectedWeapon().ShootAnimation();
                }

                //Revolver or Shotgun
                if(weapon_manager.getCurrentSelectedWeapon().bullet_type == WeaponBulletType.BULLET)
                {
                    weapon_manager.getCurrentSelectedWeapon().ShootAnimation();
                    BulletFired();
                }

                //Arrow or Spear
                else
                {
                    if (is_aiming)
                    {
                        weapon_manager.getCurrentSelectedWeapon().ShootAnimation();

                        if(weapon_manager.getCurrentSelectedWeapon().bullet_type == WeaponBulletType.ARROW)
                        {
                            ThrowArrowOrSpear(true);
                        }

                        else if(weapon_manager.getCurrentSelectedWeapon().bullet_type == WeaponBulletType.SPEAR)
                        {
                            ThrowArrowOrSpear(false);
                        }
                    }

                }

            }
            
        }
    }

    void zoomInAndOut()
    {
        //We are going to aim with our camera on the weapon
        if(weapon_manager.getCurrentSelectedWeapon().weapon_aim == WeaponAim.AIM)
        {
            if (Input.GetMouseButtonDown(1))
            {
                zoomCameraAnimator.Play(AnimationTags.ZOOM_IN_ANIMATION);
                crosshair.SetActive(false);
            }
            //release right mouse button
            if (Input.GetMouseButtonUp(1))
            {
                zoomCameraAnimator.Play(AnimationTags.ZOOM_OUT_ANIMATION);
                crosshair.SetActive(true);
            }

        }

        if(weapon_manager.getCurrentSelectedWeapon().weapon_aim == WeaponAim.SELF_AIM)
        {
            if (Input.GetMouseButtonDown(1))
            {
                weapon_manager.getCurrentSelectedWeapon().Aim(true);
                is_aiming = true;
            }

            if (Input.GetMouseButtonUp(1))
            {
                weapon_manager.getCurrentSelectedWeapon().Aim(false);
                is_aiming = false;
            }
        }
    }

    void ThrowArrowOrSpear(bool throwArrow)
    {
        if (throwArrow)
        {
            GameObject arrow = Instantiate(arrow_prefab);
            arrow.transform.position = arrow_spear_start_position.position;

            arrow.GetComponent<BowAndArrow>().Launch(main_camera);

        }

        else
        {
            GameObject spear = Instantiate(spear_prefab);
            spear.transform.position = arrow_spear_start_position.position;

            spear.GetComponent<BowAndArrow>().Launch(main_camera);
        }
    }


    void BulletFired()
    {
        RaycastHit hit;

        if (Physics.Raycast(main_camera.transform.position, main_camera.transform.forward, out hit))
        {

            if (hit.transform.tag == Tags.ENEMY_TAG)
            {
                hit.transform.GetComponent<HealthScript>().ApplyDamage(damage);
            }

        }

    } // bullet fired

}
