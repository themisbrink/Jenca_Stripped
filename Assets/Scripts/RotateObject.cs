using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    Vector2 clickPos;   
    Vector2 offsetPos; 
    public int divider = 10;
    public bool focused = false;

    void Start()
    {
        clickPos = new Vector2(0, 0);
        offsetPos = new Vector2(0, 0);
    }

    void Update()
    {
        if(!focused || !GameManager.Instance.canRotate) return;
        offsetPos = new Vector2(0, 0);

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            GameManager.Instance.rotating = true;
            clickPos = mouseXY();
            
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            offsetPos = clickPos - mouseXY();
            transform.localEulerAngles = new Vector3(0, offsetPos.x / divider, 0.0f);
        }

        if(Input.GetKeyUp(KeyCode.Mouse1)) {
            GameManager.Instance.rotating = false;
        }

        // Rotate the GameObject
        // transform.Rotate(new Vector3(0, offsetPos.x / divider, 0.0f), Space.World);
        
    }

    // Debug Code: Prints the current mouse position
    void OnGUI()
    {
        /*GUI.Label(Rect(10,350,200,100), "mouse X = " + Input.mousePosition.x);
        GUI.Label(Rect(10,370,200,100), "mouse Y = " + Input.mousePosition.y);

        GUI.Label(Rect(120,350,200,100), "click X = " + clickPos.x);
        GUI.Label(Rect(120,370,200,100), "click Y = " + clickPos.y);

        GUI.Label(Rect(210,350,200,100), "offset X = " + offsetPos.x);
        GUI.Label(Rect(210,370,200,100), "offset Y = " + offsetPos.y);*/
    }

    // Return true when left mouse is clicked or hold
    // void leftClick()
    // {
    //     return KeyCode.Mouse0;
    // }

    //Immediate location of the mouse
    Vector2 mouseXY()
    {
        return new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    }

    //Immediate location of the mouse's X coordinate
    float mouseX()
    {
        return Input.mousePosition.x;
    }

    //Immediate location of the mouse's Y coordinate
    float mouseY()
    {
        return Input.mousePosition.y;
    }
}
