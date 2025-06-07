using SteelProtocol.UI.HUD;
using SteelProtocol.Data.Enum;

namespace SteelProtocol.Controller.Tank.Common.HP
{
    public class EnemyHealthController : HealthController
    {
        public override void TakeDamage(float damage, Faction sourceFaction)
        {
            base.TakeDamage(damage);

            if (sourceFaction == Faction.Player)
            {
                var hud = FindFirstObjectByType<AiHealthbarController>();
                if (hud != null)
                    hud.ShowEnemyHealth(currentHealth, maxHealth);
            }
            
        }
    }
}