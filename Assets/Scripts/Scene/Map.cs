using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour {

    public int minSize;
    public int maxSize;

    public List<Wave> waves = new List<Wave>();
    private List<Wave> waveInstances = new List<Wave>();

    IntVector2 bottomLeft;
    IntVector2 topRight;

	// Use this for initialization
	void Start () {
        bottomLeft = new IntVector2(-minSize, -maxSize);
        topRight = new IntVector2(minSize, maxSize);
        MapBuilder builder = new MapBuilder();
        waveInstances = builder.buildMap(bottomLeft, topRight, waves, transform);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
