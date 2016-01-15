using UnityEngine;
using System.Collections;

public class ServerEntityLogic : MonoBehaviour
{
    NetworkEntityState _networkState;

    private Vector3 _startPos;

    public Vector3 DestinationPos;

    void Start()
    {
        _networkState = this.GetComponentInChildren<NetworkEntityState>();
        _startPos = _networkState.transform.position;

        if (_networkState.PrefabType == PrefabType.Npc)
        {
            InvokeRepeating("chooseDestination", 0, 2);
        }
    }

    void chooseDestination()
    {
        var random2D = (20 * Random.insideUnitCircle);
        DestinationPos = _startPos + new Vector3(random2D.x, transform.position.y, random2D.y);
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, DestinationPos, .1f);
    }
}

