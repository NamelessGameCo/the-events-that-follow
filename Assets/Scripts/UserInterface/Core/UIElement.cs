using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIElement : MonoBehaviourExtended {
	protected RectTransform transform2D;
	Image image;
	Text text;

	[SerializeField]
	Sprite[] alternateSprites;
	public bool hasImage {
		get {
			return image != null;
		}
	}
	public bool hasText {
		get {
			return text != null;
		}
	}
	public bool hasAlternateSprites {
		get {
			return alternateSprites.Length > 0;
		}
	}
		
	protected override void SetReferences () {
		base.SetReferences();
		image = GetComponentInChildren<Image>();
		text = GetComponentInChildren<Text>();
		transform2D = GetComponent<RectTransform>();
	}

	public void Show () {
		gameObject.SetActive(true);
	}

	public void Hide () {
		gameObject.SetActive(false);
	}

	public void RandomSprite () {
		if (hasImage && hasAlternateSprites) {
			this.image.sprite = alternateSprites[Random.Range(0, alternateSprites.Length)];
		}
	}

	public void SetText (string text) {
		if (hasText) {
			this.text.text = text;
		}
	}
}
