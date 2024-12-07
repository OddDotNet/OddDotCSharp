using OddDotNet.Proto.Common.V1;
using OddDotNet.Proto.Logs.V1;
using OddDotNet.Proto.Resource.V1;
using OpenTelemetry.Proto.Logs.V1;

namespace OddDotCSharp.Tests;

public class LogQueryRequestBuilderTests
{
    public class BuildShould
    {
        [Fact]
        public void SetTakeToTakeFirstWhenNotSpecified()
        {
            var request = new LogQueryRequestBuilder().Build();
            
            Assert.Equal(Take.ValueOneofCase.TakeFirst, request.Take.ValueCase);
        }

        [Fact]
        public void SetDurationToDefaultWhenNotSpecified()
        {
            int expectedMsValue = 30000;
            var request = new LogQueryRequestBuilder().Build();
            
            Assert.Equal(expectedMsValue, request.Duration.Milliseconds);
        }
    }

    public class TakeShould
    {
        [Fact]
        public void SetTakeToTakeFirst()
        {
            var builder = new LogQueryRequestBuilder();
            builder.TakeFirst();
        }
        
        [Fact]
        public void SetTakeToTakeExact()
        {
            var builder = new LogQueryRequestBuilder();
            builder.TakeExact(123);
            var request = builder.Build();
            
            Assert.Equal(Take.ValueOneofCase.TakeExact, request.Take.ValueCase);
            Assert.Equal(123, request.Take.TakeExact.Count);
        }
        
        [Fact]
        public void SetTakeToTakeAll()
        {
            var builder = new LogQueryRequestBuilder();
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
            var builder = new LogQueryRequestBuilder();
            TimeSpan duration = TimeSpan.FromMilliseconds(500);
            builder.Wait(duration);
            var request = builder.Build();
            
            Assert.Equal(duration.TotalMilliseconds, request.Duration.Milliseconds);
        }
    }

    public class WhereShould
    {
        [Fact]
        public void AddTimeUnixNanoPropertyFilter()
        {
            var builder = new LogQueryRequestBuilder();
            const uint time = 12345;
            var request = builder.Where(filters => filters.AddTimeUnixNanoFilter(time, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.TimeUnixNano, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.TimeUnixNano.CompareAs);
            Assert.Equal(time, request.Filters[0].Property.TimeUnixNano.Compare);
        }
        
        [Fact]
        public void AddObservedTimeUnixNanoPropertyFilter()
        {
            var builder = new LogQueryRequestBuilder();
            const uint time = 12345;
            var request = builder.Where(filters => filters.AddObservedTimeUnixNanoFilter(time, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.ObservedTimeUnixNano, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.ObservedTimeUnixNano.CompareAs);
            Assert.Equal(time, request.Filters[0].Property.ObservedTimeUnixNano.Compare);
        }
        
        [Fact]
        public void AddSeverityNumberPropertyFilter()
        {
            var builder = new LogQueryRequestBuilder();
            const SeverityNumber sev = SeverityNumber.Debug;
            var request = builder.Where(filters => filters.AddSeverityNumberFilter(sev, EnumCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.SeverityNumber, request.Filters[0].Property.ValueCase);
            Assert.Equal(EnumCompareAsType.Equals, request.Filters[0].Property.SeverityNumber.CompareAs);
            Assert.Equal(sev, request.Filters[0].Property.SeverityNumber.Compare);
        }
        
        [Fact]
        public void AddSeverityTextPropertyFilter()
        {
            var builder = new LogQueryRequestBuilder();
            const string severityText = "Debug";
            var request = builder.Where(filters => filters.AddSeverityTextFilter(severityText, StringCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.SeverityText, request.Filters[0].Property.ValueCase);
            Assert.Equal(StringCompareAsType.Equals, request.Filters[0].Property.SeverityText.CompareAs);
            Assert.Equal(severityText, request.Filters[0].Property.SeverityText.Compare);
        }
        
        [Fact]
        public void AddStringAttributePropertyFilter()
        {
            var builder = new LogQueryRequestBuilder();
            const string key = "key";
            const string value = "value";
            var request = builder.Where(filters => filters.AddAttributeFilter(key, value, StringCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Attributes, request.Filters[0].Property.ValueCase);
            Assert.Equal(StringCompareAsType.Equals, request.Filters[0].Property.Attributes.Values[0].Value.StringValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Attributes.Values[0].Key);
            Assert.Equal(value, request.Filters[0].Property.Attributes.Values[0].Value.StringValue.Compare);
        }
        
        [Fact]
        public void AddByteStringAttributePropertyFilter()
        {
            var builder = new LogQueryRequestBuilder();
            const string key = "key";
            byte[] value = [123];
            var request = builder.Where(filters => filters.AddAttributeFilter(key, value, ByteStringCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Attributes, request.Filters[0].Property.ValueCase);
            Assert.Equal(ByteStringCompareAsType.Equals, request.Filters[0].Property.Attributes.Values[0].Value.ByteStringValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Attributes.Values[0].Key);
            Assert.Equal(value, request.Filters[0].Property.Attributes.Values[0].Value.ByteStringValue.Compare);
        }
        
        [Fact]
        public void AddBoolAttributePropertyFilter()
        {
            var builder = new LogQueryRequestBuilder();
            const string key = "key";
            const bool value = true;
            var request = builder.Where(filters => filters.AddAttributeFilter(key, value, BoolCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Attributes, request.Filters[0].Property.ValueCase);
            Assert.Equal(BoolCompareAsType.Equals, request.Filters[0].Property.Attributes.Values[0].Value.BoolValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Attributes.Values[0].Key);
            Assert.Equal(value, request.Filters[0].Property.Attributes.Values[0].Value.BoolValue.Compare);
        }
        
        [Fact]
        public void AddInt64AttributePropertyFilter()
        {
            var builder = new LogQueryRequestBuilder();
            const string key = "key";
            const long value = 123;
            var request = builder.Where(filters => filters.AddAttributeFilter(key, value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Attributes, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Attributes.Values[0].Value.IntValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Attributes.Values[0].Key);
            Assert.Equal(value, request.Filters[0].Property.Attributes.Values[0].Value.IntValue.Compare);
        }
        
        [Fact]
        public void AddDoubleAttributePropertyFilter()
        {
            var builder = new LogQueryRequestBuilder();
            const string key = "key";
            const double value = 123.0;
            var request = builder.Where(filters => filters.AddAttributeFilter(key, value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Attributes, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Attributes.Values[0].Value.DoubleValue.CompareAs);
            Assert.Equal(key, request.Filters[0].Property.Attributes.Values[0].Key);
            Assert.Equal(value, request.Filters[0].Property.Attributes.Values[0].Value.DoubleValue.Compare);
        }
        
        [Fact]
        public void AddArrayValueAttributePropertyFilter()
        {
            var builder = new LogQueryRequestBuilder();
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
            var builder = new LogQueryRequestBuilder();
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
        public void AddStringBodyPropertyFilter()
        {
            var builder = new LogQueryRequestBuilder();
            const string value = "value";
            var request = builder.Where(filters => filters.AddBodyFilter(value, StringCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Body, request.Filters[0].Property.ValueCase);
            Assert.Equal(StringCompareAsType.Equals, request.Filters[0].Property.Body.StringValue.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.Body.StringValue.Compare);
        }
        
        [Fact]
        public void AddByteStringBodyPropertyFilter()
        {
            var builder = new LogQueryRequestBuilder();
            byte[] value = [123];
            var request = builder.Where(filters => filters.AddBodyFilter(value, ByteStringCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Body, request.Filters[0].Property.ValueCase);
            Assert.Equal(ByteStringCompareAsType.Equals, request.Filters[0].Property.Body.ByteStringValue.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.Body.ByteStringValue.Compare);
        }
        
        [Fact]
        public void AddBoolBodyPropertyFilter()
        {
            var builder = new LogQueryRequestBuilder();
            const bool value = true;
            var request = builder.Where(filters => filters.AddBodyFilter(value, BoolCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Body, request.Filters[0].Property.ValueCase);
            Assert.Equal(BoolCompareAsType.Equals, request.Filters[0].Property.Body.BoolValue.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.Body.BoolValue.Compare);
        }
        
        [Fact]
        public void AddInt64BodyPropertyFilter()
        {
            var builder = new LogQueryRequestBuilder();
            const long value = 123;
            var request = builder.Where(filters => filters.AddBodyFilter(value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Body, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Body.IntValue.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.Body.IntValue.Compare);
        }
        
        [Fact]
        public void AddDoubleBodyPropertyFilter()
        {
            var builder = new LogQueryRequestBuilder();
            const double value = 123.0;
            var request = builder.Where(filters => filters.AddBodyFilter(value, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Body, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Body.DoubleValue.CompareAs);
            Assert.Equal(value, request.Filters[0].Property.Body.DoubleValue.Compare);
        }
        
        [Fact]
        public void AddArrayBodyPropertyFilter()
        {
            var builder = new LogQueryRequestBuilder();
            const string stringValue = "test";
            const long intValue = 123;
            byte[] byteValue = [1, 2, 3];
            const double doubleValue = 123.0;
            const double anotherDoubleValue = 456.0;
            const bool boolValue = true;
            const string key = "key";
            var request = builder.Where(filters =>
            {
                filters.AddBodyArrayFilter(arrayFilters =>
                {
                    arrayFilters
                        .AddFilter(doubleValue, NumberCompareAsType.Equals) // index 0
                        .AddFilter(stringValue, StringCompareAsType.Equals) // 1
                        .AddFilter(intValue, NumberCompareAsType.Equals) // 2
                        .AddFilter(byteValue, ByteStringCompareAsType.Equals) // 3
                        .AddFilter(boolValue, BoolCompareAsType.Equals) // 4
                        .AddArrayFilter(moreArrayFilters =>
                        {
                            moreArrayFilters.AddFilter(anotherDoubleValue, NumberCompareAsType.Equals);
                        }) // 5
                        .AddKeyValueListFilter(keyValueListFilters =>
                        {
                            keyValueListFilters.AddFilter(key, stringValue, StringCompareAsType.Equals);
                        }); // 6
                });
            }).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Body, request.Filters[0].Property.ValueCase);
            
            // Check the double value
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Body.ArrayValue.Values[0].DoubleValue.CompareAs);
            Assert.Equal(doubleValue, request.Filters[0].Property.Body.ArrayValue.Values[0].DoubleValue.Compare);
            
            // Check the string value
            Assert.Equal(StringCompareAsType.Equals, request.Filters[0].Property.Body.ArrayValue.Values[1].StringValue.CompareAs);
            Assert.Equal(stringValue, request.Filters[0].Property.Body.ArrayValue.Values[1].StringValue.Compare);
            
            // Check the int value
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Body.ArrayValue.Values[2].IntValue.CompareAs);
            Assert.Equal(intValue, request.Filters[0].Property.Body.ArrayValue.Values[2].IntValue.Compare);
            
            // Check the ByteString value
            Assert.Equal(ByteStringCompareAsType.Equals, request.Filters[0].Property.Body.ArrayValue.Values[3].ByteStringValue.CompareAs);
            Assert.Equal(byteValue, request.Filters[0].Property.Body.ArrayValue.Values[3].ByteStringValue.Compare);
            
            // Check the bool value
            Assert.Equal(BoolCompareAsType.Equals, request.Filters[0].Property.Body.ArrayValue.Values[4].BoolValue.CompareAs);
            Assert.Equal(boolValue, request.Filters[0].Property.Body.ArrayValue.Values[4].BoolValue.Compare);
            
            // Check the ArrayValue value
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Body.ArrayValue.Values[5].ArrayValue.Values[0].DoubleValue.CompareAs);
            Assert.Equal(anotherDoubleValue, request.Filters[0].Property.Body.ArrayValue.Values[5].ArrayValue.Values[0].DoubleValue.Compare);
            
            // Check the KeyValueList value
            Assert.Equal(key, request.Filters[0].Property.Body.ArrayValue.Values[6].KvlistValue.Values[0].Key);
            Assert.Equal(StringCompareAsType.Equals, request.Filters[0].Property.Body.ArrayValue.Values[6].KvlistValue.Values[0].Value.StringValue.CompareAs);
            Assert.Equal(stringValue, request.Filters[0].Property.Body.ArrayValue.Values[6].KvlistValue.Values[0].Value.StringValue.Compare);
        }
        
        [Fact]
        public void AddKeyValueListBodyPropertyFilter()
        {
            var builder = new LogQueryRequestBuilder();
            const string stringValue = "test";
            const long intValue = 123;
            byte[] byteValue = [1, 2, 3];
            const double doubleValue = 123.0;
            const double anotherDoubleValue = 456.0;
            const bool boolValue = true;
            const string key = "key";
            var request = builder.Where(filters =>
            {
                filters.AddBodyKeyValueListFilter(keyValueListFilters =>
                {
                    keyValueListFilters
                        .AddFilter(key, stringValue, StringCompareAsType.Equals) // Index 0
                        .AddFilter(key, intValue, NumberCompareAsType.Equals) // 1
                        .AddFilter(key, byteValue, ByteStringCompareAsType.Equals) // 2
                        .AddFilter(key, doubleValue, NumberCompareAsType.Equals) // 3
                        .AddFilter(key, boolValue, BoolCompareAsType.Equals) // 4
                        .AddArrayFilter(key, arrayFilters =>
                        {
                            arrayFilters.AddFilter(stringValue, StringCompareAsType.Equals);
                        }) // 5
                        .AddKeyValueListFilter(key, kvlFilters =>
                        {
                            kvlFilters.AddFilter(key, stringValue, StringCompareAsType.Equals);
                        }); // 6
                });
            }).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Body, request.Filters[0].Property.ValueCase);
            
            // 1st KV - Index 0
            Assert.Equal(key, request.Filters[0].Property.Body.KvlistValue.Values[0].Key);
            Assert.Equal(StringCompareAsType.Equals, request.Filters[0].Property.Body.KvlistValue.Values[0].Value.StringValue.CompareAs);
            Assert.Equal(stringValue, request.Filters[0].Property.Body.KvlistValue.Values[0].Value.StringValue.Compare);
            
            // 2nd KV - Index 1
            Assert.Equal(key, request.Filters[0].Property.Body.KvlistValue.Values[1].Key);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Body.KvlistValue.Values[1].Value.IntValue.CompareAs);
            Assert.Equal(intValue, request.Filters[0].Property.Body.KvlistValue.Values[1].Value.IntValue.Compare);
            
            // 3rd KV - Index 2
            Assert.Equal(key, request.Filters[0].Property.Body.KvlistValue.Values[2].Key);
            Assert.Equal(ByteStringCompareAsType.Equals, request.Filters[0].Property.Body.KvlistValue.Values[2].Value.ByteStringValue.CompareAs);
            Assert.Equal(byteValue, request.Filters[0].Property.Body.KvlistValue.Values[2].Value.ByteStringValue.Compare);
            
            // 4th KV - Index 3
            Assert.Equal(key, request.Filters[0].Property.Body.KvlistValue.Values[3].Key);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Body.KvlistValue.Values[3].Value.DoubleValue.CompareAs);
            Assert.Equal(doubleValue, request.Filters[0].Property.Body.KvlistValue.Values[3].Value.DoubleValue.Compare);
            
            // 5th KV - Index 4
            Assert.Equal(key, request.Filters[0].Property.Body.KvlistValue.Values[4].Key);
            Assert.Equal(BoolCompareAsType.Equals, request.Filters[0].Property.Body.KvlistValue.Values[4].Value.BoolValue.CompareAs);
            Assert.Equal(boolValue, request.Filters[0].Property.Body.KvlistValue.Values[4].Value.BoolValue.Compare);
            
            // 6th KV - Index 5
            Assert.Equal(key, request.Filters[0].Property.Body.KvlistValue.Values[5].Key);
            Assert.Equal(StringCompareAsType.Equals, request.Filters[0].Property.Body.KvlistValue.Values[5].Value.ArrayValue.Values[0].StringValue.CompareAs);
            Assert.Equal(stringValue, request.Filters[0].Property.Body.KvlistValue.Values[5].Value.ArrayValue.Values[0].StringValue.Compare);
            
            // 7th KV - Index 6
            Assert.Equal(key, request.Filters[0].Property.Body.KvlistValue.Values[6].Value.KvlistValue.Values[0].Key);
            Assert.Equal(StringCompareAsType.Equals, request.Filters[0].Property.Body.KvlistValue.Values[6].Value.KvlistValue.Values[0].Value.StringValue.CompareAs);
            Assert.Equal(stringValue, request.Filters[0].Property.Body.KvlistValue.Values[6].Value.KvlistValue.Values[0].Value.StringValue.Compare);
        }
        
        [Fact]
        public void AddDroppedAttributesCountPropertyFilter()
        {
            var builder = new LogQueryRequestBuilder();
            const uint count = 12345;
            var request = builder.Where(filters => filters.AddDroppedAttributesCountFilter(count, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.DroppedAttributesCount, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.DroppedAttributesCount.CompareAs);
            Assert.Equal(count, request.Filters[0].Property.DroppedAttributesCount.Compare);
        }
        
        [Fact]
        public void AddFlagsPropertyFilter()
        {
            var builder = new LogQueryRequestBuilder();
            const uint flags = 12345;
            var request = builder.Where(filters => filters.AddFlagsFilter(flags, NumberCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.Flags, request.Filters[0].Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, request.Filters[0].Property.Flags.CompareAs);
            Assert.Equal(flags, request.Filters[0].Property.Flags.Compare);
        }
        
        [Fact]
        public void AddTraceIdPropertyFilter()
        {
            var builder = new LogQueryRequestBuilder();
            byte[] traceId = [1, 2, 3, 4, 5, 6, 7, 8, 1, 2, 3, 4, 5, 6, 7, 8];
            var request = builder.Where(filters => filters.AddTraceIdFilter(traceId, ByteStringCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.TraceId, request.Filters[0].Property.ValueCase);
            Assert.Equal(ByteStringCompareAsType.Equals, request.Filters[0].Property.TraceId.CompareAs);
            Assert.Equal(traceId, request.Filters[0].Property.TraceId.Compare);
        }
        
        [Fact]
        public void AddSpanIdPropertyFilter()
        {
            var builder = new LogQueryRequestBuilder();
            byte[] spanId = [1, 2, 3, 4, 5, 6, 7, 8];
            var request = builder.Where(filters => filters.AddSpanIdFilter(spanId, ByteStringCompareAsType.Equals)).Build();
            
            Assert.NotEmpty(request.Filters);
            Assert.Equal(Where.ValueOneofCase.Property, request.Filters[0].ValueCase);
            Assert.Equal(PropertyFilter.ValueOneofCase.SpanId, request.Filters[0].Property.ValueCase);
            Assert.Equal(ByteStringCompareAsType.Equals, request.Filters[0].Property.SpanId.CompareAs);
            Assert.Equal(spanId, request.Filters[0].Property.SpanId.Compare);
        }
        
        [Fact]
        public void AddOrFilter()
        {
            var builder = new LogQueryRequestBuilder();
            const uint firstFlags = 123;
            const uint secondFlags = 456;
            builder.Where(filters =>
            {
                filters.AddOrFilter(orFilters =>
                {
                    orFilters.AddFlagsFilter(firstFlags, NumberCompareAsType.Equals);
                    orFilters.AddFlagsFilter(secondFlags, NumberCompareAsType.Equals);
                });
            });
            var request = builder.Build();
            
            Assert.NotEmpty(request.Filters);
            
            var filterToFind = request.Filters.First();
            
            Assert.Equal(Where.ValueOneofCase.Or, filterToFind.ValueCase);

            var firstOr = filterToFind.Or.Filters[0];
            
            Assert.Equal(PropertyFilter.ValueOneofCase.Flags, firstOr.Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, firstOr.Property.Flags.CompareAs);
            Assert.Equal(firstFlags, firstOr.Property.Flags.Compare);
            
            var secondOr = filterToFind.Or.Filters[1];
            
            Assert.Equal(PropertyFilter.ValueOneofCase.Flags, secondOr.Property.ValueCase);
            Assert.Equal(NumberCompareAsType.Equals, secondOr.Property.Flags.CompareAs);
            Assert.Equal(secondFlags, secondOr.Property.Flags.Compare);
        }
        
        [Fact]
        public void AddInstrumentationScopeNamePropertyFilter()
        {
            var builder = new LogQueryRequestBuilder();
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
            var builder = new LogQueryRequestBuilder();
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
            var builder = new LogQueryRequestBuilder();
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
            var builder = new LogQueryRequestBuilder();
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
            var builder = new LogQueryRequestBuilder();
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
            var builder = new LogQueryRequestBuilder();
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
            var builder = new LogQueryRequestBuilder();
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
            var builder = new LogQueryRequestBuilder();
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
        
        [Fact]
        public void AddInstrumentationScopeVersionPropertyFilter()
        {
            var builder = new LogQueryRequestBuilder();
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
            var builder = new LogQueryRequestBuilder();
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
            var builder = new LogQueryRequestBuilder();
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
            var builder = new LogQueryRequestBuilder();
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
            var builder = new LogQueryRequestBuilder();
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
            var builder = new LogQueryRequestBuilder();
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
            var builder = new LogQueryRequestBuilder();
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
            var builder = new LogQueryRequestBuilder();
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
            var builder = new LogQueryRequestBuilder();
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
            var builder = new LogQueryRequestBuilder();
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
            var builder = new LogQueryRequestBuilder();
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
            var builder = new LogQueryRequestBuilder();
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