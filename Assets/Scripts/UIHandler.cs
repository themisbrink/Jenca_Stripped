using System;
using System.Collections;
using System.Collections.Generic;
using Doozy.Engine.UI;
using TMPro;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    public static UIHandler Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    public Animation animations;
    public GameObject gameInfo;
    private bool infoOpen = false;
    public GameObject orderPanel;
    
    private bool orderOpen = false;

    public void ClickGameInfo() {
        if(infoOpen){
            animations.Play("CloseInfo");
            infoOpen = false;
        }else{
            // gameInfo.SetActive(true);
            animations.Play("OpenInfo");
            // orderPanel.SetActive(false);
            infoOpen = true;
            orderOpen = false;
        }
            
    }
     public void ClickOrder() {
        if(orderOpen){
            animations.Play("CloseOrder");
            orderOpen = false;
        }else{
            animations.Play("OpenOrder");
            // orderPanel.SetActive(true);
            // gameInfo.SetActive(false);
            infoOpen = false;
            orderOpen = true;
        }
            
    }


    
}
