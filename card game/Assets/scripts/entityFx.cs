using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class entityFx : MonoBehaviour
{
    [Header("flash fx")]
    [SerializeField] private Material hitMat;
    private Material originalMat;
    private SpriteRenderer render;

    private void Start()
    {
        render  = GetComponent<SpriteRenderer>();
        originalMat = render.material;

    }

    private IEnumerator flashFX()
    {
        render.material = hitMat;
        yield return new WaitForSeconds(.1f);
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
}
