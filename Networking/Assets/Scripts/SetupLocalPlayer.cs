using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SetupLocalPlayer : NetworkBehaviour {
    NetworkConnection conn;
    public override void OnStartServer()
	{
		Debug.Log("Starting Server"  + isLocalPlayer);
	}

	public override void OnStartClient()
	{
		Debug.Log("Starting Client"  + isLocalPlayer);
	}

	void Awake()
	{
        Debug.Log("Awaking Player " + isLocalPlayer);
	}

	// Use this for initialization
	void Start () 
	{
		if(isLocalPlayer)   
		{
			GetComponent<PlayerController>().enabled = true;
            gameObject.transform.GetComponentInChildren<Camera>().enabled = true;
		}
		else
		{
			GetComponent<PlayerController>().enabled = false;
            gameObject.transform.GetComponentInChildren<Camera>().enabled = false;
        }
		Debug.Log("Starting Player " + isLocalPlayer);
        if (NetworkServer.connections.Count > 0)
        {
            Debug.Log("This is the host.");
            GameObject.Find("SpawnPoint").SetActive(false);
        }
        else
        {
            Debug.Log("This is a client.");
        }
    }
}
