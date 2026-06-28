
public abstract class BaseJutsu
{
    BaseJutsuData jutsuData;
    
    public BaseJutsu(BaseJutsuData jutsuData)
    {
        this.jutsuData = jutsuData;
    }
    
    public abstract void StartJutsu(JutsuContext jutsuContext);
    public abstract void UpdateJutsu(JutsuContext jutsuContext);
    public virtual bool IsComplete { get; set; }
    public virtual bool CanCancel { get; set;  }
}

