using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectObject : MonoBehaviour
{
	public GameObject currentSelection = null;
	public Material outlineMaterial;
	public static SelectObject instance;

	public GameObject selectableObjectPanel,repairPanel;
	public Text selectableObjectTitle, selectableObjectHealth, selectableObjectOutput;

	public ColorBlock repairingColors, notRepairingColors;
    // Start is called before the first frame update
    void Start()
    {
		instance = this;
		InvokeRepeating("UpdateSelectedPanel", .5f, .5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonUp("Fire1"))
		{
			RaycastHit hit;
			
			Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

			

			List<RaycastResult> uiOut = new List<RaycastResult>();
			PointerEventData newData = new PointerEventData(EventSystem.current);
			newData.position = Input.mousePosition;
			EventSystem.current.RaycastAll(newData, uiOut);
			if (uiOut.Count <= 0)
			{
				if (currentSelection != null)
				{
					foreach (MeshRenderer mr in currentSelection.GetComponentsInChildren<MeshRenderer>())
					{
						for (int i = 0; i < mr.materials.Length; i++)
						{
							List<Material> matArray = new List<Material>(mr.materials);
							int index = -1;
							for (int j = 0; j < matArray.Count; j++)
							{
								if (matArray[j].name == outlineMaterial.name + " (Instance)")
								{
									index = j;
								}
							}
							if (index != -1)
							{
								matArray.RemoveAt(index);
								mr.materials = matArray.ToArray();
							}
						}
					}
				}
				if (Physics.Raycast(mouseRay, out hit, Mathf.Infinity))
				{

					if (hit.collider.tag == "Selectable")
					{
						currentSelection = hit.collider.gameObject;
						UpdatePersonButtons();
						UpdateSelectedPanel();
						foreach (MeshRenderer mr in currentSelection.GetComponentsInChildren<MeshRenderer>())
						{
							bool setOutline = false;
							for (int i = 0; i < mr.materials.Length; i++)
							{
								if (mr.materials[i] == null)
								{
									mr.materials[i] = outlineMaterial;
									setOutline = true;
								}
							}
							if (setOutline == false)
							{
								List<Material> matArray = new List<Material>(mr.materials);
								matArray.Add(outlineMaterial);
								mr.materials = matArray.ToArray();
							}
						}
					}
				}
				else
				{
					currentSelection = null;
					UpdateSelectedPanel();
				}
			}
		}
    }

	public void UpdateSelectedPanel()
	{
		if(currentSelection != null)
		{
			SelectableObject so = currentSelection.GetComponent<SelectableObject>();
			if (so)
			{
				if (repairPanel.activeSelf)
				{
					selectableObjectPanel.SetActive(false);
				}
				else { 
					selectableObjectPanel.SetActive(true);
				}
				selectableObjectTitle.text = so.title;
				selectableObjectHealth.text = "Health: " + (so.health/100).ToString("00.00%");
				selectableObjectOutput.text = "Output: " + (so.output/100).ToString("00.00%");
			}
			else
			{
				selectableObjectPanel.SetActive(false);
			}
		}
		else
		{
			selectableObjectPanel.SetActive(false);
		}
	}

	public void UpdatePersonButtons()
	{
		foreach (Person p in FindObjectsOfType<Person>())
		{
			if (p.repairingObject == currentSelection)
			{
				p.repairButton.colors = repairingColors;
			}
			else
			{
				p.repairButton.colors = notRepairingColors;
			}
		}
	}
}
