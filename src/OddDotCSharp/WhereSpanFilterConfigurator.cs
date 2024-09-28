using System;
using System.Collections.Generic;
using Google.Protobuf;
using OddDotNet.Proto.Spans.V1;

namespace OddDotCSharp
{
    public class WhereSpanFilterConfigurator
    {
        internal List<WhereSpanFilter> Filters { get; } = new List<WhereSpanFilter>();

        public WhereSpanFilterConfigurator AddFilter(WhereSpanPropertyFilter.PropertyOneofCase property, string compare,
            StringCompareAsType compareAs)
        {
            ThrowOnPropertyTypeMismatch(typeof(StringProperty), property);
            
            var stringProperty = new StringProperty
            {
                CompareAs = compareAs,
                Compare = compare
            };
            
            AddFilter(property, stringProperty);
            return this;
        }

        public WhereSpanFilterConfigurator AddFilter(WhereSpanPropertyFilter.PropertyOneofCase property, byte[] compare,
            ByteStringCompareAsType compareAs)
        {
            ThrowOnPropertyTypeMismatch(typeof(ByteStringProperty), property);
            
            var byteStringProperty = new ByteStringProperty
            {
                CompareAs = compareAs,
                Compare = ByteString.CopyFrom(compare)
            };
            
            AddFilter(property, byteStringProperty);
            return this;
        }
        
        public WhereSpanFilterConfigurator AddFilter(WhereSpanPropertyFilter.PropertyOneofCase property, ulong compare,
            UInt64CompareAsType compareAs)
        {
            ThrowOnPropertyTypeMismatch(typeof(UInt64Property), property);
            
            var uint64Property = new UInt64Property
            {
                CompareAs = compareAs,
                Compare = compare
            };
            
            AddFilter(property, uint64Property);
            return this;
        }
        
        public WhereSpanFilterConfigurator AddFilter(WhereSpanPropertyFilter.PropertyOneofCase property, SpanStatusCode compare,
            EnumCompareAsType compareAs)
        {
            ThrowOnPropertyTypeMismatch(typeof(SpanStatusCodeProperty), property);
            
            var spanStatusCodeProperty = new SpanStatusCodeProperty
            {
                CompareAs = compareAs,
                Compare = compare
            };
            
            AddFilter(property, spanStatusCodeProperty);
            return this;
        }
        
        public WhereSpanFilterConfigurator AddFilter(WhereSpanPropertyFilter.PropertyOneofCase property, SpanKind compare,
            EnumCompareAsType compareAs)
        {
            ThrowOnPropertyTypeMismatch(typeof(SpanKindProperty), property);
            
            var spanKindProperty = new SpanKindProperty
            {
                CompareAs = compareAs,
                Compare = compare
            };
            
            AddFilter(property, spanKindProperty);
            return this;
        }
        
        public WhereSpanFilterConfigurator AddFilter(WhereSpanPropertyFilter.PropertyOneofCase property, uint compare,
            UInt32CompareAsType compareAs)
        {
            ThrowOnPropertyTypeMismatch(typeof(UInt32Property), property);
            
            var uint32Property = new UInt32Property
            {
                CompareAs = compareAs,
                Compare = compare
            };
            
            AddFilter(property, uint32Property);
            return this;
        }
        
        public WhereSpanFilterConfigurator AddFilter(WhereSpanPropertyFilter.PropertyOneofCase property, string key, string compare,
            StringCompareAsType compareAs)
        {
            ThrowOnPropertyTypeMismatch(typeof(KeyValueProperty), property);
            
            var keyValueProperty = new KeyValueProperty
            {
                Key = key,
                StringValue = new StringProperty
                {
                    CompareAs = compareAs,
                    Compare = compare
                }
            };
            
            AddFilter(property, keyValueProperty);
            return this;
        }
        
        public WhereSpanFilterConfigurator AddFilter(WhereSpanPropertyFilter.PropertyOneofCase property, string key, bool compare,
            BoolCompareAsType compareAs)
        {
            ThrowOnPropertyTypeMismatch(typeof(KeyValueProperty), property);
            
            var keyValueProperty = new KeyValueProperty
            {
                Key = key,
                BoolValue = new BoolProperty
                {
                    CompareAs = compareAs,
                    Compare = compare
                }
            };
            
            AddFilter(property, keyValueProperty);
            return this;
        }
        
        public WhereSpanFilterConfigurator AddFilter(WhereSpanPropertyFilter.PropertyOneofCase property, string key, long compare,
            Int64CompareAsType compareAs)
        {
            ThrowOnPropertyTypeMismatch(typeof(KeyValueProperty), property);
            
            var keyValueProperty = new KeyValueProperty
            {
                Key = key,
                Int64Value = new Int64Property
                {
                    CompareAs = compareAs,
                    Compare = compare
                }
            };
            
            AddFilter(property, keyValueProperty);
            return this;
        }
        
        public WhereSpanFilterConfigurator AddFilter(WhereSpanPropertyFilter.PropertyOneofCase property, string key, double compare,
            DoubleCompareAsType compareAs)
        {
            ThrowOnPropertyTypeMismatch(typeof(KeyValueProperty), property);
            
            var keyValueProperty = new KeyValueProperty
            {
                Key = key,
                DoubleValue = new DoubleProperty
                {
                    CompareAs = compareAs,
                    Compare = compare
                }
            };
            
            AddFilter(property, keyValueProperty);
            return this;
        }
        
        public WhereSpanFilterConfigurator AddFilter(WhereSpanPropertyFilter.PropertyOneofCase property, string key, byte[] compare,
            ByteStringCompareAsType compareAs)
        {
            ThrowOnPropertyTypeMismatch(typeof(KeyValueProperty), property);
            
            var keyValueProperty = new KeyValueProperty
            {
                Key = key,
                ByteStringValue = new ByteStringProperty
                {
                    CompareAs = compareAs,
                    Compare = ByteString.CopyFrom(compare)
                }
            };
            
            AddFilter(property, keyValueProperty);
            return this;
        }

        public WhereSpanFilterConfigurator AddOrFilter(Action<WhereSpanFilterConfigurator> configure)
        {
            var configurator = new WhereSpanFilterConfigurator();
            configure(configurator);

            var orFilter = new WhereSpanOrFilter();
            orFilter.Filters.AddRange(configurator.Filters);

            var filter = new WhereSpanFilter
            {
                SpanOr = orFilter
            };
            
            Filters.Add(filter);
            return this;
        }

        private void ThrowOnPropertyTypeMismatch(Type expectedType, WhereSpanPropertyFilter.PropertyOneofCase property)
        {
            var propertyInfo = typeof(WhereSpanPropertyFilter).GetProperty(property.ToString());
            if (propertyInfo == null)
                throw new Exception("Could not find matching property name for WhereSpanPropertyFilter"); // TODO better exception
            if (propertyInfo.PropertyType != expectedType)
                throw new Exception(
                    $"Property {property.ToString()} is of type {propertyInfo.PropertyType}, but you supplied {expectedType}");
        }

        private void AddFilter(WhereSpanPropertyFilter.PropertyOneofCase property, object spanProperty)
        {
            var spanPropertyFilter = new WhereSpanPropertyFilter();
            
            // If we've gotten this far, we know the property exists, and we know it's the right type, so `!` because
            // we know the PropertyInfo will not be null.
            spanPropertyFilter.GetType().GetProperty(property.ToString())!.SetValue(spanPropertyFilter, spanProperty);
            var filter = new WhereSpanFilter
            {
                SpanProperty = spanPropertyFilter
            };
            Filters.Add(filter);
        }
    }
}