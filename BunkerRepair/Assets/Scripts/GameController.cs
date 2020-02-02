using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
	public GameObject repairPanel, repairButtonPrefab, personPrefab;
	public List<Button> repairButtons;
	public List<Person> people = new List<Person>();
	public Texture foodIcon, waterIcon, scienceIcon, engeneeringIcon;

	public static GameController instance;
    // Start is called before the first frame update
    void Start()
    {
		instance = this;
		for (int i = 0; i < 3; i++)
		{
			GeneratePerson();
		}
		
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void GeneratePerson()
	{
		GameObject newPersonObject = Instantiate(personPrefab, Vector3.zero, Quaternion.identity);
		Person newPerson = newPersonObject.GetComponent<Person>();
		people.Add(newPerson);
		GameObject newPersonButtonObject = Instantiate(repairButtonPrefab, Vector3.zero, Quaternion.identity);
		Button newPersonButton = newPersonButtonObject.GetComponent<Button>();
		newPerson.repairButton = newPersonButton;
		newPersonButton.transform.parent = repairPanel.transform;
		RectTransform newRect = newPersonButton.GetComponent<RectTransform>();
		newRect.anchorMin = new Vector2(0, 1);
		newRect.anchorMax = new Vector2(1, 1);
		newRect.offsetMin = new Vector2(20, (-repairButtons.Count * 30)-60);
		newRect.offsetMax = new Vector2(-20, (-repairButtons.Count * 30)-30);
		newPersonButton.onClick.AddListener(delegate { ClickWorkerButton(newPerson); });


		repairButtons.Add(newPersonButton);

	}

	void ClickWorkerButton(Person personToDo)
	{
		Image repairImage = personToDo.repairButton.gameObject.transform.Find("CurrentlyRepairing").GetComponent<Image>();
		repairImage.sprite = SelectObject.instance.currentSelection.GetComponent<SelectableObject>().icon;
	}
}
public enum RepairType
{
	None,
	Bunker,
	Water,
	Farm,
	Generator
}

public enum RepairStrength
{
	Charm,
	Farming,
	Engineering,
	Chemical
}