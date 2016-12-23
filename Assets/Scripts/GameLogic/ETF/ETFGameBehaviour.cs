/*
 * Author: Isaiah Mann
 * Description: Runs an Instance of the Events That Follow game
 */

using UnityEngine;
using System.IO;

public class ETFGameBehaviour : GameBehaviour {
	const string JSON_DIR = "JSON";
	static string GAME_DATA_PATH {
		get {
			return Path.Combine(JSON_DIR, "GameData");
		}
	}

	ETFGameStateManager _stateManager {
		get {
			return stateManager as ETFGameStateManager;
		}
	}
	ETFGameDataStore _dataStore {
		get {
			return dataStore as ETFGameDataStore;
		}
	}

	protected override void SetReferences () {
		base.SetReferences ();
		dataStore = parseClassFromJSON<ETFGameDataStore>(GAME_DATA_PATH);
		stateManager = new ETFGameStateManager(_dataStore.GameStates);
	}
}
