namespace Assets.Scripts.NewGameScripts.Enemy.EnemyDecorators.Decorators
{
    public class AlwaysJumpingEnemyDecorator : AEnemyDecorator
    {
        public AlwaysJumpingEnemyDecorator(AEnemy enemy)
        : base(enemy) { }

        public override bool IsGoingToJump()
        {
            return true;
        }
    }
}
