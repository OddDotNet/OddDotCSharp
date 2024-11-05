using OddDotNet.Proto.Common.V1;
using OddDotNet.Proto.Metrics.V1;

namespace OddDotCSharp.Tests;

public class MetricQueryRequestBuilderTests
{
    public class BuildShould
    {
        [Fact]
        public void SetTakeToTakeFirstWhenNotSpecified()
        {
            var request = new MetricQueryRequestBuilder().Build();
            
            Assert.Equal(Take.ValueOneofCase.TakeFirst, request.Take.ValueCase);
        }

        [Fact]
        public void SetDurationToDefaultWhenNotSpecified()
        {
            int expectedMsValue = 30000;
            var request = new MetricQueryRequestBuilder().Build();
            
            Assert.Equal(expectedMsValue, request.Duration.Milliseconds);
        }
    }

    public class TakeShould
    {
        [Fact]
        public void SetTakeToTakeFirst()
        {
            var builder = new MetricQueryRequestBuilder();
            builder.TakeFirst();
            var request = builder.Build();
            
            Assert.Equal(Take.ValueOneofCase.TakeFirst, request.Take.ValueCase);
        }

        [Fact]
        public void SetTakeToTakeExact()
        {
            var builder = new MetricQueryRequestBuilder();
            builder.TakeExact(123);
            var request = builder.Build();
            
            Assert.Equal(Take.ValueOneofCase.TakeExact, request.Take.ValueCase);
            Assert.Equal(123, request.Take.TakeExact.Count);
        }

        [Fact]
        public void SetTakeToTakeAll()
        {
            var builder = new MetricQueryRequestBuilder();
            builder.TakeAll();
            var request = builder.Build();
            
            Assert.Equal(Take.ValueOneofCase.TakeAll, request.Take.ValueCase);
        }
    }
    
    public class WaitShould
    {
        [Fact]
        public void SetDuration()
        {
            var builder = new MetricQueryRequestBuilder();
            TimeSpan duration = TimeSpan.FromMilliseconds(500);
            builder.Wait(duration);
            var request = builder.Build();
            
            Assert.Equal(duration.TotalMilliseconds, request.Duration.Milliseconds);
        }
    }

    public class WhereShould
    {
        [Fact]
        public void AddNamePropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string name = "test";
            var request = builder.Where(filters => filters.AddNameFilter(name, StringCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Name, request.Filters[0].Property.ValueCase);
            Assert.Equal(StringCompareAsType.Equals, request.Filters[0].Property.Name.CompareAs);
            Assert.Equal(name, request.Filters[0].Property.Name.Compare);
        }
        
        [Fact]
        public void AddDescriptionPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string description = "test";
            var request = builder.Where(filters => filters.AddDescriptionFilter(description, StringCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Description, request.Filters[0].Property.ValueCase);
            Assert.Equal(StringCompareAsType.Equals, request.Filters[0].Property.Description.CompareAs);
            Assert.Equal(description, request.Filters[0].Property.Description.Compare);
        }
        
        [Fact]
        public void AddUnitPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string unit = "test";
            var request = builder.Where(filters => filters.AddUnitFilter(unit, StringCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Unit, request.Filters[0].Property.ValueCase);
            Assert.Equal(StringCompareAsType.Equals, request.Filters[0].Property.Unit.CompareAs);
            Assert.Equal(unit, request.Filters[0].Property.Unit.Compare);
        }
        
        [Fact]
        public void AddMetadataStringPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string key = "service.name";
            const string value = "test";
            var request = builder.Where(filters => filters.AddMetadataFilter(key, value, StringCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Metadata, request.Filters[0].Property.ValueCase);
            Assert.Equal(StringCompareAsType.Equals, request.Filters[0].Property.Metadata.StringValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Metadata.Key);
            Assert.Equal(value, request.Filters[0].Property.Metadata.StringValue.Compare);
        }
        
        [Fact]
        public void AddMetadataBoolPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string key = "service.name";
            const bool value = true;
            var request = builder.Where(filters => filters.AddMetadataFilter(key, value, BoolCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Metadata, request.Filters[0].Property.ValueCase);
            Assert.Equal(BoolCompareAsType.Equals, request.Filters[0].Property.Metadata.BoolValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Metadata.Key);
            Assert.Equal(value, request.Filters[0].Property.Metadata.BoolValue.Compare);
        }
        
        [Fact]
        public void AddMetadataInt64PropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string key = "service.name";
            const long value = 123;
            var request = builder.Where(filters => filters.AddMetadataFilter(key, value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Metadata, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Metadata.Int64Value.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Metadata.Key);
            Assert.Equal(value, request.Filters[0].Property.Metadata.Int64Value.Compare);
        }
        
        [Fact]
        public void AddMetadataDoublePropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string key = "service.name";
            const double value = 123;
            var request = builder.Where(filters => filters.AddMetadataFilter(key, value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Metadata, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Metadata.DoubleValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Metadata.Key);
            Assert.Equal(value, request.Filters[0].Property.Metadata.DoubleValue.Compare);
        }
        
        [Fact]
        public void AddMetadataByteStringPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string key = "service.name";
            byte[] value = [123];
            var request = builder.Where(filters => filters.AddMetadataFilter(key, value, ByteStringCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Metadata, request.Filters[0].Property.ValueCase);
            Assert.Equal(ByteStringCompareAsType.Equals, request.Filters[0].Property.Metadata.ByteStringValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Metadata.Key);
            Assert.Equal(value, request.Filters[0].Property.Metadata.ByteStringValue.Compare);
        }
    }
}