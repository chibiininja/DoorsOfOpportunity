using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextZoneTrigger : MonoBehaviour
{
    [SerializeField]
    private TMP_Text TMPTipText;
    [SerializeField]
    private TMP_Text TMPQuoteText;
    [SerializeField]
    private CanvasGroup TipAlpha;
    [SerializeField]
    private CanvasGroup QuoteAlpha;
    [SerializeField]
    [TextAreaAttribute]
    private string TipText;
    [SerializeField]
    [TextAreaAttribute]
    private string QuoteText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CharacterController>(out CharacterController controller))
        {
            StartCoroutine(ChangeQuoteAlpha());
            StartCoroutine(ChangeTipAlpha());
            TMPQuoteText.text = QuoteText;
            TMPTipText.text = QuoteText;
        }
    }

    private IEnumerator ChangeQuoteAlpha()
    {
        float time = 0;
        while (time < 1)
        {
            QuoteAlpha.alpha = Mathf.Lerp(0f, 1f, time);
            yield return null;
            time = Time.deltaTime;
        }
        yield return new WaitForSeconds(7f);
        time = 0;
        while (time < 1)
        {
            QuoteAlpha.alpha = Mathf.Lerp(1f, 0f, time);
            yield return null;
            time = Time.deltaTime;
        }
    }

    private IEnumerator ChangeTipAlpha()
    {
        float time = 0;
        while (time < 1)
        {
            TipAlpha.alpha = Mathf.Lerp(0f, 1f, time);
            yield return null;
            time = Time.deltaTime;
        }
        yield return new WaitForSeconds(7f);
        time = 0;
        while (time < 1)
        {
            TipAlpha.alpha = Mathf.Lerp(1f, 0f, time);
            yield return null;
            time = Time.deltaTime;
        }
    }
}
