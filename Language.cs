using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Language : MonoBehaviour
{
    private static Language _inst;
    public static Language Instance {
        get {
            if (_inst == null) {
                _inst = FindObjectOfType<Language>();
            }
            return _inst;
        }
    }
    [Serializable] public class StringMesDictionary : SerializableDictionary<string, StringTransDictionary> { }
    [Serializable] public class StringTransDictionary : SerializableDictionary<string, translation> { }
    [Serializable] public class Mes : SerializableDictionary<string, tex> { }
    [Serializable]
    public class tex
    {
        public Font font;

    }
    [Serializable]
    public class translation
    {
        public String text;


    }
    public Mes translations;
    public StringMesDictionary trans;
    public static string Current;
    public void Awake() {
        if (PlayerPrefs.HasKey("Language")) {
            Language.SetLanguage(PlayerPrefs.GetString("Language"));
        }
        else {
            Language.SetLanguage("en");
        }
    }
    public void WriteToText(Text text, string mes) {
        text.text = mes;
    }
    public static void SetLanguage(string Language) {
        Current = Language;
        if (OnLanguageChanged != null)
            OnLanguageChanged.Invoke(Language);
    }
    public static Action<string> OnLanguageChanged;
}

