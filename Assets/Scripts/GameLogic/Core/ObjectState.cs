/*
 * Author: Isaiah Mann
 * Description: Represents a single state in Finite State Machine
 */

[System.Serializable]
public class ObjectState {

	public string ID;

	// The IDs of other states this state can transition into
	public string[] ValidTransitionStates;

	public ObjectState (string id, string[] validTransitionStates) {
		this.ID = id;
		this.ValidTransitionStates = validTransitionStates;
	}

	public bool CanTransition (ObjectState toState) {
		return ArrayUtil.Contains(ValidTransitionStates, toState.ID);
	}
}
