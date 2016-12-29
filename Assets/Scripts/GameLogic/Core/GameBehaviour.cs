/*
 * Author: Isaiah Mann
 * Description: Controls the behaviour and operations of a game
 */

using UnityEngine;

public class GameBehaviour : SingletonController<GameBehaviour> {
	[SerializeField]
	Transform world;
	public float worldScale {
		get {
			return world.localScale.x;
		}
	}
	protected GameDataStore dataStore;
	protected GameStateManager stateManager;
}
