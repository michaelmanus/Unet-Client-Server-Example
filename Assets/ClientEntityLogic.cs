using UnityEngine;
using System.Collections;

public class ClientEntityLogic : MonoBehaviour
{

    NetworkEntityState _networkState;
    public Vector3 DestinationPos;
    void Start()
    {
        _networkState = this.GetComponentInChildren<NetworkEntityState>();

        var material = this.GetComponent<MeshRenderer>().material;
        if (_networkState.PrefabType == PrefabType.Player)
        {
            material.color = Color.green;
        }
        else
        {//npc

            material.color = Color.red;
        }
    }

    void Update()
    {
        if (_networkState.isLocalPlayer)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.position = transform.position + Vector3.forward;
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.position = transform.position + Vector3.back;
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.position = transform.position + Vector3.left;
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position = transform.position + Vector3.right;
            }
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, DestinationPos, .2f);
        }

    }
}
