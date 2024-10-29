using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player instance = null;
    public static Player Instance { get => instance; }

    public TriggeredAction trigger;

    private void Start()
    {
        instance = this;
    }
}
