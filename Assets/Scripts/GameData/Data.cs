/*
 * Author(s): Isaiah Mann
 * Description: Abstract data class for Lucid data objects
 */

using System.Linq;

[System.Serializable]
public abstract class Data {
	protected const int DEFAULT_INT_VALUE = -1;

	// Adapated from: http://stackoverflow.com/questions/4734116/find-and-extract-a-number-from-a-string
	protected bool tryParseFirstInt (string inString, out int result) {
		return int.TryParse(new string(inString
			.SkipWhile(x => !char.IsDigit(x))
			.TakeWhile(x => char.IsDigit(x))
			.ToArray()), out result);
	}

	protected string padWithZeroes (int number, int targetLength) {
		string numberAsString = number.ToString();
		while (numberAsString.Length < targetLength) {
			numberAsString = "0" + numberAsString;
		}
		return numberAsString;
	}
}
