using System;
using OddDotNet.Proto.Common.V1;
using OddDotNet.Proto.Logs.V1;

namespace OddDotCSharp
{
    public class LogQueryRequestBuilder
    {
        private const int DefaultDurationMilliseconds = 30000;
        
        private readonly LogQueryRequest _request;
        private readonly WhereLogFilterConfigurator _whereLogFilterConfigurator;

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
        /// Takes the first Log that it matches against.
        /// </summary>
        /// <returns>this <see cref="LogQueryRequestBuilder"/></returns>
        public LogQueryRequestBuilder TakeFirst()
        {
            _request.Take = new Take { TakeFirst = new TakeFirst() };
            return this;
        }
        
        /// <summary>
        /// Takes the exact number of Logs specified that it matches against.
        /// </summary>
        /// <param name="count">The number of Logs to find.</param>
        /// <returns>this <see cref="QueryRequestBuilder"/></returns>
        public LogQueryRequestBuilder TakeExact(int count)
        {
            _request.Take = new Take { TakeExact = new TakeExact { Count = count } };
            return this;
        }
        
        /// <summary>
        /// Takes all Logs that match the given filters within the provided timeframe.
        /// </summary>
        /// <returns>this <see cref="LogQueryRequestBuilder"/></returns>
        public LogQueryRequestBuilder TakeAll()
        {
            _request.Take = new Take { TakeAll = new TakeAll() };
            return this;
        }
        
        /// <summary>
        /// Allows for specifying the amount of time to wait for a matching Log to be received. 
        /// </summary>
        /// <param name="timeSpan">
        /// The TimeSpan specifying how long to wait for Logs. Negative values will result in a Duration of 0.
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
        /// This shows how to configure a filter for the Name of the metric:
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