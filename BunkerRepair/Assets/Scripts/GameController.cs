using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
	public int peopleCount = 6;
	public GameObject repairPanel, repairButtonPrefab, personPrefab;
	public GameObject gameOverScreen, gameWinScreen;
	public List<Button> repairButtons;
	public List<Person> people = new List<Person>();
	public Sprite foodIcon, waterIcon, scienceIcon, engeneeringIcon;

	public bool gameOver = false;
	public bool gameWin = false;

	public static GameController instance;
    // Start is called before the first frame update
    void Start()
    {
		instance = this;
		for (int i = 0; i < peopleCount; i++)
		{
			GeneratePerson();
		}
		
    }

    // Update is called once per frame
    void Update()
    {
        if(gameOver)
		{
			gameOverScreen.SetActive(true);
			Time.timeScale = 0;
		}
		else if (gameWin)
		{
			gameWinScreen.SetActive(true);
			Time.timeScale = 0;
		}
		else
		{
			Time.timeScale = 1;
		}
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
		personToDo.repairingObject = SelectObject.instance.currentSelection;
		EventSystem.current.SetSelectedGameObject(null);
		SelectObject.instance.UpdatePersonButtons();
	}

	public void RestartLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void MainMenu()
	{
		SceneManager.LoadScene(0);
	}

	public void ExitGame()
	{
		Application.Quit();
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