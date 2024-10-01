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
                    MillisecondsValue = DefaultDurationMilliseconds
                }
            };
            
            _whereSpanFilterConfigurator = new WhereSpanFilterConfigurator();
        }

        /// <summary>
        /// Takes the first Span that it matches against.
        /// </summary>
        /// <returns>this <see cref="SpanQueryRequestBuilder"/></returns>
        public SpanQueryRequestBuilder TakeFirst()
        {
            _request.Take = new Take { TakeFirst = new TakeFirst() };
            return this;
        }

        /// <summary>
        /// Takes the exact number of Spans specified that it matches against.
        /// </summary>
        /// <param name="count">The number of Spans to find.</param>
        /// <returns>this <see cref="SpanQueryRequestBuilder"/></returns>
        public SpanQueryRequestBuilder TakeExact(int count)
        {
            _request.Take = new Take { TakeExact = new TakeExact { Count = count } };
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>this <see cref="SpanQueryRequestBuilder"/></returns>
        public SpanQueryRequestBuilder TakeAll()
        {
            _request.Take = new Take { TakeAll = new TakeAll() };
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="duration"></param>
        /// <returns>this <see cref="SpanQueryRequestBuilder"/></returns>
        public SpanQueryRequestBuilder Wait(TimeSpan duration)
        {
            _request.Duration = new Duration
            {
                MillisecondsValue = (ulong)duration.TotalMilliseconds // TODO change to int, ulong ms is way too long
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