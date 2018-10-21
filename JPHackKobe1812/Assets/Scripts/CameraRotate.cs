using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{

	[SerializeField]
	private JoyStick _joystick = null;

	[SerializeField]
	private float speed = 1.0f;

	[SerializeField]
	GameObject camera;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		camera.transform.Rotate(Vector3.up * speed * _joystick.Position.x);
        camera.transform.Rotate(Vector3.right * -speed * _joystick.Position.y);
	}
}
