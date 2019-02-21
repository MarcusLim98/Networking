using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class GatlingGun : MonoBehaviour
{
    // Gameobjects need to control rotation and aiming
    public Transform go_baseRotation;
    public Transform go_GunBody;
    public Transform go_barrel;

    // Gun barrel rotation
    public float barrelRotationSpeed;
    float currentRotationSpeed;

    // Particle system for the muzzel flash
    public ParticleSystem muzzelFlash;

    // Used to start and stop the turret firing
    bool canFire = false, overheated, inSight;

    //VRTK objects
    public VRTK_ControllerEvents leftCtrller, rightCtrller;
    public GameObject headsetObj;

    float gunTimer = 20f, coolDown;

    public bool isFiring;


    void Update()
    {

        if (leftCtrller.triggerPressed && rightCtrller.triggerPressed || Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.X))
        {
            canFire = true;
        }
        else { canFire = false; }

        FireEffects();
    }

    void FireEffects()
    {
        // Gun barrel rotation
        go_barrel.transform.Rotate(0, 0, currentRotationSpeed * Time.deltaTime);

        // if can fire turret activates
        if (canFire && !overheated && inSight)
        {
            // start rotation
            currentRotationSpeed = barrelRotationSpeed;

            gunTimer -= Time.deltaTime;
            if (gunTimer < 0)
            {
                overheated = true;
                coolDown = 0f;
            }

            // start particle system 
            if (!muzzelFlash.isPlaying)
            {
                muzzelFlash.Play();
            }
        }
        else
        {
            // slow down barrel rotation and stop
            currentRotationSpeed = Mathf.Lerp(currentRotationSpeed, 0, 10 * Time.deltaTime);
            
            coolDown += Time.deltaTime;
            if (coolDown > 5f)
            {
                gunTimer = 20f;
                overheated = false;
            }

            // stop the particle system
            if (muzzelFlash.isPlaying)
            {
                muzzelFlash.Stop();
            }
        }
    }

    private void FixedUpdate()
    {
        //aiming
        RaycastHit hit;
        if (Physics.Raycast(headsetObj.transform.position, headsetObj.transform.TransformDirection(transform.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(headsetObj.transform.position, headsetObj.transform.TransformDirection(transform.forward) * hit.distance, Color.red);

            Vector3 targetDir = hit.point - transform.position;
            float angleToPlayer = Vector3.Angle(targetDir, transform.forward);

            if (angleToPlayer >= -75 && angleToPlayer <= 75)    //limited 180 degree FOV
            {

                //turns head to face where player is looking
                Vector3 baseTargetPostition = new Vector3(hit.point.x, this.transform.position.y, hit.point.z);
                Vector3 gunBodyTargetPostition = new Vector3(hit.point.x, hit.point.y, hit.point.z);

                go_baseRotation.transform.LookAt(baseTargetPostition);
                go_GunBody.transform.LookAt(gunBodyTargetPostition);

                inSight = true;
            }
            else { inSight = false; }
        }
    }
}