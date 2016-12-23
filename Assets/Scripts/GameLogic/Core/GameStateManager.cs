/*
 * Author: Isaiah Mann
 * Description: Controls the state of a game
 */

public class GameStateManager : StateManager {
	protected GameState currentState;

	public GameStateManager (GameState[] gameStates) : base(gameStates) {
		
	}
}
