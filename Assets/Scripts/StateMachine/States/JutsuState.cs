using UnityEngine;

public class JutsuState : BaseState
{
    private BaseJutsuData _jutsuData;
    private JutsuContext _context;
    private BaseJutsu _jutsu;
    
    public int startFrame = 0;
    public int endFrame = 0;
    
    /// <summary>
    /// We need some kind of frame data System here to handle the jutsu execution and update and exiting.
    /// </summary>
    /// <param name="fsm"></param>
    
    public JutsuState(FSM fsm) : base(fsm)
    {
        
    }

    public void PrepareJutsu(BaseJutsuData jutsuData, JutsuContext context)
    {
        _jutsuData = jutsuData;
        _context = context;
    }

    public override void EnterState()
    {
        _jutsu = _jutsuData.CreateJutsu();
        _jutsu.StartJutsu(_context);
        _context.playerContext.animatable.animator.Play("LightningBladeAttackInit");
        _context.playerContext.animatable.animator.SetTrigger("JutsuStart");

    }

    public override void UpdateState()
    {
        _jutsu.UpdateJutsu(_context);
    }

    public override void ExitState()
    {
        // We need a end jutsu here and a transition to idel
        
        _fsm.RequestStateChange(_fsm.StateFactory.IdleState);
    }
    
    
}