using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Neightbour
{
    public List<Vector3> NeighbourType = new List<Vector3>() { new Vector3(1, 0, 0), new Vector3(1, 0, 1), new Vector3(1, 0, -1), new Vector3(-1, 0, 1), new Vector3(-1, 0, 0), new Vector3(0, 0, 1), new Vector3(0, 0, -1), new Vector3(-1, 0, -1) };
}
