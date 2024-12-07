using System;
using Google.Protobuf;
using Google.Protobuf.Collections;
using OddDotNet.Proto.Common.V1;

namespace OddDotCSharp
{
    /// <summary>
    /// Handles the configuration of filters associated with the ArrayValueProperty
    /// type, which is a property type available on <see cref="AnyValueProperty"/>.
    ///
    /// Each filter added is an individual entry whose existence is checked for in
    /// the property being checked. For example, if property is an ArrayValueProperty
    /// that contains [{string: "test"},{int: 123},{bool: true},{double: 123.00}], and
    /// filters are added for string == "test", bool = true, and double = 123.00, the
    /// collection of filters will return true, because *EACH* item in the filters exists
    /// in the array being checked, even though the array being checked contains additional
    /// values.
    /// </summary>
    public class ArrayValueFilterConfigurator
    {
        internal readonly RepeatedField<AnyValueProperty> Properties = new RepeatedField<AnyValueProperty>();

        internal ArrayValueFilterConfigurator()
        {
        }

        /// <summary>
        /// Adds a double filter to the list of filters. This value must exist in the array
        /// being checked for the property to match.
        /// </summary>
        /// <param name="compare">The value to compare against.</param>
        /// <param name="compareAs">The comparison type to perform.</param>
        /// <returns>This configurator.</returns>
        public ArrayValueFilterConfigurator AddFilter(double compare, NumberCompareAsType compareAs)
        {
            var property = new AnyValueProperty
            {
                DoubleValue = new DoubleProperty
                {
                    CompareAs = compareAs,
                    Compare = compare
                }
            };
            
            Properties.Add(property);
            return this;
        }
        
        /// <summary>
        /// Adds a string filter to the list of filters. This value must exist in the array
        /// being checked for the property to match.
        /// </summary>
        /// <param name="compare">The value to compare against.</param>
        /// <param name="compareAs">The comparison type to perform.</param>
        /// <returns>This configurator.</returns>
        public ArrayValueFilterConfigurator AddFilter(string compare, StringCompareAsType compareAs)
        {
            var property = new AnyValueProperty
            {
                StringValue = new StringProperty
                {
                    CompareAs = compareAs,
                    Compare = compare
                }
            };
            
            Properties.Add(property);
            return this;
        }
        
        /// <summary>
        /// Adds a long filter to the list of filters. This value must exist in the array
        /// being checked for the property to match.
        /// </summary>
        /// <param name="compare">The value to compare against.</param>
        /// <param name="compareAs">The comparison type to perform.</param>
        /// <returns>This configurator.</returns>
        public ArrayValueFilterConfigurator AddFilter(long compare, NumberCompareAsType compareAs)
        {
            var property = new AnyValueProperty
            {
                IntValue = new Int64Property
                {
                    CompareAs = compareAs,
                    Compare = compare
                }
            };
            
            Properties.Add(property);
            return this;
        }
        
        /// <summary>
        /// Adds a byte[] filter to the list of filters. This value must exist in the array
        /// being checked for the property to match.
        /// </summary>
        /// <param name="compare">The value to compare against.</param>
        /// <param name="compareAs">The comparison type to perform.</param>
        /// <returns>This configurator.</returns>
        public ArrayValueFilterConfigurator AddFilter(byte[] compare, ByteStringCompareAsType compareAs)
        {
            var property = new AnyValueProperty
            {
                ByteStringValue = new ByteStringProperty
                {
                    CompareAs = compareAs,
                    Compare = ByteString.CopyFrom(compare)
                }
            };
            
            Properties.Add(property);
            return this;
        }
        
        /// <summary>
        /// Adds a bool filter to the list of filters. This value must exist in the array
        /// being checked for the property to match.
        /// </summary>
        /// <param name="compare">The value to compare against.</param>
        /// <param name="compareAs">The comparison type to perform.</param>
        /// <returns>This configurator.</returns>
        public ArrayValueFilterConfigurator AddFilter(bool compare, BoolCompareAsType compareAs)
        {
            var property = new AnyValueProperty
            {
                BoolValue = new BoolProperty
                {
                    CompareAs = compareAs,
                    Compare = compare
                }
            };
            
            Properties.Add(property);
            return this;
        }
        
        /// <summary>
        /// Adds an array filter to the list of filters. Use this when the ArrayValueProperty
        /// contains an entry that is itself an ArrayValueProperty. The action can be used
        /// to configure additional filters on the sub-array.
        /// </summary>
        /// <param name="configure">The action used to configure the filters for the sub-array.</param>
        /// <returns>This configurator.</returns>
        public ArrayValueFilterConfigurator AddArrayFilter(Action<ArrayValueFilterConfigurator> configure)
        {
            var arrayValueFilterConfigurator = new ArrayValueFilterConfigurator();
            configure(arrayValueFilterConfigurator);
            
            var property = new AnyValueProperty
            {
                ArrayValue = new ArrayValueProperty()
            };
            
            property.ArrayValue.Values.AddRange(arrayValueFilterConfigurator.Properties);
            Properties.Add(property);
            return this;
        }
        
        /// <summary>
        /// Adds a KeyValueList filter to the list of filters. Use this when the ArrayValueProperty
        /// contains an entry that is a KeyValueListProperty. The action can be used to configure
        /// additional filters on the KeyValueListProperty.
        /// </summary>
        /// <param name="configure">The action used to configure the filters for the KeyValueListProperty.</param>
        /// <returns>This configurator.</returns>
        public ArrayValueFilterConfigurator AddKeyValueListFilter(Action<KeyValueListFilterConfigurator> configure)
        {
            var keyValueListFilterConfigurator = new KeyValueListFilterConfigurator();
            configure(keyValueListFilterConfigurator);

            var property = new AnyValueProperty
            {
                KvlistValue = new KeyValueListProperty()
            };
            
            property.KvlistValue.Values.AddRange(keyValueListFilterConfigurator.Properties);
            Properties.Add(property);
            return this;
        }
    }
}