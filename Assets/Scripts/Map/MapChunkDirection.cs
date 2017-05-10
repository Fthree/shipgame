public static class MapChunkDirection
{
    public static IntVector2[] vectorDirections =
    {
        new IntVector2(0, 0), //MIDDLE
        new IntVector2(-1, 0), //LEFT
        new IntVector2(1, 0), //RIGHT
        new IntVector2(0, 1), //TOP
        new IntVector2(0, -1), //BOTTOM
        new IntVector2(1, 1), //TOP_RIGHT
        new IntVector2(-1, 1), //TOP_LEFT
        new IntVector2(1, -1), //BOTTOM_RIGHT
        new IntVector2(-1, -1) //BOTTOM_LEFT
    };

    public static string[] directionNames =
    {
        "MIDDLE",
        "LEFT",
        "RIGHT",
        "TOP",
        "BOTTOM",
        "TOP_RIGHT",
        "TOP_LEFT",
        "BOTTOM_RIGHT",
        "BOTTOM_LEFT"
    };

    public static string getName(this MapChunkPosition position)
    {
        return directionNames[(int)position];
    }

    public static IntVector2 getVectorDirection(this MapChunkPosition position, int offsetSize)
    {
        //Get specific vector for the position (mul by offset size to get the real position)
        IntVector2 direction = vectorDirections[(int)position];
        return direction * offsetSize;
    } 
}
