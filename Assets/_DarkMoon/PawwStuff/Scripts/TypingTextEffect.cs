using UnityEngine;
using TMPro;
using System.Collections;

public class TypewriterEffect : MonoBehaviour
{
    public TMP_Text textUI;

    [TextArea]
    public string fullText;

    public float typingSpeed = 0.05f;

    [Header("Typing Audio")]
    public AudioSource audioSource;
    public AudioClip typingSound;

    [Range(0f, 1f)]
    public float soundChance = 0.8f;

    void Start()
    {
        textUI.text = "";
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            textUI.text = fullText.Substring(0, i);

            // play typing noise
            if (audioSource != null &&
                typingSound != null &&
                Random.value < soundChance)
            {
                audioSource.pitch =
                    Random.Range(0.9f, 1.1f);

                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(typingSound);
                }
            }

            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
