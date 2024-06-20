using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleOrderController : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public string sortingLayerName = "Foreground";
    public int sortingOrder = 10;

    void Start()
    {
        var renderer = particleSystem.GetComponent<Renderer>();
        renderer.sortingLayerName = sortingLayerName;
        renderer.sortingOrder = sortingOrder;
    }
}
