using Google.Protobuf;
using OddDotNet.Proto.Common.V1;
using OddDotNet.Proto.Metrics.V1;

namespace OddDotCSharp
{
    /// <summary>
    /// Configurator for properties specific to a Metric Gauge.
    /// </summary>
    public class WhereMetricGaugeFilterConfigurator
    {
        /// <summary>
        /// Use this to access properties specific to the DataPoint of this Metric Gauge.
        /// </summary>
        public WhereMetricGaugeDataPointFilterConfigurator DataPoint { get; }
        internal WhereMetricGaugeFilterConfigurator(WhereMetricFilterConfigurator configurator)
        {
            DataPoint = new WhereMetricGaugeDataPointFilterConfigurator(configurator);
        }
    }
}