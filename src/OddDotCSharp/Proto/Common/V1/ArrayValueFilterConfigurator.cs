using System;
using Google.Protobuf;
using Google.Protobuf.Collections;
using OddDotNet.Proto.Common.V1;

namespace OddDotCSharp.Proto.Common.V1
{
    public class ArrayValueFilterConfigurator
    {
        internal readonly RepeatedField<AnyValueProperty> Properties = new RepeatedField<AnyValueProperty>();
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