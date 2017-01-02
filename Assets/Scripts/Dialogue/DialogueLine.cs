/*
 * Author: Isaiah Mann
 * Description: Represents a text in the mobile game Lucid
 */

[System.Serializable]
public class DialogueLine : Data {
	public string ID;
	public int IDNum {
		get {
			int idNum;
			if (tryParseFirstInt(ID, out idNum)) {
				return idNum;
			} else {
				return DEFAULT_INT_VALUE;
			}
		}
	}

	public string Speaker;
	public string Body;
	public string[] Responses;
	public bool IsPlayer;

	// Used to determine equality
	public override string ToString () {
		return string.Format ("[Dialogue Line: ID={0}, Body={1}, Speaker={2}, Responses={3}]", ID, Body, Speaker, ArrayUtil.ToString(Responses));
	}

	public override int GetHashCode () {
		return ToString().GetHashCode();
	}

	public override bool Equals (object obj) {
		if (obj is DialogueLine) {
			return (obj as DialogueLine).ToString().Equals(this.ToString());
		} else {
			return base.Equals (obj);
		}
	}
}
