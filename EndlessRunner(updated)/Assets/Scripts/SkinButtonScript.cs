using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SkinButtonScript : MonoBehaviour {

	public int skinIndex;
	public SkinSelect skinSelect;

	void Start(){
		skinSelect = new SkinSelect();
		LoadSettings();
	}

	public void skinButton1Function(){
		skinSelect.skinIndex = 0;
		SaveSettings();
	}

	public void skinButton2Function(){
		skinSelect.skinIndex = 1;
		SaveSettings();
	}

	public void skinButton3Function(){
		skinSelect.skinIndex = 2;
		SaveSettings();
	}

	public void SaveSettings()
	{
		string jsonData = JsonUtility.ToJson(skinSelect, true);
		File.WriteAllText(Application.dataPath + "/skinSelect.json", jsonData);
	}

	public void LoadSettings()
	{
		skinSelect = JsonUtility.FromJson<SkinSelect>(File.ReadAllText(Application.dataPath + "/skinSelect.json"));
	}
}
