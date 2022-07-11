using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

	public float Sensitivity
	{
		get { return sensitivity; }
		set { sensitivity = value; }
	}
	[Range(0.1f, 9f)] [SerializeField] float sensitivity = 2f;
	[Tooltip("Limits vertical camera rotation. Prevents the flipping that happens when rotation goes above 90.")]
	[Range(0f, 90f)] [SerializeField] float yRotationLimit = 88f;

	Vector2 rotation = Vector2.zero;
	const string xAxis = "Mouse X"; //Strings in direct code generate garbage, storing and re-using them creates no garbage
	const string yAxis = "Mouse Y";

	void Start()
	{
		Cursor.visible = false;
	}

	void Update()
	{
		// -------------------Code for Zooming Out------------
		if (Input.GetMouseButton(1))
		{
			if (Camera.main.fieldOfView > 16)
				Camera.main.fieldOfView -= 2;
			if (Camera.main.orthographicSize >= 2)
				Camera.main.orthographicSize -= 0.5f;
			

		}
        else
		{
			if (Camera.main.fieldOfView <= 80)
				Camera.main.fieldOfView += 2;
			if (Camera.main.orthographicSize <= 10)
				Camera.main.orthographicSize += 0.5f;
		}

		// -------Code to switch camera between Perspective and Orthographic--------
		if (Input.GetKeyUp(KeyCode.B))
		{
			if (Camera.main.orthographic == true)
				Camera.main.orthographic = false;
			else
				Camera.main.orthographic = true;
		}

		rotation.x += Input.GetAxis(xAxis) * sensitivity;
		rotation.y += Input.GetAxis(yAxis) * sensitivity;
		rotation.y = Mathf.Clamp(rotation.y, -yRotationLimit, yRotationLimit);
		var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
		var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);

		transform.localRotation = xQuat * yQuat; //Quaternions seem to rotate more consistently than EulerAngles. Sensitivity seemed to change slightly at certain degrees using Euler. transform.localEulerAngles = new Vector3(-rotation.y, rotation.x, 0);
	}
}