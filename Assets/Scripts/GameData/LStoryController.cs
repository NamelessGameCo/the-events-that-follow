/*
 * Author(s): Isaiah Mann
 * Description: Handles tracking progress in the game's narrative
 */

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LStoryController : SingletonController<LStoryController>, IStoryController {
	public LTime CurrentTime{get; private set;}
	DataController data;
	CharacterController characters;
	LMessageController messaging;

	List<LConversation> activeConversations = new List<LConversation>();
	[SerializeField]
	CharacterDescriptor player;
	public LConversation[] Conversations {
		get {
			return activeConversations.ToArray();
		}
	}

	protected override void SetReferences () {
		base.SetReferences ();
		Reset();
	}

	protected override void FetchReferences () {
		base.FetchReferences ();
		data = DataController.Instance;
		characters = CharacterController.Instance;
		messaging = LMessageController.Instance;
	}
		
	public CharacterDescriptor Player {
		get {
			return player;
		}
	}

	public void Set (LTime time, LConversation[] conversations) {
		this.CurrentTime = time;
		this.activeConversations = new List<LConversation>(conversations);
	}

	public void SetDay (int day, LDayPhase dayPhase, int hour, int minute = 0) {
		CurrentTime = new LTime(day, hour, minute, dayPhase);
		sendDayPhaseTransitionEvent(dayPhase);
		data.Save();
	}

	public void Reset () {
		CurrentTime = LTime.Default;
		activeConversations = new List<LConversation>();
	}

	public void TrackConversation (LConversation conversation) {
		// Remove any previous iterations of the conversation
		untrackConversation(conversation);
		activeConversations.Add(conversation);
		if (data) {
			data.Save();
		}
	}

	void untrackConversation (LConversation conversation) {
		LConversation tracked = activeConversations.Find(convo => convo.ID.Equals(conversation.ID));
		if (tracked != null) {
			activeConversations.Remove(tracked);
		}
	}

	public bool IsTrackingConversation (LConversation conversation) {
		return activeConversations.Find(convo => convo.ID.Equals(conversation.ID)) != null;
	}

	// Checks for whether all the conversations for the day have been complete
	public bool ReadyToAdvanceDayPhase () {
		foreach (CharacterDescriptor contact in characters.IContacts.Elements) {
			// Some contacts do not have any messages during certain day phases
			if (messaging.HasConversationForContactAtTime(contact)) {
				LConversation conversation = activeConversations.Find(convo => convo.ID .Equals(contact.Name));
				// Case: Conversation not yet begun or not yet complete
				if (conversation == null || !conversation.CheckIsComplete()) {
					return false;
				}
			}
		}
		return true;
	}

	public bool TryLoadConversation (string conversationID, out LConversation convo) {
		convo = activeConversations.Find(conversation => conversation.ID.Equals(conversationID));
		return convo != null;
	}

	public void AdvanceDayPhase () {
		activeConversations.Clear();
		int indexOfDayPhase = (int) CurrentTime.Phase;
		if (Enum.GetNames(typeof(LDayPhase)).Length <= indexOfDayPhase + 1) {
			CurrentTime.Phase = (LDayPhase) 0;
			CurrentTime.Day++;
		} else {
			CurrentTime.Phase = (LDayPhase) (indexOfDayPhase + 1);
		}
		CurrentTime.SetDefaultTimeFromDayPhase();
		data.Save();
	}

	void sendDayPhaseTransitionEvent (LDayPhase newPhase) {
		switch (newPhase) {
		case LDayPhase.Morning:
			EventController.Event(LEvent.TransitionToDay);
			break;
		case LDayPhase.Afternoon:
			EventController.Event(LEvent.TransitionToEvening);
			break;
		case LDayPhase.Evening:
			EventController.Event(LEvent.TransitionToNight);
			break;
		}
	}
}
