using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Person : MonoBehaviour
{
	public int health = 100, stamina = 100;
	public float charm = 5, farming = 5, engineering = 5, chemical = 5;
	public GameObject repairingObject;
	public Button repairButton;
	public RepairStrength proficiency;
    // Start is called before the first frame update
    void Start()
    {
		GenerateStats();
		GameController.instance.people.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void GenerateStats()
	{
		charm += Random.Range(0f, 5f);
		farming += Random.Range(0f, 5f);
		engineering += Random.Range(0f, 5f);
		chemical += Random.Range(0f, 5f);

		
		if (charm < farming)
		{
			proficiency = RepairStrength.Farming;
			charm *= Random.Range(.4f, .75f);
			engineering *= Random.Range(.4f, .75f);
			chemical *= Random.Range(.4f, .75f);
		}
		else if (charm < engineering)
		{
			proficiency = RepairStrength.Engineering;
			charm *= Random.Range(.4f, .75f);
			farming *= Random.Range(.4f, .75f);
			chemical *= Random.Range(.4f, .75f);
		}
		else if (charm < chemical)
		{
			proficiency = RepairStrength.Chemical;
			charm *= Random.Range(.4f, .75f);
			farming *= Random.Range(.4f, .75f);
			engineering *= Random.Range(.4f, .75f);
		}
		else
		{
			proficiency = RepairStrength.Charm;
			farming *= Random.Range(.4f, .75f);
			engineering *= Random.Range(.4f, .75f);
			chemical *= Random.Range(.4f, .75f);
		}
	}
}

