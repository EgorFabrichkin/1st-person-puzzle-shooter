using GameCore.Weapons.Projectiles;

namespace GameCore.Weapons
{
    public interface IWeapon
    {
        public void TryShoot(bool flag, ScaleState scaleState);
    }
}