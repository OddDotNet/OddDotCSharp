using System.Collections.Generic;
using Google.Protobuf;
using OddDotNet.Proto.Common.V1;
using OddDotNet.Proto.Metrics.V1;

namespace OddDotCSharp
{
    public class WhereMetricFilterConfigurator
    {
        internal List<Where> Filters { get; } = new List<Where>();
        public WhereMetricGaugeFilterConfigurator Gauge { get; }
        public WhereMetricSumFilterConfigurator Sum { get; }
        public WhereMetricHistogramFilterConfigurator Histogram { get; }
        public WhereMetricExponentialHistogramFilterConfigurator ExponentialHistogram { get; }

        public WhereMetricFilterConfigurator()
        {
            Gauge = new WhereMetricGaugeFilterConfigurator(this);
            Sum = new WhereMetricSumFilterConfigurator(this);
            Histogram = new WhereMetricHistogramFilterConfigurator(this);
            ExponentialHistogram = new WhereMetricExponentialHistogramFilterConfigurator(this);
        }
        
        /// <summary>
        /// Adds a filter for Name to the list of filters.
        /// </summary>
        /// <param name="compare">The string to compare the Name against.</param>
        /// <param name="compareAs">The type of comparison to perform.</param>
        /// <returns>this <see cref="WhereMetricFilterConfigurator"/></returns>
        public WhereMetricFilterConfigurator AddNameFilter(string compare, StringCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Name = new StringProperty
                    {
                        CompareAs = compareAs,
                        Compare = compare
                    }
                }
            };
            
            Filters.Add(filter);
            return this;
        }
        
        /// <summary>
        /// Adds a filter for Description to the list of filters.
        /// </summary>
        /// <param name="compare">The string to compare the Description against.</param>
        /// <param name="compareAs">The type of comparison to perform.</param>
        /// <returns>this <see cref="WhereMetricFilterConfigurator"/></returns>
        public WhereMetricFilterConfigurator AddDescriptionFilter(string compare, StringCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Description = new StringProperty
                    {
                        CompareAs = compareAs,
                        Compare = compare
                    }
                }
            };
            
            Filters.Add(filter);
            return this;
        }
        
        /// <summary>
        /// Adds a filter for Unit to the list of filters.
        /// </summary>
        /// <param name="compare">The string to compare the Unit against.</param>
        /// <param name="compareAs">The type of comparison to perform.</param>
        /// <returns>this <see cref="WhereMetricFilterConfigurator"/></returns>
        public WhereMetricFilterConfigurator AddUnitFilter(string compare, StringCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Unit = new StringProperty
                    {
                        CompareAs = compareAs,
                        Compare = compare
                    }
                }
            };
            
            Filters.Add(filter);
            return this;
        }
        
        /// <summary>
        /// Adds a Metadata filter to the list of filters.
        /// </summary>
        /// <param name="key">The key of the KeyValue Metadata</param>
        /// <param name="compare">The string to compare the KeyValue value to.</param>
        /// <param name="compareAs">They type of comparison to do. <see cref="StringCompareAsType"/> for more details</param>
        /// <returns>this <see cref="WhereMetricFilterConfigurator"/></returns>
        public WhereMetricFilterConfigurator AddMetadataFilter(string key, string compare,
            StringCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Metadata = new KeyValueProperty
                    {
                        Key = key,
                        StringValue = new StringProperty
                        {
                            CompareAs = compareAs,
                            Compare = compare
                        }
                    }
                }
            };
                    
            Filters.Add(filter);
            return this;
        }
        
        /// <summary>
        /// Adds a Metadata filter to the list of filters.
        /// </summary>
        /// <param name="key">The key of the KeyValue Metadata</param>
        /// <param name="compare">The bool to compare the KeyValue value to.</param>
        /// <param name="compareAs">They type of comparison to do. <see cref="BoolCompareAsType"/> for more details</param>
        /// <returns>this <see cref="WhereMetricFilterConfigurator"/></returns>
        public WhereMetricFilterConfigurator AddMetadataFilter(string key, bool compare,
            BoolCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Metadata = new KeyValueProperty
                    {
                        Key = key,
                        BoolValue = new BoolProperty
                        {
                            CompareAs = compareAs,
                            Compare = compare
                        }
                    }
                }
            };
                    
            Filters.Add(filter);
            return this;
        }
        
        /// <summary>
        /// Adds a Metadata filter to the list of filters.
        /// </summary>
        /// <param name="key">The key of the KeyValue Metadata</param>
        /// <param name="compare">The long to compare the KeyValue value to.</param>
        /// <param name="compareAs">They type of comparison to do. <see cref="NumberCompareAsType"/> for more details</param>
        /// <returns>this <see cref="WhereMetricFilterConfigurator"/></returns>
        public WhereMetricFilterConfigurator AddMetadataFilter(string key, long compare,
            NumberCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Metadata = new KeyValueProperty
                    {
                        Key = key,
                        Int64Value = new Int64Property
                        {
                            CompareAs = compareAs,
                            Compare = compare
                        }
                    }
                }
            };
                    
            Filters.Add(filter);
            return this;
        }
        
        /// <summary>
        /// Adds a Metadata filter to the list of filters.
        /// </summary>
        /// <param name="key">The key of the KeyValue Metadata</param>
        /// <param name="compare">The double to compare the KeyValue value to.</param>
        /// <param name="compareAs">They type of comparison to do. <see cref="NumberCompareAsType"/> for more details</param>
        /// <returns>this <see cref="WhereMetricFilterConfigurator"/></returns>
        public WhereMetricFilterConfigurator AddMetadataFilter(string key, double compare,
            NumberCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Metadata = new KeyValueProperty
                    {
                        Key = key,
                        DoubleValue = new DoubleProperty
                        {
                            CompareAs = compareAs,
                            Compare = compare
                        }
                    }
                }
            };
                    
            Filters.Add(filter);
            return this;
        }
        
        /// <summary>
        /// Adds a Metadata filter to the list of filters.
        /// </summary>
        /// <param name="key">The key of the KeyValue Metadata</param>
        /// <param name="compare">The byte[] to compare the KeyValue value to.</param>
        /// <param name="compareAs">They type of comparison to do. <see cref="ByteStringCompareAsType"/> for more details</param>
        /// <returns>this <see cref="WhereMetricFilterConfigurator"/></returns>
        public WhereMetricFilterConfigurator AddMetadataFilter(string key, byte[] compare,
            ByteStringCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Metadata = new KeyValueProperty
                    {
                        Key = key,
                        ByteStringValue = new ByteStringProperty
                        {
                            CompareAs = compareAs,
                            Compare = ByteString.CopyFrom(compare)
                        }
                    }
                }
            };
                    
            Filters.Add(filter);
            return this;
        }
    }
}