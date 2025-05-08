using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PerkPopup : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    PerkUIHandler handler;

    public string perkName;
    public string perkInfo;

    private void Start()
    {
        handler = GameObject.FindGameObjectWithTag("UI").GetComponent<PerkUIHandler>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        handler.SetPerkInformation(perkName, perkInfo);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        handler.HidePerkInformation();
    }

}
