using System;
using Google.Protobuf;
using OddDotNet.Proto.Common.V1;
using OddDotNet.Proto.Metrics.V1;

namespace OddDotCSharp
{
    /// <summary>
    /// Configurator for properties specific to the DataPoint of a Metric Summary.
    /// </summary>
    public class WhereMetricSummaryDataPointFilterConfigurator
    {
        private readonly WhereMetricFilterConfigurator _configurator;
        
        private readonly ArrayValueFilterConfigurator _arrayValueFilterConfigurator;
        private readonly KeyValueListFilterConfigurator _keyValueListFilterConfigurator;
        
        /// <summary>
        /// Use this to access properties specific to the ValueAtQuantile of this Summary.
        /// </summary>
        public WhereMetricSummaryDataPointValueAtQuantileFilterConfigurator ValueAtQuantile { get;  }

        internal WhereMetricSummaryDataPointFilterConfigurator(WhereMetricFilterConfigurator configurator)
        {
            _configurator = configurator;
            ValueAtQuantile = new WhereMetricSummaryDataPointValueAtQuantileFilterConfigurator(configurator);
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
                    Summary = new SummaryFilter
                    {
                        DataPoint = new SummaryDataPointFilter
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
                    Summary = new SummaryFilter
                    {
                        DataPoint = new SummaryDataPointFilter
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
                    Summary = new SummaryFilter
                    {
                        DataPoint = new SummaryDataPointFilter
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
                    Summary = new SummaryFilter
                    {
                        DataPoint = new SummaryDataPointFilter
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
                    Summary = new SummaryFilter
                    {
                        DataPoint = new SummaryDataPointFilter
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
                    Summary = new SummaryFilter
                    {
                        DataPoint = new SummaryDataPointFilter
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
            filter.Property.Summary.DataPoint.Attributes.Values[0].Value.ArrayValue.Values.AddRange(_arrayValueFilterConfigurator.Properties);
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
                    Summary = new SummaryFilter
                    {
                        DataPoint = new SummaryDataPointFilter
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
            filter.Property.Summary.DataPoint.Attributes.Values[0].Value.KvlistValue.Values.AddRange(_keyValueListFilterConfigurator.Properties);
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
                    Summary = new SummaryFilter
                    {
                        DataPoint = new SummaryDataPointFilter
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
                    Summary = new SummaryFilter
                    {
                        DataPoint = new SummaryDataPointFilter
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
                    Summary = new SummaryFilter
                    {
                        DataPoint = new SummaryDataPointFilter
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
                    Summary = new SummaryFilter
                    {
                        DataPoint = new SummaryDataPointFilter
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
        /// Adds a filter for Flags to the list of filters.
        /// </summary>
        /// <param name="compare">The uint to compare the Flags against.</param>
        /// <param name="compareAs">The type of comparison to perform.</param>
        /// <returns>this <see cref="WhereMetricFilterConfigurator"/></returns>
        public WhereMetricFilterConfigurator AddFlagsFilter(uint compare, NumberCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Summary = new SummaryFilter
                    {
                        DataPoint = new SummaryDataPointFilter
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
    }
}