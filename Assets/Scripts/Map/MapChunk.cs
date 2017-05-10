using UnityEngine;
using System;
using System.Collections.Generic;

public class MapChunk : MonoBehaviour {

    MapChunkPosition mapPosition;

    public void setPosition(MapChunkPosition newPosition)
    {
        mapPosition = newPosition;
        name = "chunk-" + MapChunkDirection.getName(mapPosition);
    }

    public void build(int chunkSize, IntVector2 offset, List<WavesAndWeights> wavesAndWeights, Transform parent)
    {
        transform.SetParent(parent);
        maxCoord = new IntVector2(chunkSize + offset.x, chunkSize + offset.y);
        minCoord = new IntVector2(-chunkSize + offset.x, -chunkSize + offset.y);
        new MapBuilder().buildMap(minCoord, maxCoord, wavesAndWeights, transform);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("We've hit a ship! I am " + name);
    }

    public IntVector2 minCoord;
    public IntVector2 midCoord;
    public IntVector2 maxCoord;
    public MapChunkPosition position;
}
