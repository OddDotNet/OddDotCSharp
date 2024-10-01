using System;
using OddDotNet.Proto.Spans.V1;

namespace OddDotCSharp
{
    /// <summary>
    /// Fluent API-styled builder for creating <see cref="SpanQueryRequest"/> objects
    /// to be sent to the SpanQueryService.
    /// </summary>
    public class SpanQueryRequestBuilder
    {
        private const ulong DefaultDurationMilliseconds = 30000;
        
        private readonly SpanQueryRequest _request;
        private readonly WhereSpanFilterConfigurator _whereSpanFilterConfigurator;

        public SpanQueryRequestBuilder()
        {
            _request = new SpanQueryRequest
            {
                Take = new Take
                {
                    TakeFirst = new TakeFirst()
                },
                Duration = new Duration
                {
                    MillisecondsValue = DefaultDurationMilliseconds
                }
            };
            
            _whereSpanFilterConfigurator = new WhereSpanFilterConfigurator();
        }

        public SpanQueryRequestBuilder TakeFirst()
        {
            _request.Take = new Take { TakeFirst = new TakeFirst() };
            return this;
        }

        public SpanQueryRequestBuilder TakeExact(int count)
        {
            _request.Take = new Take { TakeExact = new TakeExact { Count = count } };
            return this;
        }

        public SpanQueryRequestBuilder TakeAll()
        {
            _request.Take = new Take { TakeAll = new TakeAll() };
            return this;
        }

        public SpanQueryRequestBuilder Wait(TimeSpan duration)
        {
            _request.Duration = new Duration
            {
                MillisecondsValue = (ulong)duration.TotalMilliseconds // TODO change to int, ulong ms is way too long
            };
            return this;
        }

        public SpanQueryRequestBuilder Where(Action<WhereSpanFilterConfigurator> configure)
        {
            configure(_whereSpanFilterConfigurator);
            return this;
        }

        public SpanQueryRequest Build()
        {
            _request.Filters.AddRange(_whereSpanFilterConfigurator.Filters);
            return _request;
        }
    }
}