using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MenuNetworking : NetworkManager
{
   public void StartUpHost()
    {
        SetPort();
        NetworkManager.singleton.StartHost();
    }

    public void JoinGame()
    {
        SetIPAddress();
        SetPort();
        NetworkManager.singleton.StartClient();
    }

    void SetIPAddress()
    {
        string ipAddress = GameObject.Find("InputFieldIPAddress").transform.Find("Text").GetComponent<Text>().text;
        NetworkManager.singleton.networkAddress = ipAddress;
    }

    void SetPort()
    {
        NetworkManager.singleton.networkPort = 7777;
    }

    void OnLevelWasLoaded(int level)
    {
        if (level == 0)
        {
            SetUpMenuButtons();
        }
        else
        {
            SetUpGameButtons();
        }
    }

    void SetUpMenuButtons()
    {
        GameObject.Find("Host").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Host").GetComponent<Button>().onClick.AddListener(StartUpHost);
        GameObject.Find("Join").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Join").GetComponent<Button>().onClick.AddListener(JoinGame);
    }

    void SetUpGameButtons()
    {
        GameObject.Find("DisconnectBtn").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("DisconnectBtn").GetComponent<Button>().onClick.AddListener(NetworkManager.singleton.StopHost);
    }

}
