using UnityEngine;
using System.Collections;

public enum PrefabType
{
    Player,
    Npc,
    SomeOtherNpc
}
public class GlobalAssets : MonoBehaviour
{
    public GameObject NetworkEntityStatePrototype;

    public GameObject ServerEntityPrototype;

    public GameObject ClientEntityPrefab;

    public GameObject PlayerClientPrefab;
}
