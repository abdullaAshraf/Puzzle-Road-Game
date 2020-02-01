using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Piece", order = 1)]
public class PieceInfo : ScriptableObject
{
    //                     L  T  R  B
    public int[] edges = { 0, 0, 0, 0 };

    public Sprite sprite;
}
