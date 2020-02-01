using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
	public GameObject repairPanel, repairButtonPrefab, personPrefab;
	public List<Button> repairButtons;
	public List<Person> people = new List<Person>();
	public List<Texture> proficiencyIcons = new List<Texture>();

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
		newPersonButton.transform.parent = repairPanel.transform;
		RectTransform newRect = newPersonButton.GetComponent<RectTransform>();
		newRect.anchorMin = new Vector2(0, 1);
		newRect.anchorMax = new Vector2(1, 1);
		newRect.offsetMin = new Vector2(20, (-repairButtons.Count * 30)-60);
		newRect.offsetMax = new Vector2(-20, (-repairButtons.Count * 30)-30);
		repairButtons.Add(newPersonButton);

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