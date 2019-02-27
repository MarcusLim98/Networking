using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDestroy : MonoBehaviour
{
    GameObject ps;
     void Destroy1()
    {
        ps = GameObject.Find("Attack1(Clone)");
        Destroy(ps);
    }
}
