namespace OddDotCSharp
{
    public class WhereMetricExponentialHistogramDataPointBucketFilterConfigurator
    {
        private readonly WhereMetricFilterConfigurator _configurator;
        private readonly ExponentialHistogramDataPointBucket _bucketType;
        public WhereMetricExponentialHistogramDataPointBucketFilterConfigurator(
            WhereMetricFilterConfigurator configurator, ExponentialHistogramDataPointBucket bucketType)
        {
            _configurator = configurator;
            _bucketType = bucketType;
        }
    }

    public enum ExponentialHistogramDataPointBucket
    {
        Positive,
        Negative
    }
}