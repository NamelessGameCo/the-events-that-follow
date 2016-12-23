/*
 * Author: Isaiah Mann
 * Description: Represents a state of the game
 */

[System.Serializable]
public class GameState : ObjectState {
	// Create state manager which handles whether the current state of game is 'play', 'dialogue', 'menu', 'cutscene', etc. Work with sam to integrate this state manager's controls into player
	// movement code

	public bool InputEnabled;
	public bool OverworldVisible;

	public GameState (string id, string[] validTransitionStates, bool inputEnabled, bool overworldVisible) : base (id, validTransitionStates) {
		this.InputEnabled = inputEnabled;
		this.OverworldVisible = overworldVisible;
	}
}
