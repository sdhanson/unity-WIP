using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public static class OVRDebug
{
	private static List<string> logs = new List<string> ();
	private static Text text;
	public static int nLogs = 10;
	// max number of lines to display. Can be changed


	static OVRDebug ()
	{
		// will only be called once
		OVRCameraRig oVRCamera = GameObject.FindObjectOfType<OVRCameraRig> ();
		if (oVRCamera == null) {
			Debug.Log ("Missing OVR camera");
			return;
		}
		// create a canvas and a text element to display the logs
		GameObject goCanvas = new GameObject ("Canvas");
		goCanvas.transform.parent = oVRCamera.gameObject.transform;
		goCanvas.transform.position = new Vector3 (0, 4, -4); // arbitrary, works well for me

		Canvas canvas = goCanvas.AddComponent<Canvas> ();
		canvas.renderMode = RenderMode.WorldSpace;

		GameObject goText = new GameObject ("Text");
		goText.transform.parent = goCanvas.transform;
		goText.transform.position = Vector3.zero;
		text = goText.AddComponent<Text> ();
		text.fontSize = 30;
		text.color = Color.black;
		RectTransform tr = goText.GetComponent<RectTransform> ();
		tr.localScale = new Vector3 (0.01f, 0.01f, 0.01f);
		tr.sizeDelta = new Vector2 (1000, 1000);
	}

	public static void Log (object log)
	{
		if (text == null) {
			// No OVR camera, fallback to normal logging
			Debug.Log (log);
			return;
		}

		// add the log to the queue
		string s = log.ToString ();
		logs.Add (s);
		// make sure we don't keep too many logs
		if (logs.Count > nLogs)
			logs.RemoveAt (0);
		PrintLogs ();
	}

	private static void PrintLogs ()
	{
		string s = "";
		foreach (string k in logs) {
			s += k + "\n";
		}
		text.text = s;
	}
}
