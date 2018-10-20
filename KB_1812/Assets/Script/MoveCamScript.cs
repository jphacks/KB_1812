using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamScript : MonoBehaviour {

    [SerializeField]
    private TrackPadScript _trackpad = null;

    //移動速度
    public float speed = 0.5f;
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = transform.position;

        pos.x += _trackpad.Position.x * speed;
        pos.z += _trackpad.Position.y * speed;

        transform.position = pos;
		
	}
}
