using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIElement : MonoBehaviourExtended {
	protected RectTransform transform2D;
	Image image;
	Text text;
	protected CanvasGroup canvas;

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
	protected bool hasCanvasGroup {	
		get {
			return canvas != null;
		}
	}

	protected override void SetReferences () {
		base.SetReferences();
		image = GetComponentInChildren<Image>();
		text = GetComponentInChildren<Text>();
		canvas = GetComponent<CanvasGroup>();
		transform2D = GetComponent<RectTransform>();
	}

	public virtual void Show () {
		gameObject.SetActive(true);
	}

	public virtual void Hide () {
		gameObject.SetActive(false);
	}

	public void RandomSprite () {
		if (hasImage && hasAlternateSprites) {
			this.image.sprite = alternateSprites[Random.Range(0, alternateSprites.Length)];
		}
	}

	public virtual void SetText (string text) {
		if (hasText) {
			this.text.text = text;
		}
	}

	protected virtual void toggleCanvasGroup (CanvasGroup canvas, bool active) {
		canvas.alpha = active ? 1 : 0;
		canvas.blocksRaycasts = active;
		canvas.interactable = active;
	}
}
