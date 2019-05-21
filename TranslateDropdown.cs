using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TranslateDropdown : MonoBehaviour {

    public Text TemplateText;
    public Text Label;
    Library library;
    Dropdown dropdown;
    List<Dropdown.OptionData> Opts;
    string cur {
        get { return Language.Current; }
    }
    private void OnEnable()
    {
        Start();
    }

    public void Start()
    {
        Language.OnLanguageChanged += SetLanguage;
        dropdown=GetComponent<Dropdown>();
        if (Opts == null)
        {
            Opts = dropdown.options;
        }
        library = GetComponent<Library>();
        SetLanguage(cur);
    }
    public void SetLanguage(string lang)
    {
        SetText();
    }
    public void SetText()
    {
        List<Dropdown.OptionData> options = dropdown.options;
        TemplateText.font = Language.Instance.translations[cur].font;
        Label.font = Language.Instance.translations[cur].font;
        if(Opts!=null)
        for (int i = 0; i < Opts.Count; i++)
        {
                if(library.library.ContainsKey(Opts[i].text))
            options[i].text = library.GetTranslation(Opts[i].text);
        }
        dropdown.options = options;
    }
}
