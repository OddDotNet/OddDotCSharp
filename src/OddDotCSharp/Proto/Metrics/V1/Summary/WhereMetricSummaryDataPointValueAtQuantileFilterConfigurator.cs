using OddDotNet.Proto.Common.V1;
using OddDotNet.Proto.Metrics.V1;

namespace OddDotCSharp
{
    public class WhereMetricSummaryDataPointValueAtQuantileFilterConfigurator
    {
        private readonly WhereMetricFilterConfigurator _configurator;

        public WhereMetricSummaryDataPointValueAtQuantileFilterConfigurator(WhereMetricFilterConfigurator configurator)
        {
            _configurator = configurator;
        }
        
        /// <summary>
        /// Adds a filter for Quantile to the list of filters.
        /// </summary>
        /// <param name="compare">The double to compare the Quantile against.</param>
        /// <param name="compareAs">The type of comparison to perform.</param>
        /// <returns>this <see cref="WhereMetricFilterConfigurator"/></returns>
        public WhereMetricFilterConfigurator AddQuantileFilter(double compare, NumberCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Summary = new SummaryFilter
                    {
                        DataPoint = new SummaryDataPointFilter
                        {
                            QuantileValue = new ValueAtQuantileFilter
                            {
                                Quantile = new DoubleProperty
                                {
                                    CompareAs = compareAs,
                                    Compare = compare
                                }
                            }
                        }
                    }
                }
            };
            
            _configurator.Filters.Add(filter);
            return _configurator;
        }
        
        /// <summary>
        /// Adds a filter for Value to the list of filters.
        /// </summary>
        /// <param name="compare">The double to compare the Value against.</param>
        /// <param name="compareAs">The type of comparison to perform.</param>
        /// <returns>this <see cref="WhereMetricFilterConfigurator"/></returns>
        public WhereMetricFilterConfigurator AddValueFilter(double compare, NumberCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Summary = new SummaryFilter
                    {
                        DataPoint = new SummaryDataPointFilter
                        {
                            QuantileValue = new ValueAtQuantileFilter
                            {
                                Value = new DoubleProperty
                                {
                                    CompareAs = compareAs,
                                    Compare = compare
                                }
                            }
                        }
                    }
                }
            };
            
            _configurator.Filters.Add(filter);
            return _configurator;
        }
    }
}