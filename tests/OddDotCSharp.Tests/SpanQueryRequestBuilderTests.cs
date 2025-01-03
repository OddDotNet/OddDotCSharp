using OddDotNet.Proto.Common.V1;
using OddDotNet.Proto.Resource.V1;
using OddDotNet.Proto.Trace.V1;
using OpenTelemetry.Proto.Common.V1;
using OpenTelemetry.Proto.Trace.V1;

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
        public void AddSpanStatusMessagePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            const string message = "test";
            builder.Where(filters =>
            {
                filters.Status.AddMessageFilter(message, StringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(Where.ValueOneofCase.Property, filterToFind.ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Status, filterToFind.Property.ValueCase);
            Assert.Equal(StringCompareAsType.Equals, filterToFind.Property.Status.Message.CompareAs);
            Assert.Equal(message, filterToFind.Property.Status.Message.Compare);
        }
        
        [Fact]
        public void AddSpanStatusCodePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            Status.Types.StatusCode code = Status.Types.StatusCode.Ok;
            builder.Where(filters =>
            {
                filters.Status.AddCodeFilter(code, EnumCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(Where.ValueOneofCase.Property, filterToFind.ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Status, filterToFind.Property.ValueCase);
            Assert.Equal(EnumCompareAsType.Equals, filterToFind.Property.Status.Code.CompareAs);
            Assert.Equal(code, filterToFind.Property.Status.Code.Compare);
        }
        
        [Fact]
        public void AddSpanKindPropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            Span.Types.SpanKind kind = Span.Types.SpanKind.Internal;
            builder.Where(filters =>
            {
                filters.AddKindFilter(kind, EnumCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(Where.ValueOneofCase.Property, filterToFind.ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Kind, filterToFind.Property.ValueCase);
            Assert.Equal(EnumCompareAsType.Equals, filterToFind.Property.Kind.CompareAs);
            Assert.Equal(kind, filterToFind.Property.Kind.Compare);
        }

        [Fact]
        public void AddOrFilter()
        {
            var builder = new SpanQueryRequestBuilder();
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
        public void AddSpanNamePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            const string name = "service1";
            builder.Where(filters =>
            {
                filters.AddNameFilter(name, StringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters.First().ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Name, request.Filters.First().Property.ValueCase);
            Assert.Equal(StringCompareAsType.Equals, request.Filters.First().Property.Name.CompareAs);
            Assert.Equal(name, request.Filters.First().Property.Name.Compare);
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
            
            Assert.Equal(Where.ValueOneofCase.Property, filterToFind.ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.SpanId, filterToFind.Property.ValueCase);
            Assert.Equal(ByteStringCompareAsType.Equals, filterToFind.Property.SpanId.CompareAs);
            Assert.Equal(spanId, filterToFind.Property.SpanId.Compare);
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
            
            Assert.Equal(Where.ValueOneofCase.Property, filterToFind.ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.TraceId, filterToFind.Property.ValueCase);
            Assert.Equal(ByteStringCompareAsType.Equals, filterToFind.Property.TraceId.CompareAs);
            Assert.Equal(traceId, filterToFind.Property.TraceId.Compare);
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
            
            Assert.Equal(Where.ValueOneofCase.Property, filterToFind.ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.ParentSpanId, filterToFind.Property.ValueCase);
            Assert.Equal(ByteStringCompareAsType.Equals, filterToFind.Property.ParentSpanId.CompareAs);
            Assert.Equal(spanId, filterToFind.Property.ParentSpanId.Compare);
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
            
            Assert.Equal(Where.ValueOneofCase.Property, filterToFind.ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.StartTimeUnixNano, filterToFind.Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, filterToFind.Property.StartTimeUnixNano.CompareAs);
            Assert.Equal(startTimeUnixNano, filterToFind.Property.StartTimeUnixNano.Compare);
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
            
            Assert.Equal(Where.ValueOneofCase.Property, filterToFind.ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.EndTimeUnixNano, filterToFind.Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, filterToFind.Property.EndTimeUnixNano.CompareAs);
            Assert.Equal(endTimeUnixNano, filterToFind.Property.EndTimeUnixNano.Compare);
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
            
            Assert.Equal(Where.ValueOneofCase.Property, filterToFind.ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Attributes, filterToFind.Property.ValueCase);
            Assert.Equal(StringCompareAsType.Equals, filterToFind.Property.Attributes.Values[0].Value.StringValue.CompareAs);
            Assert.Equal(key, filterToFind.Property.Attributes.Values[0].Key);
            Assert.Equal(value, filterToFind.Property.Attributes.Values[0].Value.StringValue.Compare);
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
            
            Assert.Equal(Where.ValueOneofCase.Property, filterToFind.ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Attributes, filterToFind.Property.ValueCase);
            Assert.Equal(BoolCompareAsType.Equals, filterToFind.Property.Attributes.Values[0].Value.BoolValue.CompareAs);
            Assert.Equal(key, filterToFind.Property.Attributes.Values[0].Key);
            Assert.Equal(value, filterToFind.Property.Attributes.Values[0].Value.BoolValue.Compare);
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
            
            Assert.Equal(Where.ValueOneofCase.Property, filterToFind.ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Attributes, filterToFind.Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, filterToFind.Property.Attributes.Values[0].Value.IntValue.CompareAs);
            Assert.Equal(key, filterToFind.Property.Attributes.Values[0].Key);
            Assert.Equal(value, filterToFind.Property.Attributes.Values[0].Value.IntValue.Compare);
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
            
            Assert.Equal(Where.ValueOneofCase.Property, filterToFind.ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Attributes, filterToFind.Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, filterToFind.Property.Attributes.Values[0].Value.DoubleValue.CompareAs);
            Assert.Equal(key, filterToFind.Property.Attributes.Values[0].Key);
            Assert.Equal(value, filterToFind.Property.Attributes.Values[0].Value.DoubleValue.Compare);
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
            
            Assert.Equal(Where.ValueOneofCase.Property, filterToFind.ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Attributes, filterToFind.Property.ValueCase);
            Assert.Equal(ByteStringCompareAsType.Equals, filterToFind.Property.Attributes.Values[0].Value.ByteStringValue.CompareAs);
            Assert.Equal(key, filterToFind.Property.Attributes.Values[0].Key);
            Assert.Equal(value, filterToFind.Property.Attributes.Values[0].Value.ByteStringValue.Compare);
        }
        
        [Fact]
        public void AddArrayValueAttributePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            const string key = "key";
            const double value = 123.0;
            var request = builder.Where(filters => filters.AddAttributeArrayFilter(key, arrayFilters =>
            {
                arrayFilters.AddFilter(value, NumberCompareAsType.Equals);
            })).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Attributes, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Attributes.Values[0].Value.ArrayValue.Values[0].DoubleValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Attributes.Values[0].Key);
            Assert.Equal(value, request.Filters[0].Property.Attributes.Values[0].Value.ArrayValue.Values[0].DoubleValue.Compare);
        }
        
        [Fact]
        public void AddKeyValueListAttributePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            const string key = "key";
            const double value = 123.0;
            var request = builder.Where(filters => filters.AddAttributeKeyValueListFilter(key, kvlFilters =>
            {
                kvlFilters.AddFilter(key, value, NumberCompareAsType.Equals);
            })).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Attributes, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Attributes.Values[0].Value.KvlistValue.Values[0].Value.DoubleValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Attributes.Values[0].Key);
            Assert.Equal(key, request.Filters[0].Property.Attributes.Values[0].Value.KvlistValue.Values[0].Key);
            Assert.Equal(value, request.Filters[0].Property.Attributes.Values[0].Value.KvlistValue.Values[0].Value.DoubleValue.Compare);
        }
        
        [Fact]
        public void AddEventTimeUnixNanoPropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            ulong eventTimeUnixNano = 1000000000;
            builder.Where(filters =>
            {
                filters.Event.AddTimeUnixNanoFilter(eventTimeUnixNano, NumberCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(Where.ValueOneofCase.Property, filterToFind.ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Event, filterToFind.Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, filterToFind.Property.Event.TimeUnixNano.CompareAs);
            Assert.Equal(eventTimeUnixNano, filterToFind.Property.Event.TimeUnixNano.Compare);
        }
        
        [Fact]
        public void AddEventNamePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            const string name = "service1";
            builder.Where(filters =>
            {
                filters.Event.AddNameFilter(name, StringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(Where.ValueOneofCase.Property, filterToFind.ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Event, filterToFind.Property.ValueCase);
            Assert.Equal(StringCompareAsType.Equals, filterToFind.Property.Event.Name.CompareAs);
            Assert.Equal(name, filterToFind.Property.Event.Name.Compare);
        }
        
        [Fact]
        public void AddEventDroppedAttributesCountPropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            const uint name = 123;
            builder.Where(filters =>
            {
                filters.Event.AddDroppedAttributesCountFilter(name, NumberCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(Where.ValueOneofCase.Property, filterToFind.ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Event, filterToFind.Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, filterToFind.Property.Event.DroppedAttributesCount.CompareAs);
            Assert.Equal(name, filterToFind.Property.Event.DroppedAttributesCount.Compare);
        }
        
        [Fact]
        public void AddLinkTraceStatePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            const string name = "service1";
            builder.Where(filters =>
            {
                filters.Link.AddTraceStateFilter(name, StringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(Where.ValueOneofCase.Property, filterToFind.ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Link, filterToFind.Property.ValueCase);
            Assert.Equal(StringCompareAsType.Equals, filterToFind.Property.Link.TraceState.CompareAs);
            Assert.Equal(name, filterToFind.Property.Link.TraceState.Compare);
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
            
            Assert.Equal(Where.ValueOneofCase.Property, filterToFind.ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.TraceState, filterToFind.Property.ValueCase);
            Assert.Equal(StringCompareAsType.Equals, filterToFind.Property.TraceState.CompareAs);
            Assert.Equal(name, filterToFind.Property.TraceState.Compare);
        }
        
        [Fact]
        public void AddInstrumentationScopeNamePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
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
        public void AddInstrumentationScopeVersionPropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
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
            var builder = new SpanQueryRequestBuilder();
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
        public void AddResourceSchemaUrlPropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
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
        public void AddLinkSpanIdPropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            byte[] spanId = [1, 2, 3, 4, 5, 6, 7, 8];
            builder.Where(filters =>
            {
                filters.Link.AddSpanIdFilter(spanId, ByteStringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(Where.ValueOneofCase.Property, filterToFind.ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Link, filterToFind.Property.ValueCase);
            Assert.Equal(ByteStringCompareAsType.Equals, filterToFind.Property.Link.SpanId.CompareAs);
            Assert.Equal(spanId, filterToFind.Property.Link.SpanId.Compare);
        }
        
        [Fact]
        public void AddLinkTraceIdPropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            byte[] traceId = [1, 2, 3, 4, 5, 6, 7, 8, 1, 2, 3, 4, 5, 6, 7, 8];
            builder.Where(filters =>
            {
                filters.Link.AddTraceIdFilter(traceId, ByteStringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(Where.ValueOneofCase.Property, filterToFind.ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Link, filterToFind.Property.ValueCase);
            Assert.Equal(ByteStringCompareAsType.Equals, filterToFind.Property.Link.TraceId.CompareAs);
            Assert.Equal(traceId, filterToFind.Property.Link.TraceId.Compare);
        }
        
        [Fact]
        public void AddLinkFlagsPropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            uint flags = 1;
            builder.Where(filters =>
            {
                filters.Link.AddFlagsFilter(flags, NumberCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(Where.ValueOneofCase.Property, filterToFind.ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Link, filterToFind.Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, filterToFind.Property.Link.Flags.CompareAs);
            Assert.Equal(flags, filterToFind.Property.Link.Flags.Compare);
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
            
            Assert.Equal(Where.ValueOneofCase.Property, filterToFind.ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Flags, filterToFind.Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, filterToFind.Property.Flags.CompareAs);
            Assert.Equal(flags, filterToFind.Property.Flags.Compare);
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
                filters.Event.AddAttributeFilter(key, value, StringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(Where.ValueOneofCase.Property, filterToFind.ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Event, filterToFind.Property.ValueCase);
            Assert.Equal(StringCompareAsType.Equals, filterToFind.Property.Event.Attributes.Values[0].Value.StringValue.CompareAs);
            Assert.Equal(key, filterToFind.Property.Event.Attributes.Values[0].Key);
            Assert.Equal(value, filterToFind.Property.Event.Attributes.Values[0].Value.StringValue.Compare);
        }
        
        [Fact]
        public void AddBoolEventAttributePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            string key = "service.started";
            bool value = true;
            
            builder.Where(filters =>
            {
                filters.Event.AddAttributeFilter(key, value, BoolCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(Where.ValueOneofCase.Property, filterToFind.ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Event, filterToFind.Property.ValueCase);
            Assert.Equal(BoolCompareAsType.Equals, filterToFind.Property.Event.Attributes.Values[0].Value.BoolValue.CompareAs);
            Assert.Equal(key, filterToFind.Property.Event.Attributes.Values[0].Key);
            Assert.Equal(value, filterToFind.Property.Event.Attributes.Values[0].Value.BoolValue.Compare);
        }
        
        [Fact]
        public void AddInt64EventAttributePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            string key = "service.started";
            long value = 1;
            
            builder.Where(filters =>
            {
                filters.Event.AddAttributeFilter(key, value, NumberCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(Where.ValueOneofCase.Property, filterToFind.ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Event, filterToFind.Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, filterToFind.Property.Event.Attributes.Values[0].Value.IntValue.CompareAs);
            Assert.Equal(key, filterToFind.Property.Event.Attributes.Values[0].Key);
            Assert.Equal(value, filterToFind.Property.Event.Attributes.Values[0].Value.IntValue.Compare);
        }
        
        [Fact]
        public void AddDoubleEventAttributePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            string key = "service.started";
            double value = 1;
            
            builder.Where(filters =>
            {
                filters.Event.AddAttributeFilter(key, value, NumberCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(Where.ValueOneofCase.Property, filterToFind.ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Event, filterToFind.Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, filterToFind.Property.Event.Attributes.Values[0].Value.DoubleValue.CompareAs);
            Assert.Equal(key, filterToFind.Property.Event.Attributes.Values[0].Key);
            Assert.Equal(value, filterToFind.Property.Event.Attributes.Values[0].Value.DoubleValue.Compare);
        }
        
        [Fact]
        public void AddByteStringEventAttributePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            string key = "service.name";
            byte[] value = [1, 2, 3, 4];
            
            builder.Where(filters =>
            {
                filters.Event.AddAttributeFilter(key, value, ByteStringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(Where.ValueOneofCase.Property, filterToFind.ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Event, filterToFind.Property.ValueCase);
            Assert.Equal(ByteStringCompareAsType.Equals, filterToFind.Property.Event.Attributes.Values[0].Value.ByteStringValue.CompareAs);
            Assert.Equal(key, filterToFind.Property.Event.Attributes.Values[0].Key);
            Assert.Equal(value, filterToFind.Property.Event.Attributes.Values[0].Value.ByteStringValue.Compare);
        }
        
        [Fact]
        public void AddArrayValueEventAttributePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            const string key = "key";
            const double value = 123.0;
            var request = builder.Where(filters => filters.Event.AddAttributeArrayFilter(key, arrayFilters =>
            {
                arrayFilters.AddFilter(value, NumberCompareAsType.Equals);
            })).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Event, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Event.Attributes.Values[0].Value.ArrayValue.Values[0].DoubleValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Event.Attributes.Values[0].Key);
            Assert.Equal(value, request.Filters[0].Property.Event.Attributes.Values[0].Value.ArrayValue.Values[0].DoubleValue.Compare);
        }
        
        [Fact]
        public void AddKeyValueListEventAttributePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            const string key = "key";
            const double value = 123.0;
            var request = builder.Where(filters => filters.Event.AddAttributeKeyValueListFilter(key, kvlFilters =>
            {
                kvlFilters.AddFilter(key, value, NumberCompareAsType.Equals);
            })).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Event, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Event.Attributes.Values[0].Value.KvlistValue.Values[0].Value.DoubleValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Event.Attributes.Values[0].Key);
            Assert.Equal(key, request.Filters[0].Property.Event.Attributes.Values[0].Value.KvlistValue.Values[0].Key);
            Assert.Equal(value, request.Filters[0].Property.Event.Attributes.Values[0].Value.KvlistValue.Values[0].Value.DoubleValue.Compare);
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
                filters.Link.AddAttributeFilter(key, value, StringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(Where.ValueOneofCase.Property, filterToFind.ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Link, filterToFind.Property.ValueCase);
            Assert.Equal(StringCompareAsType.Equals, filterToFind.Property.Link.Attributes.Values[0].Value.StringValue.CompareAs);
            Assert.Equal(key, filterToFind.Property.Link.Attributes.Values[0].Key);
            Assert.Equal(value, filterToFind.Property.Link.Attributes.Values[0].Value.StringValue.Compare);
        }
        
        [Fact]
        public void AddBoolLinkAttributePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            string key = "service.started";
            bool value = true;
            
            builder.Where(filters =>
            {
                filters.Link.AddAttributeFilter(key, value, BoolCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(Where.ValueOneofCase.Property, filterToFind.ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Link, filterToFind.Property.ValueCase);
            Assert.Equal(BoolCompareAsType.Equals, filterToFind.Property.Link.Attributes.Values[0].Value.BoolValue.CompareAs);
            Assert.Equal(key, filterToFind.Property.Link.Attributes.Values[0].Key);
            Assert.Equal(value, filterToFind.Property.Link.Attributes.Values[0].Value.BoolValue.Compare);
        }
        
        [Fact]
        public void AddInt64LinkAttributePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            string key = "service.started";
            long value = 1;
            
            builder.Where(filters =>
            {
                filters.Link.AddAttributeFilter(key, value, NumberCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(Where.ValueOneofCase.Property, filterToFind.ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Link, filterToFind.Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, filterToFind.Property.Link.Attributes.Values[0].Value.IntValue.CompareAs);
            Assert.Equal(key, filterToFind.Property.Link.Attributes.Values[0].Key);
            Assert.Equal(value, filterToFind.Property.Link.Attributes.Values[0].Value.IntValue.Compare);
        }
        
        [Fact]
        public void AddDoubleLinkAttributePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            string key = "service.started";
            double value = 1;
            
            builder.Where(filters =>
            {
                filters.Link.AddAttributeFilter(key, value, NumberCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(Where.ValueOneofCase.Property, filterToFind.ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Link, filterToFind.Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, filterToFind.Property.Link.Attributes.Values[0].Value.DoubleValue.CompareAs);
            Assert.Equal(key, filterToFind.Property.Link.Attributes.Values[0].Key);
            Assert.Equal(value, filterToFind.Property.Link.Attributes.Values[0].Value.DoubleValue.Compare);
        }
        
        [Fact]
        public void AddByteStringLinkAttributePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            string key = "service.name";
            byte[] value = [1, 2, 3, 4];
            
            builder.Where(filters =>
            {
                filters.Link.AddAttributeFilter(key, value, ByteStringCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(Where.ValueOneofCase.Property, filterToFind.ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Link, filterToFind.Property.ValueCase);
            Assert.Equal(ByteStringCompareAsType.Equals, filterToFind.Property.Link.Attributes.Values[0].Value.ByteStringValue.CompareAs);
            Assert.Equal(key, filterToFind.Property.Link.Attributes.Values[0].Key);
            Assert.Equal(value, filterToFind.Property.Link.Attributes.Values[0].Value.ByteStringValue.Compare);
        }
        
        [Fact]
        public void AddArrayValueLinkAttributePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            const string key = "key";
            const double value = 123.0;
            var request = builder.Where(filters => filters.Link.AddAttributeArrayFilter(key, arrayFilters =>
            {
                arrayFilters.AddFilter(value, NumberCompareAsType.Equals);
            })).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Link, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Link.Attributes.Values[0].Value.ArrayValue.Values[0].DoubleValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Link.Attributes.Values[0].Key);
            Assert.Equal(value, request.Filters[0].Property.Link.Attributes.Values[0].Value.ArrayValue.Values[0].DoubleValue.Compare);
        }
        
        [Fact]
        public void AddKeyValueListLinkAttributePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            const string key = "key";
            const double value = 123.0;
            var request = builder.Where(filters => filters.Link.AddAttributeKeyValueListFilter(key, kvlFilters =>
            {
                kvlFilters.AddFilter(key, value, NumberCompareAsType.Equals);
            })).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Link, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Link.Attributes.Values[0].Value.KvlistValue.Values[0].Value.DoubleValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Link.Attributes.Values[0].Key);
            Assert.Equal(key, request.Filters[0].Property.Link.Attributes.Values[0].Value.KvlistValue.Values[0].Key);
            Assert.Equal(value, request.Filters[0].Property.Link.Attributes.Values[0].Value.KvlistValue.Values[0].Value.DoubleValue.Compare);
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
            var builder = new SpanQueryRequestBuilder();
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
            var builder = new SpanQueryRequestBuilder();
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
            var builder = new SpanQueryRequestBuilder();
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
            var builder = new SpanQueryRequestBuilder();
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
        public void AddArrayValueInstrumentationScopeAttributePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            const string key = "key";
            const double value = 123.0;
            var request = builder.Where(filters => filters.InstrumentationScope.AddAttributeArrayFilter(key, arrayFilters =>
            {
                arrayFilters.AddFilter(value, NumberCompareAsType.Equals);
            })).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.InstrumentationScope, request.Filters[0].ValueCase);
            Assert.Equal(InstrumentationScopeFilter.ValueOneofCase.Attributes, request.Filters[0].InstrumentationScope.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].InstrumentationScope.Attributes.Values[0].Value.ArrayValue.Values[0].DoubleValue.CompareAs);
            Assert.Equal(key, request.Filters[0].InstrumentationScope.Attributes.Values[0].Key);
            Assert.Equal(value, request.Filters[0].InstrumentationScope.Attributes.Values[0].Value.ArrayValue.Values[0].DoubleValue.Compare);
        }
        
        [Fact]
        public void AddKeyValueListInstrumentationScopeAttributePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            const string key = "key";
            const double value = 123.0;
            var request = builder.Where(filters => filters.InstrumentationScope.AddAttributeKeyValueListFilter(key, kvlFilters =>
            {
                kvlFilters.AddFilter(key, value, NumberCompareAsType.Equals);
            })).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.InstrumentationScope, request.Filters[0].ValueCase);
            Assert.Equal(InstrumentationScopeFilter.ValueOneofCase.Attributes, request.Filters[0].InstrumentationScope.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].InstrumentationScope.Attributes.Values[0].Value.KvlistValue.Values[0].Value.DoubleValue.CompareAs);
            Assert.Equal(key, request.Filters[0].InstrumentationScope.Attributes.Values[0].Key);
            Assert.Equal(key, request.Filters[0].InstrumentationScope.Attributes.Values[0].Value.KvlistValue.Values[0].Key);
            Assert.Equal(value, request.Filters[0].InstrumentationScope.Attributes.Values[0].Value.KvlistValue.Values[0].Value.DoubleValue.Compare);
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
            var builder = new SpanQueryRequestBuilder();
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
            var builder = new SpanQueryRequestBuilder();
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
            var builder = new SpanQueryRequestBuilder();
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
            var builder = new SpanQueryRequestBuilder();
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
        public void AddArrayValueResourceAttributePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            const string key = "key";
            const double value = 123.0;
            var request = builder.Where(filters => filters.Resource.AddAttributeArrayFilter(key, arrayFilters =>
            {
                arrayFilters.AddFilter(value, NumberCompareAsType.Equals);
            })).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Resource, request.Filters[0].ValueCase);
            Assert.Equal(ResourceFilter.ValueOneofCase.Attributes, request.Filters[0].Resource.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Resource.Attributes.Values[0].Value.ArrayValue.Values[0].DoubleValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Resource.Attributes.Values[0].Key);
            Assert.Equal(value, request.Filters[0].Resource.Attributes.Values[0].Value.ArrayValue.Values[0].DoubleValue.Compare);
        }
        
        [Fact]
        public void AddKeyValueListResourceAttributePropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            const string key = "key";
            const double value = 123.0;
            var request = builder.Where(filters => filters.Resource.AddAttributeKeyValueListFilter(key, kvlFilters =>
            {
                kvlFilters.AddFilter(key, value, NumberCompareAsType.Equals);
            })).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Resource, request.Filters[0].ValueCase);
            Assert.Equal(ResourceFilter.ValueOneofCase.Attributes, request.Filters[0].Resource.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Resource.Attributes.Values[0].Value.KvlistValue.Values[0].Value.DoubleValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Resource.Attributes.Values[0].Key);
            Assert.Equal(key, request.Filters[0].Resource.Attributes.Values[0].Value.KvlistValue.Values[0].Key);
            Assert.Equal(value, request.Filters[0].Resource.Attributes.Values[0].Value.KvlistValue.Values[0].Value.DoubleValue.Compare);
        }
        
        [Fact]
        public void AddResourceDroppedAttributesCountPropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
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
        
        [Fact]
        public void AddDroppedAttributesCountPropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            const uint value = 123;
            builder.Where(filters =>
            {
                filters.AddDroppedAttributesCountFilter(value, NumberCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(Where.ValueOneofCase.Property, filterToFind.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, filterToFind.Property.DroppedAttributesCount.CompareAs);
            Assert.Equal(value, filterToFind.Property.DroppedAttributesCount.Compare);
        }
        
        [Fact]
        public void AddDroppedEventsCountPropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            const uint value = 123;
            builder.Where(filters =>
            {
                filters.AddDroppedEventsCountFilter(value, NumberCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(Where.ValueOneofCase.Property, filterToFind.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, filterToFind.Property.DroppedEventsCount.CompareAs);
            Assert.Equal(value, filterToFind.Property.DroppedEventsCount.Compare);
        }
        
        [Fact]
        public void AddDroppedLinksCountPropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            const uint value = 123;
            builder.Where(filters =>
            {
                filters.AddDroppedLinksCountFilter(value, NumberCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(Where.ValueOneofCase.Property, filterToFind.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, filterToFind.Property.DroppedLinksCount.CompareAs);
            Assert.Equal(value, filterToFind.Property.DroppedLinksCount.Compare);
        }
        
        [Fact]
        public void AddInstrumentationScopeDroppedAttributesCountPropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
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
        public void AddLinkDroppedAttributesCountPropertyFilter()
        {
            var builder = new SpanQueryRequestBuilder();
            const uint value = 123;
            builder.Where(filters =>
            {
                filters.Link.AddDroppedAttributesCountFilter(value, NumberCompareAsType.Equals);
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(Where.ValueOneofCase.Property, filterToFind.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, filterToFind.Property.Link.DroppedAttributesCount.CompareAs);
            Assert.Equal(value, filterToFind.Property.Link.DroppedAttributesCount.Compare);
        }
    }
}