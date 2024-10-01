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
                filters.AddSpanStatusCodeFilter(code, EnumCompareAsType.Equals);
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
                filters.AddSpanKindFilter(kind, EnumCompareAsType.Equals);
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
        
        [Fact]
        public void AddSpanNamePropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            const string name = "service1";
            builder.Where(filters =>
            {
                filters.AddSpanNameFilter(name, StringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(WhereSpanFilter.FilterOneofCase.SpanProperty, request.Filters.First().FilterCase);
            Assert.Equal(WhereSpanPropertyFilter.PropertyOneofCase.Name, request.Filters.First().SpanProperty.PropertyCase);
            Assert.Equal(StringCompareAsType.Equals, request.Filters.First().SpanProperty.Name.CompareAs);
            Assert.Equal(name, request.Filters.First().SpanProperty.Name.Compare);
        }
        
        [Fact]
        public void AddSpanIdPropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            byte[] spanId = [1, 2, 3, 4, 5, 6, 7, 8];
            builder.Where(filters =>
            {
                filters.AddSpanIdFilter(spanId, ByteStringCompareAsType.Equals);
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
        public void AddTraceIdPropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            byte[] traceId = [1, 2, 3, 4, 5, 6, 7, 8, 1, 2, 3, 4, 5, 6, 7, 8];
            builder.Where(filters =>
            {
                filters.AddTraceIdFilter(traceId, ByteStringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.FilterOneofCase.SpanProperty, filterToFind.FilterCase);
            Assert.Equal(WhereSpanPropertyFilter.PropertyOneofCase.TraceId, filterToFind.SpanProperty.PropertyCase);
            Assert.Equal(ByteStringCompareAsType.Equals, filterToFind.SpanProperty.TraceId.CompareAs);
            Assert.Equal(traceId, filterToFind.SpanProperty.TraceId.Compare);
        }
        
        [Fact]
        public void AddParentSpanIdPropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            byte[] spanId = [1, 2, 3, 4, 5, 6, 7, 8];
            builder.Where(filters =>
            {
                filters.AddParentSpanIdFilter(spanId, ByteStringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.FilterOneofCase.SpanProperty, filterToFind.FilterCase);
            Assert.Equal(WhereSpanPropertyFilter.PropertyOneofCase.ParentSpanId, filterToFind.SpanProperty.PropertyCase);
            Assert.Equal(ByteStringCompareAsType.Equals, filterToFind.SpanProperty.ParentSpanId.CompareAs);
            Assert.Equal(spanId, filterToFind.SpanProperty.ParentSpanId.Compare);
        }
        
        [Fact]
        public void AddStartTimeUnixNanoPropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            ulong startTimeUnixNano = 1000000000;
            builder.Where(filters =>
            {
                filters.AddStartTimeUnixNanoFilter(startTimeUnixNano, UInt64CompareAsType.Equals);
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
        public void AddEndTimeUnixNanoPropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            ulong endTimeUnixNano = 1000000000;
            builder.Where(filters =>
            {
                filters.AddEndTimeUnixNanoFilter(endTimeUnixNano, UInt64CompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.FilterOneofCase.SpanProperty, filterToFind.FilterCase);
            Assert.Equal(WhereSpanPropertyFilter.PropertyOneofCase.EndTimeUnixNano, filterToFind.SpanProperty.PropertyCase);
            Assert.Equal(UInt64CompareAsType.Equals, filterToFind.SpanProperty.EndTimeUnixNano.CompareAs);
            Assert.Equal(endTimeUnixNano, filterToFind.SpanProperty.EndTimeUnixNano.Compare);
        }
        
        [Fact]
        public void AddStringAttributePropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            string key = "service.name";
            string value = "test";
            
            builder.Where(filters =>
            {
                filters.AddAttributeFilter(key, value, StringCompareAsType.Equals);
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
        public void AddBoolAttributePropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            string key = "service.started";
            bool value = true;
            
            builder.Where(filters =>
            {
                filters.AddAttributeFilter(key, value, BoolCompareAsType.Equals);
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
        public void AddInt64AttributePropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            string key = "service.started";
            long value = 1;
            
            builder.Where(filters =>
            {
                filters.AddAttributeFilter(key, value, Int64CompareAsType.Equals);
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
        public void AddDoubleAttributePropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            string key = "service.started";
            double value = 1;
            
            builder.Where(filters =>
            {
                filters.AddAttributeFilter(key, value, DoubleCompareAsType.Equals);
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
        public void AddByteStringAttributePropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            string key = "service.name";
            byte[] value = [1, 2, 3, 4];
            
            builder.Where(filters =>
            {
                filters.AddAttributeFilter(key, value, ByteStringCompareAsType.Equals);
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
        public void AddEventTimeUnixNanoPropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            ulong eventTimeUnixNano = 1000000000;
            builder.Where(filters =>
            {
                filters.AddEventTimeUnixNanoFilter(eventTimeUnixNano, UInt64CompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.FilterOneofCase.SpanProperty, filterToFind.FilterCase);
            Assert.Equal(WhereSpanPropertyFilter.PropertyOneofCase.EventTimeUnixNano, filterToFind.SpanProperty.PropertyCase);
            Assert.Equal(UInt64CompareAsType.Equals, filterToFind.SpanProperty.EventTimeUnixNano.CompareAs);
            Assert.Equal(eventTimeUnixNano, filterToFind.SpanProperty.EventTimeUnixNano.Compare);
        }
        
        [Fact]
        public void AddEventNamePropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            const string name = "service1";
            builder.Where(filters =>
            {
                filters.AddEventNameFilter(name, StringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.FilterOneofCase.SpanProperty, filterToFind.FilterCase);
            Assert.Equal(WhereSpanPropertyFilter.PropertyOneofCase.EventName, filterToFind.SpanProperty.PropertyCase);
            Assert.Equal(StringCompareAsType.Equals, filterToFind.SpanProperty.EventName.CompareAs);
            Assert.Equal(name, filterToFind.SpanProperty.EventName.Compare);
        }
        
        [Fact]
        public void AddLinkTraceStatePropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            const string name = "service1";
            builder.Where(filters =>
            {
                filters.AddLinkTraceStateFilter(name, StringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.FilterOneofCase.SpanProperty, filterToFind.FilterCase);
            Assert.Equal(WhereSpanPropertyFilter.PropertyOneofCase.LinkTraceState, filterToFind.SpanProperty.PropertyCase);
            Assert.Equal(StringCompareAsType.Equals, filterToFind.SpanProperty.LinkTraceState.CompareAs);
            Assert.Equal(name, filterToFind.SpanProperty.LinkTraceState.Compare);
        }
        
        [Fact]
        public void AddTraceStatePropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            const string name = "service1";
            builder.Where(filters =>
            {
                filters.AddTraceStateFilter(name, StringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.FilterOneofCase.SpanProperty, filterToFind.FilterCase);
            Assert.Equal(WhereSpanPropertyFilter.PropertyOneofCase.TraceState, filterToFind.SpanProperty.PropertyCase);
            Assert.Equal(StringCompareAsType.Equals, filterToFind.SpanProperty.TraceState.CompareAs);
            Assert.Equal(name, filterToFind.SpanProperty.TraceState.Compare);
        }
        
        [Fact]
        public void AddInstrumentationScopeNamePropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            const string name = "service1";
            builder.Where(filters =>
            {
                filters.AddInstrumentationScopeNameFilter(name, StringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.FilterOneofCase.SpanProperty, filterToFind.FilterCase);
            Assert.Equal(WhereSpanPropertyFilter.PropertyOneofCase.InstrumentationScopeName, filterToFind.SpanProperty.PropertyCase);
            Assert.Equal(StringCompareAsType.Equals, filterToFind.SpanProperty.InstrumentationScopeName.CompareAs);
            Assert.Equal(name, filterToFind.SpanProperty.InstrumentationScopeName.Compare);
        }
        
        [Fact]
        public void AddInstrumentationScopeVersionPropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            const string name = "service1";
            builder.Where(filters =>
            {
                filters.AddInstrumentationScopeVersionFilter(name, StringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.FilterOneofCase.SpanProperty, filterToFind.FilterCase);
            Assert.Equal(WhereSpanPropertyFilter.PropertyOneofCase.InstrumentationScopeVersion, filterToFind.SpanProperty.PropertyCase);
            Assert.Equal(StringCompareAsType.Equals, filterToFind.SpanProperty.InstrumentationScopeVersion.CompareAs);
            Assert.Equal(name, filterToFind.SpanProperty.InstrumentationScopeVersion.Compare);
        }
        
        [Fact]
        public void AddInstrumentationScopeSchemaUrlPropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            const string name = "service1";
            builder.Where(filters =>
            {
                filters.AddInstrumentationScopeSchemaUrlFilter(name, StringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.FilterOneofCase.SpanProperty, filterToFind.FilterCase);
            Assert.Equal(WhereSpanPropertyFilter.PropertyOneofCase.InstrumentationScopeSchemaUrl, filterToFind.SpanProperty.PropertyCase);
            Assert.Equal(StringCompareAsType.Equals, filterToFind.SpanProperty.InstrumentationScopeSchemaUrl.CompareAs);
            Assert.Equal(name, filterToFind.SpanProperty.InstrumentationScopeSchemaUrl.Compare);
        }
        
        [Fact]
        public void AddResourceSchemaUrlPropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            const string name = "service1";
            builder.Where(filters =>
            {
                filters.AddResourceSchemaUrlFilter(name, StringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.FilterOneofCase.SpanProperty, filterToFind.FilterCase);
            Assert.Equal(WhereSpanPropertyFilter.PropertyOneofCase.ResourceSchemaUrl, filterToFind.SpanProperty.PropertyCase);
            Assert.Equal(StringCompareAsType.Equals, filterToFind.SpanProperty.ResourceSchemaUrl.CompareAs);
            Assert.Equal(name, filterToFind.SpanProperty.ResourceSchemaUrl.Compare);
        }
        
        [Fact]
        public void AddLinkSpanIdPropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            byte[] spanId = [1, 2, 3, 4, 5, 6, 7, 8];
            builder.Where(filters =>
            {
                filters.AddLinkSpanIdFilter(spanId, ByteStringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.FilterOneofCase.SpanProperty, filterToFind.FilterCase);
            Assert.Equal(WhereSpanPropertyFilter.PropertyOneofCase.LinkSpanId, filterToFind.SpanProperty.PropertyCase);
            Assert.Equal(ByteStringCompareAsType.Equals, filterToFind.SpanProperty.LinkSpanId.CompareAs);
            Assert.Equal(spanId, filterToFind.SpanProperty.LinkSpanId.Compare);
        }
        
        [Fact]
        public void AddLinkTraceIdPropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            byte[] traceId = [1, 2, 3, 4, 5, 6, 7, 8, 1, 2, 3, 4, 5, 6, 7, 8];
            builder.Where(filters =>
            {
                filters.AddLinkTraceIdFilter(traceId, ByteStringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.FilterOneofCase.SpanProperty, filterToFind.FilterCase);
            Assert.Equal(WhereSpanPropertyFilter.PropertyOneofCase.LinkTraceId, filterToFind.SpanProperty.PropertyCase);
            Assert.Equal(ByteStringCompareAsType.Equals, filterToFind.SpanProperty.LinkTraceId.CompareAs);
            Assert.Equal(traceId, filterToFind.SpanProperty.LinkTraceId.Compare);
        }
        
        [Fact]
        public void AddLinkFlagsPropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            uint flags = 1;
            builder.Where(filters =>
            {
                filters.AddLinkFlagsFilter(flags, UInt32CompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.FilterOneofCase.SpanProperty, filterToFind.FilterCase);
            Assert.Equal(WhereSpanPropertyFilter.PropertyOneofCase.LinkFlags, filterToFind.SpanProperty.PropertyCase);
            Assert.Equal(UInt32CompareAsType.Equals, filterToFind.SpanProperty.LinkFlags.CompareAs);
            Assert.Equal(flags, filterToFind.SpanProperty.LinkFlags.Compare);
        }
        
        [Fact]
        public void AddFlagsPropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            uint flags = 1;
            builder.Where(filters =>
            {
                filters.AddFlagsFilter(flags, UInt32CompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.FilterOneofCase.SpanProperty, filterToFind.FilterCase);
            Assert.Equal(WhereSpanPropertyFilter.PropertyOneofCase.Flags, filterToFind.SpanProperty.PropertyCase);
            Assert.Equal(UInt32CompareAsType.Equals, filterToFind.SpanProperty.Flags.CompareAs);
            Assert.Equal(flags, filterToFind.SpanProperty.Flags.Compare);
        }
        
        // Event attributes
        [Fact]
        public void AddStringEventAttributePropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            string key = "service.name";
            string value = "test";
            
            builder.Where(filters =>
            {
                filters.AddEventAttributeFilter(key, value, StringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.FilterOneofCase.SpanProperty, filterToFind.FilterCase);
            Assert.Equal(WhereSpanPropertyFilter.PropertyOneofCase.EventAttribute, filterToFind.SpanProperty.PropertyCase);
            Assert.Equal(StringCompareAsType.Equals, filterToFind.SpanProperty.EventAttribute.StringValue.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.EventAttribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.EventAttribute.StringValue.Compare);
        }
        
        [Fact]
        public void AddBoolEventAttributePropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            string key = "service.started";
            bool value = true;
            
            builder.Where(filters =>
            {
                filters.AddEventAttributeFilter(key, value, BoolCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.FilterOneofCase.SpanProperty, filterToFind.FilterCase);
            Assert.Equal(WhereSpanPropertyFilter.PropertyOneofCase.EventAttribute, filterToFind.SpanProperty.PropertyCase);
            Assert.Equal(BoolCompareAsType.Equals, filterToFind.SpanProperty.EventAttribute.BoolValue.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.EventAttribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.EventAttribute.BoolValue.Compare);
        }
        
        [Fact]
        public void AddInt64EventAttributePropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            string key = "service.started";
            long value = 1;
            
            builder.Where(filters =>
            {
                filters.AddEventAttributeFilter(key, value, Int64CompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.FilterOneofCase.SpanProperty, filterToFind.FilterCase);
            Assert.Equal(WhereSpanPropertyFilter.PropertyOneofCase.EventAttribute, filterToFind.SpanProperty.PropertyCase);
            Assert.Equal(Int64CompareAsType.Equals, filterToFind.SpanProperty.EventAttribute.Int64Value.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.EventAttribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.EventAttribute.Int64Value.Compare);
        }
        
        [Fact]
        public void AddDoubleEventAttributePropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            string key = "service.started";
            double value = 1;
            
            builder.Where(filters =>
            {
                filters.AddEventAttributeFilter(key, value, DoubleCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.FilterOneofCase.SpanProperty, filterToFind.FilterCase);
            Assert.Equal(WhereSpanPropertyFilter.PropertyOneofCase.EventAttribute, filterToFind.SpanProperty.PropertyCase);
            Assert.Equal(DoubleCompareAsType.Equals, filterToFind.SpanProperty.EventAttribute.DoubleValue.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.EventAttribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.EventAttribute.DoubleValue.Compare);
        }
        
        [Fact]
        public void AddByteStringEventAttributePropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            string key = "service.name";
            byte[] value = [1, 2, 3, 4];
            
            builder.Where(filters =>
            {
                filters.AddEventAttributeFilter(key, value, ByteStringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.FilterOneofCase.SpanProperty, filterToFind.FilterCase);
            Assert.Equal(WhereSpanPropertyFilter.PropertyOneofCase.EventAttribute, filterToFind.SpanProperty.PropertyCase);
            Assert.Equal(ByteStringCompareAsType.Equals, filterToFind.SpanProperty.EventAttribute.ByteStringValue.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.EventAttribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.EventAttribute.ByteStringValue.Compare);
        }
        
        // Link Attributes
        [Fact]
        public void AddStringLinkAttributePropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            string key = "service.name";
            string value = "test";
            
            builder.Where(filters =>
            {
                filters.AddLinkAttributeFilter(key, value, StringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.FilterOneofCase.SpanProperty, filterToFind.FilterCase);
            Assert.Equal(WhereSpanPropertyFilter.PropertyOneofCase.LinkAttribute, filterToFind.SpanProperty.PropertyCase);
            Assert.Equal(StringCompareAsType.Equals, filterToFind.SpanProperty.LinkAttribute.StringValue.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.LinkAttribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.LinkAttribute.StringValue.Compare);
        }
        
        [Fact]
        public void AddBoolLinkAttributePropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            string key = "service.started";
            bool value = true;
            
            builder.Where(filters =>
            {
                filters.AddLinkAttributeFilter(key, value, BoolCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.FilterOneofCase.SpanProperty, filterToFind.FilterCase);
            Assert.Equal(WhereSpanPropertyFilter.PropertyOneofCase.LinkAttribute, filterToFind.SpanProperty.PropertyCase);
            Assert.Equal(BoolCompareAsType.Equals, filterToFind.SpanProperty.LinkAttribute.BoolValue.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.LinkAttribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.LinkAttribute.BoolValue.Compare);
        }
        
        [Fact]
        public void AddInt64LinkAttributePropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            string key = "service.started";
            long value = 1;
            
            builder.Where(filters =>
            {
                filters.AddLinkAttributeFilter(key, value, Int64CompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.FilterOneofCase.SpanProperty, filterToFind.FilterCase);
            Assert.Equal(WhereSpanPropertyFilter.PropertyOneofCase.LinkAttribute, filterToFind.SpanProperty.PropertyCase);
            Assert.Equal(Int64CompareAsType.Equals, filterToFind.SpanProperty.LinkAttribute.Int64Value.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.LinkAttribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.LinkAttribute.Int64Value.Compare);
        }
        
        [Fact]
        public void AddDoubleLinkAttributePropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            string key = "service.started";
            double value = 1;
            
            builder.Where(filters =>
            {
                filters.AddLinkAttributeFilter(key, value, DoubleCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.FilterOneofCase.SpanProperty, filterToFind.FilterCase);
            Assert.Equal(WhereSpanPropertyFilter.PropertyOneofCase.LinkAttribute, filterToFind.SpanProperty.PropertyCase);
            Assert.Equal(DoubleCompareAsType.Equals, filterToFind.SpanProperty.LinkAttribute.DoubleValue.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.LinkAttribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.LinkAttribute.DoubleValue.Compare);
        }
        
        [Fact]
        public void AddByteStringLinkAttributePropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            string key = "service.name";
            byte[] value = [1, 2, 3, 4];
            
            builder.Where(filters =>
            {
                filters.AddLinkAttributeFilter(key, value, ByteStringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.FilterOneofCase.SpanProperty, filterToFind.FilterCase);
            Assert.Equal(WhereSpanPropertyFilter.PropertyOneofCase.LinkAttribute, filterToFind.SpanProperty.PropertyCase);
            Assert.Equal(ByteStringCompareAsType.Equals, filterToFind.SpanProperty.LinkAttribute.ByteStringValue.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.LinkAttribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.LinkAttribute.ByteStringValue.Compare);
        }
        
        // Scope attributes
        [Fact]
        public void AddStringInstrumentationScopeAttributePropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            string key = "service.name";
            string value = "test";
            
            builder.Where(filters =>
            {
                filters.AddInstrumentationScopeAttributeFilter(key, value, StringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.FilterOneofCase.SpanProperty, filterToFind.FilterCase);
            Assert.Equal(WhereSpanPropertyFilter.PropertyOneofCase.InstrumentationScopeAttribute, filterToFind.SpanProperty.PropertyCase);
            Assert.Equal(StringCompareAsType.Equals, filterToFind.SpanProperty.InstrumentationScopeAttribute.StringValue.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.InstrumentationScopeAttribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.InstrumentationScopeAttribute.StringValue.Compare);
        }
        
        [Fact]
        public void AddBoolInstrumentationScopeAttributePropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            string key = "service.started";
            bool value = true;
            
            builder.Where(filters =>
            {
                filters.AddInstrumentationScopeAttributeFilter(key, value, BoolCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.FilterOneofCase.SpanProperty, filterToFind.FilterCase);
            Assert.Equal(WhereSpanPropertyFilter.PropertyOneofCase.InstrumentationScopeAttribute, filterToFind.SpanProperty.PropertyCase);
            Assert.Equal(BoolCompareAsType.Equals, filterToFind.SpanProperty.InstrumentationScopeAttribute.BoolValue.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.InstrumentationScopeAttribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.InstrumentationScopeAttribute.BoolValue.Compare);
        }
        
        [Fact]
        public void AddInt64InstrumentationScopeAttributePropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            string key = "service.started";
            long value = 1;
            
            builder.Where(filters =>
            {
                filters.AddInstrumentationScopeAttributeFilter(key, value, Int64CompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.FilterOneofCase.SpanProperty, filterToFind.FilterCase);
            Assert.Equal(WhereSpanPropertyFilter.PropertyOneofCase.InstrumentationScopeAttribute, filterToFind.SpanProperty.PropertyCase);
            Assert.Equal(Int64CompareAsType.Equals, filterToFind.SpanProperty.InstrumentationScopeAttribute.Int64Value.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.InstrumentationScopeAttribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.InstrumentationScopeAttribute.Int64Value.Compare);
        }
        
        [Fact]
        public void AddDoubleInstrumentationScopeAttributePropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            string key = "service.started";
            double value = 1;
            
            builder.Where(filters =>
            {
                filters.AddInstrumentationScopeAttributeFilter(key, value, DoubleCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.FilterOneofCase.SpanProperty, filterToFind.FilterCase);
            Assert.Equal(WhereSpanPropertyFilter.PropertyOneofCase.InstrumentationScopeAttribute, filterToFind.SpanProperty.PropertyCase);
            Assert.Equal(DoubleCompareAsType.Equals, filterToFind.SpanProperty.InstrumentationScopeAttribute.DoubleValue.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.InstrumentationScopeAttribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.InstrumentationScopeAttribute.DoubleValue.Compare);
        }
        
        [Fact]
        public void AddByteStringInstrumentationScopeAttributePropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            string key = "service.name";
            byte[] value = [1, 2, 3, 4];
            
            builder.Where(filters =>
            {
                filters.AddInstrumentationScopeAttributeFilter(key, value, ByteStringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.FilterOneofCase.SpanProperty, filterToFind.FilterCase);
            Assert.Equal(WhereSpanPropertyFilter.PropertyOneofCase.InstrumentationScopeAttribute, filterToFind.SpanProperty.PropertyCase);
            Assert.Equal(ByteStringCompareAsType.Equals, filterToFind.SpanProperty.InstrumentationScopeAttribute.ByteStringValue.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.InstrumentationScopeAttribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.InstrumentationScopeAttribute.ByteStringValue.Compare);
        }
        
        // Resource attributes
        [Fact]
        public void AddStringResourceAttributePropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            string key = "service.name";
            string value = "test";
            
            builder.Where(filters =>
            {
                filters.AddResourceAttributeFilter(key, value, StringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.FilterOneofCase.SpanProperty, filterToFind.FilterCase);
            Assert.Equal(WhereSpanPropertyFilter.PropertyOneofCase.ResourceAttribute, filterToFind.SpanProperty.PropertyCase);
            Assert.Equal(StringCompareAsType.Equals, filterToFind.SpanProperty.ResourceAttribute.StringValue.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.ResourceAttribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.ResourceAttribute.StringValue.Compare);
        }
        
        [Fact]
        public void AddBoolResourceAttributePropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            string key = "service.started";
            bool value = true;
            
            builder.Where(filters =>
            {
                filters.AddResourceAttributeFilter(key, value, BoolCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.FilterOneofCase.SpanProperty, filterToFind.FilterCase);
            Assert.Equal(WhereSpanPropertyFilter.PropertyOneofCase.ResourceAttribute, filterToFind.SpanProperty.PropertyCase);
            Assert.Equal(BoolCompareAsType.Equals, filterToFind.SpanProperty.ResourceAttribute.BoolValue.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.ResourceAttribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.ResourceAttribute.BoolValue.Compare);
        }
        
        [Fact]
        public void AddInt64ResourceAttributePropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            string key = "service.started";
            long value = 1;
            
            builder.Where(filters =>
            {
                filters.AddResourceAttributeFilter(key, value, Int64CompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.FilterOneofCase.SpanProperty, filterToFind.FilterCase);
            Assert.Equal(WhereSpanPropertyFilter.PropertyOneofCase.ResourceAttribute, filterToFind.SpanProperty.PropertyCase);
            Assert.Equal(Int64CompareAsType.Equals, filterToFind.SpanProperty.ResourceAttribute.Int64Value.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.ResourceAttribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.ResourceAttribute.Int64Value.Compare);
        }
        
        [Fact]
        public void AddDoubleResourceAttributePropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            string key = "service.started";
            double value = 1;
            
            builder.Where(filters =>
            {
                filters.AddResourceAttributeFilter(key, value, DoubleCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.FilterOneofCase.SpanProperty, filterToFind.FilterCase);
            Assert.Equal(WhereSpanPropertyFilter.PropertyOneofCase.ResourceAttribute, filterToFind.SpanProperty.PropertyCase);
            Assert.Equal(DoubleCompareAsType.Equals, filterToFind.SpanProperty.ResourceAttribute.DoubleValue.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.ResourceAttribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.ResourceAttribute.DoubleValue.Compare);
        }
        
        [Fact]
        public void AddByteStringResourceAttributePropertyFilter()
        {
            var builder = SpanQueryRequestBuilder.Create();
            string key = "service.name";
            byte[] value = [1, 2, 3, 4];
            
            builder.Where(filters =>
            {
                filters.AddResourceAttributeFilter(key, value, ByteStringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.FilterOneofCase.SpanProperty, filterToFind.FilterCase);
            Assert.Equal(WhereSpanPropertyFilter.PropertyOneofCase.ResourceAttribute, filterToFind.SpanProperty.PropertyCase);
            Assert.Equal(ByteStringCompareAsType.Equals, filterToFind.SpanProperty.ResourceAttribute.ByteStringValue.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.ResourceAttribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.ResourceAttribute.ByteStringValue.Compare);
        }
    }
}