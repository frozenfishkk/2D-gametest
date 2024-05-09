using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public enum swordType
{
    regular,
    bounce,
    pierce,
    spin
}
public class throwSwordSkill : skill
{
    public swordType SwordType = swordType.regular;
    [SerializeField] private GameObject swordPrefab;
    [SerializeField]private Vector2 launchSpeed;
    [SerializeField] float swordGravity;
    [SerializeField] public float returningSpeed;
    private Vector2 finalDirection;

    [Header("aim dots info")] 
    [SerializeField]private int dotsNumber;

    [SerializeField] private float spaceBetweenDots;
    [SerializeField] private GameObject dotsPrefab;
    [SerializeField] private Transform dotsParent;
    private GameObject[] dots;
    [Header("bounce info")]
    [SerializeField]private int amountOfBounce;
    private void changeSwordType(GameObject sword)
    {
        throwSwordController swordController = sword.GetComponent<throwSwordController>();
        if (SwordType == swordType.regular)
        {
            
        }
        else if (SwordType == swordType.bounce)
        {   
            
            swordController.setUpBounce(true,amountOfBounce);
        }
        else if (SwordType == swordType.pierce)
        {
            
        }
        else if (SwordType == swordType.spin)
        {
            
        }
    }
    public void createSword()
    {
        GameObject newSword = Instantiate(swordPrefab);
        newSword.transform.position = player.transform.position;
        changeSwordType(newSword);
        newSword.GetComponent<throwSwordController>().throwSword(finalDirection,swordGravity);
        generateDots(false);
        player.assginSword(newSword);
    }

    public void callBackSword()
    {
        player.sword.GetComponent<throwSwordController>().callBackSword(returningSpeed);
    }

    #region aimRegion

    public Vector2 aimDirection()
    {
        Vector2 playerPosition = player.transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition-playerPosition;
        return direction;
    }

    public void generateDots(bool active)
    {
        for (int i = 0; i < dotsNumber; i++)
        {
            dots[i].SetActive(active);
        }
    }
    public void createDots()
    {
        dots = new GameObject[dotsNumber];
        for (int i = 0; i < dotsNumber; i++)
        {
            dots[i] = Instantiate(dotsPrefab, player.transform.position, quaternion.identity, dotsParent);
            dots[i].SetActive(false);
        }
    }
    private Vector2 dotsPosition(float time)
    {
        Vector2 postion = (Vector2)player.transform.position + new Vector2(aimDirection().normalized.x * launchSpeed.x,
                              aimDirection().normalized.y * launchSpeed.y) * time +
                          .5f * (Physics2D.gravity * swordGravity) * (time * time);
        return postion;
    }

    #endregion
    
    protected override void Update()
    {
        base.Update();
  
        if (Input.GetKeyUp(KeyCode.Q))
        {
            finalDirection = new Vector2(aimDirection().normalized.x * launchSpeed.x,
                aimDirection().normalized.y * launchSpeed.y);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            for (int i = 0; i < dotsNumber; i++)
            {
                dots[i].transform.position = dotsPosition(i*spaceBetweenDots);
            }
        }
    }


}
