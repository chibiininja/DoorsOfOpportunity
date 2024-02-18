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
            StartCoroutine(PhaseInQuoteAlpha());
            StartCoroutine(PhaseInTipAlpha());
            StartCoroutine(PhaseOutQuoteAlpha());
            StartCoroutine(PhaseOutTipAlpha());
            TMPQuoteText.text = QuoteText;
            TMPTipText.text = TipText;
        }
    }

    private IEnumerator PhaseInQuoteAlpha()
    {
        float time = 0;
        while (time < 1)
        {
            QuoteAlpha.alpha = Mathf.Lerp(0f, 1f, time);
            yield return null;
            time += Time.deltaTime;
        }
    }

    private IEnumerator PhaseOutQuoteAlpha()
    {
        yield return new WaitForSeconds(7f);
        float time = 0;
        while (time < 1)
        {
            QuoteAlpha.alpha = Mathf.Lerp(1f, 0f, time);
            yield return null;
            time += Time.deltaTime;
        }
    }

    private IEnumerator PhaseInTipAlpha()
    {
        float time = 0;
        while (time < 1)
        {
            TipAlpha.alpha = Mathf.Lerp(0f, 1f, time);
            yield return null;
            time += Time.deltaTime;
        }
    }

    private IEnumerator PhaseOutTipAlpha()
    {
        yield return new WaitForSeconds(7f);
        float time = 0;
        while (time < 1)
        {
            TipAlpha.alpha = Mathf.Lerp(1f, 0f, time);
            yield return null;
            time += Time.deltaTime;
        }
    }
}
