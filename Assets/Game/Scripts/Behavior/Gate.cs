using ModestTree;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public PackageColor Color;

    private Material _material;

    private void UpdateColor()
    {
        if (_material == null)
        {
            _material = new Material(GetComponentInChildren<Renderer>().sharedMaterial);
            GetComponentsInChildren<Renderer>().ForEach(r => r.sharedMaterial = _material);
        }

        _material.color = Color.ToColor();
        GetComponentInChildren<GateCollider>().SetColor(Color);
    }

    private void Start()
    {
        UpdateColor();
    }

    void OnValidate()
    {
        #if UNITY_EDITOR
        if (UnityEditor.PrefabUtility.GetPrefabParent(gameObject) != null)
        {
            UpdateColor();
        }
        #endif
    }
}