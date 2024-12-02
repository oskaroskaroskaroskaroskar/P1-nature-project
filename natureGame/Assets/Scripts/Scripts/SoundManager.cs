using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource musicGood;
    public AudioSource musicBad;
    private bool isMusicGoodPlaying = false;
    private bool isMusicBadPlaying = false;

    void Start()
    {
        // Ensure initial state matches the score
        CheckAndUpdateMusic();
    }

    void Update()
    {
        // Continually check the current score
        CheckAndUpdateMusic();
    }

    void CheckAndUpdateMusic()
    {
        // Check for changes in environmentScore
        if (GameManager.Instance != null)
        {
            float currentScore = GameManager.Instance.environmentScore;

            if (currentScore < -3)
            {
                // Play bad music if it's not already playing
                if (!isMusicBadPlaying)
                {
                    musicBad.Play();
                    isMusicBadPlaying = true;
                }

                // Pause good music if it's playing
                if (isMusicGoodPlaying)
                {
                    musicGood.Pause();
                    isMusicGoodPlaying = false;
                }
            }
            else if (currentScore >= -2)
            {
                // Play good music if it's not already playing
                if (!isMusicGoodPlaying)
                {
                    musicGood.Play();
                    isMusicGoodPlaying = true;
                }

                // Pause bad music if it's playing
                if (isMusicBadPlaying)
                {
                    musicBad.Pause();
                    isMusicBadPlaying = false;
                }
            }
        }
    }
}
