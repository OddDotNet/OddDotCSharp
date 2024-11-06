namespace OddDotCSharp
{
    public class WhereMetricSummaryFilterConfigurator
    {
        public WhereMetricSummaryDataPointFilterConfigurator DataPoint { get; }

        public WhereMetricSummaryFilterConfigurator(WhereMetricFilterConfigurator configurator)
        {
            DataPoint = new WhereMetricSummaryDataPointFilterConfigurator(configurator);
        }
    }
}