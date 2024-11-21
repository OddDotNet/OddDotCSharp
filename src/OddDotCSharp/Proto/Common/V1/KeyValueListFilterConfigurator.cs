using System;
using Google.Protobuf;
using Google.Protobuf.Collections;
using OddDotNet.Proto.Common.V1;

namespace OddDotCSharp.Proto.Common.V1
{
    public class KeyValueListFilterConfigurator
    {
        internal readonly RepeatedField<KeyValueProperty> Properties = new RepeatedField<KeyValueProperty>();

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