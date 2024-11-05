using Google.Protobuf;
using OddDotNet.Proto.Common.V1;
using OddDotNet.Proto.Metrics.V1;

namespace OddDotCSharp
{
    public class WhereMetricGaugeDataPointExemplarFilterConfigurator
    {
        private readonly WhereMetricFilterConfigurator _configurator;

        public WhereMetricGaugeDataPointExemplarFilterConfigurator(WhereMetricFilterConfigurator configurator)
        {
            _configurator = configurator;
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
                    Gauge = new GaugeFilter
                    {
                        DataPoint = new NumberDataPointFilter
                        {
                            Exemplar = new ExemplarFilter
                            {
                                FilteredAttribute = new KeyValueProperty
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
                    Gauge = new GaugeFilter
                    {
                        DataPoint = new NumberDataPointFilter
                        {
                            Exemplar = new ExemplarFilter
                            {
                                FilteredAttribute = new KeyValueProperty
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
                    Gauge = new GaugeFilter
                    {
                        DataPoint = new NumberDataPointFilter
                        {
                            Exemplar = new ExemplarFilter
                            {
                                FilteredAttribute = new KeyValueProperty
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
                    Gauge = new GaugeFilter
                    {
                        DataPoint = new NumberDataPointFilter
                        {
                            Exemplar = new ExemplarFilter
                            {
                                FilteredAttribute = new KeyValueProperty
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
                    Gauge = new GaugeFilter
                    {
                        DataPoint = new NumberDataPointFilter
                        {
                            Exemplar = new ExemplarFilter
                            {
                                FilteredAttribute = new KeyValueProperty
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
                    Gauge = new GaugeFilter
                    {
                        DataPoint = new NumberDataPointFilter
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
                    Gauge = new GaugeFilter
                    {
                        DataPoint = new NumberDataPointFilter
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
                    Gauge = new GaugeFilter
                    {
                        DataPoint = new NumberDataPointFilter
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
                    Gauge = new GaugeFilter
                    {
                        DataPoint = new NumberDataPointFilter
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