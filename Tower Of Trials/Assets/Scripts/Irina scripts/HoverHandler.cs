using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoverHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private FMOD.Studio.EventInstance uiScrollInstance;

    void Start()
    {
        uiScrollInstance = FMODUnity.RuntimeManager.CreateInstance("event:/UI/Scroll");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //hover noise
        uiScrollInstance.start();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //hoverstop noise
    }

}
