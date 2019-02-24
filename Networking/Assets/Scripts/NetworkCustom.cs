using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

// The format of the message we pass around the network, trigger particular function to occur
// Data structure for holding our message that we are going to send around
public class MsgTypes
{
    //message number, MsgType.Highest gets the last ID number of the highest registered message
    public const short PlayerPrefabSelect = MsgType.Highest + 1;

    public class PlayerPrefabMsg : MessageBase
    {
        public short controllerID;  // who it is
        public short prefabIndex;   // which prefab
    }
}
public class NetworkCustom : NetworkManager
{
    public short playerPrefabIndex;

    public int selGridInt = 0;
    public string[] selStrings = new string[] { "Princess", "Brutius", "Jane", "Funkey" };

    void OnGUI()
    {
        if (!isNetworkActive)
        {
            selGridInt = GUI.SelectionGrid(new Rect(Screen.width - 200, 10, 200, 50), selGridInt, selStrings, 2);
            playerPrefabIndex = (short)(selGridInt + 1);
        }
    }

    // This function gets called when the server starts to run
    public override void OnStartServer()
    {
        //RegisterHandler registers a function OnResponsePrefab, which runs when one of the message gets passed through
        NetworkServer.RegisterHandler(MsgTypes.PlayerPrefabSelect, OnResponsePrefab);
        base.OnStartServer();
    }

    // Runs on the server when a client connects
    public override void OnClientConnect(NetworkConnection conn)
    {
        //Wait for the message and run OnRequestPrefab function.
        client.RegisterHandler(MsgTypes.PlayerPrefabSelect, OnRequestPrefab);
        base.OnClientConnect(conn);
    }

    private void OnRequestPrefab(NetworkMessage netMsg)
    {
        MsgTypes.PlayerPrefabMsg msg = new MsgTypes.PlayerPrefabMsg();  //create a new message
        msg.controllerID = netMsg.ReadMessage<MsgTypes.PlayerPrefabMsg>().controllerID;
        msg.prefabIndex = playerPrefabIndex;    //put index of player prefab into the message
        client.Send(MsgTypes.PlayerPrefabSelect, msg);  //Send out message for us
    }

    private void OnResponsePrefab(NetworkMessage netMsg)
    {
        //pick up the message we sent, and read it, event trigger programming. 
        MsgTypes.PlayerPrefabMsg msg = netMsg.ReadMessage<MsgTypes.PlayerPrefabMsg>();
        playerPrefab = spawnPrefabs[msg.prefabIndex];   //setup the player prefab, registered spawnable prefabs 1-4
        base.OnServerAddPlayer(netMsg.conn, msg.controllerID);
    }

    // Runs when you add your player prefab onto the scene, instantiate player
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        MsgTypes.PlayerPrefabMsg msg = new MsgTypes.PlayerPrefabMsg();
        msg.controllerID = playerControllerId;
        NetworkServer.SendToClient(conn.connectionId, MsgTypes.PlayerPrefabSelect, msg); //spawning
    }

    public void SwitchPlayer(SetupLocalPlayer player, int cid)
    {
        Debug.Log("Switching Player");
        GameObject newPlayer = Instantiate(spawnPrefabs[cid],
                            player.gameObject.transform.position,
                            player.gameObject.transform.rotation);
        Destroy(player.gameObject);
        playerPrefab = spawnPrefabs[cid];
        //ReplacePlayerForConnection changes the player character for a particular connected client
        //connectionToClient - which id, which connection wants to change its character
        NetworkServer.ReplacePlayerForConnection(player.connectionToClient, newPlayer, 0);
    }
}
