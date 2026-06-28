using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


//THis is data for the player. We will expand this to hold different buffs based on the ninja created and set.
[CreateAssetMenu(fileName = "NinjaData", menuName = "Data/Ninja", order = 0)]
public class NinjaData : ScriptableObject
{
    public string Name;
    public List<BaseJutsuData> JutsuList = new();
    public int chakra;
    public float health;
    public float speed;
}