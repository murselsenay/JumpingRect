using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBG : MonoBehaviour
{

    public float scrollSpeed = 0.3f;
    private MeshRenderer meshRenderer;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();

    }

    void Update()
    {
        Scroll();
    }
    void Scroll()
    {
        Vector2 offset = meshRenderer.sharedMaterial.GetTextureOffset("_MainTex");
        offset.y += Time.deltaTime * scrollSpeed;
        meshRenderer.sharedMaterial.SetTextureOffset("_MainTex",offset);
    }
}
