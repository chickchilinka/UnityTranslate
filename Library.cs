using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Library : MonoBehaviour {

    public SetLanguageImage.Lib library;
	public string cur
    {
        get
        {
            return Language.Current;
        }
    }
	public void SetText(Text text, string en)
    {

        text.font = Language.Instance.translations[cur].font;
        if (cur == "ar")
        {
            text.text= ArabicSupport.ArabicFixer.Fix(library[en][cur].text);
        }
        else
            text.text = library[en][cur].text;
    }
	public string GetTranslation(string en)
    {
        if (cur == "ar")
            return ArabicSupport.ArabicFixer.Fix(library[en][cur].text);
        return library[en][cur].text;
    }

}
