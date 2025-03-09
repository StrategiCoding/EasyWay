﻿namespace EasyWay.Samples.Queries
{
    public class SampleQuery : Query<SampleQueryResult>
    {
        public required string Name { get; init; }

        public required int Age { get; init; }
    }
}
