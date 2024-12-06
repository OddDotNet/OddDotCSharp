using System;
using System.Collections.Generic;
using Google.Protobuf;
using OddDotNet.Proto.Common.V1;
using OddDotNet.Proto.Metrics.V1;

namespace OddDotCSharp
{
    /// <summary>
    /// Configurator for properties specific to a Metric.
    /// </summary>
    public class WhereMetricFilterConfigurator
    {
        internal List<Where> Filters { get; } = new List<Where>();
        
        /// <summary>
        /// Use this to access properties specific to the Gauge of this Metric.
        /// </summary>
        public WhereMetricGaugeFilterConfigurator Gauge { get; }
        
        /// <summary>
        /// Use this to access properties specific to the Sum of this Metric.
        /// </summary>
        public WhereMetricSumFilterConfigurator Sum { get; }
        
        /// <summary>
        /// Use this to access properties specific to the Histogram of this Metric.
        /// </summary>
        public WhereMetricHistogramFilterConfigurator Histogram { get; }
        
        /// <summary>
        /// Use this to access properties specific to the ExponentialHistogram of this Metric.
        /// </summary>
        public WhereMetricExponentialHistogramFilterConfigurator ExponentialHistogram { get; }
        
        /// <summary>
        /// Use this to access properties specific to the Summary of this Metric.
        /// </summary>
        public WhereMetricSummaryFilterConfigurator Summary { get; }
        
        /// <summary>
        /// Use this to access properties specific to the InstrumentationScope of this Metric.
        /// </summary>
        public WhereMetricInstrumentationScopeFilterConfigurator InstrumentationScope { get; }
        
        /// <summary>
        /// Use this to access properties specific to the Resource of this Metric.
        /// </summary>
        public WhereMetricResourceFilterConfigurator Resource { get; }
        
        private readonly ArrayValueFilterConfigurator _arrayValueFilterConfigurator;
        private readonly KeyValueListFilterConfigurator _keyValueListFilterConfigurator;

        internal WhereMetricFilterConfigurator()
        {
            Gauge = new WhereMetricGaugeFilterConfigurator(this);
            Sum = new WhereMetricSumFilterConfigurator(this);
            Histogram = new WhereMetricHistogramFilterConfigurator(this);
            ExponentialHistogram = new WhereMetricExponentialHistogramFilterConfigurator(this);
            Summary = new WhereMetricSummaryFilterConfigurator(this);
            InstrumentationScope = new WhereMetricInstrumentationScopeFilterConfigurator(this);
            Resource = new WhereMetricResourceFilterConfigurator(this);
            
            _arrayValueFilterConfigurator = new ArrayValueFilterConfigurator();
            _keyValueListFilterConfigurator = new KeyValueListFilterConfigurator();
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
                    Metadata = new KeyValueListProperty
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
                    Metadata = new KeyValueListProperty
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
                    Metadata = new KeyValueListProperty
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
                    Metadata = new KeyValueListProperty
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
                    Metadata = new KeyValueListProperty
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
            };
                    
            Filters.Add(filter);
            return this;
        }
        
        /// <summary>
        /// Adds an array filter to the list of filters. <see cref="ArrayValueFilterConfigurator"/> for more details.
        /// </summary>
        /// <param name="key">The key of the metadata being checked.</param>
        /// <param name="configure">The action used to configure the ArrayValueProperty filters being checked.</param>
        /// <returns>This configurator.</returns>
        public WhereMetricFilterConfigurator AddMetadataArrayFilter(string key,
            Action<ArrayValueFilterConfigurator> configure)
        {
            configure(_arrayValueFilterConfigurator);
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Metadata = new KeyValueListProperty
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
            };
            filter.Property.Metadata.Values[0].Value.ArrayValue.Values.AddRange(_arrayValueFilterConfigurator.Properties);
            Filters.Add(filter);
            return this;
        }
        
        /// <summary>
        /// Adds a KeyValueList filter to the list of filters. <see cref="KeyValueListFilterConfigurator"/> for more details.
        /// </summary>
        /// <param name="key">The key of the metadata being checked.</param>
        /// <param name="configure">The action used to configure the KeyValueListProperty filters being checked.</param>
        /// <returns>This configurator.</returns>
        public WhereMetricFilterConfigurator AddMetadataKeyValueListFilter(string key,
            Action<KeyValueListFilterConfigurator> configure)
        {
            configure(_keyValueListFilterConfigurator);
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Metadata = new KeyValueListProperty
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
            };
            filter.Property.Metadata.Values[0].Value.KvlistValue.Values.AddRange(_keyValueListFilterConfigurator.Properties);
            Filters.Add(filter);
            return this;
        }
        
        /// <summary>
        /// Add a <see cref="OrFilter"/> filter to the list of filters.
        /// </summary>
        /// <param name="configure">Action used to configure the filters. This action behaves the same way as
        /// the action passed to the Where() method. See <see cref="MetricQueryRequestBuilder.Where"/>.</param>
        /// <returns>this <see cref="WhereMetricFilterConfigurator"/>.</returns>
        public WhereMetricFilterConfigurator AddOrFilter(Action<WhereMetricFilterConfigurator> configure)
        {
            var configurator = new WhereMetricFilterConfigurator();
            configure(configurator);

            var orFilter = new OrFilter();
            orFilter.Filters.AddRange(configurator.Filters);

            var filter = new Where
            {
                Or = orFilter
            };
            
            Filters.Add(filter);
            return this;
        }
    }
}