using System;
using System.Collections.Generic;
using Google.Protobuf;
using OddDotNet.Proto.Trace.V1;
using OpenTelemetry.Proto.Trace.V1;


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
        public WhereSpanFilterConfigurator AddFilter(WhereSpanPropertyFilter.ValueOneofCase property, string compare,
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
        public WhereSpanFilterConfigurator AddFilter(WhereSpanPropertyFilter.ValueOneofCase property, byte[] compare,
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
        /// <param name="compareAs">The type of comparison to do. See <see cref="NumberCompareAsType"/> for more details.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        /// <exception cref="Exception">When the <see cref="WhereSpanPropertyFilter"/> Property is not a <see cref="UInt64Property"/></exception>
        public WhereSpanFilterConfigurator AddFilter(WhereSpanPropertyFilter.ValueOneofCase property, ulong compare,
            NumberCompareAsType compareAs)
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
        public WhereSpanFilterConfigurator AddSpanStatusCodeFilter(Status.Types.StatusCode compare,
            EnumCompareAsType compareAs)
        {
            var spanStatusCodeProperty = new SpanStatusCodeProperty
            {
                CompareAs = compareAs,
                Compare = compare
            };
            
            AddFilter(WhereSpanPropertyFilter.ValueOneofCase.StatusCode, spanStatusCodeProperty);
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
            var spanKindProperty = new SpanKindProperty
            {
                CompareAs = compareAs,
                Compare = compare
            };
            
            AddFilter(WhereSpanPropertyFilter.ValueOneofCase.Kind, spanKindProperty);
            return this;
        }
        
        /// <summary>
        /// Adds a <see cref="UInt32Property"/> filter to the list of filters.
        /// </summary>
        /// <param name="property">The enumeration identifying which SpanProperty to filter on.</param>
        /// <param name="compare">The uint to compare the property against.</param>
        /// <param name="compareAs">The type of comparison to do. See <see cref="NumberCompareAsType"/> for more details.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        /// <exception cref="Exception">When the <see cref="WhereSpanPropertyFilter"/> Property is not a <see cref="UInt32Property"/></exception>
        public WhereSpanFilterConfigurator AddFilter(WhereSpanPropertyFilter.ValueOneofCase property, uint compare,
            NumberCompareAsType compareAs)
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
        public WhereSpanFilterConfigurator AddFilter(WhereSpanPropertyFilter.ValueOneofCase property, string key, string compare,
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
        public WhereSpanFilterConfigurator AddFilter(WhereSpanPropertyFilter.ValueOneofCase property, string key, bool compare,
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
        /// <param name="compareAs">The type of comparison to do. See <see cref="NumberCompareAsType"/> for more details.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/>.</returns>
        /// <exception cref="Exception">When the <see cref="WhereSpanPropertyFilter"/> Property is not a <see cref="KeyValueProperty"/></exception>
        public WhereSpanFilterConfigurator AddFilter(WhereSpanPropertyFilter.ValueOneofCase property, string key, long compare,
            NumberCompareAsType compareAs)
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
        /// <param name="compareAs">The type of comparison to do. See <see cref="NumberCompareAsType"/> for more details.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/>.</returns>
        /// <exception cref="Exception">When the <see cref="WhereSpanPropertyFilter"/> Property is not a <see cref="KeyValueProperty"/></exception>
        public WhereSpanFilterConfigurator AddFilter(WhereSpanPropertyFilter.ValueOneofCase property, string key, double compare,
            NumberCompareAsType compareAs)
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
        public WhereSpanFilterConfigurator AddFilter(WhereSpanPropertyFilter.ValueOneofCase property, string key, byte[] compare,
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

        /// <summary>
        /// Adds a filter for SpanName to the list of filters.
        /// </summary>
        /// <param name="compare">The string to compare the Span Name against.</param>
        /// <param name="compareAs">The type of comparison to perform.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddSpanNameFilter(string compare, StringCompareAsType compareAs)
        {
            return AddFilter(WhereSpanPropertyFilter.ValueOneofCase.Name, compare, compareAs);
        }
        
        /// <summary>
        /// Adds a filter for SpanId to the list of filters.
        /// </summary>
        /// <param name="compare">The byte[] to compare the Span Id against.</param>
        /// <param name="compareAs">The type of comparison to perform.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddSpanIdFilter(byte[] compare, ByteStringCompareAsType compareAs)
        {
            return AddFilter(WhereSpanPropertyFilter.ValueOneofCase.SpanId, compare, compareAs);
        }
        
        /// <summary>
        /// Adds a filter for TraceId to the list of filters.
        /// </summary>
        /// <param name="compare">The byte[] to compare the Trace Id against.</param>
        /// <param name="compareAs">The type of comparison to perform.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddTraceIdFilter(byte[] compare, ByteStringCompareAsType compareAs)
        {
            return AddFilter(WhereSpanPropertyFilter.ValueOneofCase.TraceId, compare, compareAs);
        }
        
        /// <summary>
        /// Adds a filter for ParentSpanId to the list of filters.
        /// </summary>
        /// <param name="compare">The byte[] to compare the Parent Span Id against.</param>
        /// <param name="compareAs">The type of comparison to perform.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddParentSpanIdFilter(byte[] compare, ByteStringCompareAsType compareAs)
        {
            return AddFilter(WhereSpanPropertyFilter.ValueOneofCase.ParentSpanId, compare, compareAs);
        }
        
        /// <summary>
        /// Adds a filter for LinkSpanId to the list of filters.
        /// </summary>
        /// <param name="compare">The byte[] to compare the Span Id against.</param>
        /// <param name="compareAs">The type of comparison to perform.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddLinkSpanIdFilter(byte[] compare, ByteStringCompareAsType compareAs)
        {
            return AddFilter(WhereSpanPropertyFilter.ValueOneofCase.LinkSpanId, compare, compareAs);
        }
        
        /// <summary>
        /// Adds a filter for LinkTraceId to the list of filters.
        /// </summary>
        /// <param name="compare">The byte[] to compare the Trace Id against.</param>
        /// <param name="compareAs">The type of comparison to perform.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/></returns>
        public WhereSpanFilterConfigurator AddLinkTraceIdFilter(byte[] compare, ByteStringCompareAsType compareAs)
        {
            return AddFilter(WhereSpanPropertyFilter.ValueOneofCase.LinkTraceId, compare, compareAs);
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
            return AddFilter(WhereSpanPropertyFilter.ValueOneofCase.StartTimeUnixNano, compare, compareAs);
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
            return AddFilter(WhereSpanPropertyFilter.ValueOneofCase.EndTimeUnixNano, compare, compareAs);
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
            return AddFilter(WhereSpanPropertyFilter.ValueOneofCase.Attribute, key, compare, compareAs);
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
            return AddFilter(WhereSpanPropertyFilter.ValueOneofCase.Attribute, key, compare, compareAs);
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
            return AddFilter(WhereSpanPropertyFilter.ValueOneofCase.Attribute, key, compare, compareAs);
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
            return AddFilter(WhereSpanPropertyFilter.ValueOneofCase.Attribute, key, compare, compareAs);
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
            return AddFilter(WhereSpanPropertyFilter.ValueOneofCase.Attribute, key, compare, compareAs);
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
            return AddFilter(WhereSpanPropertyFilter.ValueOneofCase.EventTimeUnixNano, compare, compareAs);
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
            return AddFilter(WhereSpanPropertyFilter.ValueOneofCase.EventName, compare, compareAs);
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
            return AddFilter(WhereSpanPropertyFilter.ValueOneofCase.LinkTraceState, compare, compareAs);
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
            return AddFilter(WhereSpanPropertyFilter.ValueOneofCase.TraceState, compare, compareAs);
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
            return AddFilter(WhereSpanPropertyFilter.ValueOneofCase.InstrumentationScopeName, compare, compareAs);
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
            return AddFilter(WhereSpanPropertyFilter.ValueOneofCase.InstrumentationScopeVersion, compare, compareAs);
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
            return AddFilter(WhereSpanPropertyFilter.ValueOneofCase.InstrumentationScopeSchemaUrl, compare, compareAs);
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
            return AddFilter(WhereSpanPropertyFilter.ValueOneofCase.ResourceSchemaUrl, compare, compareAs);
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
            return AddFilter(WhereSpanPropertyFilter.ValueOneofCase.LinkFlags, compare, compareAs);
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
            return AddFilter(WhereSpanPropertyFilter.ValueOneofCase.Flags, compare, compareAs);
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
            return AddFilter(WhereSpanPropertyFilter.ValueOneofCase.EventAttribute, key, compare, compareAs);
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
            return AddFilter(WhereSpanPropertyFilter.ValueOneofCase.EventAttribute, key, compare, compareAs);
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
            return AddFilter(WhereSpanPropertyFilter.ValueOneofCase.EventAttribute, key, compare, compareAs);
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
            return AddFilter(WhereSpanPropertyFilter.ValueOneofCase.EventAttribute, key, compare, compareAs);
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
            return AddFilter(WhereSpanPropertyFilter.ValueOneofCase.EventAttribute, key, compare, compareAs);
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
            return AddFilter(WhereSpanPropertyFilter.ValueOneofCase.LinkAttribute, key, compare, compareAs);
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
            return AddFilter(WhereSpanPropertyFilter.ValueOneofCase.LinkAttribute, key, compare, compareAs);
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
            return AddFilter(WhereSpanPropertyFilter.ValueOneofCase.LinkAttribute, key, compare, compareAs);
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
            return AddFilter(WhereSpanPropertyFilter.ValueOneofCase.LinkAttribute, key, compare, compareAs);
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
            return AddFilter(WhereSpanPropertyFilter.ValueOneofCase.LinkAttribute, key, compare, compareAs);
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
            return AddFilter(WhereSpanPropertyFilter.ValueOneofCase.InstrumentationScopeAttribute, key, compare, compareAs);
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
            return AddFilter(WhereSpanPropertyFilter.ValueOneofCase.InstrumentationScopeAttribute, key, compare, compareAs);
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
            return AddFilter(WhereSpanPropertyFilter.ValueOneofCase.InstrumentationScopeAttribute, key, compare, compareAs);
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
            return AddFilter(WhereSpanPropertyFilter.ValueOneofCase.InstrumentationScopeAttribute, key, compare, compareAs);
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
            return AddFilter(WhereSpanPropertyFilter.ValueOneofCase.InstrumentationScopeAttribute, key, compare, compareAs);
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
            return AddFilter(WhereSpanPropertyFilter.ValueOneofCase.ResourceAttribute, key, compare, compareAs);
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
            return AddFilter(WhereSpanPropertyFilter.ValueOneofCase.ResourceAttribute, key, compare, compareAs);
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
            return AddFilter(WhereSpanPropertyFilter.ValueOneofCase.ResourceAttribute, key, compare, compareAs);
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
            return AddFilter(WhereSpanPropertyFilter.ValueOneofCase.ResourceAttribute, key, compare, compareAs);
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
            return AddFilter(WhereSpanPropertyFilter.ValueOneofCase.ResourceAttribute, key, compare, compareAs);
        }

        private void ThrowOnPropertyTypeMismatch(Type expectedType, WhereSpanPropertyFilter.ValueOneofCase property)
        {
            var propertyInfo = typeof(WhereSpanPropertyFilter).GetProperty(property.ToString());
            if (propertyInfo == null)
                throw new Exception("Could not find matching property name for WhereSpanPropertyFilter"); // TODO better exception
            if (propertyInfo.PropertyType != expectedType)
                throw new Exception(
                    $"Property {property.ToString()} is of type {propertyInfo.PropertyType}, but you supplied {expectedType}");
        }

        private void AddFilter(WhereSpanPropertyFilter.ValueOneofCase property, object spanProperty)
        {
            var spanPropertyFilter = new WhereSpanPropertyFilter();
            
            // If we've gotten this far, we know the property exists, and we know it's the right type, so `!` because
            // we know the PropertyInfo will not be null. TODO: Probably a better way to do this.
            spanPropertyFilter.GetType().GetProperty(property.ToString())!.SetValue(spanPropertyFilter, spanProperty);
            var filter = new WhereSpanFilter
            {
                SpanProperty = spanPropertyFilter
            };
            Filters.Add(filter);
        }
    }
}