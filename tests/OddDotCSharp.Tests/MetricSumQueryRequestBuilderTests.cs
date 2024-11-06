using OddDotNet.Proto.Common.V1;
using OddDotNet.Proto.Metrics.V1;
using OpenTelemetry.Proto.Metrics.V1;

namespace OddDotCSharp.Tests;

public class MetricSumQueryRequestBuilderTests
{
    public class WhereShould
    {
        [Fact]
        public void AddDataPointAttributeStringPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string key = "service.name";
            const string value = "test";
            var request = builder.Where(filters => filters.Sum.DataPoint.AddAttributeFilter(key, value, StringCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Sum, request.Filters[0].Property.ValueCase);
            Assert.Equal(StringCompareAsType.Equals, request.Filters[0].Property.Sum.DataPoint.Attribute.StringValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Sum.DataPoint.Attribute.Key);
            Assert.Equal(value, request.Filters[0].Property.Sum.DataPoint.Attribute.StringValue.Compare);
        }
        
        [Fact]
        public void AddDataPointAttributeBoolPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string key = "service.name";
            const bool value = true;
            var request = builder.Where(filters => filters.Sum.DataPoint.AddAttributeFilter(key, value, BoolCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Sum, request.Filters[0].Property.ValueCase);
            Assert.Equal(BoolCompareAsType.Equals, request.Filters[0].Property.Sum.DataPoint.Attribute.BoolValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Sum.DataPoint.Attribute.Key);
            Assert.Equal(value, request.Filters[0].Property.Sum.DataPoint.Attribute.BoolValue.Compare);
        }
        
        [Fact]
        public void AddDataPointAttributeInt64PropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string key = "service.name";
            const long value = 123;
            var request = builder.Where(filters => filters.Sum.DataPoint.AddAttributeFilter(key, value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Sum, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Sum.DataPoint.Attribute.Int64Value.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Sum.DataPoint.Attribute.Key);
            Assert.Equal(value, request.Filters[0].Property.Sum.DataPoint.Attribute.Int64Value.Compare);
        }
        
        [Fact]
        public void AddDataPointAttributeDoublePropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string key = "service.name";
            const double value = 123;
            var request = builder.Where(filters => filters.Sum.DataPoint.AddAttributeFilter(key, value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Sum, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Sum.DataPoint.Attribute.DoubleValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Sum.DataPoint.Attribute.Key);
            Assert.Equal(value, request.Filters[0].Property.Sum.DataPoint.Attribute.DoubleValue.Compare);
        }
        
        [Fact]
        public void AddDataPointAttributeByteStringPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string key = "service.name";
            byte[] value = [123];
            var request = builder.Where(filters => filters.Sum.DataPoint.AddAttributeFilter(key, value, ByteStringCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Sum, request.Filters[0].Property.ValueCase);
            Assert.Equal(ByteStringCompareAsType.Equals, request.Filters[0].Property.Sum.DataPoint.Attribute.ByteStringValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Sum.DataPoint.Attribute.Key);
            Assert.Equal(value, request.Filters[0].Property.Sum.DataPoint.Attribute.ByteStringValue.Compare);
        }
        
        [Fact]
        public void AddDataPointStartTimeUnixNanoPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const ulong value = 123;
            var request = builder.Where(filters => filters.Sum.DataPoint.AddStartTimeUnixNanoFilter(value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Sum, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Sum.DataPoint.StartTimeUnixNano.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.Sum.DataPoint.StartTimeUnixNano.Compare);
        }
        
        [Fact]
        public void AddDataPointTimeUnixNanoPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const ulong value = 123;
            var request = builder.Where(filters => filters.Sum.DataPoint.AddTimeUnixNanoFilter(value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Sum, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Sum.DataPoint.TimeUnixNano.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.Sum.DataPoint.TimeUnixNano.Compare);
        }
        
        [Fact]
        public void AddDataPointValueAsDoublePropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const double value = 123;
            var request = builder.Where(filters => filters.Sum.DataPoint.AddValueAsDoubleFilter(value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Sum, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Sum.DataPoint.ValueAsDouble.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.Sum.DataPoint.ValueAsDouble.Compare);
        }
        
        [Fact]
        public void AddDataPointValueAsIntPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const long value = 123;
            var request = builder.Where(filters => filters.Sum.DataPoint.AddValueAsIntFilter(value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Sum, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Sum.DataPoint.ValueAsInt.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.Sum.DataPoint.ValueAsInt.Compare);
        }
        
        [Fact]
        public void AddDataPointExemplarFilteredAttributeStringPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string key = "service.name";
            const string value = "test";
            var request = builder.Where(filters => filters.Sum.DataPoint.Exemplar.AddFilteredAttributeFilter(key, value, StringCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Sum, request.Filters[0].Property.ValueCase);
            Assert.Equal(StringCompareAsType.Equals, request.Filters[0].Property.Sum.DataPoint.Exemplar.FilteredAttribute.StringValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Sum.DataPoint.Exemplar.FilteredAttribute.Key);
            Assert.Equal(value, request.Filters[0].Property.Sum.DataPoint.Exemplar.FilteredAttribute.StringValue.Compare);
        }
        
        [Fact]
        public void AddDataPointExemplarFilteredAttributeBoolPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string key = "service.name";
            const bool value = true;
            var request = builder.Where(filters => filters.Sum.DataPoint.Exemplar.AddFilteredAttributeFilter(key, value, BoolCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Sum, request.Filters[0].Property.ValueCase);
            Assert.Equal(BoolCompareAsType.Equals, request.Filters[0].Property.Sum.DataPoint.Exemplar.FilteredAttribute.BoolValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Sum.DataPoint.Exemplar.FilteredAttribute.Key);
            Assert.Equal(value, request.Filters[0].Property.Sum.DataPoint.Exemplar.FilteredAttribute.BoolValue.Compare);
        }
        
        [Fact]
        public void AddDataPointExemplarFilteredAttributeInt64PropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string key = "service.name";
            const long value = 123;
            var request = builder.Where(filters => filters.Sum.DataPoint.Exemplar.AddFilteredAttributeFilter(key, value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Sum, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Sum.DataPoint.Exemplar.FilteredAttribute.Int64Value.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Sum.DataPoint.Exemplar.FilteredAttribute.Key);
            Assert.Equal(value, request.Filters[0].Property.Sum.DataPoint.Exemplar.FilteredAttribute.Int64Value.Compare);
        }
        
        [Fact]
        public void AddDataPointExemplarFilteredAttributeDoublePropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string key = "service.name";
            const double value = 123;
            var request = builder.Where(filters => filters.Sum.DataPoint.Exemplar.AddFilteredAttributeFilter(key, value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Sum, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Sum.DataPoint.Exemplar.FilteredAttribute.DoubleValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Sum.DataPoint.Exemplar.FilteredAttribute.Key);
            Assert.Equal(value, request.Filters[0].Property.Sum.DataPoint.Exemplar.FilteredAttribute.DoubleValue.Compare);
        }
        
        [Fact]
        public void AddDataPointExemplarFilteredAttributeByteStringPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string key = "service.name";
            byte[] value = [123];
            var request = builder.Where(filters => filters.Sum.DataPoint.Exemplar.AddFilteredAttributeFilter(key, value, ByteStringCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Sum, request.Filters[0].Property.ValueCase);
            Assert.Equal(ByteStringCompareAsType.Equals, request.Filters[0].Property.Sum.DataPoint.Exemplar.FilteredAttribute.ByteStringValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Sum.DataPoint.Exemplar.FilteredAttribute.Key);
            Assert.Equal(value, request.Filters[0].Property.Sum.DataPoint.Exemplar.FilteredAttribute.ByteStringValue.Compare);
        }
        
        [Fact]
        public void AddDataPointExemplarTimeUnixNanoPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const ulong value = 123;
            var request = builder.Where(filters => filters.Sum.DataPoint.Exemplar.AddTimeUnixNanoFilter(value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Sum, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Sum.DataPoint.Exemplar.TimeUnixNano.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.Sum.DataPoint.Exemplar.TimeUnixNano.Compare);
        }
        
        [Fact]
        public void AddDataPointExemplarValueAsDoublePropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const double value = 123;
            var request = builder.Where(filters => filters.Sum.DataPoint.Exemplar.AddValueAsDoubleFilter(value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Sum, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Sum.DataPoint.Exemplar.ValueAsDouble.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.Sum.DataPoint.Exemplar.ValueAsDouble.Compare);
        }
        
        [Fact]
        public void AddDataPointExemplarValueAsIntPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const long value = 123;
            var request = builder.Where(filters => filters.Sum.DataPoint.Exemplar.AddValueAsIntFilter(value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Sum, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Sum.DataPoint.Exemplar.ValueAsInt.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.Sum.DataPoint.Exemplar.ValueAsInt.Compare);
        }
        
        [Fact]
        public void AddDataPointExemplarSpanIdPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            byte[] value = [1,2,3,4,5,6,7,8];
            var request = builder.Where(filters => filters.Sum.DataPoint.Exemplar.AddSpanIdFilter(value, ByteStringCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Sum, request.Filters[0].Property.ValueCase);
            Assert.Equal(ByteStringCompareAsType.Equals, request.Filters[0].Property.Sum.DataPoint.Exemplar.SpanId.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.Sum.DataPoint.Exemplar.SpanId.Compare);
        }
        
        [Fact]
        public void AddDataPointExemplarTraceIdPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            byte[] value = [1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16];
            var request = builder.Where(filters => filters.Sum.DataPoint.Exemplar.AddTraceIdFilter(value, ByteStringCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Sum, request.Filters[0].Property.ValueCase);
            Assert.Equal(ByteStringCompareAsType.Equals, request.Filters[0].Property.Sum.DataPoint.Exemplar.TraceId.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.Sum.DataPoint.Exemplar.TraceId.Compare);
        }
        
        [Fact]
        public void AddDataPointFlagsPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const uint value = 123;
            var request = builder.Where(filters => filters.Sum.DataPoint.AddFlagsFilter(value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Sum, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Sum.DataPoint.Flags.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.Sum.DataPoint.Flags.Compare);
        }
        
        [Fact]
        public void AddAggregationTemporalityPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const AggregationTemporality value = AggregationTemporality.Cumulative;
            var request = builder.Where(filters => filters.Sum.AddAggregationTemporalityFilter(value, EnumCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Sum, request.Filters[0].Property.ValueCase);
            Assert.Equal(EnumCompareAsType.Equals, request.Filters[0].Property.Sum.AggregationTemporality.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.Sum.AggregationTemporality.Compare);
        }
        
        [Fact]
        public void AddIsMonotonicPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const bool value = true;
            var request = builder.Where(filters => filters.Sum.AddIsMonotonicFilter(value, BoolCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Sum, request.Filters[0].Property.ValueCase);
            Assert.Equal(BoolCompareAsType.Equals, request.Filters[0].Property.Sum.IsMonotonic.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.Sum.IsMonotonic.Compare);
        }
    }
}