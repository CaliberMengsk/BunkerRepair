using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableObject : MonoBehaviour
{
	public string title = "Ain't no title here brah";
	public float health = 100, output = 100, decayRate = 1f;
	public AnimationCurve outputCurve;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		health -= decayRate * Time.deltaTime;
		output = outputCurve.Evaluate(health/100)*100;
    }
}
