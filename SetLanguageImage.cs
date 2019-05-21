/*
 * Created by SharpDevelop.
 * User: almaz
 * Date: 27.07.2018
 * Time: 17:38
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class SetLanguageImage:MonoBehaviour{
    public string cur
    {
        get { return Language.Current; }
    }
    public string Reference;
    public bool capital=false;
    public int MaxSize=0;
    public bool UseLibrary = false;
    [Serializable] public class Lib : SerializableDictionary<string, Mes> { }
    [Serializable] public class Mes : SerializableDictionary<string, tex> { }
    [Serializable]
    public class tex
    {
        public Sprite sprite=null;
        [TextArea]
        public string text=null;
        public Font font = null;
        public int fontsize = 0;
    }
    public Mes translations;
    Text _text;
    Image _image;
    void Start(){
		Language.OnLanguageChanged+=SetLanguage;
        _text = GetComponent<Text>();
        _image = GetComponent<Image>();
        SetText();
	}
    public void SetText()
    {
        if (_text != null)
        {
            if (!UseLibrary)
            {
                if (MaxSize != 0)
                {
                    _text.resizeTextForBestFit = true;
                    _text.resizeTextMaxSize = MaxSize;
                }
                if (translations.Keys.Contains(cur))
                {
                    if (cur != "ar")
                    {
                        if (!capital)
                            _text.text = translations[cur].text;
                        else _text.text = translations[cur].text.ToUpper();
                    }
                    else
                    {
                        _text.text = ArabicSupport.ArabicFixer.Fix(translations[cur].text);
                    }
                }
                else
                {
                    if (translations.Keys.Contains("en"))
                    
                        _text.text = translations["en"].text;
                }
                if (translations.Keys.Contains(cur))
                {
                    if (translations[cur].font != null)
                    {
                        _text.font = translations[cur].font;

                    }
                    else
                    {
                        _text.font = Language.Instance.translations[cur].font;
                    }

                    if (translations[cur].fontsize != 0)
                    {
                        _text.fontSize = translations[cur].fontsize;
                    }
                    else
                    {
                        if (translations["en"].fontsize != 0)
                            _text.fontSize = translations["en"].fontsize;
                    }
                }
            }
            else
            {

            }
        }
        if (_image != null)
        {
            if(translations.ContainsKey(cur))
            _image.sprite = translations[cur].sprite;
        }
    }
	public void SetLanguage(string Language){
        SetText();
	}
}


