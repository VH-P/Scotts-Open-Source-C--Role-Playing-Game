using System;
using Engine.Models;

namespace Engine.Actions
{
    public class AttackWithWeapon : BaseAction, IAction
    {
        private readonly int _minimumDamage;
        private readonly int _maximumDamage;
        public AttackWithWeapon(GameItem itemInUse, int minimumDamage, int maximumDamage):base(itemInUse)
        {
            if(itemInUse.Category != GameItem.ItemCategory.Weapon) 
            {
                throw new ArgumentException($"{itemInUse.Name} is not a weapon.");
            }
            if (minimumDamage < 0)
            {
                throw new ArgumentException("minimumDamage must be equal or higher than 0.");
            }
            if (maximumDamage < minimumDamage)
            {
                throw new ArgumentException("maximumDamage must be equal or higher than minimumDamage.");
            }
            _minimumDamage = minimumDamage;
            _maximumDamage = maximumDamage;
        }
        public void Execute(LivingEntity actor, LivingEntity target)
        {
            int damage= RandomNumberGenerator.NumberBetween(_minimumDamage, _maximumDamage);
            string actorName = (actor is Player) ? "You" : $"the {actor.Name.ToLower()}";
            string targetName = (target is Player) ? "you" : $"the {target.Name.ToLower()}";
            if(damage == 0) 
            {
                ReportResult($"{actor.Name} missed {target.Name}.");
            }
            else
            {
                ReportResult($"{actor.Name} hit {target.Name} for {damage} point{(damage > 1 ? "s" : "")}.");
                target.TakeDamage(damage);
            }
        }
    }
}
