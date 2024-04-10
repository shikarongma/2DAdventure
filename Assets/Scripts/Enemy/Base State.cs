public abstract class BaseState
{
    protected Enemy currentEnemy;
    //����
    public abstract void OnEnter(Enemy enemy);
    //boolֵ�ж�
    public abstract void LogicUpdate();
    //�����ж�
    public abstract void PhysicsUpdate();
    //�˳�
    public abstract void OnExit();

}
