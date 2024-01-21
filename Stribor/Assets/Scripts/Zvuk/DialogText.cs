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

    private IEnumerator DelaySeconds(float time)
    {
        yield return new WaitForSeconds(time);
    }

    public void StartCounter(float time)
    {
        StartCoroutine(DelaySeconds(time));
    }

    public void Check()
    {
        if (i <= strings.Length - 1)
        { 
            if(i == 4)
            {
                StartCoroutine(DelaySeconds(1.5f));
            }
            if (i == 5)
            {
                StartCoroutine(DelaySeconds(0.5f));
            }
            if (i == 6)
            {
                StartCoroutine(DelaySeconds(1.5f));
            }
            if (i == 8)
            {
                StartCounter(2.0f);
            }
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
            if(i == 9)
            {
                m_TextMeshPro.color = Color.red;
            }
            int visible = current % (totalChars + 1);
            m_TextMeshPro.maxVisibleCharacters = visible;

            if(visible >= totalChars)
            {
                i++;
                if (i == 2)
                {
                    StartCoroutine(DelaySeconds(0.2f));
                }
                Invoke("Check", wordTime);
                break;
            }

            current++;
            yield return new WaitForSeconds(charTime);
        }
    }

}
