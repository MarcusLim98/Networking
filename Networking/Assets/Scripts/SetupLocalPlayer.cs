using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetupLocalPlayer : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (isLocalPlayer)
        {
            GetComponent<PlayerLogic>().enabled = true;
        }
        else
        {
            GetComponent<PlayerLogic>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
