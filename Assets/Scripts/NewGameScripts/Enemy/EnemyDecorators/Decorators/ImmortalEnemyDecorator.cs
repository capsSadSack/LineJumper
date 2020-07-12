namespace Assets.Scripts.NewGameScripts.Enemy.EnemyDecorators.Decorators
{
    public class ImmortalEnemyDecorator : AEnemyDecorator
    {
        public override Enemy EnemyType => Enemy.Immortal;

        public ImmortalEnemyDecorator(AEnemy enemy)
            : base(enemy) { }
    }
}
