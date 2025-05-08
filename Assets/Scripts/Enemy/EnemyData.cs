using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "ScriptableObjects/Enemy")]
public class EnemyData : ScriptableObject
{
    // Enemy Identity
    public string title;
    public AudioClip[] sounds;
    public double pointReward;
    public int enemyValue;

    // Enemy Stats
    public int maxhp;
    public int damage;
    public int speed;
}
