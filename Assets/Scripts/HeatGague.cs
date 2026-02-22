using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeatGague : MonoBehaviour
{
    //the base heat level
    public float heat = 0f;
    //the amount the heat cools down on its own
    public float coolOff = 0.25f;
    //the amount the heat is increased when you boost
    public float boostHeat = 0f;
    //the amount the heat is decreased when you vent it
    public float ventHeat = 5f;
    //the amount heat needs to reach to stall
    public float stallLvl = 300; 
    //the maximum value the heat gague is capable of reaching
    public float maxHeat = 400;
    [HideInInspector] public bool suspended = false;

    public GameObject steam;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    void FixedUpdate()
    {
        HandleHeat();
       
    }

    // Update is called once per frame
    void HandleHeat()
    {
        if (Keyboard.current == null || suspended) return;

        boostHeat = 0f;
        //if you are boosting, the boost increment value is changed from 0 to 1.5
        //if you are not boosting, heat will passivley decrease by the cool down value
        if (Keyboard.current.shiftKey.isPressed) boostHeat = 1.5f;
        else if (heat > 0) heat = heat - coolOff;

        heat = heat + boostHeat;
        //if you press x, you can vent heat by 5. this will be the actual diagetic input later
        if (Keyboard.current.xKey.isPressed && heat >= 5)
        {
            heat = heat - ventHeat;
            steam.SetActive(true);
        } else
        {
            steam.SetActive(false);
        }


        //this makes sure heat cant go into the negative
        if (heat < 0) heat = 0f;

        //this makes sure heat wont go over maxheat
        if (heat > maxHeat) heat = maxHeat;

        //this makes it so that if the heat level goes over the stall cap, the "stall" bool in tankmovement script is set to true
        if (heat > stallLvl) GetComponent<TankMovement>().stalled = true;
        //this makes is so that once the heat level is lowered by a certain point, it stops stalling.
        //the heat needed to fix a stall should be lower than the actual stall point level to give a margin of punishment by a few seconds
        if (heat < 250f) GetComponent<TankMovement>().stalled = false;

    }

    
}
