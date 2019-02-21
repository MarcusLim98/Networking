using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{

    GatlingGun gunScript;
    //EnemyCtrl enemy;
    int shotDmg;


    void Start()
    {
        
    }

    void Update()
    {
        if (gunScript.isFiring)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        //RaycastHit hit;
        //if (Physics.Raycast(transform.position, transform.TransformDirection(transform.forward), out hit, Mathf.Infinity))
        //{
        //    if (hit.collider != null && hit.collider.GetComponent<EnemyCtrl>())
        //    {
        //        enemy = hit.collider.GetComponent<EnemyCtrl>();
        //        enemy.GetDamaged(shotDmg);
        //    }
        //    else return;
        //}
    }
}
