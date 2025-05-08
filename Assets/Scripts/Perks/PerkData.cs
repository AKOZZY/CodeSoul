using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Perk", menuName = "ScriptableObjects/Perk")]
public class PerkData : ScriptableObject
{
    // Perk Identity
    public int id;
    public string title;
    public string description;
    public Sprite logo;
}
