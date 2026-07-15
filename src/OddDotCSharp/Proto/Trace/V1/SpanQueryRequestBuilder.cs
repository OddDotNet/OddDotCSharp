using System;
using OddDotNet.Proto.Common.V1;
using OddDotNet.Proto.Trace.V1;

namespace OddDotCSharp
{
    /// <summary>
    /// Fluent API-styled builder for creating <see cref="SpanQueryRequest"/> objects
    /// to be sent to the SpanQueryService.
    /// </summary>
    public class SpanQueryRequestBuilder
    {
        private const int DefaultDurationMilliseconds = 30000;
        
        private readonly SpanQueryRequest _request;
        private readonly WhereSpanFilterConfigurator _whereSpanFilterConfigurator;

        /// <summary>
        /// Create a new instance with defaults:
        /// Take - TakeFirst
        /// Duration - <see cref="DefaultDurationMilliseconds"/>
        ///
        /// No filters are added, so if a request is made with the defaults the first span detected within the
        /// timeframe will be returned. Add filters to limit the results returned even more.
        /// </summary>
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
                    Milliseconds = DefaultDurationMilliseconds
                }
            };
            
            _whereSpanFilterConfigurator = new WhereSpanFilterConfigurator();
        }

        /// <summary>
        /// Configures the query to return as soon as the first matching Span is found, or when
        /// the Wait duration elapses if none is found. This is the default.
        /// </summary>
        /// <returns>this <see cref="SpanQueryRequestBuilder"/></returns>
        public SpanQueryRequestBuilder TakeFirst()
        {
            _request.Take = new Take { TakeFirst = new TakeFirst() };
            return this;
        }

        /// <summary>
        /// Configures the query to return as soon as <paramref name="count"/> matching Spans are
        /// found, or when the Wait duration elapses with fewer than <paramref name="count"/> found.
        /// </summary>
        /// <param name="count">The number of Spans to find.</param>
        /// <returns>this <see cref="SpanQueryRequestBuilder"/></returns>
        public SpanQueryRequestBuilder TakeExact(int count)
        {
            _request.Take = new Take { TakeExact = new TakeExact { Count = count } };
            return this;
        }

        /// <summary>
        /// Configures the query to collect every matching Span seen over the whole Wait duration.
        /// TakeAll never returns early — the query always blocks for the full duration — so prefer
        /// <see cref="TakeFirst"/> or <see cref="TakeExact"/> with a filter to return as soon as a
        /// specific Span arrives.
        /// </summary>
        /// <returns>this <see cref="SpanQueryRequestBuilder"/></returns>
        public SpanQueryRequestBuilder TakeAll()
        {
            _request.Take = new Take { TakeAll = new TakeAll() };
            return this;
        }

        /// <summary>
        /// Sets the maximum time the query blocks for matching Spans. A value of zero or less
        /// selects the sink default of 30 seconds; it does not return immediately.
        /// </summary>
        /// <param name="timeSpan">
        /// The maximum time to wait for Spans. Zero or negative produces a Duration of 0, which the
        /// sink treats as its 30-second default.
        /// </param>
        /// <returns>this <see cref="SpanQueryRequestBuilder"/></returns>
        public SpanQueryRequestBuilder Wait(TimeSpan timeSpan)
        {
            int duration = timeSpan.TotalMilliseconds < 0 ? 0 : (int)timeSpan.TotalMilliseconds;
                
            _request.Duration = new Duration
            {
                Milliseconds = duration
            };
            return this;
        }

        /// <summary>
        /// Allows for filtering of Spans by properties of the Span. <seealso cref="WhereSpanFilterConfigurator"/>
        /// This method can be called multiple times. Each call to this method is stacking, so all filters defined in
        /// each call to Where() will be stacked together and included in the query request.
        ///
        /// Under most circumstances it makes sense to call the Where() method a single time. Use the action to define
        /// all the filters desired. 
        /// </summary>
        /// <param name="configure">The action used to configure the list of filters.</param>
        /// <returns>this <see cref="SpanQueryRequestBuilder"/></returns>
        /// <example>
        /// This shows how to configure a filter for the Name of the span:
        /// <code>
        ///     var request = new SpanQueryRequestBuilder().
        ///         .Where(filters =>
        ///         {
        ///             filters.AddSpanNameFilter("GET", StringCompareAsType.Equals);
        ///         })
        ///         .Build();
        /// </code>
        /// </example>
        public SpanQueryRequestBuilder Where(Action<WhereSpanFilterConfigurator> configure)
        {
            configure(_whereSpanFilterConfigurator);
            return this;
        }

        /// <summary>
        /// Builds a <see cref="SpanQueryRequest"/> using the setup of this <see cref="SpanQueryRequestBuilder"/>.
        /// </summary>
        /// <returns>The <see cref="SpanQueryRequest"/>. This can be used to make a query.</returns>
        public SpanQueryRequest Build()
        {
            _request.Filters.AddRange(_whereSpanFilterConfigurator.Filters);
            return _request;
        }
    }
}