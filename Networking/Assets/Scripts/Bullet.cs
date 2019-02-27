using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed, time;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Destroy());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.forward * speed * Time.deltaTime;
    }
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.25f);
        gameObject.tag = "Damage";
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
