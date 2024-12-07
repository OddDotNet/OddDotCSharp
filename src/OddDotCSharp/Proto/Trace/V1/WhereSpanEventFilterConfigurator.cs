using System;
using Google.Protobuf;
using OddDotNet.Proto.Common.V1;
using OddDotNet.Proto.Trace.V1;

namespace OddDotCSharp
{
    /// <summary>
    /// Configurator for properties specific to the Events of a Span.
    /// </summary>
    public class WhereSpanEventFilterConfigurator
    {
        private readonly WhereSpanFilterConfigurator _configurator;
        
        private readonly ArrayValueFilterConfigurator _arrayValueFilterConfigurator;
        private readonly KeyValueListFilterConfigurator _keyValueListFilterConfigurator;

        internal WhereSpanEventFilterConfigurator(WhereSpanFilterConfigurator configurator)
        {
            _configurator = configurator;
            _arrayValueFilterConfigurator = new ArrayValueFilterConfigurator();
            _keyValueListFilterConfigurator = new KeyValueListFilterConfigurator();
        }
        
        /// <summary>
        /// Adds an EventTimeUnixNano filter to the list of filters.
        /// </summary>
        /// <param name="compare">The ulong to compare the property against.</param>
        /// <param name="compareAs">The type of comparison to do. See <see cref="NumberCompareAsType"/> for more details.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddTimeUnixNanoFilter(ulong compare,
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
            
            _configurator.Filters.Add(filter);
            return _configurator;
        }
        
        /// <summary>
        /// Adds an EventName filter to the list of filters.
        /// </summary>
        /// <param name="compare">The string to compare the property against.</param>
        /// <param name="compareAs">The type of comparison to do. See <see cref="StringCompareAsType"/> for more details.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddNameFilter(string compare,
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
            
            _configurator.Filters.Add(filter);
            return _configurator;
        }
        
        /// <summary>
        /// Adds an EventAttribute filter to the list of filters.
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
                    Event = new EventFilter
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
        /// Adds an EventAttribute filter to the list of filters.
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
                    Event = new EventFilter
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
        /// Adds an EventAttribute filter to the list of filters.
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
                    Event = new EventFilter
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
        /// Adds an EventAttribute filter to the list of filters.
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
                    Event = new EventFilter
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
        /// Adds an EventAttribute filter to the list of filters.
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
                    Event = new EventFilter
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
        /// Adds an array filter to the list of filters. <see cref="ArrayValueFilterConfigurator"/> for more details.
        /// </summary>
        /// <param name="key">The key of the attribute being checked.</param>
        /// <param name="configure">The action used to configure the ArrayValueProperty filters being checked.</param>
        /// <returns>This configurator.</returns>
        public WhereSpanFilterConfigurator AddAttributeArrayFilter(string key,
            Action<ArrayValueFilterConfigurator> configure)
        {
            configure(_arrayValueFilterConfigurator);
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Event = new EventFilter
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
            };
            filter.Property.Event.Attributes.Values[0].Value.ArrayValue.Values.AddRange(_arrayValueFilterConfigurator.Properties);
            _configurator.Filters.Add(filter);
            return _configurator;
        }
        
        /// <summary>
        /// Adds a KeyValueList filter to the list of filters. <see cref="KeyValueListFilterConfigurator"/> for more details.
        /// </summary>
        /// <param name="key">The key of the attribute being checked.</param>
        /// <param name="configure">The action used to configure the KeyValueListProperty filters being checked.</param>
        /// <returns>This configurator.</returns>
        public WhereSpanFilterConfigurator AddAttributeKeyValueListFilter(string key,
            Action<KeyValueListFilterConfigurator> configure)
        {
            configure(_keyValueListFilterConfigurator);
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Event = new EventFilter
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
            };
            filter.Property.Event.Attributes.Values[0].Value.KvlistValue.Values.AddRange(_keyValueListFilterConfigurator.Properties);
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
                    Event = new EventFilter
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