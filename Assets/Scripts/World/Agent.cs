/*
 * Author(s): Isaiah Mann
 * Description: Super class for all agents operating in the game world
 */

using UnityEngine;
using System.Collections;

public abstract class Agent : MobileObjectBehaviour {
	protected ETFUIController ui;

	public virtual bool TryGetPortrait (out Sprite portrait) {
		portrait = null;
		return false;
	}

	protected override void FetchReferences () {
		base.FetchReferences ();
		ui = ETFUIController.Instance;
	}
}
