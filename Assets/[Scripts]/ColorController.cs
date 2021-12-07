using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using System.Linq;

public class ColorController : MonoBehaviour
{
    public Color platformColor;

    private List<SpriteRenderer> tileSpritesRen;
    // Start is called before the first frame update
    void Start()
    {

        tileSpritesRen = GetComponentsInChildren<SpriteRenderer>().ToList();
        SetColor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetColor()
    {
        foreach (var renderer in tileSpritesRen)
        {
            renderer.material.SetColor("_Color", platformColor);
        }
    }
}
