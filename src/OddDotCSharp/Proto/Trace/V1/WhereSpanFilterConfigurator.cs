using System;
using System.Collections.Generic;
using Google.Protobuf;
using OddDotNet.Proto.Common.V1;
using OddDotNet.Proto.Trace.V1;
using OpenTelemetry.Proto.Trace.V1;


namespace OddDotCSharp
{
    public class WhereSpanFilterConfigurator
    {
        internal List<Where> Filters { get; } = new List<Where>();
        public WhereSpanEventFilterConfigurator Event { get; }
        public WhereSpanLinkFilterConfigurator Link { get; }
        public WhereSpanResourceFilterConfigurator Resource { get; }
        public WhereSpanStatusFilterConfigurator Status { get; }
        public WhereSpanInstrumentationScopeFilterConfigurator InstrumentationScope { get; }

        public WhereSpanFilterConfigurator()
        {
            Event = new WhereSpanEventFilterConfigurator(this);
            Link = new WhereSpanLinkFilterConfigurator(this);
            Resource = new WhereSpanResourceFilterConfigurator(this);
            Status = new WhereSpanStatusFilterConfigurator(this);
            InstrumentationScope = new WhereSpanInstrumentationScopeFilterConfigurator(this);
        }
        
        /// <summary>
        /// Adds a <see cref="SpanKindProperty"/> filter to the list of filters.
        /// </summary>
        /// <param name="compare">The <see cref="Span.Types.SpanKind"/> to compare the property against.</param>
        /// <param name="compareAs">The type of comparison to do. See <see cref="EnumCompareAsType"/> for more details.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/>.</returns>
        public WhereSpanFilterConfigurator AddKindFilter(Span.Types.SpanKind compare,
            EnumCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Kind = new SpanKindProperty
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
        /// Add a <see cref="OrFilter"/> filter to the list of filters.
        /// </summary>
        /// <param name="configure">Action used to configure the filters. This action behaves the same way as
        /// the action passed to the Where() method. See <see cref="SpanQueryRequestBuilder.Where"/>.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/>.</returns>
        public WhereSpanFilterConfigurator AddOrFilter(Action<WhereSpanFilterConfigurator> configure)
        {
            var configurator = new WhereSpanFilterConfigurator();
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

        /// <summary>
        /// Adds a filter for SpanName to the list of filters.
        /// </summary>
        /// <param name="compare">The string to compare the Span Name against.</param>
        /// <param name="compareAs">The type of comparison to perform.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddNameFilter(string compare, StringCompareAsType compareAs)
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
        /// Adds a filter for SpanId to the list of filters.
        /// </summary>
        /// <param name="compare">The byte[] to compare the Span Id against.</param>
        /// <param name="compareAs">The type of comparison to perform.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddSpanIdFilter(byte[] compare, ByteStringCompareAsType compareAs)
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
        /// Adds a filter for TraceId to the list of filters.
        /// </summary>
        /// <param name="compare">The byte[] to compare the Trace Id against.</param>
        /// <param name="compareAs">The type of comparison to perform.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddTraceIdFilter(byte[] compare, ByteStringCompareAsType compareAs)
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
        /// Adds a filter for ParentSpanId to the list of filters.
        /// </summary>
        /// <param name="compare">The byte[] to compare the Parent Span Id against.</param>
        /// <param name="compareAs">The type of comparison to perform.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddParentSpanIdFilter(byte[] compare, ByteStringCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    ParentSpanId = new ByteStringProperty
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
        /// Adds a StartTimeUnixNano filter to the list of filters.
        /// </summary>
        /// <param name="compare">The ulong to compare the property against.</param>
        /// <param name="compareAs">The type of comparison to do. See <see cref="NumberCompareAsType"/> for more details.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddStartTimeUnixNanoFilter(ulong compare,
            NumberCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    StartTimeUnixNano = new UInt64Property
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
        /// Adds an EndTimeUnixNano filter to the list of filters.
        /// </summary>
        /// <param name="compare">The ulong to compare the property against.</param>
        /// <param name="compareAs">The type of comparison to do. See <see cref="NumberCompareAsType"/> for more details.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddEndTimeUnixNanoFilter(ulong compare,
            NumberCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    EndTimeUnixNano = new UInt64Property
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
        /// Adds an attribute filter to the list of filters.
        /// </summary>
        /// <param name="key">The key of the KeyValue attribute</param>
        /// <param name="compare">The string to compare the KeyValue value to.</param>
        /// <param name="compareAs">They type of comparison to do. <see cref="StringCompareAsType"/> for more details</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddAttributeFilter(string key, string compare, StringCompareAsType compareAs)
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
        /// Adds an attribute filter to the list of filters.
        /// </summary>
        /// <param name="key">The key of the KeyValue attribute</param>
        /// <param name="compare">The Int64 to compare the KeyValue value to.</param>
        /// <param name="compareAs">They type of comparison to do. <see cref="NumberCompareAsType"/> for more details</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddAttributeFilter(string key, long compare, NumberCompareAsType compareAs)
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
        /// Adds an attribute filter to the list of filters.
        /// </summary>
        /// <param name="key">The key of the KeyValue attribute</param>
        /// <param name="compare">The bool to compare the KeyValue value to.</param>
        /// <param name="compareAs">They type of comparison to do. <see cref="BoolCompareAsType"/> for more details</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddAttributeFilter(string key, bool compare, BoolCompareAsType compareAs)
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
        /// Adds an attribute filter to the list of filters.
        /// </summary>
        /// <param name="key">The key of the KeyValue attribute</param>
        /// <param name="compare">The double to compare the KeyValue value to.</param>
        /// <param name="compareAs">They type of comparison to do. <see cref="NumberCompareAsType"/> for more details</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddAttributeFilter(string key, double compare, NumberCompareAsType compareAs)
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
        /// Adds an attribute filter to the list of filters.
        /// </summary>
        /// <param name="key">The key of the KeyValue attribute</param>
        /// <param name="compare">The byte array to compare the KeyValue value to.</param>
        /// <param name="compareAs">They type of comparison to do. <see cref="ByteStringCompareAsType"/> for more details</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddAttributeFilter(string key, byte[] compare, ByteStringCompareAsType compareAs)
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
        /// Adds an TraceState filter to the list of filters.
        /// </summary>
        /// <param name="compare">The string to compare the property against.</param>
        /// <param name="compareAs">The type of comparison to do. See <see cref="StringCompareAsType"/> for more details.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddTraceStateFilter(string compare,
            StringCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    TraceState = new StringProperty
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
        /// Adds a Flags filter to the list of filters.
        /// </summary>
        /// <param name="compare">The ulong to compare the property against.</param>
        /// <param name="compareAs">The type of comparison to do. See <see cref="NumberCompareAsType"/> for more details.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddFlagsFilter(uint compare,
            NumberCompareAsType compareAs)
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
        /// Adds a DroppedAttributesCount filter to the list of filters.
        /// </summary>
        /// <param name="compare">The uint to compare the property against.</param>
        /// <param name="compareAs">The type of comparison to do. See <see cref="NumberCompareAsType"/> for more details.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddDroppedAttributesCountFilter(uint compare,
            NumberCompareAsType compareAs)
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
        /// Adds a DroppedEventsCount filter to the list of filters.
        /// </summary>
        /// <param name="compare">The uint to compare the property against.</param>
        /// <param name="compareAs">The type of comparison to do. See <see cref="NumberCompareAsType"/> for more details.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddDroppedEventsCountFilter(uint compare,
            NumberCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    DroppedEventsCount = new UInt32Property
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
        /// Adds a DroppedLinksCount filter to the list of filters.
        /// </summary>
        /// <param name="compare">The uint to compare the property against.</param>
        /// <param name="compareAs">The type of comparison to do. See <see cref="NumberCompareAsType"/> for more details.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddDroppedLinksCountFilter(uint compare,
            NumberCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    DroppedLinksCount = new UInt32Property
                    {
                        CompareAs = compareAs,
                        Compare = compare
                    }
                }
            };
            
            Filters.Add(filter);
            return this;
        }
    }
}