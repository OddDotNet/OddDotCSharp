using OddDotNet.Proto.Common.V1;
using OddDotNet.Proto.Metrics.V1;
using OpenTelemetry.Proto.Metrics.V1;

namespace OddDotCSharp.Tests;

public class MetricExponentialHistogramQueryRequestBuilderTests
{
    public class WhereShould
    {
        [Fact]
        public void AddDataPointAttributeStringPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string key = "service.name";
            const string value = "test";
            var request = builder.Where(filters => filters.ExponentialHistogram.DataPoint.AddAttributeFilter(key, value, StringCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.ExponentialHistogram, request.Filters[0].Property.ValueCase);
            Assert.Equal(StringCompareAsType.Equals, request.Filters[0].Property.ExponentialHistogram.DataPoint.Attributes.Values[0].Value.StringValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.ExponentialHistogram.DataPoint.Attributes.Values[0].Key);
            Assert.Equal(value, request.Filters[0].Property.ExponentialHistogram.DataPoint.Attributes.Values[0].Value.StringValue.Compare);
        }
        
        [Fact]
        public void AddDataPointAttributeBoolPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string key = "service.name";
            const bool value = true;
            var request = builder.Where(filters => filters.ExponentialHistogram.DataPoint.AddAttributeFilter(key, value, BoolCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.ExponentialHistogram, request.Filters[0].Property.ValueCase);
            Assert.Equal(BoolCompareAsType.Equals, request.Filters[0].Property.ExponentialHistogram.DataPoint.Attributes.Values[0].Value.BoolValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.ExponentialHistogram.DataPoint.Attributes.Values[0].Key);
            Assert.Equal(value, request.Filters[0].Property.ExponentialHistogram.DataPoint.Attributes.Values[0].Value.BoolValue.Compare);
        }
        
        [Fact]
        public void AddDataPointAttributeInt64PropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string key = "service.name";
            const long value = 123;
            var request = builder.Where(filters => filters.ExponentialHistogram.DataPoint.AddAttributeFilter(key, value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.ExponentialHistogram, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.ExponentialHistogram.DataPoint.Attributes.Values[0].Value.IntValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.ExponentialHistogram.DataPoint.Attributes.Values[0].Key);
            Assert.Equal(value, request.Filters[0].Property.ExponentialHistogram.DataPoint.Attributes.Values[0].Value.IntValue.Compare);
        }
        
        [Fact]
        public void AddDataPointAttributeDoublePropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string key = "service.name";
            const double value = 123;
            var request = builder.Where(filters => filters.ExponentialHistogram.DataPoint.AddAttributeFilter(key, value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.ExponentialHistogram, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.ExponentialHistogram.DataPoint.Attributes.Values[0].Value.DoubleValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.ExponentialHistogram.DataPoint.Attributes.Values[0].Key);
            Assert.Equal(value, request.Filters[0].Property.ExponentialHistogram.DataPoint.Attributes.Values[0].Value.DoubleValue.Compare);
        }
        
        [Fact]
        public void AddDataPointAttributeByteStringPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string key = "service.name";
            byte[] value = [123];
            var request = builder.Where(filters => filters.ExponentialHistogram.DataPoint.AddAttributeFilter(key, value, ByteStringCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.ExponentialHistogram, request.Filters[0].Property.ValueCase);
            Assert.Equal(ByteStringCompareAsType.Equals, request.Filters[0].Property.ExponentialHistogram.DataPoint.Attributes.Values[0].Value.ByteStringValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.ExponentialHistogram.DataPoint.Attributes.Values[0].Key);
            Assert.Equal(value, request.Filters[0].Property.ExponentialHistogram.DataPoint.Attributes.Values[0].Value.ByteStringValue.Compare);
        }
        
        [Fact]
        public void AddDataPointStartTimeUnixNanoPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const ulong value = 123;
            var request = builder.Where(filters => filters.ExponentialHistogram.DataPoint.AddStartTimeUnixNanoFilter(value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.ExponentialHistogram, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.ExponentialHistogram.DataPoint.StartTimeUnixNano.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.ExponentialHistogram.DataPoint.StartTimeUnixNano.Compare);
        }
        
        [Fact]
        public void AddDataPointTimeUnixNanoPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const ulong value = 123;
            var request = builder.Where(filters => filters.ExponentialHistogram.DataPoint.AddTimeUnixNanoFilter(value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.ExponentialHistogram, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.ExponentialHistogram.DataPoint.TimeUnixNano.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.ExponentialHistogram.DataPoint.TimeUnixNano.Compare);
        }
        
        [Fact]
        public void AddDataPointCountPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const ulong value = 123;
            var request = builder.Where(filters => filters.ExponentialHistogram.DataPoint.AddCountFilter(value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.ExponentialHistogram, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.ExponentialHistogram.DataPoint.Count.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.ExponentialHistogram.DataPoint.Count.Compare);
        }
        
        [Fact]
        public void AddDataPointSumPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const double value = 123;
            var request = builder.Where(filters => filters.ExponentialHistogram.DataPoint.AddSumFilter(value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.ExponentialHistogram, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.ExponentialHistogram.DataPoint.Sum.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.ExponentialHistogram.DataPoint.Sum.Compare);
        }
        
        [Fact]
        public void AddDataPointScalePropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const int value = 123;
            var request = builder.Where(filters => filters.ExponentialHistogram.DataPoint.AddScaleFilter(value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.ExponentialHistogram, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.ExponentialHistogram.DataPoint.Scale.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.ExponentialHistogram.DataPoint.Scale.Compare);
        }
        
        [Fact]
        public void AddDataPointZerCountPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const ulong value = 123;
            var request = builder.Where(filters => filters.ExponentialHistogram.DataPoint.AddZeroCountFilter(value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.ExponentialHistogram, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.ExponentialHistogram.DataPoint.ZeroCount.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.ExponentialHistogram.DataPoint.ZeroCount.Compare);
        }
        
        [Fact]
        public void AddDataPointPositiveOffsetPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const int value = 123;
            var request = builder.Where(filters => filters.ExponentialHistogram.DataPoint.Positive.AddOffsetFilter(value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.ExponentialHistogram, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.ExponentialHistogram.DataPoint.Positive.Offset.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.ExponentialHistogram.DataPoint.Positive.Offset.Compare);
        }
        
        [Fact]
        public void AddDataPointPositiveBucketCountPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const ulong value = 123;
            var request = builder.Where(filters => filters.ExponentialHistogram.DataPoint.Positive.AddBucketCountFilter(value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.ExponentialHistogram, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.ExponentialHistogram.DataPoint.Positive.BucketCount.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.ExponentialHistogram.DataPoint.Positive.BucketCount.Compare);
        }
        
        [Fact]
        public void AddDataPointNegativeOffsetPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const int value = 123;
            var request = builder.Where(filters => filters.ExponentialHistogram.DataPoint.Negative.AddOffsetFilter(value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.ExponentialHistogram, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.ExponentialHistogram.DataPoint.Negative.Offset.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.ExponentialHistogram.DataPoint.Negative.Offset.Compare);
        }
        
        [Fact]
        public void AddDataPointNegativeBucketCountPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const ulong value = 123;
            var request = builder.Where(filters => filters.ExponentialHistogram.DataPoint.Negative.AddBucketCountFilter(value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.ExponentialHistogram, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.ExponentialHistogram.DataPoint.Negative.BucketCount.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.ExponentialHistogram.DataPoint.Negative.BucketCount.Compare);
        }
        
        [Fact]
        public void AddDataPointExemplarFilteredAttributeStringPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string key = "service.name";
            const string value = "test";
            var request = builder.Where(filters => filters.ExponentialHistogram.DataPoint.Exemplar.AddFilteredAttributeFilter(key, value, StringCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.ExponentialHistogram, request.Filters[0].Property.ValueCase);
            Assert.Equal(StringCompareAsType.Equals, request.Filters[0].Property.ExponentialHistogram.DataPoint.Exemplar.FilteredAttributes.Values[0].Value.StringValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.ExponentialHistogram.DataPoint.Exemplar.FilteredAttributes.Values[0].Key);
            Assert.Equal(value, request.Filters[0].Property.ExponentialHistogram.DataPoint.Exemplar.FilteredAttributes.Values[0].Value.StringValue.Compare);
        }
        
        [Fact]
        public void AddDataPointExemplarFilteredAttributeBoolPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string key = "service.name";
            const bool value = true;
            var request = builder.Where(filters => filters.ExponentialHistogram.DataPoint.Exemplar.AddFilteredAttributeFilter(key, value, BoolCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.ExponentialHistogram, request.Filters[0].Property.ValueCase);
            Assert.Equal(BoolCompareAsType.Equals, request.Filters[0].Property.ExponentialHistogram.DataPoint.Exemplar.FilteredAttributes.Values[0].Value.BoolValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.ExponentialHistogram.DataPoint.Exemplar.FilteredAttributes.Values[0].Key);
            Assert.Equal(value, request.Filters[0].Property.ExponentialHistogram.DataPoint.Exemplar.FilteredAttributes.Values[0].Value.BoolValue.Compare);
        }
        
        [Fact]
        public void AddDataPointExemplarFilteredAttributeInt64PropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string key = "service.name";
            const long value = 123;
            var request = builder.Where(filters => filters.ExponentialHistogram.DataPoint.Exemplar.AddFilteredAttributeFilter(key, value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.ExponentialHistogram, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.ExponentialHistogram.DataPoint.Exemplar.FilteredAttributes.Values[0].Value.IntValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.ExponentialHistogram.DataPoint.Exemplar.FilteredAttributes.Values[0].Key);
            Assert.Equal(value, request.Filters[0].Property.ExponentialHistogram.DataPoint.Exemplar.FilteredAttributes.Values[0].Value.IntValue.Compare);
        }
        
        [Fact]
        public void AddDataPointExemplarFilteredAttributeDoublePropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string key = "service.name";
            const double value = 123;
            var request = builder.Where(filters => filters.ExponentialHistogram.DataPoint.Exemplar.AddFilteredAttributeFilter(key, value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.ExponentialHistogram, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.ExponentialHistogram.DataPoint.Exemplar.FilteredAttributes.Values[0].Value.DoubleValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.ExponentialHistogram.DataPoint.Exemplar.FilteredAttributes.Values[0].Key);
            Assert.Equal(value, request.Filters[0].Property.ExponentialHistogram.DataPoint.Exemplar.FilteredAttributes.Values[0].Value.DoubleValue.Compare);
        }
        
        [Fact]
        public void AddDataPointExemplarFilteredAttributeByteStringPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string key = "service.name";
            byte[] value = [123];
            var request = builder.Where(filters => filters.ExponentialHistogram.DataPoint.Exemplar.AddFilteredAttributeFilter(key, value, ByteStringCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.ExponentialHistogram, request.Filters[0].Property.ValueCase);
            Assert.Equal(ByteStringCompareAsType.Equals, request.Filters[0].Property.ExponentialHistogram.DataPoint.Exemplar.FilteredAttributes.Values[0].Value.ByteStringValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.ExponentialHistogram.DataPoint.Exemplar.FilteredAttributes.Values[0].Key);
            Assert.Equal(value, request.Filters[0].Property.ExponentialHistogram.DataPoint.Exemplar.FilteredAttributes.Values[0].Value.ByteStringValue.Compare);
        }
        
        [Fact]
        public void AddDataPointExemplarTimeUnixNanoPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const ulong value = 123;
            var request = builder.Where(filters => filters.ExponentialHistogram.DataPoint.Exemplar.AddTimeUnixNanoFilter(value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.ExponentialHistogram, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.ExponentialHistogram.DataPoint.Exemplar.TimeUnixNano.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.ExponentialHistogram.DataPoint.Exemplar.TimeUnixNano.Compare);
        }
        
        [Fact]
        public void AddDataPointExemplarValueAsDoublePropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const double value = 123;
            var request = builder.Where(filters => filters.ExponentialHistogram.DataPoint.Exemplar.AddValueAsDoubleFilter(value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.ExponentialHistogram, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.ExponentialHistogram.DataPoint.Exemplar.ValueAsDouble.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.ExponentialHistogram.DataPoint.Exemplar.ValueAsDouble.Compare);
        }
        
        [Fact]
        public void AddDataPointExemplarValueAsIntPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const long value = 123;
            var request = builder.Where(filters => filters.ExponentialHistogram.DataPoint.Exemplar.AddValueAsIntFilter(value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.ExponentialHistogram, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.ExponentialHistogram.DataPoint.Exemplar.ValueAsInt.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.ExponentialHistogram.DataPoint.Exemplar.ValueAsInt.Compare);
        }
        
        [Fact]
        public void AddDataPointExemplarSpanIdPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            byte[] value = [1,2,3,4,5,6,7,8];
            var request = builder.Where(filters => filters.ExponentialHistogram.DataPoint.Exemplar.AddSpanIdFilter(value, ByteStringCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.ExponentialHistogram, request.Filters[0].Property.ValueCase);
            Assert.Equal(ByteStringCompareAsType.Equals, request.Filters[0].Property.ExponentialHistogram.DataPoint.Exemplar.SpanId.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.ExponentialHistogram.DataPoint.Exemplar.SpanId.Compare);
        }
        
        [Fact]
        public void AddDataPointExemplarTraceIdPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            byte[] value = [1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16];
            var request = builder.Where(filters => filters.ExponentialHistogram.DataPoint.Exemplar.AddTraceIdFilter(value, ByteStringCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.ExponentialHistogram, request.Filters[0].Property.ValueCase);
            Assert.Equal(ByteStringCompareAsType.Equals, request.Filters[0].Property.ExponentialHistogram.DataPoint.Exemplar.TraceId.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.ExponentialHistogram.DataPoint.Exemplar.TraceId.Compare);
        }
        
        [Fact]
        public void AddDataPointFlagsPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const uint value = 123;
            var request = builder.Where(filters => filters.ExponentialHistogram.DataPoint.AddFlagsFilter(value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.ExponentialHistogram, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.ExponentialHistogram.DataPoint.Flags.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.ExponentialHistogram.DataPoint.Flags.Compare);
        }
        
        [Fact]
        public void AddDataPointMinPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const double value = 123;
            var request = builder.Where(filters => filters.ExponentialHistogram.DataPoint.AddMinFilter(value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.ExponentialHistogram, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.ExponentialHistogram.DataPoint.Min.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.ExponentialHistogram.DataPoint.Min.Compare);
        }
        
        [Fact]
        public void AddDataPointMaxPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const double value = 123;
            var request = builder.Where(filters => filters.ExponentialHistogram.DataPoint.AddMaxFilter(value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.ExponentialHistogram, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.ExponentialHistogram.DataPoint.Max.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.ExponentialHistogram.DataPoint.Max.Compare);
        }
        
        [Fact]
        public void AddDataPointZeroThresholdPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const double value = 123;
            var request = builder.Where(filters => filters.ExponentialHistogram.DataPoint.AddZeroThresholdFilter(value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.ExponentialHistogram, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.ExponentialHistogram.DataPoint.ZeroThreshold.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.ExponentialHistogram.DataPoint.ZeroThreshold.Compare);
        }
        
        [Fact]
        public void AddAggregationTemporalityPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const AggregationTemporality value = AggregationTemporality.Cumulative;
            var request = builder.Where(filters => filters.ExponentialHistogram.AddAggregationTemporalityFilter(value, EnumCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.ExponentialHistogram, request.Filters[0].Property.ValueCase);
            Assert.Equal(EnumCompareAsType.Equals, request.Filters[0].Property.ExponentialHistogram.AggregationTemporality.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.ExponentialHistogram.AggregationTemporality.Compare);
        }
    }
}