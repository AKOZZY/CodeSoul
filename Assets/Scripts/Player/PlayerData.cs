using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "ScriptableObjects/Player")]
public class PlayerData : ScriptableObject
{
    public string characterName;
    public Sprite portraitNobackground;
    public Sprite portraitWithBackground;
    public Sprite specialAbilityPortrait;
    public int hp;
    public int speed;
    public RuntimeAnimatorController controller;
}
