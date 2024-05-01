using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PointerEventController : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI Buttontext;
    public TextMeshProUGUI alternatetext;
    public Sprite origimage;
    public Sprite altimage;

    private static Vector3 scaleChange = new Vector3(0.2f,0.2f,0.2f);

    public int buttonType;
    //1: Button with changing text on hover
    //2: Button with changing size and sprite on hover
    //3: Button with changing size on hover

    void Awake(){
        switch (buttonType){
            case 1:
                Buttontext.enabled=true;
                alternatetext.enabled=false;
                break;
            case 2:
                gameObject.GetComponent<RectTransform>().localScale = new Vector3(1f,1f,1f);
                gameObject.GetComponent<UnityEngine.UI.Image>().sprite=origimage;
                break;
            case 3:
                gameObject.GetComponent<RectTransform>().localScale = new Vector3(1f,1f,1f);
                break;
        }
    }

    //Determines button change with pointer enters 
    public void OnPointerEnter(PointerEventData data)
    {
        switch (buttonType){
            case 1:
                Buttontext.enabled=false;
                alternatetext.enabled=true;
                break;
            case 2:
                gameObject.GetComponent<RectTransform>().localScale+=scaleChange;
                gameObject.GetComponent<UnityEngine.UI.Image>().sprite=altimage;
                break;
            case 3:
                gameObject.GetComponent<RectTransform>().localScale+=scaleChange;
                break;
        }
    }

    //Determines button change with pointer exits 
    public void OnPointerExit(PointerEventData eventData)
    {
        switch (buttonType){
            case 1:
                Buttontext.enabled=true;
                alternatetext.enabled=false;
                break;
            case 2:
                gameObject.GetComponent<RectTransform>().localScale-=scaleChange;
                gameObject.GetComponent<UnityEngine.UI.Image>().sprite=origimage;
                break;
            case 3:
                gameObject.GetComponent<RectTransform>().localScale-=scaleChange;
                break;
        }
    }

    //Resets the size of the button when pressed
    public void resetScale(){
        gameObject.GetComponent<RectTransform>().localScale=new Vector3(1f,1f,1f);
    }
}
