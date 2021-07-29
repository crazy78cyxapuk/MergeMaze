using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CustomEventParamEntry : MonoBehaviour {

	[SerializeField] InputField _ParameterNameInputField = null;
	[SerializeField] InputField _ParameterValueInputField = null;

	public string GetKey() {
		return _ParameterNameInputField.text;
	}

	public object GetValue() {
		return _ParameterValueInputField.text;
	}
}
