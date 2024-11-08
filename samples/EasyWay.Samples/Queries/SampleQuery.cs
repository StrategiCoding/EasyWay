namespace EasyWay.Samples.Queries
{
    public class SampleQuery : Query<SampleModule, SampleQueryResult>
    {
        public string Name { get; set; }
    }
}
