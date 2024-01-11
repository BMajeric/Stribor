using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_TextMeshPro;
    [SerializeField] float charTime;
    [SerializeField] float wordTime;

    int i = 0;
    public string[] strings;
    void Start()
    {
        StartCoroutine(DelayStart());
    }

    private IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(0.7f);
        Check();
    }

    public void Check()
    {
        if (i <= strings.Length - 1)
        {
            m_TextMeshPro.text = strings[i];
            StartCoroutine(TextInside());
        }
    }

    private IEnumerator TextInside()
    {
        m_TextMeshPro.ForceMeshUpdate();
        int totalChars = m_TextMeshPro.textInfo.characterCount;
        int current = 0;

        while (true)
        {
            int visible = current % (totalChars + 1);
            m_TextMeshPro.maxVisibleCharacters = visible;

            if(visible  >= totalChars)
            {
                i++;
                Invoke("Check", wordTime);
                break;
            }

            current++;
            yield return new WaitForSeconds(charTime);
        }
    }

}
