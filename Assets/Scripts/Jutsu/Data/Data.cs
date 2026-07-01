using System;
using System.Collections.Generic;
using UnityEngine;






[CreateAssetMenu(fileName = "Genjutsu", menuName = "Jutsu/Genjutsu")]
public class Genjutsu : BaseJutsuData
{
    // this will either be the a projectile or the object using this move
    public override BaseJutsu CreateJutsu()
    {
        throw new NotImplementedException();
    }
}

[CreateAssetMenu(fileName = "TaiJutsu", menuName = "Jutsu/Taijutsu")]
public class Taijutsu : BaseJutsuData
{
    // this will either be the a projectile or the object using this move
    public override BaseJutsu CreateJutsu()
    {
        throw new NotImplementedException();
    }
}

[CreateAssetMenu(fileName = "Senjutsu", menuName = "Jutsu/Senjutsu")]
public class Senjutsu : BaseJutsuData
{
    // this will either be the a projectile or the object using this move
    public override BaseJutsu CreateJutsu()
    {
        throw new NotImplementedException();
    }
}

