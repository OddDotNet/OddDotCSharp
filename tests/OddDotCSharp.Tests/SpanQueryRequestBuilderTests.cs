using OddDotNet.Proto.Spans.V1;

namespace OddDotCSharp.Tests;

public class SpanQueryRequestBuilderTests
{
    public class BuildShould
    {
        [Fact]
        public void SetTakeToTakeFirstWhenNotSpecified()
        {
            var request = SpanQueryRequestBuilder.Create().Build();
            
            Assert.Equal(Take.TakeTypeOneofCase.TakeFirst, request.Take.TakeTypeCase);
        }

        [Fact]
        public void SetDurationToDefaultWhenNotSpecified()
        {
            ulong expectedMsValue = 30000;
            var request = SpanQueryRequestBuilder.Create().Build();
            
            Assert.Equal(Duration.ValueOneofCase.MillisecondsValue, request.Duration.ValueCase);
            Assert.Equal(expectedMsValue, request.Duration.MillisecondsValue);
        }
    }

    public class TakeFirstShould
    {
        [Fact]
        public void SetTakeToTakeFirst()
        {
            var builder = SpanQueryRequestBuilder.Create();
            builder.TakeFirst();
            var request = builder.Build();
            
            Assert.Equal(Take.TakeTypeOneofCase.TakeFirst, request.Take.TakeTypeCase);
        }
    }

    public class TakeExactShould
    {
        [Fact]
        public void SetTakeToTakeExact()
        {
            var builder = SpanQueryRequestBuilder.Create();
            int number = 3;
            builder.TakeExact(number);
            var request = builder.Build();
            
            Assert.Equal(Take.TakeTypeOneofCase.TakeExact, request.Take.TakeTypeCase);
            Assert.Equal(number, request.Take.TakeExact.Count);
        }
    }

    public class TakeAllShould
    {
        [Fact]
        public void SetTakeToTakeAll()
        {
            var builder = SpanQueryRequestBuilder.Create();
            builder.TakeAll();
            var request = builder.Build();
            
            Assert.Equal(Take.TakeTypeOneofCase.TakeAll, request.Take.TakeTypeCase);
        }
    }

    public class WaitShould
    {
        [Fact]
        public void SetDuration()
        {
            var builder = SpanQueryRequestBuilder.Create();
            TimeSpan duration = TimeSpan.FromMilliseconds(500);
            builder.Wait(duration);
            var request = builder.Build();
            
            Assert.Equal(Duration.ValueOneofCase.MillisecondsValue, request.Duration.ValueCase);
            Assert.Equal(duration.TotalMilliseconds, request.Duration.MillisecondsValue);
        }
    }

    public class WhereShould
    {
        [Fact]
        public void ThrowOnInvalidProperty()
        {
            var builder = SpanQueryRequestBuilder.Create();
            const string name = "service1";

            Assert.Throws<Exception>(() =>
            {
                builder.Where(filters =>
                {
                    filters.AddFilter(WhereSpanPropertyFilter.PropertyOneofCase.None, name, StringCompareAsType.Equals);
                });
            });
        }

        [Fact]
        public void ThrowOnMismatchPropertyType()
        {
            var builder = SpanQueryRequestBuilder.Create();
            const string name = "service1";

            Assert.Throws<Exception>(() =>
            {
                builder.Where(filters =>
                {
                    // TraceId is a ByteString, not a String
                    filters.AddFilter(WhereSpanPropertyFilter.PropertyOneofCase.TraceId, name, StringCompareAsType.Equals);
                });
            });
        }
        
        [Fact]
        public void AddStringPropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            const string name = "service1";
            builder.Where(filters =>
            {
                filters.AddFilter(WhereSpanPropertyFilter.PropertyOneofCase.Name, name, StringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(WhereSpanFilter.FilterOneofCase.SpanProperty, request.Filters.First().FilterCase);
            Assert.Equal(WhereSpanPropertyFilter.PropertyOneofCase.Name, request.Filters.First().SpanProperty.PropertyCase);
            Assert.Equal(StringCompareAsType.Equals, request.Filters.First().SpanProperty.Name.CompareAs);
            Assert.Equal(name, request.Filters.First().SpanProperty.Name.Compare);
        }
        
        [Fact]
        public void AddByteStringPropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            byte[] spanId = [1, 2, 3, 4, 5, 6, 7, 8];
            builder.Where(filters =>
            {
                filters.AddFilter(WhereSpanPropertyFilter.PropertyOneofCase.SpanId, spanId, ByteStringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.FilterOneofCase.SpanProperty, filterToFind.FilterCase);
            Assert.Equal(WhereSpanPropertyFilter.PropertyOneofCase.SpanId, filterToFind.SpanProperty.PropertyCase);
            Assert.Equal(ByteStringCompareAsType.Equals, filterToFind.SpanProperty.SpanId.CompareAs);
            Assert.Equal(spanId, filterToFind.SpanProperty.SpanId.Compare);
        }
        
        [Fact]
        public void AddUInt64PropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            ulong startTimeUnixNano = 1000000000;
            builder.Where(filters =>
            {
                filters.AddFilter(WhereSpanPropertyFilter.PropertyOneofCase.StartTimeUnixNano, startTimeUnixNano, UInt64CompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.FilterOneofCase.SpanProperty, filterToFind.FilterCase);
            Assert.Equal(WhereSpanPropertyFilter.PropertyOneofCase.StartTimeUnixNano, filterToFind.SpanProperty.PropertyCase);
            Assert.Equal(UInt64CompareAsType.Equals, filterToFind.SpanProperty.StartTimeUnixNano.CompareAs);
            Assert.Equal(startTimeUnixNano, filterToFind.SpanProperty.StartTimeUnixNano.Compare);
        }
        
        [Fact]
        public void AddSpanStatusCodePropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            SpanStatusCode code = SpanStatusCode.Ok;
            builder.Where(filters =>
            {
                filters.AddFilter(WhereSpanPropertyFilter.PropertyOneofCase.StatusCode, code, EnumCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.FilterOneofCase.SpanProperty, filterToFind.FilterCase);
            Assert.Equal(WhereSpanPropertyFilter.PropertyOneofCase.StatusCode, filterToFind.SpanProperty.PropertyCase);
            Assert.Equal(EnumCompareAsType.Equals, filterToFind.SpanProperty.StatusCode.CompareAs);
            Assert.Equal(code, filterToFind.SpanProperty.StatusCode.Compare);
        }
        
        [Fact]
        public void AddSpanKindPropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            SpanKind kind = SpanKind.Internal;
            builder.Where(filters =>
            {
                filters.AddFilter(WhereSpanPropertyFilter.PropertyOneofCase.Kind, kind, EnumCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.FilterOneofCase.SpanProperty, filterToFind.FilterCase);
            Assert.Equal(WhereSpanPropertyFilter.PropertyOneofCase.Kind, filterToFind.SpanProperty.PropertyCase);
            Assert.Equal(EnumCompareAsType.Equals, filterToFind.SpanProperty.Kind.CompareAs);
            Assert.Equal(kind, filterToFind.SpanProperty.Kind.Compare);
        }
        
        [Fact]
        public void AddUInt32PropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            uint flags = 1000000000;
            builder.Where(filters =>
            {
                filters.AddFilter(WhereSpanPropertyFilter.PropertyOneofCase.Flags, flags, UInt32CompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.FilterOneofCase.SpanProperty, filterToFind.FilterCase);
            Assert.Equal(WhereSpanPropertyFilter.PropertyOneofCase.Flags, filterToFind.SpanProperty.PropertyCase);
            Assert.Equal(UInt32CompareAsType.Equals, filterToFind.SpanProperty.Flags.CompareAs);
            Assert.Equal(flags, filterToFind.SpanProperty.Flags.Compare);
        }
        
        [Fact]
        public void AddKeyValueStringPropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            string key = "service.name";
            string value = "test";
            
            builder.Where(filters =>
            {
                filters.AddFilter(WhereSpanPropertyFilter.PropertyOneofCase.Attribute, key, value, StringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.FilterOneofCase.SpanProperty, filterToFind.FilterCase);
            Assert.Equal(WhereSpanPropertyFilter.PropertyOneofCase.Attribute, filterToFind.SpanProperty.PropertyCase);
            Assert.Equal(StringCompareAsType.Equals, filterToFind.SpanProperty.Attribute.StringValue.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.Attribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.Attribute.StringValue.Compare);
        }
        
        [Fact]
        public void AddKeyValueBoolPropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            string key = "service.started";
            bool value = true;
            
            builder.Where(filters =>
            {
                filters.AddFilter(WhereSpanPropertyFilter.PropertyOneofCase.Attribute, key, value, BoolCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.FilterOneofCase.SpanProperty, filterToFind.FilterCase);
            Assert.Equal(WhereSpanPropertyFilter.PropertyOneofCase.Attribute, filterToFind.SpanProperty.PropertyCase);
            Assert.Equal(BoolCompareAsType.Equals, filterToFind.SpanProperty.Attribute.BoolValue.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.Attribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.Attribute.BoolValue.Compare);
        }
        
        [Fact]
        public void AddKeyValueInt64PropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            string key = "service.started";
            long value = 1;
            
            builder.Where(filters =>
            {
                filters.AddFilter(WhereSpanPropertyFilter.PropertyOneofCase.Attribute, key, value, Int64CompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.FilterOneofCase.SpanProperty, filterToFind.FilterCase);
            Assert.Equal(WhereSpanPropertyFilter.PropertyOneofCase.Attribute, filterToFind.SpanProperty.PropertyCase);
            Assert.Equal(Int64CompareAsType.Equals, filterToFind.SpanProperty.Attribute.Int64Value.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.Attribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.Attribute.Int64Value.Compare);
        }
        
        [Fact]
        public void AddKeyValueDoublePropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            string key = "service.started";
            double value = 1;
            
            builder.Where(filters =>
            {
                filters.AddFilter(WhereSpanPropertyFilter.PropertyOneofCase.Attribute, key, value, DoubleCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.FilterOneofCase.SpanProperty, filterToFind.FilterCase);
            Assert.Equal(WhereSpanPropertyFilter.PropertyOneofCase.Attribute, filterToFind.SpanProperty.PropertyCase);
            Assert.Equal(DoubleCompareAsType.Equals, filterToFind.SpanProperty.Attribute.DoubleValue.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.Attribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.Attribute.DoubleValue.Compare);
        }
        
        [Fact]
        public void AddKeyValueByteStringPropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            string key = "trace.parentSpanId";
            byte[] value = [1, 2, 3, 4, 5, 6, 7, 8];
            
            builder.Where(filters =>
            {
                filters.AddFilter(WhereSpanPropertyFilter.PropertyOneofCase.Attribute, key, value, ByteStringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.FilterOneofCase.SpanProperty, filterToFind.FilterCase);
            Assert.Equal(WhereSpanPropertyFilter.PropertyOneofCase.Attribute, filterToFind.SpanProperty.PropertyCase);
            Assert.Equal(ByteStringCompareAsType.Equals, filterToFind.SpanProperty.Attribute.ByteStringValue.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.Attribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.Attribute.ByteStringValue.Compare);
        }

        [Fact]
        public void AddOrFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            builder.Where(filters =>
            {
                filters.AddOrFilter(orFilters =>
                {
                    orFilters.AddFilter(WhereSpanPropertyFilter.PropertyOneofCase.Name, "GET", StringCompareAsType.Equals);
                    orFilters.AddFilter(WhereSpanPropertyFilter.PropertyOneofCase.Name, "POST", StringCompareAsType.Equals);
                });
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.FilterOneofCase.SpanOr, filterToFind.FilterCase);

            var firstOr = filterToFind.SpanOr.Filters[0];
            
            Assert.Equal(WhereSpanPropertyFilter.PropertyOneofCase.Name, firstOr.SpanProperty.PropertyCase);
            Assert.Equal(StringCompareAsType.Equals, firstOr.SpanProperty.Name.CompareAs);
            Assert.Equal("GET", firstOr.SpanProperty.Name.Compare);
            
            var secondOr = filterToFind.SpanOr.Filters[1];
            
            Assert.Equal(WhereSpanPropertyFilter.PropertyOneofCase.Name, secondOr.SpanProperty.PropertyCase);
            Assert.Equal(StringCompareAsType.Equals, secondOr.SpanProperty.Name.CompareAs);
            Assert.Equal("POST", secondOr.SpanProperty.Name.Compare);
        }
    }
}