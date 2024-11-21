using System;
using System.Collections.Generic;
using Google.Protobuf;
using OddDotCSharp.Proto.Common.V1;
using OddDotNet.Proto.Common.V1;
using OddDotNet.Proto.Logs.V1;
using OpenTelemetry.Proto.Logs.V1;

namespace OddDotCSharp
{
    public class WhereLogFilterConfigurator
    {
        internal List<Where> Filters { get; } = new List<Where>();
        public WhereLogInstrumentationScopeFilterConfigurator InstrumentationScope { get; }
        public WhereLogResourceFilterConfigurator Resource { get; }

        private readonly ArrayValueFilterConfigurator _arrayValueFilterConfigurator;
        private readonly KeyValueListFilterConfigurator _keyValueListFilterConfigurator;

        public WhereLogFilterConfigurator()
        {
            InstrumentationScope = new WhereLogInstrumentationScopeFilterConfigurator(this);
            Resource = new WhereLogResourceFilterConfigurator(this);
            _arrayValueFilterConfigurator = new ArrayValueFilterConfigurator();
            _keyValueListFilterConfigurator = new KeyValueListFilterConfigurator();
        }
        
        /// <summary>
        /// Adds a filter for TimeUnixNano to the list of filters.
        /// </summary>
        /// <param name="compare">The ulong to compare the TimeUnixNano against.</param>
        /// <param name="compareAs">The type of comparison to perform.</param>
        /// <returns>this <see cref="WhereLogFilterConfigurator"/></returns>
        public WhereLogFilterConfigurator AddTimeUnixNanoFilter(ulong compare, NumberCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    TimeUnixNano = new UInt64Property
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
        /// Adds a filter for ObservedTimeUnixNano to the list of filters.
        /// </summary>
        /// <param name="compare">The ulong to compare the ObservedTimeUnixNano against.</param>
        /// <param name="compareAs">The type of comparison to perform.</param>
        /// <returns>this <see cref="WhereLogFilterConfigurator"/></returns>
        public WhereLogFilterConfigurator AddObservedTimeUnixNanoFilter(ulong compare, NumberCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    ObservedTimeUnixNano = new UInt64Property
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
        /// Adds a filter for SeverityNumber to the list of filters.
        /// </summary>
        /// <param name="compare">The enum to compare the SeverityNumber against.</param>
        /// <param name="compareAs">The type of comparison to perform.</param>
        /// <returns>this <see cref="WhereLogFilterConfigurator"/></returns>
        public WhereLogFilterConfigurator AddSeverityNumberFilter(SeverityNumber compare, EnumCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    SeverityNumber = new SeverityNumberProperty
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
        /// Adds a filter for SeverityText to the list of filters.
        /// </summary>
        /// <param name="compare">The string to compare the SeverityText against.</param>
        /// <param name="compareAs">The type of comparison to perform.</param>
        /// <returns>this <see cref="WhereLogFilterConfigurator"/></returns>
        public WhereLogFilterConfigurator AddSeverityTextFilter(string compare, StringCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    SeverityText = new StringProperty
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
        /// Adds a filter for Body to the list of filters.
        /// </summary>
        /// <param name="compare">The string to compare the Body against.</param>
        /// <param name="compareAs">The type of comparison to perform.</param>
        /// <returns>this <see cref="WhereLogFilterConfigurator"/></returns>
        public WhereLogFilterConfigurator AddBodyFilter(string compare, StringCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Body = new AnyValueProperty
                    {
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
        /// Adds a filter for Body to the list of filters.
        /// </summary>
        /// <param name="compare">The byte[] to compare the Body against.</param>
        /// <param name="compareAs">The type of comparison to perform.</param>
        /// <returns>this <see cref="WhereLogFilterConfigurator"/></returns>
        public WhereLogFilterConfigurator AddBodyFilter(byte[] compare, ByteStringCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Body = new AnyValueProperty
                    {
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
        
        /// <summary>
        /// Adds a filter for Body to the list of filters.
        /// </summary>
        /// <param name="compare">The bool to compare the Body against.</param>
        /// <param name="compareAs">The type of comparison to perform.</param>
        /// <returns>this <see cref="WhereLogFilterConfigurator"/></returns>
        public WhereLogFilterConfigurator AddBodyFilter(bool compare, BoolCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Body = new AnyValueProperty
                    {
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
        /// Adds a filter for Body to the list of filters.
        /// </summary>
        /// <param name="compare">The long to compare the Body against.</param>
        /// <param name="compareAs">The type of comparison to perform.</param>
        /// <returns>this <see cref="WhereLogFilterConfigurator"/></returns>
        public WhereLogFilterConfigurator AddBodyFilter(long compare, NumberCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Body = new AnyValueProperty
                    {
                        IntValue = new Int64Property()
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
        /// Adds a filter for Body to the list of filters.
        /// </summary>
        /// <param name="compare">The double to compare the Body against.</param>
        /// <param name="compareAs">The type of comparison to perform.</param>
        /// <returns>this <see cref="WhereLogFilterConfigurator"/></returns>
        public WhereLogFilterConfigurator AddBodyFilter(double compare, NumberCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Body = new AnyValueProperty
                    {
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
        /// Adds a filter for Body to the list of filters.
        /// </summary>
        /// <param name="configure">The <see cref="ArrayValueFilterConfigurator"/> used for configuring the filter.</param>
        /// <returns>This <see cref="WhereLogFilterConfigurator"/></returns>
        public WhereLogFilterConfigurator AddBodyArrayFilter(Action<ArrayValueFilterConfigurator> configure)
        {
            configure(_arrayValueFilterConfigurator);
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Body = new AnyValueProperty
                    {
                        ArrayValue = new ArrayValueProperty()
                    }
                }
            };
            filter.Property.Body.ArrayValue.Values.AddRange(_arrayValueFilterConfigurator.Properties);
            Filters.Add(filter);
            return this;
        }
        
        /// <summary>
        /// Adds a filter for Body to the list of filters.
        /// </summary>
        /// <param name="configure">The <see cref="KeyValueListFilterConfigurator"/> used for configuring the filter.</param>
        /// <returns>This <see cref="WhereLogFilterConfigurator"/></returns>
        public WhereLogFilterConfigurator AddBodyKeyValueListFilter(Action<KeyValueListFilterConfigurator> configure)
        {
            configure(_keyValueListFilterConfigurator);
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Body = new AnyValueProperty
                    {
                        KvlistValue = new KeyValueListProperty()
                    }
                }
            };
            filter.Property.Body.KvlistValue.Values.AddRange(_keyValueListFilterConfigurator.Properties);
            Filters.Add(filter);
            return this;
        }
        
        /// <summary>
        /// Adds a filter for Attribute to the list of filters.
        /// </summary>
        /// <param name="key">The key of the KeyValue Attribute</param>
        /// <param name="compare">The string to compare the KeyValue against.</param>
        /// <param name="compareAs">The type of comparison to perform. <see cref="StringCompareAsType"/> for more details</param>
        /// <returns>this <see cref="WhereLogFilterConfigurator"/></returns>
        public WhereLogFilterConfigurator AddAttributeFilter(string key, string compare, StringCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
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
            };
            
            Filters.Add(filter);
            return this;
        }
        
        /// <summary>
        /// Adds a filter for Attribute to the list of filters.
        /// </summary>
        /// <param name="key">The key of the KeyValue Attribute</param>
        /// <param name="compare">The byte[] to compare the KeyValue against.</param>
        /// <param name="compareAs">The type of comparison to perform. <see cref="ByteStringCompareAsType"/> for more details</param>
        /// <returns>this <see cref="WhereLogFilterConfigurator"/></returns>
        public WhereLogFilterConfigurator AddAttributeFilter(string key, byte[] compare, ByteStringCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
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
            };
            
            Filters.Add(filter);
            return this;
        }
        
        /// <summary>
        /// Adds a filter for Attribute to the list of filters.
        /// </summary>
        /// <param name="key">The key of the KeyValue Attribute</param>
        /// <param name="compare">The bool to compare the KeyValue against.</param>
        /// <param name="compareAs">The type of comparison to perform. <see cref="BoolCompareAsType"/> for more details</param>
        /// <returns>this <see cref="WhereLogFilterConfigurator"/></returns>
        public WhereLogFilterConfigurator AddAttributeFilter(string key, bool compare, BoolCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
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
            };
            
            Filters.Add(filter);
            return this;
        }
        
        /// <summary>
        /// Adds a filter for Attribute to the list of filters.
        /// </summary>
        /// <param name="key">The key of the KeyValue Attribute</param>
        /// <param name="compare">The long to compare the KeyValue against.</param>
        /// <param name="compareAs">The type of comparison to perform. <see cref="NumberCompareAsType"/> for more details</param>
        /// <returns>this <see cref="WhereLogFilterConfigurator"/></returns>
        public WhereLogFilterConfigurator AddAttributeFilter(string key, long compare, NumberCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
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
            };
            
            Filters.Add(filter);
            return this;
        }
        
        /// <summary>
        /// Adds a filter for Attribute to the list of filters.
        /// </summary>
        /// <param name="key">The key of the KeyValue Attribute</param>
        /// <param name="compare">The double to compare the KeyValue against.</param>
        /// <param name="compareAs">The type of comparison to perform. <see cref="NumberCompareAsType"/> for more details</param>
        /// <returns>this <see cref="WhereLogFilterConfigurator"/></returns>
        public WhereLogFilterConfigurator AddAttributeFilter(string key, double compare, NumberCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
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
            };
            
            Filters.Add(filter);
            return this;
        }
        
        /// <summary>
        /// Adds a filter for DroppedAttributesCount to the list of filters.
        /// </summary>
        /// <param name="compare">The uint to compare the DroppedAttributesCount against.</param>
        /// <param name="compareAs">The type of comparison to perform.</param>
        /// <returns>this <see cref="WhereLogFilterConfigurator"/></returns>
        public WhereLogFilterConfigurator AddDroppedAttributesCountFilter(uint compare, NumberCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    DroppedAttributesCount = new UInt32Property
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
        /// Adds a filter for Flags to the list of filters.
        /// </summary>
        /// <param name="compare">The uint to compare the Flags against.</param>
        /// <param name="compareAs">The type of comparison to perform.</param>
        /// <returns>this <see cref="WhereLogFilterConfigurator"/></returns>
        public WhereLogFilterConfigurator AddFlagsFilter(uint compare, NumberCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Flags = new UInt32Property
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
        /// Adds a filter for TraceId to the list of filters.
        /// </summary>
        /// <param name="compare">The byte[] to compare the TraceId against.</param>
        /// <param name="compareAs">The type of comparison to perform.</param>
        /// <returns>this <see cref="WhereLogFilterConfigurator"/></returns>
        public WhereLogFilterConfigurator AddTraceIdFilter(byte[] compare, ByteStringCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    TraceId = new ByteStringProperty
                    {
                        CompareAs = compareAs,
                        Compare = ByteString.CopyFrom(compare)
                    }
                }
            };
            
            Filters.Add(filter);
            return this;
        }
        
        /// <summary>
        /// Adds a filter for SpanId to the list of filters.
        /// </summary>
        /// <param name="compare">The byte[] to compare the SpanId against.</param>
        /// <param name="compareAs">The type of comparison to perform.</param>
        /// <returns>this <see cref="WhereLogFilterConfigurator"/></returns>
        public WhereLogFilterConfigurator AddSpanIdFilter(byte[] compare, ByteStringCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    SpanId = new ByteStringProperty
                    {
                        CompareAs = compareAs,
                        Compare = ByteString.CopyFrom(compare)
                    }
                }
            };
            
            Filters.Add(filter);
            return this;
        }
        
        /// <summary>
        /// Add a <see cref="OrFilter"/> filter to the list of filters.
        /// </summary>
        /// <param name="configure">Action used to configure the filters. This action behaves the same way as
        /// the action passed to the Where() method. See <see cref="LogQueryRequestBuilder.Where"/>.</param>
        /// <returns>this <see cref="WhereLogFilterConfigurator"/>.</returns>
        public WhereLogFilterConfigurator AddOrFilter(Action<WhereLogFilterConfigurator> configure)
        {
            var configurator = new WhereLogFilterConfigurator();
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