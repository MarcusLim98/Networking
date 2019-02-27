using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SetupLocalPlayer : NetworkBehaviour
{

    public Text namePrefab, nameLabel;
    public Transform namePos;
    string textBoxName = "";

    PlayerName nameScript;

    [SyncVar(hook = "OnChangeName")]
    public string pName = "player";

    void OnChangeName(string n)
    {
        pName = n;
        nameLabel.text = pName;
    }

    [Command]
    public void CmdChangeName(string newName)
    {
        pName = newName;
        nameLabel.text = pName;
    }

    void Start()
    {
        nameScript = GameObject.Find("NetworkManager").GetComponent<PlayerName>();

        GameObject canvas = GameObject.FindGameObjectWithTag("MainCanvas");
        nameLabel = Instantiate(namePrefab, Vector3.zero, Quaternion.identity) as Text;
        nameLabel.transform.SetParent(canvas.transform);

        if (isLocalPlayer)
        {
            GetComponent<PlayerLogic>().enabled = true;

            CmdChangeName(nameScript.savedName);
        }
        else
        {
            GetComponent<PlayerLogic>().enabled = false;
        }
    }

    void Update()
    {
        Vector3 nameLabelPos = Camera.main.WorldToScreenPoint(namePos.position);
        nameLabel.transform.position = nameLabelPos;
    }
}
