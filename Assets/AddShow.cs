using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class AddShow : MonoBehaviour
{

    private CanvasGroup _cg;
    [SerializeField] private TMPro.TMP_Text _text;
    void Awake()
    {
        _cg = GetComponent<CanvasGroup>();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        _cg.DOFade(1, 0.4f);
        StartCoroutine(TimerAndAdd());
    }
    private void Hide()
    {
        _cg.DOFade(0, 0.2f).OnComplete(()=>gameObject.SetActive(false));
    }
    IEnumerator TimerAndAdd()
    {
        _text.text = "3";
        yield return new WaitForSeconds(0.4f);
        _text.text = "2";
        yield return new WaitForSeconds(0.4f);
        _text.text = "1";
        yield return new WaitForSeconds(0.3f);
        YandexGame.RewVideoShow(1);
        Hide();


    }
}
