using System;
using Google.Protobuf;
using Google.Protobuf.Collections;
using OddDotNet.Proto.Common.V1;

namespace OddDotCSharp
{
    /// <summary>
    /// Handles the configuration of filters associated with the KeyValueListProperty
    /// type, which is a property type available on <see cref="AnyValueProperty"/>.
    /// </summary>
    public class KeyValueListFilterConfigurator
    {
        internal readonly RepeatedField<KeyValueProperty> Properties = new RepeatedField<KeyValueProperty>();

        internal KeyValueListFilterConfigurator()
        {
        }

        /// <summary>
        /// Adds a string filter to the list of filters. This value must exist in the KeyValueListProperty
        /// being checked for the property to match.
        /// </summary>
        /// <param name="key">The key of the KeyValue being checked for in the KeyValueListProperty.</param>
        /// <param name="compare">The value to compare against.</param>
        /// <param name="compareAs">The comparison type to perform.</param>
        /// <returns>This configurator.</returns>
        public KeyValueListFilterConfigurator AddFilter(string key, string compare, StringCompareAsType compareAs)
        {
            var kvp = new KeyValueProperty
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
            };
            
            Properties.Add(kvp);
            return this;
        }
        
        /// <summary>
        /// Adds a long filter to the list of filters. This value must exist in the KeyValueListProperty
        /// being checked for the property to match.
        /// </summary>
        /// <param name="key">The key of the KeyValue being checked for in the KeyValueListProperty.</param>
        /// <param name="compare">The value to compare against.</param>
        /// <param name="compareAs">The comparison type to perform.</param>
        /// <returns>This configurator.</returns>
        public KeyValueListFilterConfigurator AddFilter(string key, long compare, NumberCompareAsType compareAs)
        {
            var kvp = new KeyValueProperty
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
            };
            
            Properties.Add(kvp);
            return this;
        }
        
        /// <summary>
        /// Adds a byte[] filter to the list of filters. This value must exist in the KeyValueListProperty
        /// being checked for the property to match.
        /// </summary>
        /// <param name="key">The key of the KeyValue being checked for in the KeyValueListProperty.</param>
        /// <param name="compare">The value to compare against.</param>
        /// <param name="compareAs">The comparison type to perform.</param>
        /// <returns>This configurator.</returns>
        public KeyValueListFilterConfigurator AddFilter(string key, byte[] compare, ByteStringCompareAsType compareAs)
        {
            var kvp = new KeyValueProperty
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
            };
            
            Properties.Add(kvp);
            return this;
        }
        
        /// <summary>
        /// Adds a double filter to the list of filters. This value must exist in the KeyValueListProperty
        /// being checked for the property to match.
        /// </summary>
        /// <param name="key">The key of the KeyValue being checked for in the KeyValueListProperty.</param>
        /// <param name="compare">The value to compare against.</param>
        /// <param name="compareAs">The comparison type to perform.</param>
        /// <returns>This configurator.</returns>
        public KeyValueListFilterConfigurator AddFilter(string key, double compare, NumberCompareAsType compareAs)
        {
            var kvp = new KeyValueProperty
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
            };
            
            Properties.Add(kvp);
            return this;
        }
        
        /// <summary>
        /// Adds a bool filter to the list of filters. This value must exist in the KeyValueListProperty
        /// being checked for the property to match.
        /// </summary>
        /// <param name="key">The key of the KeyValue being checked for in the KeyValueListProperty.</param>
        /// <param name="compare">The value to compare against.</param>
        /// <param name="compareAs">The comparison type to perform.</param>
        /// <returns>This configurator.</returns>
        public KeyValueListFilterConfigurator AddFilter(string key, bool compare, BoolCompareAsType compareAs)
        {
            var kvp = new KeyValueProperty
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
            };
            
            Properties.Add(kvp);
            return this;
        }

        /// <summary>
        /// Adds an array filter to the list of filters. Use this when the KeyValueListProperty
        /// contains an entry that is an ArrayValueProperty. The action can be used
        /// to configure additional filters on the sub-array.
        /// </summary>
        /// <param name="key">The key of the KeyValue being check for in the KeyValueListProperty.</param>
        /// <param name="configure">The action used to configure the filters for the ArrayValueProperty.</param>
        /// <returns>This configurator.</returns>
        public KeyValueListFilterConfigurator AddArrayFilter(string key, Action<ArrayValueFilterConfigurator> configure)
        {
            var arrayValueFilterConfigurator = new ArrayValueFilterConfigurator();
            configure(arrayValueFilterConfigurator);
            var kvp = new KeyValueProperty
            {
                Key = key,
                Value = new AnyValueProperty
                {
                    ArrayValue = new ArrayValueProperty()
                }
            };
            kvp.Value.ArrayValue.Values.AddRange(arrayValueFilterConfigurator.Properties);
            Properties.Add(kvp);
            return this;
        }
        
        /// <summary>
        /// Adds a KeyValueList filter to the list of filters. Use this when the KeyValueListProperty
        /// contains an entry that is itself a KeyValueListProperty. The action can be used
        /// to configure additional filters on the sub-KeyValueListProperty.
        /// </summary>
        /// <param name="key">The key of the KeyValue being check for in the KeyValueListProperty.</param>
        /// <param name="configure">The action used to configure the filters for the KeyValueListProperty.</param>
        /// <returns>This configurator.</returns>
        public KeyValueListFilterConfigurator AddKeyValueListFilter(string key, Action<KeyValueListFilterConfigurator> configure)
        {
            var keyValueListFilterConfigurator = new KeyValueListFilterConfigurator();
            configure(keyValueListFilterConfigurator);
            var kvp = new KeyValueProperty
            {
                Key = key,
                Value = new AnyValueProperty
                {
                    KvlistValue = new KeyValueListProperty()
                }
            };
            kvp.Value.KvlistValue.Values.AddRange(keyValueListFilterConfigurator.Properties);
            Properties.Add(kvp);
            return this;
        }
    }
}