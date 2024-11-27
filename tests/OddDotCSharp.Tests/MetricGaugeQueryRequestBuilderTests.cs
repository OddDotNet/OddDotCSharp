using OddDotNet.Proto.Common.V1;
using OddDotNet.Proto.Metrics.V1;

namespace OddDotCSharp.Tests;

public class MetricGaugeQueryRequestBuilderTests
{
    public class WhereShould
    {
        [Fact]
        public void AddNumberDataPointAttributeStringPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string key = "service.name";
            const string value = "test";
            var request = builder.Where(filters => filters.Gauge.DataPoint.AddAttributeFilter(key, value, StringCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Gauge, request.Filters[0].Property.ValueCase);
            Assert.Equal(StringCompareAsType.Equals, request.Filters[0].Property.Gauge.DataPoint.Attributes.Values[0].Value.StringValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Gauge.DataPoint.Attributes.Values[0].Key);
            Assert.Equal(value, request.Filters[0].Property.Gauge.DataPoint.Attributes.Values[0].Value.StringValue.Compare);
        }
        
        [Fact]
        public void AddNumberDataPointAttributeBoolPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string key = "service.name";
            const bool value = true;
            var request = builder.Where(filters => filters.Gauge.DataPoint.AddAttributeFilter(key, value, BoolCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Gauge, request.Filters[0].Property.ValueCase);
            Assert.Equal(BoolCompareAsType.Equals, request.Filters[0].Property.Gauge.DataPoint.Attributes.Values[0].Value.BoolValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Gauge.DataPoint.Attributes.Values[0].Key);
            Assert.Equal(value, request.Filters[0].Property.Gauge.DataPoint.Attributes.Values[0].Value.BoolValue.Compare);
        }
        
        [Fact]
        public void AddNumberDataPointAttributeInt64PropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string key = "service.name";
            const long value = 123;
            var request = builder.Where(filters => filters.Gauge.DataPoint.AddAttributeFilter(key, value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Gauge, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Gauge.DataPoint.Attributes.Values[0].Value.IntValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Gauge.DataPoint.Attributes.Values[0].Key);
            Assert.Equal(value, request.Filters[0].Property.Gauge.DataPoint.Attributes.Values[0].Value.IntValue.Compare);
        }
        
        [Fact]
        public void AddNumberDataPointAttributeDoublePropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string key = "service.name";
            const double value = 123;
            var request = builder.Where(filters => filters.Gauge.DataPoint.AddAttributeFilter(key, value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Gauge, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Gauge.DataPoint.Attributes.Values[0].Value.DoubleValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Gauge.DataPoint.Attributes.Values[0].Key);
            Assert.Equal(value, request.Filters[0].Property.Gauge.DataPoint.Attributes.Values[0].Value.DoubleValue.Compare);
        }
        
        [Fact]
        public void AddNumberDataPointAttributeByteStringPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string key = "service.name";
            byte[] value = [123];
            var request = builder.Where(filters => filters.Gauge.DataPoint.AddAttributeFilter(key, value, ByteStringCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Gauge, request.Filters[0].Property.ValueCase);
            Assert.Equal(ByteStringCompareAsType.Equals, request.Filters[0].Property.Gauge.DataPoint.Attributes.Values[0].Value.ByteStringValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Gauge.DataPoint.Attributes.Values[0].Key);
            Assert.Equal(value, request.Filters[0].Property.Gauge.DataPoint.Attributes.Values[0].Value.ByteStringValue.Compare);
        }
        
        [Fact]
        public void AddNumberDataPointAttributeArrayPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string key = "key";
            const double value = 123.0;
            var request = builder.Where(filters => filters.Gauge.DataPoint.AddAttributeArrayFilter(key, arrayFilters =>
            {
                arrayFilters.AddFilter(value, NumberCompareAsType.Equals);
            })).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Gauge, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Gauge.DataPoint.Attributes.Values[0].Value.ArrayValue.Values[0].DoubleValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Gauge.DataPoint.Attributes.Values[0].Key);
            Assert.Equal(value, request.Filters[0].Property.Gauge.DataPoint.Attributes.Values[0].Value.ArrayValue.Values[0].DoubleValue.Compare);
        }
        
        [Fact]
        public void AddNumberDataPointAttributeKeyValueListPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string key = "key";
            const double value = 123.0;
            var request = builder.Where(filters => filters.Gauge.DataPoint.AddAttributeKeyValueListFilter(key, kvlFilters =>
            {
                kvlFilters.AddFilter(key, value, NumberCompareAsType.Equals);
            })).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Gauge, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Gauge.DataPoint.Attributes.Values[0].Value.KvlistValue.Values[0].Value.DoubleValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Gauge.DataPoint.Attributes.Values[0].Key);
            Assert.Equal(key, request.Filters[0].Property.Gauge.DataPoint.Attributes.Values[0].Value.KvlistValue.Values[0].Key);
            Assert.Equal(value, request.Filters[0].Property.Gauge.DataPoint.Attributes.Values[0].Value.KvlistValue.Values[0].Value.DoubleValue.Compare);
        }
        
        [Fact]
        public void AddNumberDataPointStartTimeUnixNanoPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const ulong value = 123;
            var request = builder.Where(filters => filters.Gauge.DataPoint.AddStartTimeUnixNanoFilter(value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Gauge, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Gauge.DataPoint.StartTimeUnixNano.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.Gauge.DataPoint.StartTimeUnixNano.Compare);
        }
        
        [Fact]
        public void AddNumberDataPointTimeUnixNanoPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const ulong value = 123;
            var request = builder.Where(filters => filters.Gauge.DataPoint.AddTimeUnixNanoFilter(value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Gauge, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Gauge.DataPoint.TimeUnixNano.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.Gauge.DataPoint.TimeUnixNano.Compare);
        }
        
        [Fact]
        public void AddNumberDataPointValueAsDoublePropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const double value = 123;
            var request = builder.Where(filters => filters.Gauge.DataPoint.AddValueAsDoubleFilter(value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Gauge, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Gauge.DataPoint.ValueAsDouble.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.Gauge.DataPoint.ValueAsDouble.Compare);
        }
        
        [Fact]
        public void AddNumberDataPointValueAsIntPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const long value = 123;
            var request = builder.Where(filters => filters.Gauge.DataPoint.AddValueAsIntFilter(value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Gauge, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Gauge.DataPoint.ValueAsInt.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.Gauge.DataPoint.ValueAsInt.Compare);
        }
        
        [Fact]
        public void AddNumberDataPointExemplarFilteredAttributeStringPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string key = "service.name";
            const string value = "test";
            var request = builder.Where(filters => filters.Gauge.DataPoint.Exemplar.AddFilteredAttributeFilter(key, value, StringCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Gauge, request.Filters[0].Property.ValueCase);
            Assert.Equal(StringCompareAsType.Equals, request.Filters[0].Property.Gauge.DataPoint.Exemplar.FilteredAttributes.Values[0].Value.StringValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Gauge.DataPoint.Exemplar.FilteredAttributes.Values[0].Key);
            Assert.Equal(value, request.Filters[0].Property.Gauge.DataPoint.Exemplar.FilteredAttributes.Values[0].Value.StringValue.Compare);
        }
        
        [Fact]
        public void AddNumberDataPointExemplarFilteredAttributeBoolPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string key = "service.name";
            const bool value = true;
            var request = builder.Where(filters => filters.Gauge.DataPoint.Exemplar.AddFilteredAttributeFilter(key, value, BoolCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Gauge, request.Filters[0].Property.ValueCase);
            Assert.Equal(BoolCompareAsType.Equals, request.Filters[0].Property.Gauge.DataPoint.Exemplar.FilteredAttributes.Values[0].Value.BoolValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Gauge.DataPoint.Exemplar.FilteredAttributes.Values[0].Key);
            Assert.Equal(value, request.Filters[0].Property.Gauge.DataPoint.Exemplar.FilteredAttributes.Values[0].Value.BoolValue.Compare);
        }
        
        [Fact]
        public void AddNumberDataPointExemplarFilteredAttributeInt64PropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string key = "service.name";
            const long value = 123;
            var request = builder.Where(filters => filters.Gauge.DataPoint.Exemplar.AddFilteredAttributeFilter(key, value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Gauge, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Gauge.DataPoint.Exemplar.FilteredAttributes.Values[0].Value.IntValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Gauge.DataPoint.Exemplar.FilteredAttributes.Values[0].Key);
            Assert.Equal(value, request.Filters[0].Property.Gauge.DataPoint.Exemplar.FilteredAttributes.Values[0].Value.IntValue.Compare);
        }
        
        [Fact]
        public void AddNumberDataPointExemplarFilteredAttributeDoublePropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string key = "service.name";
            const double value = 123;
            var request = builder.Where(filters => filters.Gauge.DataPoint.Exemplar.AddFilteredAttributeFilter(key, value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Gauge, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Gauge.DataPoint.Exemplar.FilteredAttributes.Values[0].Value.DoubleValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Gauge.DataPoint.Exemplar.FilteredAttributes.Values[0].Key);
            Assert.Equal(value, request.Filters[0].Property.Gauge.DataPoint.Exemplar.FilteredAttributes.Values[0].Value.DoubleValue.Compare);
        }
        
        [Fact]
        public void AddNumberDataPointExemplarFilteredAttributeByteStringPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string key = "service.name";
            byte[] value = [123];
            var request = builder.Where(filters => filters.Gauge.DataPoint.Exemplar.AddFilteredAttributeFilter(key, value, ByteStringCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Gauge, request.Filters[0].Property.ValueCase);
            Assert.Equal(ByteStringCompareAsType.Equals, request.Filters[0].Property.Gauge.DataPoint.Exemplar.FilteredAttributes.Values[0].Value.ByteStringValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Gauge.DataPoint.Exemplar.FilteredAttributes.Values[0].Key);
            Assert.Equal(value, request.Filters[0].Property.Gauge.DataPoint.Exemplar.FilteredAttributes.Values[0].Value.ByteStringValue.Compare);
        }
        
        [Fact]
        public void AddNumberDataPointExemplarFilteredAttributeArrayPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string key = "key";
            const double value = 123.0;
            var request = builder.Where(filters => filters.Gauge.DataPoint.Exemplar.AddFilteredAttributeArrayFilter(key, arrayFilters =>
            {
                arrayFilters.AddFilter(value, NumberCompareAsType.Equals);
            })).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Gauge, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Gauge.DataPoint.Exemplar.FilteredAttributes.Values[0].Value.ArrayValue.Values[0].DoubleValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Gauge.DataPoint.Exemplar.FilteredAttributes.Values[0].Key);
            Assert.Equal(value, request.Filters[0].Property.Gauge.DataPoint.Exemplar.FilteredAttributes.Values[0].Value.ArrayValue.Values[0].DoubleValue.Compare);
        }
        
        [Fact]
        public void AddNumberDataPointExemplarFilteredAttributeKeyValueListPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string key = "key";
            const double value = 123.0;
            var request = builder.Where(filters => filters.Gauge.DataPoint.Exemplar.AddFilteredAttributeKeyValueListFilter(key, kvlFilters =>
            {
                kvlFilters.AddFilter(key, value, NumberCompareAsType.Equals);
            })).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Gauge, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Gauge.DataPoint.Exemplar.FilteredAttributes.Values[0].Value.KvlistValue.Values[0].Value.DoubleValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Gauge.DataPoint.Exemplar.FilteredAttributes.Values[0].Key);
            Assert.Equal(key, request.Filters[0].Property.Gauge.DataPoint.Exemplar.FilteredAttributes.Values[0].Value.KvlistValue.Values[0].Key);
            Assert.Equal(value, request.Filters[0].Property.Gauge.DataPoint.Exemplar.FilteredAttributes.Values[0].Value.KvlistValue.Values[0].Value.DoubleValue.Compare);
        }
        
        [Fact]
        public void AddNumberDataPointExemplarTimeUnixNanoPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const ulong value = 123;
            var request = builder.Where(filters => filters.Gauge.DataPoint.Exemplar.AddTimeUnixNanoFilter(value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Gauge, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Gauge.DataPoint.Exemplar.TimeUnixNano.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.Gauge.DataPoint.Exemplar.TimeUnixNano.Compare);
        }
        
        [Fact]
        public void AddNumberDataPointExemplarValueAsDoublePropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const double value = 123;
            var request = builder.Where(filters => filters.Gauge.DataPoint.Exemplar.AddValueAsDoubleFilter(value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Gauge, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Gauge.DataPoint.Exemplar.ValueAsDouble.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.Gauge.DataPoint.Exemplar.ValueAsDouble.Compare);
        }
        
        [Fact]
        public void AddNumberDataPointExemplarValueAsIntPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const long value = 123;
            var request = builder.Where(filters => filters.Gauge.DataPoint.Exemplar.AddValueAsIntFilter(value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Gauge, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Gauge.DataPoint.Exemplar.ValueAsInt.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.Gauge.DataPoint.Exemplar.ValueAsInt.Compare);
        }
        
        [Fact]
        public void AddNumberDataPointExemplarSpanIdPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            byte[] value = [1,2,3,4,5,6,7,8];
            var request = builder.Where(filters => filters.Gauge.DataPoint.Exemplar.AddSpanIdFilter(value, ByteStringCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Gauge, request.Filters[0].Property.ValueCase);
            Assert.Equal(ByteStringCompareAsType.Equals, request.Filters[0].Property.Gauge.DataPoint.Exemplar.SpanId.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.Gauge.DataPoint.Exemplar.SpanId.Compare);
        }
        
        [Fact]
        public void AddNumberDataPointExemplarTraceIdPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            byte[] value = [1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16];
            var request = builder.Where(filters => filters.Gauge.DataPoint.Exemplar.AddTraceIdFilter(value, ByteStringCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Gauge, request.Filters[0].Property.ValueCase);
            Assert.Equal(ByteStringCompareAsType.Equals, request.Filters[0].Property.Gauge.DataPoint.Exemplar.TraceId.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.Gauge.DataPoint.Exemplar.TraceId.Compare);
        }
        
        [Fact]
        public void AddNumberDataPointFlagsPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const uint value = 123;
            var request = builder.Where(filters => filters.Gauge.DataPoint.AddFlagsFilter(value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Gauge, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Gauge.DataPoint.Flags.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.Gauge.DataPoint.Flags.Compare);
        }
    }
}