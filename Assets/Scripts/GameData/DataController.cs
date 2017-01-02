/*
 * Author(s): Isaiah Mann
 * Description: Controls the game's data
 */

using UnityEngine;

using System;
using System.IO;
using System.Runtime;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;

public class DataController : SingletonController<DataController>, IDataController {
	const string DEFAULT_SAVE_PATH = "DefaultLucidSave.dat";

	LStoryController story;

	protected override void FetchReferences () {
		base.FetchReferences ();
		story = LStoryController.Instance;
		Load();
	}

	public void Save () {
		Save(DEFAULT_SAVE_PATH);
	}

	public void Save (string savePath) {
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		FileStream file;
		try {
			file = File.Open(GetSavePath(), FileMode.Open);
		} catch {
			file = File.Create(GetSavePath());
		}
		LGameSave gameSave = new LGameSave(story.CurrentTime, story.Conversations);
		binaryFormatter.Serialize(file, gameSave);
		file.Close();
	}

	public void Load () {
		Load(DEFAULT_SAVE_PATH);
	}

	// Should only be called after refs are set (end of FetchReferences())
	public void Load (string savePath) {
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		FileStream file;
		try {
			file = File.Open(GetSavePath(), FileMode.Open);
			SetGame(binaryFormatter.Deserialize(file) as LGameSave);
		} catch {
			Reset();
			return;
		}
		file.Close();
	}

	public void SetGame(LGameSave save) {
		story.Set(save.Time, save.Conversations);
	}

	public void Reset () {
		story.Reset();
	}

	public string GetSavePath (string fileName = DEFAULT_SAVE_PATH) {
		return Path.Combine(Application.persistentDataPath, fileName);
	}
}
