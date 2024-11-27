using System;
using Google.Protobuf;
using OddDotCSharp.Proto.Common.V1;
using OddDotNet.Proto.Common.V1;
using OddDotNet.Proto.Metrics.V1;

namespace OddDotCSharp
{
    public class WhereMetricGaugeDataPointFilterConfigurator
    {
        private readonly WhereMetricFilterConfigurator _configurator;
        public WhereMetricGaugeDataPointExemplarFilterConfigurator Exemplar { get; }
        
        private readonly ArrayValueFilterConfigurator _arrayValueFilterConfigurator;
        private readonly KeyValueListFilterConfigurator _keyValueListFilterConfigurator;

        public WhereMetricGaugeDataPointFilterConfigurator(WhereMetricFilterConfigurator configurator)
        {
            _configurator = configurator;
            Exemplar = new WhereMetricGaugeDataPointExemplarFilterConfigurator(configurator);
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
                    Gauge = new GaugeFilter
                    {
                        DataPoint = new NumberDataPointFilter
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
                    Gauge = new GaugeFilter
                    {
                        DataPoint = new NumberDataPointFilter
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
                    Gauge = new GaugeFilter
                    {
                        DataPoint = new NumberDataPointFilter
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
                    Gauge = new GaugeFilter
                    {
                        DataPoint = new NumberDataPointFilter
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
                    Gauge = new GaugeFilter
                    {
                        DataPoint = new NumberDataPointFilter
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
        
        public WhereMetricFilterConfigurator AddAttributeArrayFilter(string key,
            Action<ArrayValueFilterConfigurator> configure)
        {
            configure(_arrayValueFilterConfigurator);
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Gauge = new GaugeFilter
                    {
                        DataPoint = new NumberDataPointFilter
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
            filter.Property.Gauge.DataPoint.Attributes.Values[0].Value.ArrayValue.Values.AddRange(_arrayValueFilterConfigurator.Properties);
            _configurator.Filters.Add(filter);
            return _configurator;
        }
        
        public WhereMetricFilterConfigurator AddAttributeKeyValueListFilter(string key,
            Action<KeyValueListFilterConfigurator> configure)
        {
            configure(_keyValueListFilterConfigurator);
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Gauge = new GaugeFilter
                    {
                        DataPoint = new NumberDataPointFilter
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
            filter.Property.Gauge.DataPoint.Attributes.Values[0].Value.KvlistValue.Values.AddRange(_keyValueListFilterConfigurator.Properties);
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
                    Gauge = new GaugeFilter
                    {
                        DataPoint = new NumberDataPointFilter
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
                    Gauge = new GaugeFilter
                    {
                        DataPoint = new NumberDataPointFilter
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
        /// Adds a filter for ValueAsDouble to the list of filters.
        /// </summary>
        /// <param name="compare">The double to compare the ValueAsDouble against.</param>
        /// <param name="compareAs">The type of comparison to perform.</param>
        /// <returns>this <see cref="WhereMetricFilterConfigurator"/></returns>
        public WhereMetricFilterConfigurator AddValueAsDoubleFilter(double compare, NumberCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Gauge = new GaugeFilter
                    {
                        DataPoint = new NumberDataPointFilter
                        {
                            ValueAsDouble = new DoubleProperty
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
        /// Adds a filter for ValueAsInt to the list of filters.
        /// </summary>
        /// <param name="compare">The long to compare the ValueAsInt against.</param>
        /// <param name="compareAs">The type of comparison to perform.</param>
        /// <returns>this <see cref="WhereMetricFilterConfigurator"/></returns>
        public WhereMetricFilterConfigurator AddValueAsIntFilter(long compare, NumberCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Gauge = new GaugeFilter
                    {
                        DataPoint = new NumberDataPointFilter
                        {
                            ValueAsInt = new Int64Property
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
                    Gauge = new GaugeFilter
                    {
                        DataPoint = new NumberDataPointFilter
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