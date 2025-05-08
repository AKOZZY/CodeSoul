using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSoundManager : MonoBehaviour
{
    [SerializeField] AudioSource soundSFX;
    [SerializeField] AudioSource ambienceSFX;

    public AudioClip ambience;
    

    public AudioClip[] waveStartingSound;
    public AudioClip purchase;
    public AudioClip mysteryBox;

    // Perk Jingles
    public AudioClip reviveJingle;
    public AudioClip speedJingle;
    public AudioClip greedJingle;
    public AudioClip demonifierJingle;

    private void Start()
    {
        Ambience();
    }

    public void Ambience()
    {
        ambienceSFX.clip = ambience;
        ambienceSFX.Play();
    }

    public void MysteryBoxSFX()
    {
        soundSFX.PlayOneShot(mysteryBox);
    }

    public void ReviveSodaSFX()
    {
        soundSFX.PlayOneShot(reviveJingle);
    }

    public void GreedSodaSFX()
    {
        soundSFX.PlayOneShot(greedJingle);
    }

    public void SpeedSofaSFX()
    {
        soundSFX.PlayOneShot(speedJingle);
    }

    public void DemonifierSFX()
    {
        soundSFX.PlayOneShot(demonifierJingle);
    }

    public void EnemyHitSound()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            
        }
    }

    public void PlayStartingWaveSound()
    {
        AudioClip randomAudioClip = waveStartingSound[Random.Range(0, waveStartingSound.Length)];
        soundSFX.PlayOneShot(randomAudioClip);
    }

    public void Purchase()
    {
        soundSFX.PlayOneShot(purchase);
    }
}
