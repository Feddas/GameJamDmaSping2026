using UnityEngine;
using TMPro;
using System.Collections;

public class TypewriterEffect : MonoBehaviour
{
    public TMP_Text textUI;
    [TextArea]
    public string fullText;

    public float typingSpeed = 0.05f;

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
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
