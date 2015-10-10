using UnityEngine;
using System.Collections;

public class ClickHandler : MonoBehaviour {

	private static ClickHandler sInstance;
	public delegate void OnClickEvent(GameObject pGameObject);
	public event OnClickEvent OnClick;

	private ClickHandler(){
	}

	public static ClickHandler Instance(){
		if (sInstance == null) {
			sInstance = new ClickHandler ();
		}
		return sInstance;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// Ray
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		// Raycast Hit
		RaycastHit hit;
		
		if (Physics.Raycast(ray, out hit, 100))
		{
			// If we click it
			if (Input.GetMouseButtonUp(0))
			{
				// Notify of the event!
				OnClick(hit.transform.gameObject);
			}
		}
	}
}
