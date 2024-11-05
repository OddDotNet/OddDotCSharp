using Google.Protobuf;
using OddDotNet.Proto.Common.V1;
using OddDotNet.Proto.Metrics.V1;

namespace OddDotCSharp
{
    public class WhereMetricGaugeFilterConfigurator
    {
        public WhereMetricGaugeDataPointFilterConfigurator DataPoint { get; }
        public WhereMetricGaugeFilterConfigurator(WhereMetricFilterConfigurator configurator)
        {
            DataPoint = new WhereMetricGaugeDataPointFilterConfigurator(configurator);
        }
    }
}