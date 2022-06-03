using System.Collections.Generic;
using UnityEngine;

public class TileAnimation : MonoBehaviour
{
    float _currentCooldown;
    int textureIndex = 0;
    [SerializeField] float TimeBetweenFrames = .5f;

    [SerializeField] List<Texture> TextList = new List<Texture>();
    Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
    }

    public void OnUpdate()
    {
        if (TextList.Count == 0) return;

        _currentCooldown -= Time.deltaTime;

        if (_currentCooldown > 0) return;

        _currentCooldown = TimeBetweenFrames;
        textureIndex++;
        
        if (textureIndex == TextList.Count)
            textureIndex = 0;

        rend.material.SetTexture("_MainTex", TextList[textureIndex]);
    }
}
