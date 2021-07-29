using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LionStudios;

public class EventTrackingCustom : LionMenu
{
	private const int MAX_PARAMETERS = 5;

    [SerializeField] InputField _NameInputField = null;
	[SerializeField] Transform _ParameterList = null;
	[SerializeField] CustomEventParamEntry _ParameterEntryPrefab = null;
	[SerializeField] Button _AddParamButton = null;
	[SerializeField] Button _RemoveParamButton = null;

	private List<CustomEventParamEntry> eventParams = null;

	private void OnEnable() {
		if(eventParams == null || eventParams.Count == 0) {
			_RemoveParamButton.gameObject.SetActive( false );
		}
	}

	public void TrackAllSDKEvent()
    {
		// get all parameter kvps
		var parameters = new Dictionary<string, object>();
		foreach(var paramEntry in eventParams) {

			// prevent duplicate keys
			if (parameters.ContainsKey( paramEntry.GetKey() ))
				continue;

			parameters.Add( paramEntry.GetKey(), paramEntry.GetValue() );
		}

		// throw event
		Analytics.LogEvent( _NameInputField.text, parameters );
    }

	public void AddParameter() {
		if(eventParams == null) {
			eventParams = new List<CustomEventParamEntry>();
		}

		if (eventParams.Count >= MAX_PARAMETERS) {
			return;
		}

		CustomEventParamEntry newEntry = CustomEventParamEntry.Instantiate( _ParameterEntryPrefab );
		newEntry.transform.SetParent( _ParameterList );
		newEntry.transform.SetSiblingIndex( 0 );
		newEntry.transform.localScale = Vector3.one;

		eventParams.Add( newEntry );

		// show / hide buttons
		_AddParamButton.gameObject.SetActive( eventParams.Count < MAX_PARAMETERS );
		_RemoveParamButton.gameObject.SetActive( eventParams.Count > 0 );
	}

	public void RemoveParameter() {
		if (eventParams == null || eventParams.Count == 0)
			return;

		int oldIdx = eventParams.Count - 1;
		CustomEventParamEntry oldEntry = eventParams[oldIdx];

		// cleanup
		eventParams.RemoveAt( oldIdx );
		GameObject.Destroy( oldEntry.gameObject );

		// show hide buttons
		_AddParamButton.gameObject.SetActive( eventParams.Count < MAX_PARAMETERS );
		_RemoveParamButton.gameObject.SetActive( eventParams.Count > 0 );
	}
}
