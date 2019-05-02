using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Girl_Script : MonoBehaviour
{
    public Rigidbody girl;
    // Start is called before the first frame update
    void Start()
    {
        girl = GetComponent<Rigidbody>();
    }
}
