using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PerkUIHandler : MonoBehaviour
{
    public GameObject textBox;

    public TMP_Text perkNameText;
    public TMP_Text perkInfoText;

    public Vector3 offset = new Vector3(200, 100, 0);

    private void Update()
    {
        PerkInfoBoxPosition();
    }

    public void PerkInfoBoxPosition()
    {
        textBox.transform.position = Input.mousePosition + offset;
    }

    public void SetPerkInformation(string perkName, string perkInfo)
    {
        perkNameText.text = perkName;
        perkInfoText.text = perkInfo;
        textBox.SetActive(true);
    }

    public void HidePerkInformation()
    {
        perkNameText.text = string.Empty;
        perkInfoText.text = string.Empty;
        textBox.SetActive(false);
    }
}
