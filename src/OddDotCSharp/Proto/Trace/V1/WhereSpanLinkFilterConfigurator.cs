using Google.Protobuf;
using OddDotNet.Proto.Common.V1;
using OddDotNet.Proto.Trace.V1;

namespace OddDotCSharp
{
    public class WhereSpanLinkFilterConfigurator
    {
        private readonly WhereSpanFilterConfigurator _configurator;

        public WhereSpanLinkFilterConfigurator(WhereSpanFilterConfigurator configurator)
        {
            _configurator = configurator;
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
            
            _configurator.Filters.Add(filter);
            return _configurator;
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
            
            _configurator.Filters.Add(filter);
            return _configurator;
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
            
            _configurator.Filters.Add(filter);
            return _configurator;
        }
        
        /// <summary>
        /// Adds a Flags filter to the list of filters.
        /// </summary>
        /// <param name="compare">The uint to compare the property against.</param>
        /// <param name="compareAs">The type of comparison to do. See <see cref="NumberCompareAsType"/> for more details.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddFlagsFilter(uint compare,
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
            
            _configurator.Filters.Add(filter);
            return _configurator;
        }
        
        /// <summary>
        /// Adds an Attribute filter to the list of filters.
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
                    Link = new LinkFilter
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
            };
            
            _configurator.Filters.Add(filter);
            return _configurator;
        }
        
        /// <summary>
        /// Adds an Attribute filter to the list of filters.
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
                    Link = new LinkFilter
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
            };
            
            _configurator.Filters.Add(filter);
            return _configurator;
        }
        
        /// <summary>
        /// Adds an Attribute filter to the list of filters.
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
                    Link = new LinkFilter
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
            };
            
            _configurator.Filters.Add(filter);
            return _configurator;
        }
        
        /// <summary>
        /// Adds an Attribute filter to the list of filters.
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
                    Link = new LinkFilter
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
            };
            
            _configurator.Filters.Add(filter);
            return _configurator;
        }
        
        /// <summary>
        /// Adds an Attribute filter to the list of filters.
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
                    Link = new LinkFilter
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
            };
            
            _configurator.Filters.Add(filter);
            return _configurator;
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
                    Link = new LinkFilter
                    {
                        DroppedAttributesCount = new UInt32Property
                        {
                            CompareAs = compareAs,
                            Compare = compare
                        }
                    }
                }
            };
            
            _configurator.Filters.Add(filter);
            return _configurator;
        }
    }
}