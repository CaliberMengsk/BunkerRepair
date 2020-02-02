using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Person : MonoBehaviour
{
	public int health = 100, stamina = 100;
	public float charm = 5, farming = 5, engineering = 5, chemical = 5, skillModifier = .8f;
	public GameObject repairingObject;
	public Button repairButton;
	public RepairStrength proficiency;
    // Start is called before the first frame update
    void Start()
    {
		GenerateStats();
		GameController.instance.people.Add(this);
		InvokeRepeating("Repair", 1f, 1f);
    }

    // Update is called once per frame
    void Repair()
	{
		if (repairingObject != null)
		{
			SelectableObject so = repairingObject.GetComponent<SelectableObject>();
			if (proficiency == so.expertieseNeeded)
			{
				if (proficiency == RepairStrength.Charm)
				{
					so.health += charm;
				}
				if(proficiency == RepairStrength.Chemical)
				{
					so.health += chemical;
				}
				if(proficiency == RepairStrength.Engineering)
				{
					so.health += engineering;
				}
				if (proficiency == RepairStrength.Farming)
				{
					so.health += chemical;
				}
			}

			so.health += charm * Random.Range(0f, .25f);
			so.health += chemical * Random.Range(0f, .25f);			
			so.health += engineering * Random.Range(0f, .25f);
			so.health += chemical * Random.Range(0f, .25f);
			so.health = Mathf.Clamp(so.health, 0, 100);
		}

	}

	void GenerateStats()
	{
		charm -= Random.Range(0f, 5f);
		farming -= Random.Range(0f, 5f);
		engineering -= Random.Range(0f, 5f);
		chemical -= Random.Range(0f, 5f);
		Image proficiencyImage = repairButton.gameObject.transform.Find("Proficiency").GetComponent<Image>();

		if (charm < farming)
		{
			proficiency = RepairStrength.Farming;
			proficiencyImage.sprite = GameController.instance.foodIcon;
			charm *= Random.Range(.4f, .75f);
			engineering *= Random.Range(.4f, .75f);
			chemical *= Random.Range(.4f, .75f);
		}
		else if (charm < engineering)
		{
			proficiency = RepairStrength.Engineering;
			proficiencyImage.sprite = GameController.instance.engeneeringIcon;
			charm *= Random.Range(.4f, .75f);
			farming *= Random.Range(.4f, .75f);
			chemical *= Random.Range(.4f, .75f);
		}
		else if (charm < chemical)
		{
			proficiency = RepairStrength.Chemical;
			proficiencyImage.sprite = GameController.instance.waterIcon;
			charm *= Random.Range(.4f, .75f);
			farming *= Random.Range(.4f, .75f);
			engineering *= Random.Range(.4f, .75f);
		}
		else
		{
			proficiency = RepairStrength.Charm;
			proficiencyImage.sprite = GameController.instance.scienceIcon;
			farming *= Random.Range(.4f, .75f);
			engineering *= Random.Range(.4f, .75f);
			chemical *= Random.Range(.4f, .75f);
		}

		charm *= skillModifier;
		farming *= skillModifier;
		engineering *= skillModifier;
		chemical *= skillModifier;

		string[] lines = File.ReadAllLines(Application.dataPath + "/StreamingAssets/names.txt");
		repairButton.GetComponentInChildren<Text>().text = lines[Random.Range((int)0, (int)lines.Length)];
	}
}

