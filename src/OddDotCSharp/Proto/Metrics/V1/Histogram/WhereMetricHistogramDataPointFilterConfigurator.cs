using System;
using Google.Protobuf;
using OddDotNet.Proto.Common.V1;
using OddDotNet.Proto.Metrics.V1;

namespace OddDotCSharp
{
    /// <summary>
    /// Configurator for properties specific to a Histogram DataPoint.
    /// </summary>
    public class WhereMetricHistogramDataPointFilterConfigurator
    {
        private readonly WhereMetricFilterConfigurator _configurator;
        
        /// <summary>
        /// Use this to access properties specific to the Exemplar of this DataPoint.
        /// </summary>
        public WhereMetricHistogramDataPointExemplarFilterConfigurator Exemplar { get; }
        
        private readonly ArrayValueFilterConfigurator _arrayValueFilterConfigurator;
        private readonly KeyValueListFilterConfigurator _keyValueListFilterConfigurator;

        internal WhereMetricHistogramDataPointFilterConfigurator(WhereMetricFilterConfigurator configurator)
        {
            _configurator = configurator;
            Exemplar = new WhereMetricHistogramDataPointExemplarFilterConfigurator(configurator);
            _arrayValueFilterConfigurator = new ArrayValueFilterConfigurator();
            _keyValueListFilterConfigurator = new KeyValueListFilterConfigurator();
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
                    Histogram = new HistogramFilter
                    {
                        DataPoint = new HistogramDataPointFilter
                        {
                            Attributes = new KeyValueListProperty
                            {
                                Values =
                                {
                                    new KeyValueProperty
                                    {
                                        Key = key,
                                        Value = new AnyValueProperty
                                        {
                                            StringValue = new StringProperty
                                            {
                                                CompareAs = compareAs,
                                                Compare = compare
                                            }
                                        }
                                    }
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
                    Histogram = new HistogramFilter
                    {
                        DataPoint = new HistogramDataPointFilter
                        {
                            Attributes = new KeyValueListProperty
                            {
                                Values =
                                {
                                    new KeyValueProperty
                                    {
                                        Key = key,
                                        Value = new AnyValueProperty
                                        {
                                            BoolValue = new BoolProperty
                                            {
                                                CompareAs = compareAs,
                                                Compare = compare
                                            }
                                        }
                                    }
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
                    Histogram = new HistogramFilter
                    {
                        DataPoint = new HistogramDataPointFilter
                        {
                            Attributes = new KeyValueListProperty
                            {
                                Values =
                                {
                                    new KeyValueProperty
                                    {
                                        Key = key,
                                        Value = new AnyValueProperty
                                        {
                                            IntValue = new Int64Property
                                            {
                                                CompareAs = compareAs,
                                                Compare = compare
                                            }
                                        }
                                    }
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
                    Histogram = new HistogramFilter
                    {
                        DataPoint = new HistogramDataPointFilter
                        {
                            Attributes = new KeyValueListProperty
                            {
                                Values =
                                {
                                    new KeyValueProperty
                                    {
                                        Key = key,
                                        Value = new AnyValueProperty
                                        {
                                            DoubleValue = new DoubleProperty
                                            {
                                                CompareAs = compareAs,
                                                Compare = compare
                                            }
                                        }
                                    }
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
                    Histogram = new HistogramFilter
                    {
                        DataPoint = new HistogramDataPointFilter
                        {
                            Attributes = new KeyValueListProperty
                            {
                                Values =
                                {
                                    new KeyValueProperty
                                    {
                                        Key = key,
                                        Value = new AnyValueProperty
                                        {
                                            ByteStringValue = new ByteStringProperty
                                            {
                                                CompareAs = compareAs,
                                                Compare = ByteString.CopyFrom(compare)
                                            }
                                        }
                                    }
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
        /// Adds an array filter to the list of filters. <see cref="ArrayValueFilterConfigurator"/> for more details.
        /// </summary>
        /// <param name="key">The key of the attribute being checked.</param>
        /// <param name="configure">The action used to configure the ArrayValueProperty filters being checked.</param>
        /// <returns>This configurator.</returns>
        public WhereMetricFilterConfigurator AddAttributeArrayFilter(string key,
            Action<ArrayValueFilterConfigurator> configure)
        {
            configure(_arrayValueFilterConfigurator);
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Histogram = new HistogramFilter
                    {
                        DataPoint = new HistogramDataPointFilter
                        {
                            Attributes = new KeyValueListProperty
                            {
                                Values =
                                {
                                    new KeyValueProperty
                                    {
                                        Key = key,
                                        Value = new AnyValueProperty
                                        {
                                            ArrayValue = new ArrayValueProperty()
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };
            filter.Property.Histogram.DataPoint.Attributes.Values[0].Value.ArrayValue.Values.AddRange(_arrayValueFilterConfigurator.Properties);
            _configurator.Filters.Add(filter);
            return _configurator;
        }
        
        /// <summary>
        /// Adds a KeyValueList filter to the list of filters. <see cref="KeyValueListFilterConfigurator"/> for more details.
        /// </summary>
        /// <param name="key">The key of the attribute being checked.</param>
        /// <param name="configure">The action used to configure the KeyValueListProperty filters being checked.</param>
        /// <returns>This configurator.</returns>
        public WhereMetricFilterConfigurator AddAttributeKeyValueListFilter(string key,
            Action<KeyValueListFilterConfigurator> configure)
        {
            configure(_keyValueListFilterConfigurator);
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Histogram = new HistogramFilter
                    {
                        DataPoint = new HistogramDataPointFilter
                        {
                            Attributes = new KeyValueListProperty
                            {
                                Values =
                                {
                                    new KeyValueProperty
                                    {
                                        Key = key,
                                        Value = new AnyValueProperty
                                        {
                                            KvlistValue = new KeyValueListProperty()
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };
            filter.Property.Histogram.DataPoint.Attributes.Values[0].Value.KvlistValue.Values.AddRange(_keyValueListFilterConfigurator.Properties);
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
                    Histogram = new HistogramFilter
                    {
                        DataPoint = new HistogramDataPointFilter
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
                    Histogram = new HistogramFilter
                    {
                        DataPoint = new HistogramDataPointFilter
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
                    Histogram = new HistogramFilter
                    {
                        DataPoint = new HistogramDataPointFilter
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
                    Histogram = new HistogramFilter
                    {
                        DataPoint = new HistogramDataPointFilter
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
                    Histogram = new HistogramFilter
                    {
                        DataPoint = new HistogramDataPointFilter
                        {
                            BucketCount = new UInt64Property
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
        /// Adds a filter for ExplicitBound to the list of filters.
        /// </summary>
        /// <param name="compare">The double to compare the ExplicitBound against.</param>
        /// <param name="compareAs">The type of comparison to perform.</param>
        /// <returns>this <see cref="WhereMetricFilterConfigurator"/></returns>
        public WhereMetricFilterConfigurator AddExplicitBoundFilter(double compare, NumberCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Histogram = new HistogramFilter
                    {
                        DataPoint = new HistogramDataPointFilter
                        {
                            ExplicitBound = new DoubleProperty
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
                    Histogram = new HistogramFilter
                    {
                        DataPoint = new HistogramDataPointFilter
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
                    Histogram = new HistogramFilter
                    {
                        DataPoint = new HistogramDataPointFilter
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
                    Histogram = new HistogramFilter
                    {
                        DataPoint = new HistogramDataPointFilter
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