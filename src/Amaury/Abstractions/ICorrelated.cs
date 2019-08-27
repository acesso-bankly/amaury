namespace Amaury.Abstractions
{
    public interface ICorrelated
    {
        string CorrelationId { get; set; }
    }
}