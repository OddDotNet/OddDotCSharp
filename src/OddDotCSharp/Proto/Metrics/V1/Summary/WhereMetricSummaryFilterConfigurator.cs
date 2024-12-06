namespace OddDotCSharp
{
    /// <summary>
    /// Configurator for properties specific to a Metric Summary.
    /// </summary>
    public class WhereMetricSummaryFilterConfigurator
    {
        /// <summary>
        /// Use this to access properties specific to the DataPoint of this Summary.
        /// </summary>
        public WhereMetricSummaryDataPointFilterConfigurator DataPoint { get; }

        internal WhereMetricSummaryFilterConfigurator(WhereMetricFilterConfigurator configurator)
        {
            DataPoint = new WhereMetricSummaryDataPointFilterConfigurator(configurator);
        }
    }
}