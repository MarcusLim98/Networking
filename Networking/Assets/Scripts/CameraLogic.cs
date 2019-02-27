using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    public GameObject target;
    public float movementSpeed;
    public Vector3 targetOffset;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position + targetOffset, movementSpeed * Time.deltaTime);
    }
}
