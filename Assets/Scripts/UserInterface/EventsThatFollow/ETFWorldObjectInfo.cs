using UnityEngine;
using UnityEngine.EventSystems;

public class ETFWorldObjectInfo : MonoBehaviourExtended {
	ETFUIController ui;

	[SerializeField]
	string displayedInfo;
	[SerializeField]
	Vector3 infoOffset;

	WorldTextBox textBox;
	bool hasTextBox {
		get {
			return textBox != null;
		}
	}

	protected override void FetchReferences () {
		base.FetchReferences ();
		ui = ETFUIController.Instance;
	}

	void OnMouseUp () {
		if (hasTextBox) {
			ui.TeardownWorldTextBox(textBox);
		} else {
			textBox = ui.CreateWorldTextBox(displayedInfo, transform.position + infoOffset);
		}
	}
}
