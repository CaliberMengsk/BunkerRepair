using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObject : MonoBehaviour
{
	public GameObject currentSelection = null;
	public Material outlineMaterial;
	public static SelectObject instance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonUp("Fire1"))
		{
			RaycastHit hit;
			Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(mouseRay, out hit, Mathf.Infinity))
			{
				if (hit.collider.tag == "Selectable")
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
					currentSelection = hit.collider.gameObject;
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
		}
    }
}
