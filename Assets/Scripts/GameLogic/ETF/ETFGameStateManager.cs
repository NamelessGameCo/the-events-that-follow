
public class ETFGameStateManager : GameStateManager {
	public ETFGameState CurrentState {
		get {
			return currentState as ETFGameState;
		}
	}

	public ETFGameStateManager (ETFGameState[] gameStates) : base (gameStates) {}
}
