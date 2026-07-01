using UnityEngine;

public class GenericJutsu : BaseJutsu
{
    protected GenericJutsuData data;
    public GenericJutsu(GenericJutsuData data) : base(data)
    {
        this.data = data;
    }

    public override void StartJutsu(JutsuContext jutsuContext)
    {
        foreach (var effect in data.Effects)
        {
            effect.Apply(jutsuContext);
        }

    }

    public override void UpdateJutsu(JutsuContext jutsuContext)
    {
        foreach (var effect in data.Effects)
        {
            effect.Apply(jutsuContext);
        }
    }
    
}
