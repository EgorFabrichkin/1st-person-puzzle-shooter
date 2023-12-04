namespace GameCore.Weapons.Projectiles
{
    public class Scaling
    {
        public ScaleState StateScale { get; }
        
        public Scaling(ScaleState stateScale)
        {
            StateScale = stateScale;
        }
    }
    
    public enum ScaleState
    {
        Decrease,
        Increase
    }
}