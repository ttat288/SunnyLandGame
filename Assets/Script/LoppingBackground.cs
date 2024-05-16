using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoppingBackground : MonoBehaviour
{
    public float loopSpeed;
    public Renderer backgroundRenderer;

    private void Update()
    {
        backgroundRenderer.material.mainTextureOffset += new Vector2(loopSpeed * Time.deltaTime, 0f);
    }
}
