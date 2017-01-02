/*
 * Author(s): Isaiah Mann
 * Description: Controls the in game characters
 */

using System.IO;
using UnityEngine;
using System.Collections;

public class CharacterController : SingletonController<CharacterController>, ICharacterController {
	const string CONTACTS_DIRECTORY = "Contacts";
	const string SPRITES_DIRECTORY = "Sprites";
	const string JSON_FILE_NAME = "Contacts";
	JsonController json;
	CharacterGroup contacts;

	public CharacterGroup IContacts {
		get {
			return contacts;
		}
	}

	static string SpritesPath {
		get {return Path.Combine(CONTACTS_DIRECTORY, SPRITES_DIRECTORY);}
	}
	static string JsonPath {
		get {return Path.Combine(CONTACTS_DIRECTORY, JSON_FILE_NAME);}
	}
		
	protected override void FetchReferences () {
		base.FetchReferences ();
		json = JsonController.Instance;
		contacts = loadContacts(JsonPath);
	}
		
	Sprite loadCharacterSprite (string spriteName) {
		return Resources.Load<Sprite>(Path.Combine(SpritesPath, spriteName));
	}

	CharacterGroup loadContacts (string jsonPath) {
		CharacterGroup contacts = json.LoadContacts(jsonPath);
		foreach (CharacterDescriptor contact in contacts.Elements) {
			contact.SpriteContactImage = loadCharacterSprite(contact.SpriteName);
		}
		return contacts;
	}
}
