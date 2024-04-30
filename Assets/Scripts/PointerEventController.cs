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

    public int buttonType;
    //1: Button with changing text on hover
    //2: Button with changing size and sprite on hover
    //3: Button with changing size on hover

    void Start(){
        switch (buttonType){
            case 1:
                Buttontext.enabled=true;
                alternatetext.enabled=false;
                break;
            case 2:
                gameObject.GetComponent<RectTransform>().localScale=new Vector3(1,1,1);
                gameObject.GetComponent<UnityEngine.UI.Image>().sprite=origimage;
                break;
        }
    }

    public void OnPointerEnter(PointerEventData data)
    {
        switch (buttonType){
            case 1:
                Buttontext.enabled=false;
                alternatetext.enabled=true;
                break;
            case 2:
                gameObject.GetComponent<RectTransform>().localScale=new Vector3(1.2f,1.2f,1.2f);
                gameObject.GetComponent<UnityEngine.UI.Image>().sprite=altimage;
                break;
            case 3:
                gameObject.GetComponent<RectTransform>().localScale=new Vector3(1.1f,1.1f,1.1f);
                break;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        switch (buttonType){
            case 1:
                Buttontext.enabled=true;
                alternatetext.enabled=false;
                break;
            case 2:
                gameObject.GetComponent<RectTransform>().localScale=new Vector3(1f,1f,1f);
                gameObject.GetComponent<UnityEngine.UI.Image>().sprite=origimage;
                break;
            case 3:
                gameObject.GetComponent<RectTransform>().localScale=new Vector3(1f,1f,1f);
                break;
        }
    }
}
