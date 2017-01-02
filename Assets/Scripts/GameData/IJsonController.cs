/*
 * Author(s): Isaiah Mann
 * Description: The expected behaviour of a class to parse JSON
 */

public interface IJsonController {
	T Parse<T> (string json);
}
