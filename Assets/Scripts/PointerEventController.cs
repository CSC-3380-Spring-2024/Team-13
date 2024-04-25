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
    public TextMeshProUGUI Buttontext;
    public TextMeshProUGUI alternatetext;

    void Start(){
        Buttontext.enabled=true;
        alternatetext.enabled=false;
    }

    public void OnPointerEnter(PointerEventData data)
    {
        Buttontext.enabled=false;
        alternatetext.enabled=true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Buttontext.enabled=true;
        alternatetext.enabled=false;
    }
}
