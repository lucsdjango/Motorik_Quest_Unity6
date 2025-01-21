using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invoice : MonoBehaviour
{
    public Material highlight;
    private Material origMaterial;
    private Renderer rend;
    private float startTime = 0f;
    public Color origColor;

    private bool isHighlighted;
    void Start()
    {
        isHighlighted = false;
        rend = GetComponent<Renderer>();
        origMaterial = rend.material;
        
    }

    void Update()
    {
        if (isHighlighted) {
            rend.material.SetColor("_TintColor", Color.Lerp(Color.white, origColor, Mathf.PingPong(Time.time - startTime, 1f)));
        }
        
    }
    public void SetHighlighted() {
        isHighlighted = true;
        startTime = Time.time;
        rend.material = highlight;
        origColor = rend.material.GetColor("_TintColor");
    }

    public void SetNormal() {
        rend.material.SetColor("_TintColor", origColor);
        rend.material = origMaterial;
        isHighlighted = false;
        
    }
}
