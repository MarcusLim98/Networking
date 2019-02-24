using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player1 : NetworkBehaviour
{
    public GameObject playerObj, stayHere;
    NetworkConnection conn;
    // Start is called before the first frame update
    void Start()
    {
        transform.parent = GameObject.Find("M_cockpit").transform;
        stayHere = GameObject.Find("StayHere");
        transform.position = new Vector3(GameObject.Find("StayHere").transform.position.x, 
            GameObject.Find("StayHere").transform.position.y, GameObject.Find("StayHere").transform.position.z);
        //transform.parent = null;
        stayHere.transform.parent = null;
        stayHere.transform.parent = this.transform;
        gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        gameObject.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        //playerObj = GameObject.Find("[VRTK_Scripts]");
        //foreach (GameObject obj in playerObj)
        //{
        //obj.SetActive(true);
        //}   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
