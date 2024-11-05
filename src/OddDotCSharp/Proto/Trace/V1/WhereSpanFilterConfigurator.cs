using System;
using System.Collections.Generic;
using Google.Protobuf;
using OddDotNet.Proto.Common.V1;
using OddDotNet.Proto.Resource.V1;
using OddDotNet.Proto.Trace.V1;
using OpenTelemetry.Proto.Trace.V1;


namespace OddDotCSharp
{
    public class WhereSpanFilterConfigurator
    {
        internal List<Where> Filters { get; } = new List<Where>();
        
        /// <summary>
        /// Adds a <see cref="SpanStatusCodeProperty"/> filter to the list of filters.
        /// </summary>
        /// <param name="compare">The <see cref="Status.Types.StatusCode"/> to compare the property against.</param>
        /// <param name="compareAs">The type of comparison to do. See <see cref="EnumCompareAsType"/> for more details.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/>.</returns>
        public WhereSpanFilterConfigurator AddSpanStatusCodeFilter(Status.Types.StatusCode compare,
            EnumCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Status = new StatusFilter
                    {
                        Code = new SpanStatusCodeProperty
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
        /// Adds a <see cref="StringProperty"/> filter to the list of filters.
        /// </summary>
        /// <param name="compare">The string to compare the property against.</param>
        /// <param name="compareAs">The type of comparison to do. See <see cref="StringCompareAsType"/> for more details.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/>.</returns>
        public WhereSpanFilterConfigurator AddSpanStatusMessageFilter(string compare,
            StringCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Status = new StatusFilter
                    {
                        Message = new StringProperty
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
        /// Adds a <see cref="SpanKindProperty"/> filter to the list of filters.
        /// </summary>
        /// <param name="compare">The <see cref="Span.Types.SpanKind"/> to compare the property against.</param>
        /// <param name="compareAs">The type of comparison to do. See <see cref="EnumCompareAsType"/> for more details.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/>.</returns>
        public WhereSpanFilterConfigurator AddSpanKindFilter(Span.Types.SpanKind compare,
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
        /// <exception cref="Exception">When property mismatches are detected on any of the <see cref="Where"/> filters passed in.</exception>
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
        public WhereSpanFilterConfigurator AddSpanNameFilter(string compare, StringCompareAsType compareAs)
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
        /// Adds a filter for LinkSpanId to the list of filters.
        /// </summary>
        /// <param name="compare">The byte[] to compare the Span Id against.</param>
        /// <param name="compareAs">The type of comparison to perform.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddLinkSpanIdFilter(byte[] compare, ByteStringCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Link = new LinkFilter
                    {
                        SpanId = new ByteStringProperty
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
        /// Adds a filter for LinkTraceId to the list of filters.
        /// </summary>
        /// <param name="compare">The byte[] to compare the Trace Id against.</param>
        /// <param name="compareAs">The type of comparison to perform.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddLinkTraceIdFilter(byte[] compare, ByteStringCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Link = new LinkFilter
                    {
                        TraceId = new ByteStringProperty
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
            };
            
            Filters.Add(filter);
            return this;
        }
        
        /// <summary>
        /// Adds an EventTimeUnixNano filter to the list of filters.
        /// </summary>
        /// <param name="compare">The ulong to compare the property against.</param>
        /// <param name="compareAs">The type of comparison to do. See <see cref="NumberCompareAsType"/> for more details.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddEventTimeUnixNanoFilter(ulong compare,
            NumberCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Event = new EventFilter
                    {
                        TimeUnixNano = new UInt64Property
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
        /// Adds an EventName filter to the list of filters.
        /// </summary>
        /// <param name="compare">The string to compare the property against.</param>
        /// <param name="compareAs">The type of comparison to do. See <see cref="StringCompareAsType"/> for more details.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddEventNameFilter(string compare,
            StringCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Event = new EventFilter
                    {
                        Name = new StringProperty
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
        /// Adds an LinkTraceState filter to the list of filters.
        /// </summary>
        /// <param name="compare">The string to compare the property against.</param>
        /// <param name="compareAs">The type of comparison to do. See <see cref="StringCompareAsType"/> for more details.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddLinkTraceStateFilter(string compare,
            StringCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Link = new LinkFilter
                    {
                        TraceState = new StringProperty
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
        /// Adds an InstrumentationScopeName filter to the list of filters.
        /// </summary>
        /// <param name="compare">The string to compare the property against.</param>
        /// <param name="compareAs">The type of comparison to do. See <see cref="StringCompareAsType"/> for more details.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddInstrumentationScopeNameFilter(string compare,
            StringCompareAsType compareAs)
        {
            var filter = new Where
            {
                InstrumentationScope = new InstrumentationScopeFilter
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
        /// Adds an InstrumentationScopeVersion filter to the list of filters.
        /// </summary>
        /// <param name="compare">The string to compare the property against.</param>
        /// <param name="compareAs">The type of comparison to do. See <see cref="StringCompareAsType"/> for more details.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddInstrumentationScopeVersionFilter(string compare,
            StringCompareAsType compareAs)
        {
            var filter = new Where
            {
                InstrumentationScope = new InstrumentationScopeFilter
                {
                    Version = new StringProperty
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
        /// Adds an InstrumentationScopeSchemaUrl filter to the list of filters.
        /// </summary>
        /// <param name="compare">The string to compare the property against.</param>
        /// <param name="compareAs">The type of comparison to do. See <see cref="StringCompareAsType"/> for more details.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddInstrumentationScopeSchemaUrlFilter(string compare,
            StringCompareAsType compareAs)
        {
            var filter = new Where
            {
                InstrumentationScopeSchemaUrl = new StringProperty
                {
                    CompareAs = compareAs,
                    Compare = compare
                }
            };
            
            Filters.Add(filter);
            return this;
        }
        
        /// <summary>
        /// Adds a ResourceSchemaUrl filter to the list of filters.
        /// </summary>
        /// <param name="compare">The string to compare the property against.</param>
        /// <param name="compareAs">The type of comparison to do. See <see cref="StringCompareAsType"/> for more details.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddResourceSchemaUrlFilter(string compare,
            StringCompareAsType compareAs)
        {
            var filter = new Where
            {
                ResourceSchemaUrl = new StringProperty
                {
                    CompareAs = compareAs,
                    Compare = compare
                }
            };
            
            Filters.Add(filter);
            return this;
        }
        
        /// <summary>
        /// Adds a LinkFlags filter to the list of filters.
        /// </summary>
        /// <param name="compare">The uint to compare the property against.</param>
        /// <param name="compareAs">The type of comparison to do. See <see cref="NumberCompareAsType"/> for more details.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddLinkFlagsFilter(uint compare,
            NumberCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Link = new LinkFilter
                    {
                        Flags = new UInt32Property
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
        /// Adds an EventAttribute filter to the list of filters.
        /// </summary>
        /// <param name="key">The key of the KeyValue attribute</param>
        /// <param name="compare">The string to compare the KeyValue value to.</param>
        /// <param name="compareAs">They type of comparison to do. <see cref="StringCompareAsType"/> for more details</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddEventAttributeFilter(string key, string compare, StringCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Event = new EventFilter
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
            };
            
            Filters.Add(filter);
            return this;
        }
        
        /// <summary>
        /// Adds an EventAttribute filter to the list of filters.
        /// </summary>
        /// <param name="key">The key of the KeyValue attribute</param>
        /// <param name="compare">The Int64 to compare the KeyValue value to.</param>
        /// <param name="compareAs">They type of comparison to do. <see cref="NumberCompareAsType"/> for more details</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddEventAttributeFilter(string key, long compare, NumberCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Event = new EventFilter
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
            };
            
            Filters.Add(filter);
            return this;
        }
        
        /// <summary>
        /// Adds an EventAttribute filter to the list of filters.
        /// </summary>
        /// <param name="key">The key of the KeyValue attribute</param>
        /// <param name="compare">The bool to compare the KeyValue value to.</param>
        /// <param name="compareAs">They type of comparison to do. <see cref="BoolCompareAsType"/> for more details</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddEventAttributeFilter(string key, bool compare, BoolCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Event = new EventFilter
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
            };
            
            Filters.Add(filter);
            return this;
        }
        
        /// <summary>
        /// Adds an EventAttribute filter to the list of filters.
        /// </summary>
        /// <param name="key">The key of the KeyValue attribute</param>
        /// <param name="compare">The double to compare the KeyValue value to.</param>
        /// <param name="compareAs">They type of comparison to do. <see cref="NumberCompareAsType"/> for more details</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddEventAttributeFilter(string key, double compare, NumberCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Event = new EventFilter
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
            };
            
            Filters.Add(filter);
            return this;
        }
        
        /// <summary>
        /// Adds an EventAttribute filter to the list of filters.
        /// </summary>
        /// <param name="key">The key of the KeyValue attribute</param>
        /// <param name="compare">The byte array to compare the KeyValue value to.</param>
        /// <param name="compareAs">They type of comparison to do. <see cref="ByteStringCompareAsType"/> for more details</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddEventAttributeFilter(string key, byte[] compare, ByteStringCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Event = new EventFilter
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
            };
            
            Filters.Add(filter);
            return this;
        }
        
        /// <summary>
        /// Adds an LinkAttribute filter to the list of filters.
        /// </summary>
        /// <param name="key">The key of the KeyValue attribute</param>
        /// <param name="compare">The string to compare the KeyValue value to.</param>
        /// <param name="compareAs">They type of comparison to do. <see cref="StringCompareAsType"/> for more details</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddLinkAttributeFilter(string key, string compare, StringCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Link = new LinkFilter
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
            };
            
            Filters.Add(filter);
            return this;
        }
        
        /// <summary>
        /// Adds an LinkAttribute filter to the list of filters.
        /// </summary>
        /// <param name="key">The key of the KeyValue attribute</param>
        /// <param name="compare">The Int64 to compare the KeyValue value to.</param>
        /// <param name="compareAs">They type of comparison to do. <see cref="NumberCompareAsType"/> for more details</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddLinkAttributeFilter(string key, long compare, NumberCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Link = new LinkFilter
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
            };
            
            Filters.Add(filter);
            return this;
        }
        
        /// <summary>
        /// Adds an LinkAttribute filter to the list of filters.
        /// </summary>
        /// <param name="key">The key of the KeyValue attribute</param>
        /// <param name="compare">The bool to compare the KeyValue value to.</param>
        /// <param name="compareAs">They type of comparison to do. <see cref="BoolCompareAsType"/> for more details</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddLinkAttributeFilter(string key, bool compare, BoolCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Link = new LinkFilter
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
            };
            
            Filters.Add(filter);
            return this;
        }
        
        /// <summary>
        /// Adds an LinkAttribute filter to the list of filters.
        /// </summary>
        /// <param name="key">The key of the KeyValue attribute</param>
        /// <param name="compare">The double to compare the KeyValue value to.</param>
        /// <param name="compareAs">They type of comparison to do. <see cref="NumberCompareAsType"/> for more details</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddLinkAttributeFilter(string key, double compare, NumberCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Link = new LinkFilter
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
            };
            
            Filters.Add(filter);
            return this;
        }
        
        /// <summary>
        /// Adds an LinkAttribute filter to the list of filters.
        /// </summary>
        /// <param name="key">The key of the KeyValue attribute</param>
        /// <param name="compare">The byte array to compare the KeyValue value to.</param>
        /// <param name="compareAs">They type of comparison to do. <see cref="ByteStringCompareAsType"/> for more details</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddLinkAttributeFilter(string key, byte[] compare, ByteStringCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Link = new LinkFilter
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
            };
            
            Filters.Add(filter);
            return this;
        }
        
        /// <summary>
        /// Adds an InstrumentationScopeAttribute filter to the list of filters.
        /// </summary>
        /// <param name="key">The key of the KeyValue attribute</param>
        /// <param name="compare">The string to compare the KeyValue value to.</param>
        /// <param name="compareAs">They type of comparison to do. <see cref="StringCompareAsType"/> for more details</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddInstrumentationScopeAttributeFilter(string key, string compare, StringCompareAsType compareAs)
        {
            var filter = new Where
            {
                InstrumentationScope = new InstrumentationScopeFilter
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
            };
            
            Filters.Add(filter);
            return this;
        }
        
        /// <summary>
        /// Adds an InstrumentationScopeAttribute filter to the list of filters.
        /// </summary>
        /// <param name="key">The key of the KeyValue attribute</param>
        /// <param name="compare">The Int64 to compare the KeyValue value to.</param>
        /// <param name="compareAs">They type of comparison to do. <see cref="NumberCompareAsType"/> for more details</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddInstrumentationScopeAttributeFilter(string key, long compare, NumberCompareAsType compareAs)
        {
            var filter = new Where
            {
                InstrumentationScope = new InstrumentationScopeFilter
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
            };
            
            Filters.Add(filter);
            return this;
        }
        
        /// <summary>
        /// Adds an InstrumentationScopeAttribute filter to the list of filters.
        /// </summary>
        /// <param name="key">The key of the KeyValue attribute</param>
        /// <param name="compare">The bool to compare the KeyValue value to.</param>
        /// <param name="compareAs">They type of comparison to do. <see cref="BoolCompareAsType"/> for more details</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddInstrumentationScopeAttributeFilter(string key, bool compare, BoolCompareAsType compareAs)
        {
            var filter = new Where
            {
                InstrumentationScope = new InstrumentationScopeFilter
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
            };
            
            Filters.Add(filter);
            return this;
        }
        
        /// <summary>
        /// Adds an InstrumentationScopeAttribute filter to the list of filters.
        /// </summary>
        /// <param name="key">The key of the KeyValue attribute</param>
        /// <param name="compare">The double to compare the KeyValue value to.</param>
        /// <param name="compareAs">They type of comparison to do. <see cref="NumberCompareAsType"/> for more details</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddInstrumentationScopeAttributeFilter(string key, double compare, NumberCompareAsType compareAs)
        {
            var filter = new Where
            {
                InstrumentationScope = new InstrumentationScopeFilter
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
            };
            
            Filters.Add(filter);
            return this;
        }
        
        /// <summary>
        /// Adds an InstrumentationScopeAttribute filter to the list of filters.
        /// </summary>
        /// <param name="key">The key of the KeyValue attribute</param>
        /// <param name="compare">The byte array to compare the KeyValue value to.</param>
        /// <param name="compareAs">They type of comparison to do. <see cref="ByteStringCompareAsType"/> for more details</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddInstrumentationScopeAttributeFilter(string key, byte[] compare, ByteStringCompareAsType compareAs)
        {
            var filter = new Where
            {
                InstrumentationScope = new InstrumentationScopeFilter
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
            };
            
            Filters.Add(filter);
            return this;
        }
        
        /// <summary>
        /// Adds an ResourceAttribute filter to the list of filters.
        /// </summary>
        /// <param name="key">The key of the KeyValue attribute</param>
        /// <param name="compare">The string to compare the KeyValue value to.</param>
        /// <param name="compareAs">They type of comparison to do. <see cref="StringCompareAsType"/> for more details</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddResourceAttributeFilter(string key, string compare, StringCompareAsType compareAs)
        {
            var filter = new Where
            {
                Resource = new ResourceFilter
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
            };
            
            Filters.Add(filter);
            return this;
        }
        
        /// <summary>
        /// Adds an ResourceAttribute filter to the list of filters.
        /// </summary>
        /// <param name="key">The key of the KeyValue attribute</param>
        /// <param name="compare">The Int64 to compare the KeyValue value to.</param>
        /// <param name="compareAs">They type of comparison to do. <see cref="NumberCompareAsType"/> for more details</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddResourceAttributeFilter(string key, long compare, NumberCompareAsType compareAs)
        {
            var filter = new Where
            {
                Resource = new ResourceFilter
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
            };
            
            Filters.Add(filter);
            return this;
        }
        
        /// <summary>
        /// Adds an ResourceAttribute filter to the list of filters.
        /// </summary>
        /// <param name="key">The key of the KeyValue attribute</param>
        /// <param name="compare">The bool to compare the KeyValue value to.</param>
        /// <param name="compareAs">They type of comparison to do. <see cref="BoolCompareAsType"/> for more details</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddResourceAttributeFilter(string key, bool compare, BoolCompareAsType compareAs)
        {
            var filter = new Where
            {
                Resource = new ResourceFilter
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
            };
            
            Filters.Add(filter);
            return this;
        }
        
        /// <summary>
        /// Adds an ResourceAttribute filter to the list of filters.
        /// </summary>
        /// <param name="key">The key of the KeyValue attribute</param>
        /// <param name="compare">The double to compare the KeyValue value to.</param>
        /// <param name="compareAs">They type of comparison to do. <see cref="NumberCompareAsType"/> for more details</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddResourceAttributeFilter(string key, double compare, NumberCompareAsType compareAs)
        {
            var filter = new Where
            {
                Resource = new ResourceFilter
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
            };
            
            Filters.Add(filter);
            return this;
        }
        
        /// <summary>
        /// Adds an ResourceAttribute filter to the list of filters.
        /// </summary>
        /// <param name="key">The key of the KeyValue attribute</param>
        /// <param name="compare">The byte array to compare the KeyValue value to.</param>
        /// <param name="compareAs">They type of comparison to do. <see cref="ByteStringCompareAsType"/> for more details</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddResourceAttributeFilter(string key, byte[] compare, ByteStringCompareAsType compareAs)
        {
            var filter = new Where
            {
                Resource = new ResourceFilter
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
            };
            
            Filters.Add(filter);
            return this;
        }
    }
}