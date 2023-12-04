using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using MyJsonConverters;
using System;

[Serializable]
[JsonConverter(typeof(LineDataConverter))]
public struct LineData
{
	[Multiline]
	public string text;
	public string talker;
	public float time;
}

[CreateAssetMenu(menuName = "Data/Dialog/DialogData")]
public class DialogData : ScriptableObject
{
	public string id;
	//Time to show/animate, can be a straight up audio file
	//Talker
	//text
	public List<LineData> InitialLines;
	public List<LineData> CompletedLines;

	public static DialogData Create(DialogData data)
	{
		string pathName = data.id + ".asset";
		DialogData d = new DialogData();
		d.id = data.id;
		d.InitialLines = data.InitialLines;
		d.CompletedLines = data.CompletedLines;

		AssetUtils.CreateAsset<DialogData>("Assets/Resources/Data/Dialog/" + pathName, d);
		return d;
	}
}

