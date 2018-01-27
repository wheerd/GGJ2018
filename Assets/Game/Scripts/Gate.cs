using ModestTree;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public PackageColor Color;

    private void UpdateColor()
    {
        GetComponentsInChildren<MeshRenderer>().ForEach(r => r.material.color = Color.ToColor());
        GetComponentInChildren<GateCollider>().SetColor(Color);
    }

    private void Start()
    {
        UpdateColor();
    }

    void OnValidate()
    {
        UpdateColor();
    }
}