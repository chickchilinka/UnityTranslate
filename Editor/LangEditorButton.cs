using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;
[CustomEditor(typeof(SetLanguageImage))]
[CanEditMultipleObjects]
public class LangEditorButton : Editor {

    public override void OnInspectorGUI()
    {
        bool have = false;
        serializedObject.Update();
        if (GUILayout.Button("Get Translations"))
        {
            have = true;
            SetLanguageImage l = (SetLanguageImage)targets[0];

            TextAsset text = Resources.Load<TextAsset>("csvjson");
            JObject j = JObject.Parse(text.text);

            string refer = "";
            if (l.GetComponent<Text>())
            {
                refer = l.GetComponent<Text>().text;


            }
            if (l.Reference != "")
            {
                refer = l.Reference;

            }
            if (j[refer] != null)
            {
                if (!l.translations.ContainsKey("en"))
                {
                    SetLanguageImage.tex t = new SetLanguageImage.tex();
                    t.text = refer;
                    l.translations.Add("en", t);
                }
                if (!l.translations.ContainsKey("es"))
                {
                    SetLanguageImage.tex t = new SetLanguageImage.tex();
                    t.text = j[refer]["es"].ToString();
                    l.translations.Add("es", t);
                }
                if (!l.translations.ContainsKey("ru"))
                {
                    SetLanguageImage.tex t = new SetLanguageImage.tex();
                    t.text = j[refer]["ru"].ToString();
                    l.translations.Add("ru", t);
                }
                if (!l.translations.ContainsKey("fr"))
                {
                    SetLanguageImage.tex t = new SetLanguageImage.tex();
                    t.text = j[refer]["fr"].ToString();
                    l.translations.Add("fr", t);
                }
                if (!l.translations.ContainsKey("pt"))
                {
                    SetLanguageImage.tex t = new SetLanguageImage.tex();
                    t.text = j[refer]["pt"].ToString();
                    l.translations.Add("pt", t);
                }
                if (!l.translations.ContainsKey("hi"))
                {
                    SetLanguageImage.tex t = new SetLanguageImage.tex();
                    t.text = j[refer]["hi"].ToString();
                    l.translations.Add("hi", t);
                }
                if (!l.translations.ContainsKey("ar"))
                {
                    SetLanguageImage.tex t = new SetLanguageImage.tex();
                    t.text = j[refer]["ar"].ToString();
                    l.translations.Add("ar", t);
                }
                foreach (var b in targets)
                {
                    SetLanguageImage sd = (SetLanguageImage)b;
                    sd.translations = l.translations;

                }
            }
            else
            {
                Debug.LogError("No Translation Found, Use Google");
            }



          
            return;
        }
        
            if (GUILayout.Button("Get Google Translations"))
            {
            have = true;
                foreach (var b in targets)
                {
                    SetLanguageImage l = (SetLanguageImage)b;

                    string refer = "";
                    if (l.GetComponent<Text>())
                    {
                        refer = l.GetComponent<Text>().text;


                    }
                    else if (l.Reference != "")
                    {
                        refer = l.Reference;

                    }
                    if (!l.translations.ContainsKey("en"))
                    {
                        SetLanguageImage.tex t = new SetLanguageImage.tex();
                        t.text = refer;
                        l.translations.Add("en", t);
                    }
                    if (!l.translations.ContainsKey("es"))
                    {
                        marijnz.EditorCoroutines.EditorCoroutines.StartCoroutine(Process(l, "es", refer), this);
                    }
                    if (!l.translations.ContainsKey("ru"))
                    {
                        marijnz.EditorCoroutines.EditorCoroutines.StartCoroutine(Process(l, "ru", refer), this);
                    }
                    if (!l.translations.ContainsKey("fr"))
                    {
                        marijnz.EditorCoroutines.EditorCoroutines.StartCoroutine(Process(l, "fr", refer), this);
                    }
                    if (!l.translations.ContainsKey("pt"))
                    {
                        marijnz.EditorCoroutines.EditorCoroutines.StartCoroutine(Process(l, "pt", refer), this);
                    }
                    if (!l.translations.ContainsKey("hi"))
                    {
                        marijnz.EditorCoroutines.EditorCoroutines.StartCoroutine(Process(l, "hi", refer), this);
                    }
                    if (!l.translations.ContainsKey("ar"))
                    {
                        marijnz.EditorCoroutines.EditorCoroutines.StartCoroutine(Process(l, "ar", refer), this);
                    }


                }
            return;
        }
        if (GUILayout.Button("Paste To Library"))        {
            SetLanguageImage l = (SetLanguageImage)targets[0];
            l.GetComponent<Library>().library.Add(l.translations["en"].text, l.translations);
        }
        if (GUILayout.Button("Reset"))
        {
            SetLanguageImage l = (SetLanguageImage)targets[0];
            l.translations=new SetLanguageImage.Mes();
        }

        serializedObject.ApplyModifiedProperties();
        
            DrawDefaultInspector();
        //DrawDefaultInspector();
    }
    public IEnumerator Process(SetLanguageImage s,string targetLang, string sourceText)
    {
        // We use Auto by default to determine if google can figure it out.. sometimes it can't.
        string sourceLang = "en";
        // Construct the url using our variables and googles api.
        string url = "https://translate.googleapis.com/translate_a/single?client=gtx&sl="
            + sourceLang + "&tl=" + targetLang + "&dt=t&q=" + WWW.EscapeURL(sourceText);

        // Put together our unity bits for the web call.
        WWW www = new WWW(url);
        // Check to see if it's done.
        yield return www;
        // Check to see if we don't have any errors.
        if (www.isDone)
        {
            // Check to see if we don't have any errors.
            if (string.IsNullOrEmpty(www.error))
            {
                Debug.Log(www.text);
                // Parse the response using JSON.
                var N = JArray.Parse(www.text);
                // Dig through and take apart the text to get to the good stuff.
               var translatedText = N[0][0][0].ToString();
                var tex = new SetLanguageImage.tex();
                tex.text = translatedText;
                s.translations.Add(targetLang, tex);

                // This is purely for debugging in the Editor to see if it's the word you wanted.
               
            }
        }

    }

}
