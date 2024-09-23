using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mergables : MonoBehaviour
{
    public Mergable[] mergables;

    private void Start()
    {
        mergables = GameObject.FindObjectsOfType<Mergable>();
    }
}
