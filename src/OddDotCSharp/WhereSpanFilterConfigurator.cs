using System;
using System.Collections.Generic;
using Google.Protobuf;
using OddDotNet.Proto.Spans.V1;

namespace OddDotCSharp
{
    public class WhereSpanFilterConfigurator
    {
        internal List<WhereSpanFilter> Filters { get; } = new List<WhereSpanFilter>();

        /// <summary>
        /// Adds a <see cref="StringProperty"/> filter to the list of filters. 
        /// </summary>
        /// <param name="property">The enumeration identifying which SpanProperty to filter on.</param>
        /// <param name="compare">The string to compare the property against.</param>
        /// <param name="compareAs">The type of comparison to do. See <see cref="StringCompareAsType"/> for more details.</param>
        /// <returns>this WhereSpanFilterConfigurator</returns>
        /// <exception cref="Exception">When the <see cref="WhereSpanPropertyFilter"/> Property is not a <see cref="StringProperty"/></exception>
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

        /// <summary>
        /// Adds a <see cref="ByteStringProperty"/> filter to the list of filters.
        /// </summary>
        /// <param name="property">The enumeration identifying which SpanProperty to filter on.</param>
        /// <param name="compare">The byte array to compare the property against.</param>
        /// <param name="compareAs">The type of comparison to do. See <see cref="ByteStringCompareAsType"/> for more details.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        /// <exception cref="Exception">When the <see cref="WhereSpanPropertyFilter"/> Property is not a <see cref="ByteStringProperty"/></exception>
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
        
        /// <summary>
        /// Adds a <see cref="UInt64Property"/> filter to the list of filters.
        /// </summary>
        /// <param name="property">The enumeration identifying which SpanProperty to filter on.</param>
        /// <param name="compare">The ulong to compare the property against.</param>
        /// <param name="compareAs">The type of comparison to do. See <see cref="UInt64CompareAsType"/> for more details.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        /// <exception cref="Exception">When the <see cref="WhereSpanPropertyFilter"/> Property is not a <see cref="UInt64Property"/></exception>
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
        
        /// <summary>
        /// Adds a <see cref="SpanStatusCodeProperty"/> filter to the list of filters.
        /// </summary>
        /// <param name="compare">The <see cref="SpanStatusCode"/> to compare the property against.</param>
        /// <param name="compareAs">The type of comparison to do. See <see cref="EnumCompareAsType"/> for more details.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/>.</returns>
        public WhereSpanFilterConfigurator AddSpanStatusCodeFilter(SpanStatusCode compare,
            EnumCompareAsType compareAs)
        {
            var spanStatusCodeProperty = new SpanStatusCodeProperty
            {
                CompareAs = compareAs,
                Compare = compare
            };
            
            AddFilter(WhereSpanPropertyFilter.PropertyOneofCase.StatusCode, spanStatusCodeProperty);
            return this;
        }
        
        /// <summary>
        /// Adds a <see cref="SpanKindProperty"/> filter to the list of filters.
        /// </summary>
        /// <param name="compare">The <see cref="SpanKind"/> to compare the property against.</param>
        /// <param name="compareAs">The type of comparison to do. See <see cref="EnumCompareAsType"/> for more details.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/>.</returns>
        public WhereSpanFilterConfigurator AddSpanKindFilter(SpanKind compare,
            EnumCompareAsType compareAs)
        {
            var spanKindProperty = new SpanKindProperty
            {
                CompareAs = compareAs,
                Compare = compare
            };
            
            AddFilter(WhereSpanPropertyFilter.PropertyOneofCase.Kind, spanKindProperty);
            return this;
        }
        
        /// <summary>
        /// Adds a <see cref="UInt32Property"/> filter to the list of filters.
        /// </summary>
        /// <param name="property">The enumeration identifying which SpanProperty to filter on.</param>
        /// <param name="compare">The uint to compare the property against.</param>
        /// <param name="compareAs">The type of comparison to do. See <see cref="UInt32CompareAsType"/> for more details.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        /// <exception cref="Exception">When the <see cref="WhereSpanPropertyFilter"/> Property is not a <see cref="UInt32Property"/></exception>
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
        
        /// <summary>
        /// Adds a <see cref="KeyValueProperty"/> filter to the list of filters.
        /// </summary>
        /// <param name="property">The enumeration identifying which SpanProperty to filter on.</param>
        /// <param name="key">The key of the <see cref="KeyValueProperty"/>.</param>
        /// <param name="compare">The string to compare the property against.</param>
        /// <param name="compareAs">The type of comparison to do. See <see cref="StringCompareAsType"/> for more details.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/>.</returns>
        /// <exception cref="Exception">When the <see cref="WhereSpanPropertyFilter"/> Property is not a <see cref="KeyValueProperty"/></exception>
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
        
        /// <summary>
        /// Adds a <see cref="KeyValueProperty"/> filter to the list of filters.
        /// </summary>
        /// <param name="property">The enumeration identifying which SpanProperty to filter on.</param>
        /// <param name="key">The key of the <see cref="KeyValueProperty"/>.</param>
        /// <param name="compare">The bool to compare the property against.</param>
        /// <param name="compareAs">The type of comparison to do. See <see cref="BoolCompareAsType"/> for more details.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/>.</returns>
        /// <exception cref="Exception">When the <see cref="WhereSpanPropertyFilter"/> Property is not a <see cref="KeyValueProperty"/></exception>
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
        
        /// <summary>
        /// Adds a <see cref="KeyValueProperty"/> filter to the list of filters.
        /// </summary>
        /// <param name="property">The enumeration identifying which SpanProperty to filter on.</param>
        /// <param name="key">The key of the <see cref="KeyValueProperty"/>.</param>
        /// <param name="compare">The long to compare the property against.</param>
        /// <param name="compareAs">The type of comparison to do. See <see cref="Int64CompareAsType"/> for more details.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/>.</returns>
        /// <exception cref="Exception">When the <see cref="WhereSpanPropertyFilter"/> Property is not a <see cref="KeyValueProperty"/></exception>
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
        
        /// <summary>
        /// Adds a <see cref="KeyValueProperty"/> filter to the list of filters.
        /// </summary>
        /// <param name="property">The enumeration identifying which SpanProperty to filter on.</param>
        /// <param name="key">The key of the <see cref="KeyValueProperty"/>.</param>
        /// <param name="compare">The double to compare the property against.</param>
        /// <param name="compareAs">The type of comparison to do. See <see cref="DoubleCompareAsType"/> for more details.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/>.</returns>
        /// <exception cref="Exception">When the <see cref="WhereSpanPropertyFilter"/> Property is not a <see cref="KeyValueProperty"/></exception>
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
        
        /// <summary>
        /// Adds a <see cref="KeyValueProperty"/> filter to the list of filters.
        /// </summary>
        /// <param name="property">The enumeration identifying which SpanProperty to filter on.</param>
        /// <param name="key">The key of the <see cref="KeyValueProperty"/>.</param>
        /// <param name="compare">The byte array to compare the property against.</param>
        /// <param name="compareAs">The type of comparison to do. See <see cref="ByteStringCompareAsType"/> for more details.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/>.</returns>
        /// <exception cref="Exception">When the <see cref="WhereSpanPropertyFilter"/> Property is not a <see cref="KeyValueProperty"/></exception>
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

        /// <summary>
        /// Add a <see cref="WhereSpanOrFilter"/> filter to the list of filters.
        /// </summary>
        /// <param name="configure">Action used to configure the filters. This action behaves the same way as
        /// the action passed to the Where() method. See <see cref="SpanQueryRequestBuilder.Where"/>.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/>.</returns>
        /// <exception cref="Exception">When property mismatches are detected on any of the <see cref="WhereSpanFilter"/> filters passed in.</exception>
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