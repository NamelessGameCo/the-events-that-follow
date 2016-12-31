/*
 * Author: Isaiah Mann
 * Description: Events That Follow, specific game state
 */

[System.Serializable]
public class ETFGameState : GameState {

	public ETFGameState (string id, 
		string[] validTransitionStates, 
		bool inputEnabled, 
		bool overworldVisible) 
		: base (id, validTransitionStates, inputEnabled, overworldVisible) {}
	
}
