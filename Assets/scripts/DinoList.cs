using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DinoList
{
    private static List<GameObject> dinos = new List<GameObject>();

    public static void AddDinoInList(GameObject weapon) => dinos.Add(weapon);
    public static int GetAmount() { return dinos.Count; }
    public static GameObject GetDino(int index) { return dinos[index]; }



}
