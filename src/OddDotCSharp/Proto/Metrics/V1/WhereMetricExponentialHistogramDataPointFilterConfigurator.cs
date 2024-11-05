using Google.Protobuf;
using OddDotNet.Proto.Common.V1;
using OddDotNet.Proto.Metrics.V1;

namespace OddDotCSharp
{
    public class WhereMetricExponentialHistogramDataPointFilterConfigurator
    {
        private readonly WhereMetricFilterConfigurator _configurator;
        public WhereMetricExponentialHistogramDataPointExemplarFilterConfigurator Exemplar { get; }
        public WhereMetricExponentialHistogramDataPointBucketFilterConfigurator Positive { get; }
        public WhereMetricExponentialHistogramDataPointBucketFilterConfigurator Negative { get; }

        public WhereMetricExponentialHistogramDataPointFilterConfigurator(WhereMetricFilterConfigurator configurator)
        {
            _configurator = configurator;
            Exemplar = new WhereMetricExponentialHistogramDataPointExemplarFilterConfigurator(configurator);
            Positive = new WhereMetricExponentialHistogramDataPointBucketFilterConfigurator(configurator, ExponentialHistogramDataPointBucket.Positive);
            Negative = new WhereMetricExponentialHistogramDataPointBucketFilterConfigurator(configurator, ExponentialHistogramDataPointBucket.Negative);
        }
        
        /// <summary>
        /// Adds an attribute filter to the list of filters.
        /// </summary>
        /// <param name="key">The key of the KeyValue attribute</param>
        /// <param name="compare">The string to compare the KeyValue value to.</param>
        /// <param name="compareAs">They type of comparison to do. <see cref="StringCompareAsType"/> for more details</param>
        /// <returns>this <see cref="WhereMetricFilterConfigurator"/></returns>
        public WhereMetricFilterConfigurator AddAttributeFilter(string key, string compare,
            StringCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    ExponentialHistogram = new ExponentialHistogramFilter
                    {
                        DataPoint = new ExponentialHistogramDataPointFilter
                        {
                            Attribute = new KeyValueProperty
                            {
                                Key = key,
                                StringValue = new StringProperty
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
        /// Adds an attribute filter to the list of filters.
        /// </summary>
        /// <param name="key">The key of the KeyValue attribute</param>
        /// <param name="compare">The bool to compare the KeyValue value to.</param>
        /// <param name="compareAs">They type of comparison to do. <see cref="BoolCompareAsType"/> for more details</param>
        /// <returns>this <see cref="WhereMetricFilterConfigurator"/></returns>
        public WhereMetricFilterConfigurator AddAttributeFilter(string key, bool compare,
            BoolCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    ExponentialHistogram = new ExponentialHistogramFilter
                    {
                        DataPoint = new ExponentialHistogramDataPointFilter
                        {
                            Attribute = new KeyValueProperty
                            {
                                Key = key,
                                BoolValue = new BoolProperty
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
        /// Adds an attribute filter to the list of filters.
        /// </summary>
        /// <param name="key">The key of the KeyValue attribute</param>
        /// <param name="compare">The long to compare the KeyValue value to.</param>
        /// <param name="compareAs">They type of comparison to do. <see cref="NumberCompareAsType"/> for more details</param>
        /// <returns>this <see cref="WhereMetricFilterConfigurator"/></returns>
        public WhereMetricFilterConfigurator AddAttributeFilter(string key, long compare,
            NumberCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    ExponentialHistogram = new ExponentialHistogramFilter
                    {
                        DataPoint = new ExponentialHistogramDataPointFilter
                        {
                            Attribute = new KeyValueProperty
                            {
                                Key = key,
                                Int64Value = new Int64Property
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
        /// Adds an attribute filter to the list of filters.
        /// </summary>
        /// <param name="key">The key of the KeyValue attribute</param>
        /// <param name="compare">The double to compare the KeyValue value to.</param>
        /// <param name="compareAs">They type of comparison to do. <see cref="NumberCompareAsType"/> for more details</param>
        /// <returns>this <see cref="WhereMetricFilterConfigurator"/></returns>
        public WhereMetricFilterConfigurator AddAttributeFilter(string key, double compare,
            NumberCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    ExponentialHistogram = new ExponentialHistogramFilter
                    {
                        DataPoint = new ExponentialHistogramDataPointFilter
                        {
                            Attribute = new KeyValueProperty
                            {
                                Key = key,
                                DoubleValue = new DoubleProperty
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
        /// Adds an attribute filter to the list of filters.
        /// </summary>
        /// <param name="key">The key of the KeyValue attribute</param>
        /// <param name="compare">The byte[] to compare the KeyValue value to.</param>
        /// <param name="compareAs">They type of comparison to do. <see cref="ByteStringCompareAsType"/> for more details</param>
        /// <returns>this <see cref="WhereMetricFilterConfigurator"/></returns>
        public WhereMetricFilterConfigurator AddAttributeFilter(string key, byte[] compare,
            ByteStringCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    ExponentialHistogram = new ExponentialHistogramFilter
                    {
                        DataPoint = new ExponentialHistogramDataPointFilter
                        {
                            Attribute = new KeyValueProperty
                            {
                                Key = key,
                                ByteStringValue = new ByteStringProperty
                                {
                                    CompareAs = compareAs,
                                    Compare = ByteString.CopyFrom(compare)
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
        /// Adds a filter for StartTimeUnixNano to the list of filters.
        /// </summary>
        /// <param name="compare">The ulong to compare the StartTimeUnixNano against.</param>
        /// <param name="compareAs">The type of comparison to perform.</param>
        /// <returns>this <see cref="WhereMetricFilterConfigurator"/></returns>
        public WhereMetricFilterConfigurator AddStartTimeUnixNanoFilter(ulong compare, NumberCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    ExponentialHistogram = new ExponentialHistogramFilter
                    {
                        DataPoint = new ExponentialHistogramDataPointFilter
                        {
                            StartTimeUnixNano = new UInt64Property
                            {
                                CompareAs = compareAs,
                                Compare = compare
                            }
                        }
                    }
                }
            };
            
            _configurator.Filters.Add(filter);
            return _configurator;
        }
        
        /// <summary>
        /// Adds a filter for TimeUnixNano to the list of filters.
        /// </summary>
        /// <param name="compare">The ulong to compare the TimeUnixNano against.</param>
        /// <param name="compareAs">The type of comparison to perform.</param>
        /// <returns>this <see cref="WhereMetricFilterConfigurator"/></returns>
        public WhereMetricFilterConfigurator AddTimeUnixNanoFilter(ulong compare, NumberCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    ExponentialHistogram = new ExponentialHistogramFilter
                    {
                        DataPoint = new ExponentialHistogramDataPointFilter
                        {
                            TimeUnixNano = new UInt64Property
                            {
                                CompareAs = compareAs,
                                Compare = compare
                            }
                        }
                    }
                }
            };
            
            _configurator.Filters.Add(filter);
            return _configurator;
        }
        
        /// <summary>
        /// Adds a filter for Count to the list of filters.
        /// </summary>
        /// <param name="compare">The ulong to compare the Count against.</param>
        /// <param name="compareAs">The type of comparison to perform.</param>
        /// <returns>this <see cref="WhereMetricFilterConfigurator"/></returns>
        public WhereMetricFilterConfigurator AddCountFilter(ulong compare, NumberCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    ExponentialHistogram = new ExponentialHistogramFilter
                    {
                        DataPoint = new ExponentialHistogramDataPointFilter
                        {
                            Count = new UInt64Property
                            {
                                CompareAs = compareAs,
                                Compare = compare
                            }
                        }
                    }
                }
            };
            
            _configurator.Filters.Add(filter);
            return _configurator;
        }
        
        /// <summary>
        /// Adds a filter for Sum to the list of filters.
        /// </summary>
        /// <param name="compare">The double to compare the Sum against.</param>
        /// <param name="compareAs">The type of comparison to perform.</param>
        /// <returns>this <see cref="WhereMetricFilterConfigurator"/></returns>
        public WhereMetricFilterConfigurator AddSumFilter(double compare, NumberCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    ExponentialHistogram = new ExponentialHistogramFilter
                    {
                        DataPoint = new ExponentialHistogramDataPointFilter
                        {
                            Sum = new DoubleProperty
                            {
                                CompareAs = compareAs,
                                Compare = compare
                            }
                        }
                    }
                }
            };
            
            _configurator.Filters.Add(filter);
            return _configurator;
        }
        
        /// <summary>
        /// Adds a filter for Scale to the list of filters.
        /// </summary>
        /// <param name="compare">The int to compare the Scale against.</param>
        /// <param name="compareAs">The type of comparison to perform.</param>
        /// <returns>this <see cref="WhereMetricFilterConfigurator"/></returns>
        public WhereMetricFilterConfigurator AddScaleFilter(int compare, NumberCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    ExponentialHistogram = new ExponentialHistogramFilter
                    {
                        DataPoint = new ExponentialHistogramDataPointFilter
                        {
                            Scale = new Int32Property
                            {
                                CompareAs = compareAs,
                                Compare = compare
                            }
                        }
                    }
                }
            };
            
            _configurator.Filters.Add(filter);
            return _configurator;
        }
        
        /// <summary>
        /// Adds a filter for ZeroCount to the list of filters.
        /// </summary>
        /// <param name="compare">The ulong to compare the ZeroCount against.</param>
        /// <param name="compareAs">The type of comparison to perform.</param>
        /// <returns>this <see cref="WhereMetricFilterConfigurator"/></returns>
        public WhereMetricFilterConfigurator AddZeroCountFilter(ulong compare, NumberCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    ExponentialHistogram = new ExponentialHistogramFilter
                    {
                        DataPoint = new ExponentialHistogramDataPointFilter
                        {
                            ZeroCount = new UInt64Property
                            {
                                CompareAs = compareAs,
                                Compare = compare
                            }
                        }
                    }
                }
            };
            
            _configurator.Filters.Add(filter);
            return _configurator;
        }
        
        /// <summary>
        /// Adds a filter for Flags to the list of filters.
        /// </summary>
        /// <param name="compare">The long to compare the Flags against.</param>
        /// <param name="compareAs">The type of comparison to perform.</param>
        /// <returns>this <see cref="WhereMetricFilterConfigurator"/></returns>
        public WhereMetricFilterConfigurator AddFlagsFilter(uint compare, NumberCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    ExponentialHistogram = new ExponentialHistogramFilter
                    {
                        DataPoint = new ExponentialHistogramDataPointFilter
                        {
                            Flags = new UInt32Property
                            {
                                CompareAs = compareAs,
                                Compare = compare
                            }
                        }
                    }
                }
            };
            
            _configurator.Filters.Add(filter);
            return _configurator;
        }
        
        /// <summary>
        /// Adds a filter for Min to the list of filters.
        /// </summary>
        /// <param name="compare">The double to compare the Min against.</param>
        /// <param name="compareAs">The type of comparison to perform.</param>
        /// <returns>this <see cref="WhereMetricFilterConfigurator"/></returns>
        public WhereMetricFilterConfigurator AddMinFilter(double compare, NumberCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    ExponentialHistogram = new ExponentialHistogramFilter
                    {
                        DataPoint = new ExponentialHistogramDataPointFilter
                        {
                            Min = new DoubleProperty
                            {
                                CompareAs = compareAs,
                                Compare = compare
                            }
                        }
                    }
                }
            };
            
            _configurator.Filters.Add(filter);
            return _configurator;
        }
        
        /// <summary>
        /// Adds a filter for Max to the list of filters.
        /// </summary>
        /// <param name="compare">The double to compare the Max against.</param>
        /// <param name="compareAs">The type of comparison to perform.</param>
        /// <returns>this <see cref="WhereMetricFilterConfigurator"/></returns>
        public WhereMetricFilterConfigurator AddMaxFilter(double compare, NumberCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    ExponentialHistogram = new ExponentialHistogramFilter
                    {
                        DataPoint = new ExponentialHistogramDataPointFilter
                        {
                            Max = new DoubleProperty
                            {
                                CompareAs = compareAs,
                                Compare = compare
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