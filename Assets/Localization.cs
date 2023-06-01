using System.Collections;
using System.Collections;
using TMPro;
using UnityEngine;
using YG;

public class Localization : MonoBehaviour
{
    public string Ru, Eng;

    public TMP_Text text;
    private void Awake()
    {
        YandexGame.GetDataEvent += Translate;
      
    }
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.3F);
        Translate();
    }

    void Translate() 
    {
        if (YandexGame.EnvironmentData.language != "ru")
        {
            text.text = Eng;
        }
        else if(langcompare("ru") || langcompare("be") || langcompare("kk")||langcompare("uk") || langcompare("uz"))
        {
            text.text = Ru;
        }
        YandexGame.GetDataEvent -= Translate;
      
    }
    public static bool langcompare(string lang)
    {
        return YandexGame.EnvironmentData.language == lang;
    }
    private void OnEnable()
    {
        Translate();
    }
}

