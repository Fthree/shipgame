using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class MapBuilder : MonoBehaviour {

    int tileSize = 86;

	public List<Wave> buildMap(IntVector2 startPosition, IntVector2 endPosition, List<WavesAndWeights> wavesAndWeights, Transform parent)
    {
        IntVector2 current = new IntVector2(startPosition);
        List<IntVector2> mapCoordinates = new List<IntVector2>();
        buildCoordinates(startPosition, endPosition, current, mapCoordinates);

        List<Wave> waves = new List<Wave>();//get waves and weights, add a wave of that type by the weight value and shuffle the whole deck

        //We want to add the wave to the waves array by the weight count
        foreach(var waveAndWeight in wavesAndWeights)
        {
            Wave currentWave = waveAndWeight.wave;
            int weight = waveAndWeight.weight;

            waves.AddRange(Enumerable.Repeat(currentWave, weight));
        }

        //Shuffle array to make sure that we end up with a more random result every time
        waves = waves.OrderBy(x => new System.Random().Next()).ToList();

        List<Wave> ret = new List<Wave>();
        mapCoordinates.ForEach(delegate (IntVector2 coord)
        {
            Wave wave = Instantiate(waves[Random.Range(0, waves.Count)]);
            wave.transform.position = new Vector3(coord.x, coord.y, 0);
            wave.transform.parent = parent;
            ret.Add(wave);
        });

        return ret;
    }

    private void buildCoordinates(IntVector2 startPosition, IntVector2 endPosition, IntVector2 current, List<IntVector2> coordinates)
    {
        //Add the current coordinates
        coordinates.Add(new IntVector2(current.x, current.y));

        IntVector2 newCoord = new IntVector2(current);

        //Move one space to the right
        newCoord.x += tileSize;

        //Have we hit the right edge?
        if (newCoord.x > endPosition.x)
        {
            //Reset X to the start position left
            newCoord.x = startPosition.x;
            //Move one space up
            newCoord.y += tileSize;
        }

        //Have we now gone past the very top?
        if (newCoord.y > endPosition.y)
        {
            //leave the method, all coordinates have been added
            return;
        }

        //Push the new coordinates as the next coordinate to work on and recursively build
        buildCoordinates(startPosition, endPosition, newCoord, coordinates);
    }
}
