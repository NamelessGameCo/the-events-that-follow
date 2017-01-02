/*
 * Author(s): Isaiah Mann
 * Description
 */

using System.Linq;

[System.Serializable]
public class LTime : Data {
	public const string DAY = "Day";
	protected const string MORNING = "morning";
	protected const string AFTERNOON = "afternoon";
	protected const string EVENING = "evening";
	protected const string AM = "AM";
	protected const string PM = "PM";
	protected const int DEFAULT_DAY = 1;
	protected const int DEFAULT_HOUR = 9;
	protected const int DEFAULT_MINUTE = 0;
	static int[] defaultHours = new int[]{9, 1, 6};
	static int[] defaultMinutes = new int[]{0, 0, 0};
	public int Day;
	public LDayPhase Phase;
	public int Hour;
	public int Minute;

	public static LTime Default {
		get {
			return new LTime(DEFAULT_DAY, DEFAULT_HOUR, DEFAULT_MINUTE);
		}
	}

	public LTime (int day, int hour, int minute, LDayPhase phase = default(LDayPhase)) {
		this.Day = day;
		this.Phase = phase;
		this.Hour = hour;
		this.Minute = minute;
	}

	public LTime (string interactionName) {
		this.Day = determineDay(interactionName);
		this.Phase = determineDayPhase(interactionName);
	}

	protected LDayPhase determineDayPhase(string interactionName) {
		if (interactionName.Contains(MORNING)) {
			return LDayPhase.Morning;
		} else if (interactionName.ToLower().Contains(AFTERNOON)) {
			return LDayPhase.Afternoon;
		} else if (interactionName.ToLower().Contains(EVENING)) {
			return LDayPhase.Evening;
		} else { 
			return default(LDayPhase);
		}
	}
		
	// Fails if there is a different number before the day in the string
	protected int determineDay(string interactionName) {
		int day;
		if (tryParseFirstInt(interactionName, out day)) {
			return day;
		} else {
			return DEFAULT_DAY;
		}
	}
		
	public string GetTimeString () {
		return string.Format("{0}:{1} {2}", Hour, padWithZeroes(Minute, 2), GetMeridiem(Phase));
	}

	public string GetDayString () {
		return string.Format ("{0} {1}: {2}", DAY, Day, Phase);
	}

	public void SetDefaultTimeFromDayPhase () {
		int dayPhaseIndex = (int) Phase;
		this.Hour = defaultHours[dayPhaseIndex];
		this.Minute = defaultMinutes[dayPhaseIndex];
	}

	public static string GetMeridiem (LDayPhase phase) {
		switch (phase) {
		case LDayPhase.Morning:
			return AM;
		case LDayPhase.Afternoon:
		case LDayPhase.Evening:
			return PM;
		default:
			return AM;
		}
	}
}
