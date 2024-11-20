using OddDotNet.Proto.Common.V1;
using OddDotNet.Proto.Metrics.V1;
using OddDotNet.Proto.Resource.V1;

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
            Assert.Equal(StringCompareAsType.Equals, request.Filters[0].Property.Metadata.Values[0].Value.StringValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Metadata.Values[0].Key);
            Assert.Equal(value, request.Filters[0].Property.Metadata.Values[0].Value.StringValue.Compare);
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
            Assert.Equal(BoolCompareAsType.Equals, request.Filters[0].Property.Metadata.Values[0].Value.BoolValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Metadata.Values[0].Key);
            Assert.Equal(value, request.Filters[0].Property.Metadata.Values[0].Value.BoolValue.Compare);
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
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Metadata.Values[0].Value.IntValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Metadata.Values[0].Key);
            Assert.Equal(value, request.Filters[0].Property.Metadata.Values[0].Value.IntValue.Compare);
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
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Metadata.Values[0].Value.DoubleValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Metadata.Values[0].Key);
            Assert.Equal(value, request.Filters[0].Property.Metadata.Values[0].Value.DoubleValue.Compare);
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
            Assert.Equal(ByteStringCompareAsType.Equals, request.Filters[0].Property.Metadata.Values[0].Value.ByteStringValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Metadata.Values[0].Key);
            Assert.Equal(value, request.Filters[0].Property.Metadata.Values[0].Value.ByteStringValue.Compare);
        }
        
        [Fact]
        public void AddOrFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            builder.Where(filters =>
            {
                filters.AddOrFilter(orFilters =>
                {
                    orFilters.AddNameFilter("GET", StringCompareAsType.Equals);
                    orFilters.AddNameFilter("POST", StringCompareAsType.Equals);
                });
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(Where.ValueOneofCase.Or, filterToFind.ValueCase);

            var firstOr = filterToFind.Or.Filters[0];
            
            Assert.Equal(PropertyFilter.ValueOneofCase.Name, firstOr.Property.ValueCase);
            Assert.Equal(StringCompareAsType.Equals, firstOr.Property.Name.CompareAs);
            Assert.Equal("GET", firstOr.Property.Name.Compare);
            
            var secondOr = filterToFind.Or.Filters[1];
            
            Assert.Equal(PropertyFilter.ValueOneofCase.Name, secondOr.Property.ValueCase);
            Assert.Equal(StringCompareAsType.Equals, secondOr.Property.Name.CompareAs);
            Assert.Equal("POST", secondOr.Property.Name.Compare);
        }
        
        [Fact]
        public void AddInstrumentationScopeNamePropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string name = "service1";
            builder.Where(filters =>
            {
                filters.InstrumentationScope.AddNameFilter(name, StringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(Where.ValueOneofCase.InstrumentationScope, filterToFind.ValueCase);
            Assert.Equal(InstrumentationScopeFilter.ValueOneofCase.Name, filterToFind.InstrumentationScope.ValueCase);
            Assert.Equal(StringCompareAsType.Equals, filterToFind.InstrumentationScope.Name.CompareAs);
            Assert.Equal(name, filterToFind.InstrumentationScope.Name.Compare);
        }
        
        [Fact]
        public void AddStringInstrumentationScopeAttributePropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            string key = "service.name";
            string value = "test";
            
            builder.Where(filters =>
            {
                filters.InstrumentationScope.AddAttributeFilter(key, value, StringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(Where.ValueOneofCase.InstrumentationScope, filterToFind.ValueCase);
            Assert.Equal(InstrumentationScopeFilter.ValueOneofCase.Attributes, filterToFind.InstrumentationScope.ValueCase);
            Assert.Equal(StringCompareAsType.Equals, filterToFind.InstrumentationScope.Attributes.Values[0].Value.StringValue.CompareAs);
            Assert.Equal(key, filterToFind.InstrumentationScope.Attributes.Values[0].Key);
            Assert.Equal(value, filterToFind.InstrumentationScope.Attributes.Values[0].Value.StringValue.Compare);
        }
        
        [Fact]
        public void AddBoolInstrumentationScopeAttributePropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            string key = "service.started";
            bool value = true;
            
            builder.Where(filters =>
            {
                filters.InstrumentationScope.AddAttributeFilter(key, value, BoolCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(Where.ValueOneofCase.InstrumentationScope, filterToFind.ValueCase);
            Assert.Equal(InstrumentationScopeFilter.ValueOneofCase.Attributes, filterToFind.InstrumentationScope.ValueCase);
            Assert.Equal(BoolCompareAsType.Equals, filterToFind.InstrumentationScope.Attributes.Values[0].Value.BoolValue.CompareAs);
            Assert.Equal(key, filterToFind.InstrumentationScope.Attributes.Values[0].Key);
            Assert.Equal(value, filterToFind.InstrumentationScope.Attributes.Values[0].Value.BoolValue.Compare);
        }
        
        [Fact]
        public void AddInt64InstrumentationScopeAttributePropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            string key = "service.started";
            long value = 1;
            
            builder.Where(filters =>
            {
                filters.InstrumentationScope.AddAttributeFilter(key, value, NumberCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(Where.ValueOneofCase.InstrumentationScope, filterToFind.ValueCase);
            Assert.Equal(InstrumentationScopeFilter.ValueOneofCase.Attributes, filterToFind.InstrumentationScope.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, filterToFind.InstrumentationScope.Attributes.Values[0].Value.IntValue.CompareAs);
            Assert.Equal(key, filterToFind.InstrumentationScope.Attributes.Values[0].Key);
            Assert.Equal(value, filterToFind.InstrumentationScope.Attributes.Values[0].Value.IntValue.Compare);
        }
        
        [Fact]
        public void AddDoubleInstrumentationScopeAttributePropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            string key = "service.started";
            double value = 1;
            
            builder.Where(filters =>
            {
                filters.InstrumentationScope.AddAttributeFilter(key, value, NumberCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(Where.ValueOneofCase.InstrumentationScope, filterToFind.ValueCase);
            Assert.Equal(InstrumentationScopeFilter.ValueOneofCase.Attributes, filterToFind.InstrumentationScope.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, filterToFind.InstrumentationScope.Attributes.Values[0].Value.DoubleValue.CompareAs);
            Assert.Equal(key, filterToFind.InstrumentationScope.Attributes.Values[0].Key);
            Assert.Equal(value, filterToFind.InstrumentationScope.Attributes.Values[0].Value.DoubleValue.Compare);
        }
        
        [Fact]
        public void AddByteStringInstrumentationScopeAttributePropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            string key = "service.name";
            byte[] value = [1, 2, 3, 4];
            
            builder.Where(filters =>
            {
                filters.InstrumentationScope.AddAttributeFilter(key, value, ByteStringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(Where.ValueOneofCase.InstrumentationScope, filterToFind.ValueCase);
            Assert.Equal(InstrumentationScopeFilter.ValueOneofCase.Attributes, filterToFind.InstrumentationScope.ValueCase);
            Assert.Equal(ByteStringCompareAsType.Equals, filterToFind.InstrumentationScope.Attributes.Values[0].Value.ByteStringValue.CompareAs);
            Assert.Equal(key, filterToFind.InstrumentationScope.Attributes.Values[0].Key);
            Assert.Equal(value, filterToFind.InstrumentationScope.Attributes.Values[0].Value.ByteStringValue.Compare);
        }
        
        [Fact]
        public void AddInstrumentationScopeVersionPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string name = "service1";
            builder.Where(filters =>
            {
                filters.InstrumentationScope.AddVersionFilter(name, StringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(Where.ValueOneofCase.InstrumentationScope, filterToFind.ValueCase);
            Assert.Equal(InstrumentationScopeFilter.ValueOneofCase.Version, filterToFind.InstrumentationScope.ValueCase);
            Assert.Equal(StringCompareAsType.Equals, filterToFind.InstrumentationScope.Version.CompareAs);
            Assert.Equal(name, filterToFind.InstrumentationScope.Version.Compare);
        }
        
        [Fact]
        public void AddInstrumentationScopeSchemaUrlPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string name = "service1";
            builder.Where(filters =>
            {
                filters.InstrumentationScope.AddSchemaUrlFilter(name, StringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(Where.ValueOneofCase.InstrumentationScopeSchemaUrl, filterToFind.ValueCase);
            Assert.Equal(StringCompareAsType.Equals, filterToFind.InstrumentationScopeSchemaUrl.CompareAs);
            Assert.Equal(name, filterToFind.InstrumentationScopeSchemaUrl.Compare);
        }
        
        [Fact]
        public void AddInstrumentationScopeDroppedAttributesCountPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const uint value = 123;
            builder.Where(filters =>
            {
                filters.InstrumentationScope.AddDroppedAttributesCountFilter(value, NumberCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(Where.ValueOneofCase.InstrumentationScope, filterToFind.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, filterToFind.InstrumentationScope.DroppedAttributesCount.CompareAs);
            Assert.Equal(value, filterToFind.InstrumentationScope.DroppedAttributesCount.Compare);
        }
        
        [Fact]
        public void AddResourceSchemaUrlPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const string name = "service1";
            builder.Where(filters =>
            {
                filters.Resource.AddSchemaUrlFilter(name, StringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(Where.ValueOneofCase.ResourceSchemaUrl, filterToFind.ValueCase);
            Assert.Equal(StringCompareAsType.Equals, filterToFind.ResourceSchemaUrl.CompareAs);
            Assert.Equal(name, filterToFind.ResourceSchemaUrl.Compare);
        }
        
        [Fact]
        public void AddStringResourceAttributePropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            string key = "service.name";
            string value = "test";
            
            builder.Where(filters =>
            {
                filters.Resource.AddAttributeFilter(key, value, StringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(Where.ValueOneofCase.Resource, filterToFind.ValueCase);
            Assert.Equal(ResourceFilter.ValueOneofCase.Attributes, filterToFind.Resource.ValueCase);
            Assert.Equal(StringCompareAsType.Equals, filterToFind.Resource.Attributes.Values[0].Value.StringValue.CompareAs);
            Assert.Equal(key, filterToFind.Resource.Attributes.Values[0].Key);
            Assert.Equal(value, filterToFind.Resource.Attributes.Values[0].Value.StringValue.Compare);
        }
        
        [Fact]
        public void AddBoolResourceAttributePropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            string key = "service.started";
            bool value = true;
            
            builder.Where(filters =>
            {
                filters.Resource.AddAttributeFilter(key, value, BoolCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(Where.ValueOneofCase.Resource, filterToFind.ValueCase);
            Assert.Equal(ResourceFilter.ValueOneofCase.Attributes, filterToFind.Resource.ValueCase);
            Assert.Equal(BoolCompareAsType.Equals, filterToFind.Resource.Attributes.Values[0].Value.BoolValue.CompareAs);
            Assert.Equal(key, filterToFind.Resource.Attributes.Values[0].Key);
            Assert.Equal(value, filterToFind.Resource.Attributes.Values[0].Value.BoolValue.Compare);
        }
        
        [Fact]
        public void AddInt64ResourceAttributePropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            string key = "service.started";
            long value = 1;
            
            builder.Where(filters =>
            {
                filters.Resource.AddAttributeFilter(key, value, NumberCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(Where.ValueOneofCase.Resource, filterToFind.ValueCase);
            Assert.Equal(ResourceFilter.ValueOneofCase.Attributes, filterToFind.Resource.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, filterToFind.Resource.Attributes.Values[0].Value.IntValue.CompareAs);
            Assert.Equal(key, filterToFind.Resource.Attributes.Values[0].Key);
            Assert.Equal(value, filterToFind.Resource.Attributes.Values[0].Value.IntValue.Compare);
        }
        
        [Fact]
        public void AddDoubleResourceAttributePropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            string key = "service.started";
            double value = 1;
            
            builder.Where(filters =>
            {
                filters.Resource.AddAttributeFilter(key, value, NumberCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(Where.ValueOneofCase.Resource, filterToFind.ValueCase);
            Assert.Equal(ResourceFilter.ValueOneofCase.Attributes, filterToFind.Resource.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, filterToFind.Resource.Attributes.Values[0].Value.DoubleValue.CompareAs);
            Assert.Equal(key, filterToFind.Resource.Attributes.Values[0].Key);
            Assert.Equal(value, filterToFind.Resource.Attributes.Values[0].Value.DoubleValue.Compare);
        }
        
        [Fact]
        public void AddByteStringResourceAttributePropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            string key = "service.name";
            byte[] value = [1, 2, 3, 4];
            
            builder.Where(filters =>
            {
                filters.Resource.AddAttributeFilter(key, value, ByteStringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(Where.ValueOneofCase.Resource, filterToFind.ValueCase);
            Assert.Equal(ResourceFilter.ValueOneofCase.Attributes, filterToFind.Resource.ValueCase);
            Assert.Equal(ByteStringCompareAsType.Equals, filterToFind.Resource.Attributes.Values[0].Value.ByteStringValue.CompareAs);
            Assert.Equal(key, filterToFind.Resource.Attributes.Values[0].Key);
            Assert.Equal(value, filterToFind.Resource.Attributes.Values[0].Value.ByteStringValue.Compare);
        }
        
        [Fact]
        public void AddResourceDroppedAttributesCountPropertyFilter()
        {
            var builder = new MetricQueryRequestBuilder();
            const uint value = 123;
            builder.Where(filters =>
            {
                filters.Resource.AddDroppedAttributesCountFilter(value, NumberCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(Where.ValueOneofCase.Resource, filterToFind.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, filterToFind.Resource.DroppedAttributesCount.CompareAs);
            Assert.Equal(value, filterToFind.Resource.DroppedAttributesCount.Compare);
        }
    }
}