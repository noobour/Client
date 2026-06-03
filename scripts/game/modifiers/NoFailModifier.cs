public class NoFailModifier : Modifier, IFailModifier
{
    public override string Name => "NoFail";

    public bool IsFail => false;

    public bool CheckFailCondition(bool _hit, double _health) => false;
}
