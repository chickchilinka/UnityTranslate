using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class LanguageSelect : MonoBehaviour {

	public GameObject[] Panels;
	public Text Name;
	public Text Name2;
	public SceneLoader sc;
	[Serializable]public class StringMesDictionary : SerializableDictionary<string,string> {}
		
		public StringMesDictionary translations;
	public bool MainPanel;
	string selected="en";
	void Start(){
		if(PlayerPrefs.HasKey("Language")){
			Debug.Log(PlayerPrefs.GetString("Language"));
			Name.text=translations[PlayerPrefs.GetString("Language")];
			if(Name2!=null)
			Name2.text=translations[PlayerPrefs.GetString("Language")];
			Language.SetLanguage(PlayerPrefs.GetString("Language"));
			if(sc!=null){
			sc.LoadSplash();
		}
			if(MainPanel){
				gameObject.SetActive(false);
			}
		}else{
			Name.text="ENGLISH";
			if(Name2!=null)
			Name2.text="ENGLISH";
			Language.SetLanguage("en");
		}
	}
	public void Set(string lang){
		Name.text=translations[lang];if(Name2!=null)
			Name2.text=translations[lang];
		selected=lang;
			PlayerPrefs.SetString("Language", lang);
			Language.SetLanguage(lang);
			foreach(var k in Panels){
				k.SetActive(false);
			}
	}
	public void Set(){
		PlayerPrefs.SetString("Language",selected);
		//if(MainPanel)gameObject.SetActive(false);
		if(sc!=null){
			sc.LoadSplash();
		}
	}
	public void OpenClose(){
		if(Panels[0].activeInHierarchy){
foreach(var k in Panels){
				k.SetActive(false);
			}
		}else{
		foreach(var k in Panels){
				k.SetActive(true);
			}
		}
	}
}
