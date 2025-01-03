using System;
using Google.Protobuf;
using OddDotNet.Proto.Common.V1;
using OddDotNet.Proto.Metrics.V1;

namespace OddDotCSharp
{
    /// <summary>
    /// Allows for configuring filters related to the DataPoint Exemplar of an ExponentialHistogram.
    /// </summary>
    public class WhereMetricExponentialHistogramDataPointExemplarFilterConfigurator
    {
        private readonly WhereMetricFilterConfigurator _configurator;
        
        private readonly ArrayValueFilterConfigurator _arrayValueFilterConfigurator;
        private readonly KeyValueListFilterConfigurator _keyValueListFilterConfigurator;

        internal WhereMetricExponentialHistogramDataPointExemplarFilterConfigurator(WhereMetricFilterConfigurator configurator)
        {
            _configurator = configurator;
            _arrayValueFilterConfigurator = new ArrayValueFilterConfigurator();
            _keyValueListFilterConfigurator = new KeyValueListFilterConfigurator();
        }
        
        /// <summary>
        /// Adds a FilteredAttribute filter to the list of filters.
        /// </summary>
        /// <param name="key">The key of the KeyValue FilteredAttribute</param>
        /// <param name="compare">The string to compare the KeyValue value to.</param>
        /// <param name="compareAs">They type of comparison to do. <see cref="StringCompareAsType"/> for more details</param>
        /// <returns>this <see cref="WhereMetricFilterConfigurator"/></returns>
        public WhereMetricFilterConfigurator AddFilteredAttributeFilter(string key, string compare,
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
                            Exemplar = new ExemplarFilter
                            {
                                FilteredAttributes = new KeyValueListProperty
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
                }
            };
                    
            _configurator.Filters.Add(filter);
            return _configurator;
        }
        
        /// <summary>
        /// Adds a FilteredAttribute filter to the list of filters.
        /// </summary>
        /// <param name="key">The key of the KeyValue FilteredAttribute</param>
        /// <param name="compare">The bool to compare the KeyValue value to.</param>
        /// <param name="compareAs">They type of comparison to do. <see cref="BoolCompareAsType"/> for more details</param>
        /// <returns>this <see cref="WhereMetricFilterConfigurator"/></returns>
        public WhereMetricFilterConfigurator AddFilteredAttributeFilter(string key, bool compare,
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
                            Exemplar = new ExemplarFilter
                            {
                                FilteredAttributes = new KeyValueListProperty
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
                }
            };
                    
            _configurator.Filters.Add(filter);
            return _configurator;
        }
        
        /// <summary>
        /// Adds a FilteredAttribute filter to the list of filters.
        /// </summary>
        /// <param name="key">The key of the KeyValue FilteredAttribute</param>
        /// <param name="compare">The long to compare the KeyValue value to.</param>
        /// <param name="compareAs">They type of comparison to do. <see cref="NumberCompareAsType"/> for more details</param>
        /// <returns>this <see cref="WhereMetricFilterConfigurator"/></returns>
        public WhereMetricFilterConfigurator AddFilteredAttributeFilter(string key, long compare,
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
                            Exemplar = new ExemplarFilter
                            {
                                FilteredAttributes = new KeyValueListProperty
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
                }
            };
                    
            _configurator.Filters.Add(filter);
            return _configurator;
        }
        
        /// <summary>
        /// Adds a FilteredAttribute filter to the list of filters.
        /// </summary>
        /// <param name="key">The key of the KeyValue FilteredAttribute</param>
        /// <param name="compare">The double to compare the KeyValue value to.</param>
        /// <param name="compareAs">They type of comparison to do. <see cref="NumberCompareAsType"/> for more details</param>
        /// <returns>this <see cref="WhereMetricFilterConfigurator"/></returns>
        public WhereMetricFilterConfigurator AddFilteredAttributeFilter(string key, double compare,
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
                            Exemplar = new ExemplarFilter
                            {
                                FilteredAttributes = new KeyValueListProperty
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
                }
            };
                    
            _configurator.Filters.Add(filter);
            return _configurator;
        }
        
        /// <summary>
        /// Adds a FilteredAttribute filter to the list of filters.
        /// </summary>
        /// <param name="key">The key of the KeyValue FilteredAttribute</param>
        /// <param name="compare">The byte[] to compare the KeyValue value to.</param>
        /// <param name="compareAs">They type of comparison to do. <see cref="ByteStringCompareAsType"/> for more details</param>
        /// <returns>this <see cref="WhereMetricFilterConfigurator"/></returns>
        public WhereMetricFilterConfigurator AddFilteredAttributeFilter(string key, byte[] compare,
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
                            Exemplar = new ExemplarFilter
                            {
                                FilteredAttributes = new KeyValueListProperty
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
                }
            };
                    
            _configurator.Filters.Add(filter);
            return _configurator;
        }
        
        /// <summary>
        /// Adds an array filter to the list of filters. <see cref="ArrayValueFilterConfigurator"/> for more details.
        /// </summary>
        /// <param name="key">The key of the filtered attribute being checked.</param>
        /// <param name="configure">The action used to configure the ArrayValueProperty filters being checked.</param>
        /// <returns>This configurator.</returns>
        public WhereMetricFilterConfigurator AddFilteredAttributeArrayFilter(string key,
            Action<ArrayValueFilterConfigurator> configure)
        {
            configure(_arrayValueFilterConfigurator);
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    ExponentialHistogram = new ExponentialHistogramFilter
                    {
                        DataPoint = new ExponentialHistogramDataPointFilter
                        {
                            Exemplar = new ExemplarFilter
                            {
                                FilteredAttributes = new KeyValueListProperty
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
                }
            };
            filter.Property.ExponentialHistogram.DataPoint.Exemplar.FilteredAttributes.Values[0].Value.ArrayValue.Values.AddRange(_arrayValueFilterConfigurator.Properties);
            _configurator.Filters.Add(filter);
            return _configurator;
        }
        
        /// <summary>
        /// Adds a KeyValueList filter to the list of filters. <see cref="KeyValueListFilterConfigurator"/> for more details.
        /// </summary>
        /// <param name="key">The key of the filtered attribute being checked.</param>
        /// <param name="configure">The action used to configure the KeyValueListProperty filters being checked.</param>
        /// <returns>This configurator.</returns>
        public WhereMetricFilterConfigurator AddFilteredAttributeKeyValueListFilter(string key,
            Action<KeyValueListFilterConfigurator> configure)
        {
            configure(_keyValueListFilterConfigurator);
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    ExponentialHistogram = new ExponentialHistogramFilter
                    {
                        DataPoint = new ExponentialHistogramDataPointFilter
                        {
                            Exemplar = new ExemplarFilter
                            {
                                FilteredAttributes = new KeyValueListProperty
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
                }
            };
            filter.Property.ExponentialHistogram.DataPoint.Exemplar.FilteredAttributes.Values[0].Value.KvlistValue.Values.AddRange(_keyValueListFilterConfigurator.Properties);
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
                            Exemplar = new ExemplarFilter
                            {
                                TimeUnixNano = new UInt64Property
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
                    ExponentialHistogram = new ExponentialHistogramFilter
                    {
                        DataPoint = new ExponentialHistogramDataPointFilter
                        {
                            Exemplar = new ExemplarFilter
                            {
                                ValueAsDouble = new DoubleProperty
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
        /// Adds a filter for ValueAsInt to the list of filters.
        /// </summary>
        /// <param name="compare">The double to compare the ValueAsInt against.</param>
        /// <param name="compareAs">The type of comparison to perform.</param>
        /// <returns>this <see cref="WhereMetricFilterConfigurator"/></returns>
        public WhereMetricFilterConfigurator AddValueAsIntFilter(long compare, NumberCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    ExponentialHistogram = new ExponentialHistogramFilter
                    {
                        DataPoint = new ExponentialHistogramDataPointFilter
                        {
                            Exemplar = new ExemplarFilter
                            {
                                ValueAsInt = new Int64Property
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
        /// Adds a filter for SpanId to the list of filters.
        /// </summary>
        /// <param name="compare">The byte[] to compare the SpanId against.</param>
        /// <param name="compareAs">The type of comparison to perform.</param>
        /// <returns>this <see cref="WhereMetricFilterConfigurator"/></returns>
        public WhereMetricFilterConfigurator AddSpanIdFilter(byte[] compare, ByteStringCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    ExponentialHistogram = new ExponentialHistogramFilter
                    {
                        DataPoint = new ExponentialHistogramDataPointFilter
                        {
                            Exemplar = new ExemplarFilter
                            {
                                SpanId = new ByteStringProperty
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
        /// Adds a filter for TraceId to the list of filters.
        /// </summary>
        /// <param name="compare">The byte[] to compare the TraceId against.</param>
        /// <param name="compareAs">The type of comparison to perform.</param>
        /// <returns>this <see cref="WhereMetricFilterConfigurator"/></returns>
        public WhereMetricFilterConfigurator AddTraceIdFilter(byte[] compare, ByteStringCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    ExponentialHistogram = new ExponentialHistogramFilter
                    {
                        DataPoint = new ExponentialHistogramDataPointFilter
                        {
                            Exemplar = new ExemplarFilter
                            {
                                TraceId = new ByteStringProperty
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
    }
}