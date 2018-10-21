using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using Mapbox.Unity.Utilities;

public class MapEffects : MonoBehaviour
{

	private AbstractMap _map;

	[SerializeField]
	private Slider slider;

	// Use this for initialization
	void Start()
	{  
		_map = GameObject.Find("Map").GetComponent<AbstractMap>();  
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void SliderChanged()
	{
		_map.SetZoom(slider.value);
    _map.UpdateMap(_map.CenterLatitudeLongitude, _map.Zoom);
	}
}
