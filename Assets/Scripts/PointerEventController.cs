using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PointerEventController : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    public string temptext ="";
    public TextMeshProUGUI Buttontext;
    public string alternatetext;
    //public GameObject Label;

    public void OnPointerEnter(PointerEventData data)
    {
        temptext= Buttontext.text;
        Buttontext.text = alternatetext;
        //Label.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Buttontext.text = temptext;
        //Label.SetActive(false);
    }
}
