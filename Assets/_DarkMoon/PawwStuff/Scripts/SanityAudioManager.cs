using UnityEngine;
using System.Collections;

public class SanityAudioManager : MonoBehaviour
{
    [Header("Loop Sources")]
    public AudioSource MusicSource;
    public AudioSource AmbientSource;

    [Header("One Shot Source")]
    public AudioSource SFXSource;

    [Header("Music")]
    public AudioClip CalmMusic;
    public AudioClip InsaneMusic;

    [Header("Ambient")]
    public AudioClip WaryAmbient;
    public AudioClip InsaneAmbient;
    public AudioClip VeryInsaneAmbient;

    [Header("Stingers")]
    public AudioClip InsaneHit;
    public AudioClip VeryInsaneHit;

    public float FadeDuration = 2f;

    public void SetSanity(SanityLevel level)
    {
        switch (level)
        {
            case SanityLevel.Sane:
                FadeMusic(CalmMusic);
                StopAmbient();
                break;

            case SanityLevel.Wary:
                FadeAmbient(WaryAmbient);
                break;

            case SanityLevel.Insane:
                FadeMusic(InsaneMusic);
                FadeAmbient(InsaneAmbient);

                SFXSource.PlayOneShot(InsaneHit);
                break;

            case SanityLevel.VeryInsane:
                FadeAmbient(VeryInsaneAmbient);

                SFXSource.PlayOneShot(VeryInsaneHit);
                break;
        }
    }

    void FadeMusic(AudioClip clip)
    {
        StartCoroutine(FadeAudio(MusicSource, clip));
    }

    void FadeAmbient(AudioClip clip)
    {
        StartCoroutine(FadeAudio(AmbientSource, clip));
    }

    void StopAmbient()
    {
        AmbientSource.Stop();
    }

    IEnumerator FadeAudio(AudioSource source, AudioClip newClip)
    {
        float startVolume = source.volume;

        while (source.volume > 0)
        {
            source.volume -= Time.deltaTime / FadeDuration;
            yield return null;
        }

        source.clip = newClip;
        source.loop = true;
        source.Play();

        while (source.volume < startVolume)
        {
            source.volume += Time.deltaTime / FadeDuration;
            yield return null;
        }
    }
}
