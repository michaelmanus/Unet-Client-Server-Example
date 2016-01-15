using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class NetworkEntityState : NetworkBehaviour
{

    [SyncVar]
    public PrefabType PrefabType;

    public override void OnStartServer()
    {
        base.OnStartServer();
        var globals = FindObjectOfType<GlobalAssets>();

        var serverEnt = Instantiate<GameObject>(globals.ServerEntityPrototype);

        transform.SetParent(serverEnt.transform);

        serverEnt.transform.SetParent(FindObjectOfType<MyServer>().transform);


        serverEnt.name = PrefabType.ToString();
        InvokeRepeating("UpdateClientPosition", 0, .1f);
    }
    public override void OnStartClient()
    {
        base.OnStartClient();
        var globals = FindObjectOfType<GlobalAssets>();

        var clientEnt = Instantiate<GameObject>(globals.ClientEntityPrefab);

        ConfigureClientPrefab(clientEnt);

    }
    private void ConfigureClientPrefab(GameObject clientEnt)
    {

        SetIgnoreAllServerObjectCollision(clientEnt.transform);

        transform.SetParent(clientEnt.transform);
        clientEnt.transform.SetParent(FindObjectOfType<MyClient>().transform);

        clientEnt.name = PrefabType.ToString();
    }
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        var globals = FindObjectOfType<GlobalAssets>();

        var clientEnt = Instantiate<GameObject>(globals.PlayerClientPrefab);


        //At the time of OnStartClient, we couldn't know that this was a LocalPlayer.
        //We do now so we simply remove the old prefab and put in the new one with the e.g. camera.
        var oldParent = this.transform.parent;
        ConfigureClientPrefab(clientEnt);
        Destroy(oldParent.gameObject);

        InvokeRepeating("UpdatePlayerServerPosition", 0, .1f);
    }


    [Client]
    private void UpdatePlayerServerPosition()
    {
        CmdServerMove(transform.parent.position);
    }
    [Command]
    public void CmdServerMove(Vector3 position)
    {
        this.GetComponentInParent<ServerEntityLogic>().DestinationPos = position;
    }
    [Server]
    private void UpdateClientPosition()
    {
        RpcClientMove(transform.parent.position);
    }

    [ClientRpc]
    private void RpcClientMove(Vector3 position)
    {
        GetComponentInParent<ClientEntityLogic>().DestinationPos = position;
    }

    [Client]
    private void SetIgnoreAllServerObjectCollision(Transform ignoreWith)
    {
        var server = FindObjectOfType<MyServer>();
        if (server)
        {
            foreach (var collider in server.GetComponentsInChildren<Collider>())
            {
                foreach (var selfCollider in ignoreWith.GetComponentsInChildren<Collider>())
                {
                    Physics.IgnoreCollision(collider, selfCollider);
                }
            }
        }
    }

    void OnDestroy()
    {
        Destroy(this.transform.parent.gameObject);
    }

}
