using UnityEngine;
using System.Collections.Generic;

public class MapBuilder : MonoBehaviour {

    int tileSize = 86;

	public List<Wave> buildMap(IntVector2 startPosition, IntVector2 endPosition, List<Wave> waves, Transform parent)
    {
        IntVector2 current = new IntVector2(startPosition);
        List<IntVector2> mapCoordinates = new List<IntVector2>();
        buildCoordinates(startPosition, endPosition, current, mapCoordinates);

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
        coordinates.Add(new IntVector2(current.x, current.y));

        IntVector2 newCoord = new IntVector2(current);
        newCoord.x += tileSize;

        if (newCoord.x > endPosition.x)
        {
            newCoord.x = startPosition.x;
            newCoord.y += tileSize;
        }

        if (newCoord.y > endPosition.y)
        {
            return;
        }


        buildCoordinates(startPosition, endPosition, newCoord, coordinates);
    }
}
