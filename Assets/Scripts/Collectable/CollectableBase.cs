using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBase : MonoBehaviour
{
    [SerializeField] private Player player;

    private void Start()
    {
        if (!player)
            player = FindObjectOfType<Player>();
    }
}
