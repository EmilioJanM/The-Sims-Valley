using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Index: 0 = Hood, 1 = pelvis, 2 = left Shoulder, 3 = right shoulder, 4 = torso
[CreateAssetMenu(fileName = "newObject", menuName = "Item")]
public class ItemObject : ScriptableObject
{
    public Sprite image;
    public int price;
    public int index;
}