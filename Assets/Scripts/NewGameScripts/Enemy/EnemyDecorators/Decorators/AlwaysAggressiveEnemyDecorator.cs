namespace Assets.Scripts.NewGameScripts.Enemy.EnemyDecorators.Decorators
{
    public class AlwaysAggressiveEnemyDecorator : AEnemyDecorator
    {
        public AlwaysAggressiveEnemyDecorator(AEnemy enemy)
            : base(enemy) { }

        public override bool GetAggression()
        {
            return true;
        }
    }
}
