/*
 * Author(s): Isaiah Mann
 * Description: Used to load JSON
 */

using UnityEngine;
using System.Collections;

public class JsonController : SingletonController<JsonController>, IJsonController {
	public T Parse<T> (string json) {
		return JsonUtility.FromJson<T>(json);
	}

	public CharacterGroup LoadContacts (string jsonPath) {
		return Parse<CharacterGroup>(getJsonText(jsonPath));
	}

	string getJsonText (string jsonPath) {
		return Resources.Load<TextAsset>(jsonPath).text;
	}
}
