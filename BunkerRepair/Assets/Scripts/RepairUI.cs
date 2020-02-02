using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RepairUI : MonoBehaviour
{
	public bool showPanel = false;
	public GameObject[] hideObjects;
    // Start is called before the first frame update
    void Start()
    {
        
    }

	// Update is called once per frame
	void Update()
	{
		if (showPanel && Input.GetButtonUp("Fire1"))
		{

			List<RaycastResult> uiOut = new List<RaycastResult>();
			PointerEventData newData = new PointerEventData(EventSystem.current);
			newData.position = Input.mousePosition;
			EventSystem.current.RaycastAll(newData, uiOut);
			if (uiOut.Count <= 0)
			{
				showPanel = false;
			}
		}
		if (showPanel)
		{
			gameObject.SetActive(true);
			foreach(GameObject hideObject in hideObjects)
			{
				hideObject.SetActive(false);
			}
		} else { 
			gameObject.SetActive(false);
		}
	}

	public void ShowPanel(bool state)
	{
		showPanel = state;
		gameObject.SetActive(true);
	}

}
