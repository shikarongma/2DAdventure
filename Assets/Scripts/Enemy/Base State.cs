public abstract class BaseState
{
    protected Enemy currentEnemy;
    //进入
    public abstract void OnEnter(Enemy enemy);
    //bool值判断
    public abstract void LogicUpdate();
    //物理判断
    public abstract void PhysicsUpdate();
    //退出
    public abstract void OnExit();

}
