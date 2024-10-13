namespace EasyWay
{
    public interface IPolicy<TConditionalData, TInputData>
    {
        bool IsApplicable(TConditionalData data);

        void Execute(TInputData data);
    }

    public interface IPolicy<TConditionalData, TInputData, TOutputData>
    {
        bool IsApplicable(TConditionalData data);

        TOutputData Execute(TInputData data);
    }
}
