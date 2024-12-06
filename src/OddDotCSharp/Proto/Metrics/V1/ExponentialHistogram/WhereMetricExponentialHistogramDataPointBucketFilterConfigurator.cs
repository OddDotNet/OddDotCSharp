using OddDotNet.Proto.Common.V1;
using OddDotNet.Proto.Metrics.V1;

namespace OddDotCSharp
{
    /// <summary>
    /// Allows for configuring filters related to the DataPoint Bucket of an ExponentialHistogram.
    /// </summary>
    public class WhereMetricExponentialHistogramDataPointBucketFilterConfigurator
    {
        private readonly WhereMetricFilterConfigurator _configurator;
        private readonly ExponentialHistogramDataPointBucket _bucketType;
        internal WhereMetricExponentialHistogramDataPointBucketFilterConfigurator(
            WhereMetricFilterConfigurator configurator, ExponentialHistogramDataPointBucket bucketType)
        {
            _configurator = configurator;
            _bucketType = bucketType;
        }
        
        /// <summary>
        /// Adds a filter for Offset to the list of filters.
        /// </summary>
        /// <param name="compare">The int to compare the Offset against.</param>
        /// <param name="compareAs">The type of comparison to perform.</param>
        /// <returns>this <see cref="WhereMetricFilterConfigurator"/></returns>
        public WhereMetricFilterConfigurator AddOffsetFilter(int compare, NumberCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    ExponentialHistogram = new ExponentialHistogramFilter
                    {
                        DataPoint = new ExponentialHistogramDataPointFilter
                        {
                            
                        }
                    }
                }
            };
            
            switch(_bucketType)
            {
                case ExponentialHistogramDataPointBucket.Positive:
                    filter.Property.ExponentialHistogram.DataPoint.Positive = new BucketFilter
                    {
                        Offset = new Int32Property
                        {
                            CompareAs = compareAs,
                            Compare = compare
                        }
                    };
                    break;
                case ExponentialHistogramDataPointBucket.Negative:
                    filter.Property.ExponentialHistogram.DataPoint.Negative = new BucketFilter
                    {
                        Offset = new Int32Property
                        {
                            CompareAs = compareAs,
                            Compare = compare
                        }
                    };
                    break;
            }
            
            _configurator.Filters.Add(filter);
            return _configurator;
        }
        
        /// <summary>
        /// Adds a filter for BucketCount to the list of filters.
        /// </summary>
        /// <param name="compare">The ulong to compare the BucketCount against.</param>
        /// <param name="compareAs">The type of comparison to perform.</param>
        /// <returns>this <see cref="WhereMetricFilterConfigurator"/></returns>
        public WhereMetricFilterConfigurator AddBucketCountFilter(ulong compare, NumberCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    ExponentialHistogram = new ExponentialHistogramFilter
                    {
                        DataPoint = new ExponentialHistogramDataPointFilter
                        {
                            
                        }
                    }
                }
            };
            
            switch(_bucketType)
            {
                case ExponentialHistogramDataPointBucket.Positive:
                    filter.Property.ExponentialHistogram.DataPoint.Positive = new BucketFilter
                    {
                        BucketCount = new UInt64Property
                        {
                            CompareAs = compareAs,
                            Compare = compare
                        }
                    };
                    break;
                case ExponentialHistogramDataPointBucket.Negative:
                    filter.Property.ExponentialHistogram.DataPoint.Negative = new BucketFilter
                    {
                        BucketCount = new UInt64Property
                        {
                            CompareAs = compareAs,
                            Compare = compare
                        }
                    };
                    break;
            }
            
            _configurator.Filters.Add(filter);
            return _configurator;
        }
    }

    internal enum ExponentialHistogramDataPointBucket
    {
        Positive,
        Negative
    }
}