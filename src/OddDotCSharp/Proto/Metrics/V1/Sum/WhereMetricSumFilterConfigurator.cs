using OddDotNet.Proto.Common.V1;
using OddDotNet.Proto.Metrics.V1;
using OpenTelemetry.Proto.Metrics.V1;

namespace OddDotCSharp
{
    /// <summary>
    /// Configurator for properties specific to a Metric Sum.
    /// </summary>
    public class WhereMetricSumFilterConfigurator
    {
        private readonly WhereMetricFilterConfigurator _configurator;
        
        /// <summary>
        /// Use this to access properties specific to the DataPoint of this Sum.
        /// </summary>
        public WhereMetricSumDataPointFilterConfigurator DataPoint { get; }
        internal WhereMetricSumFilterConfigurator(WhereMetricFilterConfigurator configurator)
        {
            _configurator = configurator;
            DataPoint = new WhereMetricSumDataPointFilterConfigurator(configurator);
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
                    Sum = new SumFilter
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
        
        /// <summary>
        /// Adds a filter for IsMonotonic to the list of filters.
        /// </summary>
        /// <param name="compare">The bool to compare the IsMonotonic against.</param>
        /// <param name="compareAs">The type of comparison to perform.</param>
        /// <returns>this <see cref="WhereMetricFilterConfigurator"/></returns>
        public WhereMetricFilterConfigurator AddIsMonotonicFilter(bool compare, BoolCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Sum = new SumFilter
                    {
                        IsMonotonic = new BoolProperty
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