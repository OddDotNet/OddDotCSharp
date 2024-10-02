using OddDotNet.Proto.Spans.V1;

namespace OddDotCSharp.Tests;

public class SpanQueryRequestBuilderTests
{
    public class BuildShould
    {
        [Fact]
        public void SetTakeToTakeFirstWhenNotSpecified()
        {
            var request = new SpanQueryRequestBuilder().Build();
            
            Assert.Equal(Take.ValueOneofCase.TakeFirst, request.Take.ValueCase);
        }

        [Fact]
        public void SetDurationToDefaultWhenNotSpecified()
        {
            int expectedMsValue = 30000;
            var request = new SpanQueryRequestBuilder().Build();
            
            Assert.Equal(expectedMsValue, request.Duration.Milliseconds);
        }
    }

    public class TakeFirstShould
    {
        [Fact]
        public void SetTakeToTakeFirst()
        {
            var builder = new SpanQueryRequestBuilder();
            builder.TakeFirst();
            var request = builder.Build();
            
            Assert.Equal(Take.ValueOneofCase.TakeFirst, request.Take.ValueCase);
        }
    }

    public class TakeExactShould
    {
        [Fact]
        public void SetTakeToTakeExact()
        {
            var builder = new SpanQueryRequestBuilder();
            int number = 3;
            builder.TakeExact(number);
            var request = builder.Build();
            
            Assert.Equal(Take.ValueOneofCase.TakeExact, request.Take.ValueCase);
            Assert.Equal(number, request.Take.TakeExact.Count);
        }
    }

    public class TakeAllShould
    {
        [Fact]
        public void SetTakeToTakeAll()
        {
            var builder = new SpanQueryRequestBuilder();
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
            var builder = new SpanQueryRequestBuilder();
            TimeSpan duration = TimeSpan.FromMilliseconds(500);
            builder.Wait(duration);
            var request = builder.Build();
            
            Assert.Equal(duration.TotalMilliseconds, request.Duration.Milliseconds);
        }
    }

    public class WhereShould
    {
        [Fact]
        public void ThrowOnInvalidProperty()
        {
            var builder = new SpanQueryRequestBuilder();
            const string name = "service1";

            Assert.Throws<Exception>(() =>
            {
                builder.Where(filters =>
                {
                    filters.AddFilter(WhereSpanPropertyFilter.ValueOneofCase.None, name, StringCompareAsType.Equals);
                });
            });
        }

        [Fact]
        public void ThrowOnMismatchPropertyType()
        {
            var builder = new SpanQueryRequestBuilder();
            const string name = "service1";

            Assert.Throws<Exception>(() =>
            {
                builder.Where(filters =>
                {
                    // TraceId is a ByteString, not a String
                    filters.AddFilter(WhereSpanPropertyFilter.ValueOneofCase.TraceId, name, StringCompareAsType.Equals);
                });
            });
        }
        
        [Fact]
        public void AddStringPropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            const string name = "service1";
            builder.Where(filters =>
            {
                filters.AddFilter(WhereSpanPropertyFilter.ValueOneofCase.Name, name, StringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, request.Filters.First().ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.Name, request.Filters.First().SpanProperty.ValueCase);
            Assert.Equal(StringCompareAsType.Equals, request.Filters.First().SpanProperty.Name.CompareAs);
            Assert.Equal(name, request.Filters.First().SpanProperty.Name.Compare);
        }
        
        [Fact]
        public void AddByteStringPropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            byte[] spanId = [1, 2, 3, 4, 5, 6, 7, 8];
            builder.Where(filters =>
            {
                filters.AddFilter(WhereSpanPropertyFilter.ValueOneofCase.SpanId, spanId, ByteStringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.SpanId, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(ByteStringCompareAsType.Equals, filterToFind.SpanProperty.SpanId.CompareAs);
            Assert.Equal(spanId, filterToFind.SpanProperty.SpanId.Compare);
        }
        
        [Fact]
        public void AddUInt64PropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            ulong startTimeUnixNano = 1000000000;
            builder.Where(filters =>
            {
                filters.AddFilter(WhereSpanPropertyFilter.ValueOneofCase.StartTimeUnixNano, startTimeUnixNano, NumberCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.StartTimeUnixNano, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, filterToFind.SpanProperty.StartTimeUnixNano.CompareAs);
            Assert.Equal(startTimeUnixNano, filterToFind.SpanProperty.StartTimeUnixNano.Compare);
        }
        
        [Fact]
        public void AddSpanStatusCodePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            SpanStatusCode code = SpanStatusCode.Ok;
            builder.Where(filters =>
            {
                filters.AddSpanStatusCodeFilter(code, EnumCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.StatusCode, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(EnumCompareAsType.Equals, filterToFind.SpanProperty.StatusCode.CompareAs);
            Assert.Equal(code, filterToFind.SpanProperty.StatusCode.Compare);
        }
        
        [Fact]
        public void AddSpanKindPropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            SpanKind kind = SpanKind.Internal;
            builder.Where(filters =>
            {
                filters.AddSpanKindFilter(kind, EnumCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.Kind, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(EnumCompareAsType.Equals, filterToFind.SpanProperty.Kind.CompareAs);
            Assert.Equal(kind, filterToFind.SpanProperty.Kind.Compare);
        }
        
        [Fact]
        public void AddUInt32PropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            uint flags = 1000000000;
            builder.Where(filters =>
            {
                filters.AddFilter(WhereSpanPropertyFilter.ValueOneofCase.Flags, flags, NumberCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.Flags, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, filterToFind.SpanProperty.Flags.CompareAs);
            Assert.Equal(flags, filterToFind.SpanProperty.Flags.Compare);
        }
        
        [Fact]
        public void AddKeyValueStringPropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            string key = "service.name";
            string value = "test";
            
            builder.Where(filters =>
            {
                filters.AddFilter(WhereSpanPropertyFilter.ValueOneofCase.Attribute, key, value, StringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.Attribute, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(StringCompareAsType.Equals, filterToFind.SpanProperty.Attribute.StringValue.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.Attribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.Attribute.StringValue.Compare);
        }
        
        [Fact]
        public void AddKeyValueBoolPropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            string key = "service.started";
            bool value = true;
            
            builder.Where(filters =>
            {
                filters.AddFilter(WhereSpanPropertyFilter.ValueOneofCase.Attribute, key, value, BoolCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.Attribute, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(BoolCompareAsType.Equals, filterToFind.SpanProperty.Attribute.BoolValue.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.Attribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.Attribute.BoolValue.Compare);
        }
        
        [Fact]
        public void AddKeyValueInt64PropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            string key = "service.started";
            long value = 1;
            
            builder.Where(filters =>
            {
                filters.AddFilter(WhereSpanPropertyFilter.ValueOneofCase.Attribute, key, value, NumberCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.Attribute, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, filterToFind.SpanProperty.Attribute.Int64Value.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.Attribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.Attribute.Int64Value.Compare);
        }
        
        [Fact]
        public void AddKeyValueDoublePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            string key = "service.started";
            double value = 1;
            
            builder.Where(filters =>
            {
                filters.AddFilter(WhereSpanPropertyFilter.ValueOneofCase.Attribute, key, value, NumberCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.Attribute, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, filterToFind.SpanProperty.Attribute.DoubleValue.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.Attribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.Attribute.DoubleValue.Compare);
        }
        
        [Fact]
        public void AddKeyValueByteStringPropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            string key = "trace.parentSpanId";
            byte[] value = [1, 2, 3, 4, 5, 6, 7, 8];
            
            builder.Where(filters =>
            {
                filters.AddFilter(WhereSpanPropertyFilter.ValueOneofCase.Attribute, key, value, ByteStringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.Attribute, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(ByteStringCompareAsType.Equals, filterToFind.SpanProperty.Attribute.ByteStringValue.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.Attribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.Attribute.ByteStringValue.Compare);
        }

        [Fact]
        public void AddOrFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            builder.Where(filters =>
            {
                filters.AddOrFilter(orFilters =>
                {
                    orFilters.AddFilter(WhereSpanPropertyFilter.ValueOneofCase.Name, "GET", StringCompareAsType.Equals);
                    orFilters.AddFilter(WhereSpanPropertyFilter.ValueOneofCase.Name, "POST", StringCompareAsType.Equals);
                });
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanOr, filterToFind.ValueCase);

            var firstOr = filterToFind.SpanOr.Filters[0];
            
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.Name, firstOr.SpanProperty.ValueCase);
            Assert.Equal(StringCompareAsType.Equals, firstOr.SpanProperty.Name.CompareAs);
            Assert.Equal("GET", firstOr.SpanProperty.Name.Compare);
            
            var secondOr = filterToFind.SpanOr.Filters[1];
            
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.Name, secondOr.SpanProperty.ValueCase);
            Assert.Equal(StringCompareAsType.Equals, secondOr.SpanProperty.Name.CompareAs);
            Assert.Equal("POST", secondOr.SpanProperty.Name.Compare);
        }
        
        [Fact]
        public void AddSpanNamePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            const string name = "service1";
            builder.Where(filters =>
            {
                filters.AddSpanNameFilter(name, StringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, request.Filters.First().ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.Name, request.Filters.First().SpanProperty.ValueCase);
            Assert.Equal(StringCompareAsType.Equals, request.Filters.First().SpanProperty.Name.CompareAs);
            Assert.Equal(name, request.Filters.First().SpanProperty.Name.Compare);
        }
        
        [Fact]
        public void AddSpanIdPropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            byte[] spanId = [1, 2, 3, 4, 5, 6, 7, 8];
            builder.Where(filters =>
            {
                filters.AddSpanIdFilter(spanId, ByteStringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.SpanId, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(ByteStringCompareAsType.Equals, filterToFind.SpanProperty.SpanId.CompareAs);
            Assert.Equal(spanId, filterToFind.SpanProperty.SpanId.Compare);
        }
        
        [Fact]
        public void AddTraceIdPropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            byte[] traceId = [1, 2, 3, 4, 5, 6, 7, 8, 1, 2, 3, 4, 5, 6, 7, 8];
            builder.Where(filters =>
            {
                filters.AddTraceIdFilter(traceId, ByteStringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.TraceId, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(ByteStringCompareAsType.Equals, filterToFind.SpanProperty.TraceId.CompareAs);
            Assert.Equal(traceId, filterToFind.SpanProperty.TraceId.Compare);
        }
        
        [Fact]
        public void AddParentSpanIdPropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            byte[] spanId = [1, 2, 3, 4, 5, 6, 7, 8];
            builder.Where(filters =>
            {
                filters.AddParentSpanIdFilter(spanId, ByteStringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.ParentSpanId, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(ByteStringCompareAsType.Equals, filterToFind.SpanProperty.ParentSpanId.CompareAs);
            Assert.Equal(spanId, filterToFind.SpanProperty.ParentSpanId.Compare);
        }
        
        [Fact]
        public void AddStartTimeUnixNanoPropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            ulong startTimeUnixNano = 1000000000;
            builder.Where(filters =>
            {
                filters.AddStartTimeUnixNanoFilter(startTimeUnixNano, NumberCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.StartTimeUnixNano, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, filterToFind.SpanProperty.StartTimeUnixNano.CompareAs);
            Assert.Equal(startTimeUnixNano, filterToFind.SpanProperty.StartTimeUnixNano.Compare);
        }
        
        [Fact]
        public void AddEndTimeUnixNanoPropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            ulong endTimeUnixNano = 1000000000;
            builder.Where(filters =>
            {
                filters.AddEndTimeUnixNanoFilter(endTimeUnixNano, NumberCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.EndTimeUnixNano, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, filterToFind.SpanProperty.EndTimeUnixNano.CompareAs);
            Assert.Equal(endTimeUnixNano, filterToFind.SpanProperty.EndTimeUnixNano.Compare);
        }
        
        [Fact]
        public void AddStringAttributePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            string key = "service.name";
            string value = "test";
            
            builder.Where(filters =>
            {
                filters.AddAttributeFilter(key, value, StringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.Attribute, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(StringCompareAsType.Equals, filterToFind.SpanProperty.Attribute.StringValue.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.Attribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.Attribute.StringValue.Compare);
        }
        
        [Fact]
        public void AddBoolAttributePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            string key = "service.started";
            bool value = true;
            
            builder.Where(filters =>
            {
                filters.AddAttributeFilter(key, value, BoolCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.Attribute, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(BoolCompareAsType.Equals, filterToFind.SpanProperty.Attribute.BoolValue.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.Attribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.Attribute.BoolValue.Compare);
        }
        
        [Fact]
        public void AddInt64AttributePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            string key = "service.started";
            long value = 1;
            
            builder.Where(filters =>
            {
                filters.AddAttributeFilter(key, value, NumberCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.Attribute, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, filterToFind.SpanProperty.Attribute.Int64Value.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.Attribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.Attribute.Int64Value.Compare);
        }
        
        [Fact]
        public void AddDoubleAttributePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            string key = "service.started";
            double value = 1;
            
            builder.Where(filters =>
            {
                filters.AddAttributeFilter(key, value, NumberCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.Attribute, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, filterToFind.SpanProperty.Attribute.DoubleValue.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.Attribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.Attribute.DoubleValue.Compare);
        }
        
        [Fact]
        public void AddByteStringAttributePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            string key = "service.name";
            byte[] value = [1, 2, 3, 4];
            
            builder.Where(filters =>
            {
                filters.AddAttributeFilter(key, value, ByteStringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.Attribute, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(ByteStringCompareAsType.Equals, filterToFind.SpanProperty.Attribute.ByteStringValue.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.Attribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.Attribute.ByteStringValue.Compare);
        }
        
        [Fact]
        public void AddEventTimeUnixNanoPropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            ulong eventTimeUnixNano = 1000000000;
            builder.Where(filters =>
            {
                filters.AddEventTimeUnixNanoFilter(eventTimeUnixNano, NumberCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.EventTimeUnixNano, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, filterToFind.SpanProperty.EventTimeUnixNano.CompareAs);
            Assert.Equal(eventTimeUnixNano, filterToFind.SpanProperty.EventTimeUnixNano.Compare);
        }
        
        [Fact]
        public void AddEventNamePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            const string name = "service1";
            builder.Where(filters =>
            {
                filters.AddEventNameFilter(name, StringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.EventName, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(StringCompareAsType.Equals, filterToFind.SpanProperty.EventName.CompareAs);
            Assert.Equal(name, filterToFind.SpanProperty.EventName.Compare);
        }
        
        [Fact]
        public void AddLinkTraceStatePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            const string name = "service1";
            builder.Where(filters =>
            {
                filters.AddLinkTraceStateFilter(name, StringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.LinkTraceState, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(StringCompareAsType.Equals, filterToFind.SpanProperty.LinkTraceState.CompareAs);
            Assert.Equal(name, filterToFind.SpanProperty.LinkTraceState.Compare);
        }
        
        [Fact]
        public void AddTraceStatePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            const string name = "service1";
            builder.Where(filters =>
            {
                filters.AddTraceStateFilter(name, StringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.TraceState, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(StringCompareAsType.Equals, filterToFind.SpanProperty.TraceState.CompareAs);
            Assert.Equal(name, filterToFind.SpanProperty.TraceState.Compare);
        }
        
        [Fact]
        public void AddInstrumentationScopeNamePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            const string name = "service1";
            builder.Where(filters =>
            {
                filters.AddInstrumentationScopeNameFilter(name, StringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.InstrumentationScopeName, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(StringCompareAsType.Equals, filterToFind.SpanProperty.InstrumentationScopeName.CompareAs);
            Assert.Equal(name, filterToFind.SpanProperty.InstrumentationScopeName.Compare);
        }
        
        [Fact]
        public void AddInstrumentationScopeVersionPropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            const string name = "service1";
            builder.Where(filters =>
            {
                filters.AddInstrumentationScopeVersionFilter(name, StringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.InstrumentationScopeVersion, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(StringCompareAsType.Equals, filterToFind.SpanProperty.InstrumentationScopeVersion.CompareAs);
            Assert.Equal(name, filterToFind.SpanProperty.InstrumentationScopeVersion.Compare);
        }
        
        [Fact]
        public void AddInstrumentationScopeSchemaUrlPropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            const string name = "service1";
            builder.Where(filters =>
            {
                filters.AddInstrumentationScopeSchemaUrlFilter(name, StringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.InstrumentationScopeSchemaUrl, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(StringCompareAsType.Equals, filterToFind.SpanProperty.InstrumentationScopeSchemaUrl.CompareAs);
            Assert.Equal(name, filterToFind.SpanProperty.InstrumentationScopeSchemaUrl.Compare);
        }
        
        [Fact]
        public void AddResourceSchemaUrlPropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            const string name = "service1";
            builder.Where(filters =>
            {
                filters.AddResourceSchemaUrlFilter(name, StringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.ResourceSchemaUrl, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(StringCompareAsType.Equals, filterToFind.SpanProperty.ResourceSchemaUrl.CompareAs);
            Assert.Equal(name, filterToFind.SpanProperty.ResourceSchemaUrl.Compare);
        }
        
        [Fact]
        public void AddLinkSpanIdPropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            byte[] spanId = [1, 2, 3, 4, 5, 6, 7, 8];
            builder.Where(filters =>
            {
                filters.AddLinkSpanIdFilter(spanId, ByteStringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.LinkSpanId, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(ByteStringCompareAsType.Equals, filterToFind.SpanProperty.LinkSpanId.CompareAs);
            Assert.Equal(spanId, filterToFind.SpanProperty.LinkSpanId.Compare);
        }
        
        [Fact]
        public void AddLinkTraceIdPropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            byte[] traceId = [1, 2, 3, 4, 5, 6, 7, 8, 1, 2, 3, 4, 5, 6, 7, 8];
            builder.Where(filters =>
            {
                filters.AddLinkTraceIdFilter(traceId, ByteStringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.LinkTraceId, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(ByteStringCompareAsType.Equals, filterToFind.SpanProperty.LinkTraceId.CompareAs);
            Assert.Equal(traceId, filterToFind.SpanProperty.LinkTraceId.Compare);
        }
        
        [Fact]
        public void AddLinkFlagsPropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            uint flags = 1;
            builder.Where(filters =>
            {
                filters.AddLinkFlagsFilter(flags, NumberCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.LinkFlags, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, filterToFind.SpanProperty.LinkFlags.CompareAs);
            Assert.Equal(flags, filterToFind.SpanProperty.LinkFlags.Compare);
        }
        
        [Fact]
        public void AddFlagsPropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            uint flags = 1;
            builder.Where(filters =>
            {
                filters.AddFlagsFilter(flags, NumberCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.Flags, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, filterToFind.SpanProperty.Flags.CompareAs);
            Assert.Equal(flags, filterToFind.SpanProperty.Flags.Compare);
        }
        
        // Event attributes
        [Fact]
        public void AddStringEventAttributePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            string key = "service.name";
            string value = "test";
            
            builder.Where(filters =>
            {
                filters.AddEventAttributeFilter(key, value, StringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.EventAttribute, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(StringCompareAsType.Equals, filterToFind.SpanProperty.EventAttribute.StringValue.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.EventAttribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.EventAttribute.StringValue.Compare);
        }
        
        [Fact]
        public void AddBoolEventAttributePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            string key = "service.started";
            bool value = true;
            
            builder.Where(filters =>
            {
                filters.AddEventAttributeFilter(key, value, BoolCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.EventAttribute, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(BoolCompareAsType.Equals, filterToFind.SpanProperty.EventAttribute.BoolValue.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.EventAttribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.EventAttribute.BoolValue.Compare);
        }
        
        [Fact]
        public void AddInt64EventAttributePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            string key = "service.started";
            long value = 1;
            
            builder.Where(filters =>
            {
                filters.AddEventAttributeFilter(key, value, NumberCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.EventAttribute, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, filterToFind.SpanProperty.EventAttribute.Int64Value.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.EventAttribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.EventAttribute.Int64Value.Compare);
        }
        
        [Fact]
        public void AddDoubleEventAttributePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            string key = "service.started";
            double value = 1;
            
            builder.Where(filters =>
            {
                filters.AddEventAttributeFilter(key, value, NumberCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.EventAttribute, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, filterToFind.SpanProperty.EventAttribute.DoubleValue.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.EventAttribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.EventAttribute.DoubleValue.Compare);
        }
        
        [Fact]
        public void AddByteStringEventAttributePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            string key = "service.name";
            byte[] value = [1, 2, 3, 4];
            
            builder.Where(filters =>
            {
                filters.AddEventAttributeFilter(key, value, ByteStringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.EventAttribute, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(ByteStringCompareAsType.Equals, filterToFind.SpanProperty.EventAttribute.ByteStringValue.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.EventAttribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.EventAttribute.ByteStringValue.Compare);
        }
        
        // Link Attributes
        [Fact]
        public void AddStringLinkAttributePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            string key = "service.name";
            string value = "test";
            
            builder.Where(filters =>
            {
                filters.AddLinkAttributeFilter(key, value, StringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.LinkAttribute, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(StringCompareAsType.Equals, filterToFind.SpanProperty.LinkAttribute.StringValue.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.LinkAttribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.LinkAttribute.StringValue.Compare);
        }
        
        [Fact]
        public void AddBoolLinkAttributePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            string key = "service.started";
            bool value = true;
            
            builder.Where(filters =>
            {
                filters.AddLinkAttributeFilter(key, value, BoolCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.LinkAttribute, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(BoolCompareAsType.Equals, filterToFind.SpanProperty.LinkAttribute.BoolValue.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.LinkAttribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.LinkAttribute.BoolValue.Compare);
        }
        
        [Fact]
        public void AddInt64LinkAttributePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            string key = "service.started";
            long value = 1;
            
            builder.Where(filters =>
            {
                filters.AddLinkAttributeFilter(key, value, NumberCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.LinkAttribute, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, filterToFind.SpanProperty.LinkAttribute.Int64Value.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.LinkAttribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.LinkAttribute.Int64Value.Compare);
        }
        
        [Fact]
        public void AddDoubleLinkAttributePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            string key = "service.started";
            double value = 1;
            
            builder.Where(filters =>
            {
                filters.AddLinkAttributeFilter(key, value, NumberCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.LinkAttribute, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, filterToFind.SpanProperty.LinkAttribute.DoubleValue.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.LinkAttribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.LinkAttribute.DoubleValue.Compare);
        }
        
        [Fact]
        public void AddByteStringLinkAttributePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            string key = "service.name";
            byte[] value = [1, 2, 3, 4];
            
            builder.Where(filters =>
            {
                filters.AddLinkAttributeFilter(key, value, ByteStringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.LinkAttribute, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(ByteStringCompareAsType.Equals, filterToFind.SpanProperty.LinkAttribute.ByteStringValue.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.LinkAttribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.LinkAttribute.ByteStringValue.Compare);
        }
        
        // Scope attributes
        [Fact]
        public void AddStringInstrumentationScopeAttributePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            string key = "service.name";
            string value = "test";
            
            builder.Where(filters =>
            {
                filters.AddInstrumentationScopeAttributeFilter(key, value, StringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.InstrumentationScopeAttribute, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(StringCompareAsType.Equals, filterToFind.SpanProperty.InstrumentationScopeAttribute.StringValue.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.InstrumentationScopeAttribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.InstrumentationScopeAttribute.StringValue.Compare);
        }
        
        [Fact]
        public void AddBoolInstrumentationScopeAttributePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            string key = "service.started";
            bool value = true;
            
            builder.Where(filters =>
            {
                filters.AddInstrumentationScopeAttributeFilter(key, value, BoolCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.InstrumentationScopeAttribute, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(BoolCompareAsType.Equals, filterToFind.SpanProperty.InstrumentationScopeAttribute.BoolValue.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.InstrumentationScopeAttribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.InstrumentationScopeAttribute.BoolValue.Compare);
        }
        
        [Fact]
        public void AddInt64InstrumentationScopeAttributePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            string key = "service.started";
            long value = 1;
            
            builder.Where(filters =>
            {
                filters.AddInstrumentationScopeAttributeFilter(key, value, NumberCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.InstrumentationScopeAttribute, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, filterToFind.SpanProperty.InstrumentationScopeAttribute.Int64Value.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.InstrumentationScopeAttribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.InstrumentationScopeAttribute.Int64Value.Compare);
        }
        
        [Fact]
        public void AddDoubleInstrumentationScopeAttributePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            string key = "service.started";
            double value = 1;
            
            builder.Where(filters =>
            {
                filters.AddInstrumentationScopeAttributeFilter(key, value, NumberCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.InstrumentationScopeAttribute, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, filterToFind.SpanProperty.InstrumentationScopeAttribute.DoubleValue.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.InstrumentationScopeAttribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.InstrumentationScopeAttribute.DoubleValue.Compare);
        }
        
        [Fact]
        public void AddByteStringInstrumentationScopeAttributePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            string key = "service.name";
            byte[] value = [1, 2, 3, 4];
            
            builder.Where(filters =>
            {
                filters.AddInstrumentationScopeAttributeFilter(key, value, ByteStringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.InstrumentationScopeAttribute, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(ByteStringCompareAsType.Equals, filterToFind.SpanProperty.InstrumentationScopeAttribute.ByteStringValue.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.InstrumentationScopeAttribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.InstrumentationScopeAttribute.ByteStringValue.Compare);
        }
        
        // Resource attributes
        [Fact]
        public void AddStringResourceAttributePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            string key = "service.name";
            string value = "test";
            
            builder.Where(filters =>
            {
                filters.AddResourceAttributeFilter(key, value, StringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.ResourceAttribute, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(StringCompareAsType.Equals, filterToFind.SpanProperty.ResourceAttribute.StringValue.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.ResourceAttribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.ResourceAttribute.StringValue.Compare);
        }
        
        [Fact]
        public void AddBoolResourceAttributePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            string key = "service.started";
            bool value = true;
            
            builder.Where(filters =>
            {
                filters.AddResourceAttributeFilter(key, value, BoolCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.ResourceAttribute, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(BoolCompareAsType.Equals, filterToFind.SpanProperty.ResourceAttribute.BoolValue.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.ResourceAttribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.ResourceAttribute.BoolValue.Compare);
        }
        
        [Fact]
        public void AddInt64ResourceAttributePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            string key = "service.started";
            long value = 1;
            
            builder.Where(filters =>
            {
                filters.AddResourceAttributeFilter(key, value, NumberCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.ResourceAttribute, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, filterToFind.SpanProperty.ResourceAttribute.Int64Value.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.ResourceAttribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.ResourceAttribute.Int64Value.Compare);
        }
        
        [Fact]
        public void AddDoubleResourceAttributePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            string key = "service.started";
            double value = 1;
            
            builder.Where(filters =>
            {
                filters.AddResourceAttributeFilter(key, value, NumberCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.ResourceAttribute, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, filterToFind.SpanProperty.ResourceAttribute.DoubleValue.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.ResourceAttribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.ResourceAttribute.DoubleValue.Compare);
        }
        
        [Fact]
        public void AddByteStringResourceAttributePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            string key = "service.name";
            byte[] value = [1, 2, 3, 4];
            
            builder.Where(filters =>
            {
                filters.AddResourceAttributeFilter(key, value, ByteStringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(WhereSpanFilter.ValueOneofCase.SpanProperty, filterToFind.ValueCase);
            Assert.Equal(WhereSpanPropertyFilter.ValueOneofCase.ResourceAttribute, filterToFind.SpanProperty.ValueCase);
            Assert.Equal(ByteStringCompareAsType.Equals, filterToFind.SpanProperty.ResourceAttribute.ByteStringValue.CompareAs);
            Assert.Equal(key, filterToFind.SpanProperty.ResourceAttribute.Key);
            Assert.Equal(value, filterToFind.SpanProperty.ResourceAttribute.ByteStringValue.Compare);
        }
    }
}