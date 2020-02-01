using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public float moveSpeed = 3, targetHeight = 10;
	float smoothingMultiplier = 3;

	Vector3 targetPosition = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
		targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
		targetPosition.x += Input.GetAxisRaw("Horizontal") * moveSpeed;
		targetPosition.y = targetHeight;
		targetPosition.z += Input.GetAxisRaw("Vertical") * moveSpeed;

		transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime*smoothingMultiplier);
    }
}
