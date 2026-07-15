using System;
using OddDotNet.Proto.Common.V1;
using OddDotNet.Proto.Logs.V1;

namespace OddDotCSharp
{
    /// <summary>
    /// Used for building a query to check for Log signals.
    /// </summary>
    /// <example>
    /// Build a query to check for all logs associated with a TraceId:
    /// <code>
    ///     byte[] traceId = ...;
    ///     var query = new LogQueryRequestBuilder()
    ///         .TakeAll() // Take every log you find
    ///         .Wait(TimeSpan.FromSeconds(3)) // Allow for 3 seconds for logs to come in
    ///         .Where(filters =>
    ///         {
    ///             // Add a filter for the TraceId
    ///             filters.AddTraceIdFilter(traceId, ByteStringCompareAsType.Equals);
    ///         })
    ///         .Build();
    /// </code>
    /// </example>
    public class LogQueryRequestBuilder
    {
        private const int DefaultDurationMilliseconds = 30000;
        
        private readonly LogQueryRequest _request;
        private readonly WhereLogFilterConfigurator _whereLogFilterConfigurator;

        /// <summary>
        /// Constructs a builder with Take set to TakeFirst, Duration set to the default
        /// of 30 seconds, and no filters.
        /// </summary>
        public LogQueryRequestBuilder()
        {
            _request = new LogQueryRequest
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
            
            _whereLogFilterConfigurator = new WhereLogFilterConfigurator();
        }
        
        /// <summary>
        /// Configures the query to return as soon as the first matching Log is found, or when
        /// the Wait duration elapses if none is found. This is the default.
        /// </summary>
        /// <returns>this <see cref="LogQueryRequestBuilder"/></returns>
        public LogQueryRequestBuilder TakeFirst()
        {
            _request.Take = new Take { TakeFirst = new TakeFirst() };
            return this;
        }
        
        /// <summary>
        /// Configures the query to return as soon as <paramref name="count"/> matching Logs are
        /// found, or when the Wait duration elapses with fewer than <paramref name="count"/> found.
        /// </summary>
        /// <param name="count">The number of Logs to find.</param>
        /// <returns>this <see cref="LogQueryRequestBuilder"/></returns>
        public LogQueryRequestBuilder TakeExact(int count)
        {
            _request.Take = new Take { TakeExact = new TakeExact { Count = count } };
            return this;
        }
        
        /// <summary>
        /// Configures the query to collect every matching Log seen over the whole Wait duration.
        /// TakeAll never returns early — the query always blocks for the full duration — so prefer
        /// <see cref="TakeFirst"/> or <see cref="TakeExact"/> with a filter to return as soon as a
        /// specific Log arrives.
        /// </summary>
        /// <returns>this <see cref="LogQueryRequestBuilder"/></returns>
        public LogQueryRequestBuilder TakeAll()
        {
            _request.Take = new Take { TakeAll = new TakeAll() };
            return this;
        }
        
        /// <summary>
        /// Sets the maximum time the query blocks for matching Logs. A value of zero or less
        /// selects the sink default of 30 seconds; it does not return immediately.
        /// </summary>
        /// <param name="timeSpan">
        /// The maximum time to wait for Logs. Zero or negative produces a Duration of 0, which the
        /// sink treats as its 30-second default.
        /// </param>
        /// <returns>this <see cref="LogQueryRequestBuilder"/></returns>
        public LogQueryRequestBuilder Wait(TimeSpan timeSpan)
        {
            int duration = timeSpan.TotalMilliseconds <= 0 ? 0 : (int)timeSpan.TotalMilliseconds;
                
            _request.Duration = new Duration
            {
                Milliseconds = duration
            };
            return this;
        }
        
        /// <summary>
        /// Allows for filtering of Logs by properties of the Log. <seealso cref="WhereLogFilterConfigurator"/>
        /// This method can be called multiple times. Each call to this method is stacking, so all filters defined in
        /// each call to Where() will be stacked together and included in the query request.
        ///
        /// Under most circumstances it makes sense to call the Where() method a single time. Use the action to define
        /// all the filters desired. 
        /// </summary>
        /// <param name="configure">The action used to configure the list of filters.</param>
        /// <returns>this <see cref="LogQueryRequestBuilder"/></returns>
        /// <example>
        /// This shows how to configure a filter for the TimeUnixNano property of the log:
        /// <code>
        ///     var request = new LogQueryRequestBuilder().
        ///         .Where(filters =>
        ///         {
        ///             filters.AddTimeUnixNanoFilter(123, NumberCompareAsType.Equals);
        ///         })
        ///         .Build();
        /// </code>
        /// </example>
        public LogQueryRequestBuilder Where(Action<WhereLogFilterConfigurator> configure)
        {
            configure(_whereLogFilterConfigurator);
            return this;
        }
        
        /// <summary>
        /// Builds a <see cref="LogQueryRequest"/> using the setup of this <see cref="LogQueryRequestBuilder"/>.
        /// </summary>
        /// <returns>The <see cref="LogQueryRequest"/>. This can be used to make a query.</returns>
        public LogQueryRequest Build()
        {
            _request.Filters.AddRange(_whereLogFilterConfigurator.Filters);
            return _request;
        }
    }
}