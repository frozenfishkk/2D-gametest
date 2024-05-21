using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class entityFx : MonoBehaviour
{
    [Header("flash fx")]
    [SerializeField] private Material hitMat;
    private Material originalMat;
    private SpriteRenderer render;
    [SerializeField] private Color chillColor;
    [SerializeField] private Color[] igniteColor;
    [SerializeField] private Color shockColor;
    

    private void Start()
    {
        render  = GetComponent<SpriteRenderer>();
        originalMat = render.material;

    }

    private IEnumerator flashFX()
    {   
        Color currentColor = render.color;  
        render.color = Color.white;
        render.material = hitMat;
        yield return new WaitForSeconds(.1f);
        render.color = currentColor;
        render.material = originalMat;
    }
    private void colorBlink()
    {
        if(render.color!= Color.white)
        {
            render.color = Color.white;
        }
        else
            render.color = Color.red;
    }
    private void cancelBlink()
    {
        CancelInvoke();
        render.color = Color.white; 
    }

    public void invokeIgnite(float _seconds)
    {
        InvokeRepeating("igniteColorFX",0,1);
        Invoke("cancelBlink",_seconds);;
    }

    public void invokeChill(float _seconds)
    {
        render.color = chillColor;
        Invoke("cancelBlink",_seconds);
    }

    public void invokeShock(float _seconds)
    {
        render.color = shockColor;
        Invoke("cancelBlink",_seconds);
    }
    private void igniteColorFX()
    {
        if(render.color!= igniteColor[0])
        {
            render.color = igniteColor[0];
        }
        else
            render.color = igniteColor[1];
    }


    
}
