﻿/* 
 * Author(s): Kevin Wang, Isaiah Mann
 * Description: Stores some meta-data about the individual message screens
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class Conversation : Data {
	public string ID;
	public float barLocation;
	public CharacterDescriptor[] participants {get; private set;}
	public List<DialogueLine> messages {get; private set;}
	public int messageCount {
		get {
			return messages.Count;
		}
	}
	public bool HasBegun {
		get {
			return messageCount > 0;
		}
	}
	bool isComplete = false;

	public Conversation (string id, params CharacterDescriptor[] participants) {
		this.ID = id;
		this.participants = participants;
		this.messages = new List<DialogueLine>();
	}

	public void AddMessage(DialogueLine message){
		messages.Add(message);
	}
		
	public void MarkAsComplete () {
		isComplete = true;
	}

	public bool CheckIsComplete () {
		return isComplete;
	}

	public string GetLastMessage () {
		if (HasBegun) {
			return messages[messages.Count - 1].Body;
		} else {
			return string.Empty;
		}
	}
}
