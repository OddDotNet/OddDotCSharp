using OddDotNet.Proto.Common.V1;
using OddDotNet.Proto.Metrics.V1;

namespace OddDotCSharp.Tests;

public class MetricSummaryQueryRequestBuilderTests
{
    public class WhereShould
    {
        [Fact]
        public void AddDataPointAttributeStringPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string key = "service.name";
            const string value = "test";
            var request = builder.Where(filters => filters.Summary.DataPoint.AddAttributeFilter(key, value, StringCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Summary, request.Filters[0].Property.ValueCase);
            Assert.Equal(StringCompareAsType.Equals, request.Filters[0].Property.Summary.DataPoint.Attributes.Values[0].Value.StringValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Summary.DataPoint.Attributes.Values[0].Key);
            Assert.Equal(value, request.Filters[0].Property.Summary.DataPoint.Attributes.Values[0].Value.StringValue.Compare);
        }
        
        [Fact]
        public void AddDataPointAttributeBoolPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string key = "service.name";
            const bool value = true;
            var request = builder.Where(filters => filters.Summary.DataPoint.AddAttributeFilter(key, value, BoolCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Summary, request.Filters[0].Property.ValueCase);
            Assert.Equal(BoolCompareAsType.Equals, request.Filters[0].Property.Summary.DataPoint.Attributes.Values[0].Value.BoolValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Summary.DataPoint.Attributes.Values[0].Key);
            Assert.Equal(value, request.Filters[0].Property.Summary.DataPoint.Attributes.Values[0].Value.BoolValue.Compare);
        }
        
        [Fact]
        public void AddDataPointAttributeInt64PropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string key = "service.name";
            const long value = 123;
            var request = builder.Where(filters => filters.Summary.DataPoint.AddAttributeFilter(key, value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Summary, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Summary.DataPoint.Attributes.Values[0].Value.IntValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Summary.DataPoint.Attributes.Values[0].Key);
            Assert.Equal(value, request.Filters[0].Property.Summary.DataPoint.Attributes.Values[0].Value.IntValue.Compare);
        }
        
        [Fact]
        public void AddDataPointAttributeDoublePropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string key = "service.name";
            const double value = 123;
            var request = builder.Where(filters => filters.Summary.DataPoint.AddAttributeFilter(key, value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Summary, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Summary.DataPoint.Attributes.Values[0].Value.DoubleValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Summary.DataPoint.Attributes.Values[0].Key);
            Assert.Equal(value, request.Filters[0].Property.Summary.DataPoint.Attributes.Values[0].Value.DoubleValue.Compare);
        }
        
        [Fact]
        public void AddDataPointAttributeByteStringPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string key = "service.name";
            byte[] value = [123];
            var request = builder.Where(filters => filters.Summary.DataPoint.AddAttributeFilter(key, value, ByteStringCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Summary, request.Filters[0].Property.ValueCase);
            Assert.Equal(ByteStringCompareAsType.Equals, request.Filters[0].Property.Summary.DataPoint.Attributes.Values[0].Value.ByteStringValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Summary.DataPoint.Attributes.Values[0].Key);
            Assert.Equal(value, request.Filters[0].Property.Summary.DataPoint.Attributes.Values[0].Value.ByteStringValue.Compare);
        }
        
        [Fact]
        public void AddDataPointAttributeArrayPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string key = "key";
            const double value = 123.0;
            var request = builder.Where(filters => filters.Summary.DataPoint.AddAttributeArrayFilter(key, arrayFilters =>
            {
                arrayFilters.AddFilter(value, NumberCompareAsType.Equals);
            })).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Summary, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Summary.DataPoint.Attributes.Values[0].Value.ArrayValue.Values[0].DoubleValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Summary.DataPoint.Attributes.Values[0].Key);
            Assert.Equal(value, request.Filters[0].Property.Summary.DataPoint.Attributes.Values[0].Value.ArrayValue.Values[0].DoubleValue.Compare);
        }
        
        [Fact]
        public void AddDataPointAttributeKeyValueListPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string key = "key";
            const double value = 123.0;
            var request = builder.Where(filters => filters.Summary.DataPoint.AddAttributeKeyValueListFilter(key, kvlFilters =>
            {
                kvlFilters.AddFilter(key, value, NumberCompareAsType.Equals);
            })).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Summary, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Summary.DataPoint.Attributes.Values[0].Value.KvlistValue.Values[0].Value.DoubleValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Summary.DataPoint.Attributes.Values[0].Key);
            Assert.Equal(key, request.Filters[0].Property.Summary.DataPoint.Attributes.Values[0].Value.KvlistValue.Values[0].Key);
            Assert.Equal(value, request.Filters[0].Property.Summary.DataPoint.Attributes.Values[0].Value.KvlistValue.Values[0].Value.DoubleValue.Compare);
        }
        
        [Fact]
        public void AddDataPointStartTimeUnixNanoPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const ulong value = 123;
            var request = builder.Where(filters => filters.Summary.DataPoint.AddStartTimeUnixNanoFilter(value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Summary, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Summary.DataPoint.StartTimeUnixNano.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.Summary.DataPoint.StartTimeUnixNano.Compare);
        }
        
        [Fact]
        public void AddDataPointTimeUnixNanoPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const ulong value = 123;
            var request = builder.Where(filters => filters.Summary.DataPoint.AddTimeUnixNanoFilter(value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Summary, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Summary.DataPoint.TimeUnixNano.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.Summary.DataPoint.TimeUnixNano.Compare);
        }
        
        [Fact]
        public void AddDataPointCountPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const ulong value = 123;
            var request = builder.Where(filters => filters.Summary.DataPoint.AddCountFilter(value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Summary, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Summary.DataPoint.Count.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.Summary.DataPoint.Count.Compare);
        }
        
        [Fact]
        public void AddDataPointSumPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const double value = 123;
            var request = builder.Where(filters => filters.Summary.DataPoint.AddSumFilter(value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Summary, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Summary.DataPoint.Sum.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.Summary.DataPoint.Sum.Compare);
        }
        
        [Fact]
        public void AddDataPointQuantileValueQuantilePropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const double value = 123;
            var request = builder.Where(filters => filters.Summary.DataPoint.ValueAtQuantile.AddQuantileFilter(value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Summary, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Summary.DataPoint.QuantileValue.Quantile.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.Summary.DataPoint.QuantileValue.Quantile.Compare);
        }
        
        [Fact]
        public void AddDataPointQuantileValueValuePropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const double value = 123;
            var request = builder.Where(filters => filters.Summary.DataPoint.ValueAtQuantile.AddValueFilter(value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Summary, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Summary.DataPoint.QuantileValue.Value.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.Summary.DataPoint.QuantileValue.Value.Compare);
        }
        
        [Fact]
        public void AddDataPointFlagsPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const uint value = 123;
            var request = builder.Where(filters => filters.Summary.DataPoint.AddFlagsFilter(value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Summary, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Summary.DataPoint.Flags.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.Summary.DataPoint.Flags.Compare);
        }
    }
}