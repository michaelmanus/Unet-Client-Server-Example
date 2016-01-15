using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using System;

public class MyServer : MonoBehaviour
{

    void Awake()
    {
        Application.runInBackground = true;
        NetworkServer.RegisterHandler(MsgType.Connect, OnConnect);
        NetworkServer.RegisterHandler(MsgType.AddPlayer, OnAddPlayer);
        NetworkServer.RegisterHandler(MsgType.Disconnect, OnPlayerDisconnect);
        NetworkServer.Listen(7777);
        PopulateServerEntities();

    }

    private void OnPlayerDisconnect(NetworkMessage netMsg)
    {
        var playerGamePiece = netMsg.conn.playerControllers[0].gameObject;

        NetworkServer.UnSpawn(playerGamePiece);

        Destroy(playerGamePiece);
    }

    private void PopulateServerEntities()
    {
        var globals = FindObjectOfType<GlobalAssets>();

        //initialize all server-controlled entities here
        var npc = Instantiate<GameObject>(globals.NetworkEntityStatePrototype);

        npc.GetComponent<NetworkEntityState>().PrefabType = PrefabType.Npc;

        NetworkServer.Spawn(npc);

    }
    private void OnConnect(NetworkMessage netMsg)
    {
        Debug.Log("player connected!");
    }

    private void OnAddPlayer(NetworkMessage netMsg)
    {
        var globals = FindObjectOfType<GlobalAssets>();
        var playerStateGo = Instantiate<GameObject>(globals.NetworkEntityStatePrototype);
        var playerState = playerStateGo.GetComponent<NetworkEntityState>();

        playerState.PrefabType = PrefabType.Player;
        playerState.transform.SetParent(this.transform);

        NetworkServer.AddPlayerForConnection(netMsg.conn, playerStateGo, 0);
    }
}
