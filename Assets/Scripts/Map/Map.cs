using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour {

    public int size;
    public List<WavesAndWeights> waves = new List<WavesAndWeights>();
    MapChunkHandler chunkHandler;

    // Use this for initialization
    void Start () {
        //Create a chunkhandler that will spawn the chunks
        chunkHandler = new MapChunkHandler();
        chunkHandler.createChunks(waves, transform, size);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
