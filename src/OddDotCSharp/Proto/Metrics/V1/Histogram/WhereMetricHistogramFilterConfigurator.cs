using OddDotNet.Proto.Common.V1;
using OddDotNet.Proto.Metrics.V1;
using OpenTelemetry.Proto.Metrics.V1;

namespace OddDotCSharp
{
    /// <summary>
    /// Configurator for properties specific to a Histogram.
    /// </summary>
    public class WhereMetricHistogramFilterConfigurator
    {
        private readonly WhereMetricFilterConfigurator _configurator;
        
        /// <summary>
        /// Use this to access properties specific to the DataPoint of this Histogram.
        /// </summary>
        public WhereMetricHistogramDataPointFilterConfigurator DataPoint { get; }

        internal WhereMetricHistogramFilterConfigurator(WhereMetricFilterConfigurator configurator)
        {
            _configurator = configurator;
            DataPoint = new WhereMetricHistogramDataPointFilterConfigurator(configurator);
        }
        
        /// <summary>
        /// Adds a filter for AggregationTemporality to the list of filters.
        /// </summary>
        /// <param name="compare">The enum to compare the AggregationTemporality against.</param>
        /// <param name="compareAs">The type of comparison to perform.</param>
        /// <returns>this <see cref="WhereMetricFilterConfigurator"/></returns>
        public WhereMetricFilterConfigurator AddAggregationTemporalityFilter(AggregationTemporality compare, EnumCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Histogram = new HistogramFilter
                    {
                        AggregationTemporality = new AggregationTemporalityProperty
                        {
                            CompareAs = compareAs,
                            Compare = compare
                        }
                    }
                }
            };
            
            _configurator.Filters.Add(filter);
            return _configurator;
        }
    }
}