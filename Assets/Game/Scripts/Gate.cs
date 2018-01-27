using System.Collections;
using System.Collections.Generic;
using ModestTree;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public PackageColor Color;

    // Use this for initialization
    void Start()
    {
        GetComponentsInChildren<MeshRenderer>().ForEach(r => r.material.color = Color.ToColor());

        GetComponentInChildren<GateCollider>().SetColor(Color);

    }

    // Update is called once per frame
    void Update()
    {
    }
}
