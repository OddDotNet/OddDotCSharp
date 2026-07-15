using System;
using OddDotNet.Proto.Common.V1;
using OddDotNet.Proto.Metrics.V1;

namespace OddDotCSharp
{
    /// <summary>
    /// Used for building a query to check for Metric signals.
    /// </summary>
    /// <example>
    /// Build a query to check for all metrics of a specifc name:
    /// <code>
    ///     string name = ...;
    ///     var query = new MetricQueryRequestBuilder()
    ///         .TakeAll() // Take every metric you find
    ///         .Wait(TimeSpan.FromSeconds(3)) // Allow for 3 seconds for metrics to come in
    ///         .Where(filters =>
    ///         {
    ///             // Add a filter for the name
    ///             filters.AddNameFilter(name, StringCompareAsType.Equals);
    ///         })
    ///         .Build();
    /// </code>
    /// </example>
    public class MetricQueryRequestBuilder
    {
        private const int DefaultDurationMilliseconds = 30000;
        
        private readonly MetricQueryRequest _request;
        private readonly WhereMetricFilterConfigurator _whereMetricFilterConfigurator;
        
        /// <summary>
        /// Constructs a builder with Take set to TakeFirst, Duration set to the default
        /// of 30 seconds, and no filters.
        /// </summary>
        public MetricQueryRequestBuilder()
        {
            _request = new MetricQueryRequest
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
            
            _whereMetricFilterConfigurator = new WhereMetricFilterConfigurator();
        }
        
        /// <summary>
        /// Configures the query to return as soon as the first matching Metric is found, or when
        /// the Wait duration elapses if none is found. This is the default.
        /// </summary>
        /// <returns>this <see cref="MetricQueryRequestBuilder"/></returns>
        public MetricQueryRequestBuilder TakeFirst()
        {
            _request.Take = new Take { TakeFirst = new TakeFirst() };
            return this;
        }
        
        /// <summary>
        /// Configures the query to return as soon as <paramref name="count"/> matching Metrics are
        /// found, or when the Wait duration elapses with fewer than <paramref name="count"/> found.
        /// </summary>
        /// <param name="count">The number of Metrics to find.</param>
        /// <returns>this <see cref="MetricQueryRequestBuilder"/></returns>
        public MetricQueryRequestBuilder TakeExact(int count)
        {
            _request.Take = new Take { TakeExact = new TakeExact { Count = count } };
            return this;
        }
        
        /// <summary>
        /// Configures the query to collect every matching Metric seen over the whole Wait duration.
        /// TakeAll never returns early — the query always blocks for the full duration — so prefer
        /// <see cref="TakeFirst"/> or <see cref="TakeExact"/> with a filter to return as soon as a
        /// specific Metric arrives.
        /// </summary>
        /// <returns>this <see cref="MetricQueryRequestBuilder"/></returns>
        public MetricQueryRequestBuilder TakeAll()
        {
            _request.Take = new Take { TakeAll = new TakeAll() };
            return this;
        }
        
        /// <summary>
        /// Sets the maximum time the query blocks for matching Metrics. A value of zero or less
        /// selects the sink default of 30 seconds; it does not return immediately.
        /// </summary>
        /// <param name="timeSpan">
        /// The maximum time to wait for Metrics. Zero or negative produces a Duration of 0, which the
        /// sink treats as its 30-second default.
        /// </param>
        /// <returns>this <see cref="MetricQueryRequestBuilder"/></returns>
        public MetricQueryRequestBuilder Wait(TimeSpan timeSpan)
        {
            int duration = timeSpan.TotalMilliseconds < 0 ? 0 : (int)timeSpan.TotalMilliseconds;
                
            _request.Duration = new Duration
            {
                Milliseconds = duration
            };
            return this;
        }
        
        /// <summary>
        /// Allows for filtering of Metrics by properties of the Metric. <seealso cref="WhereMetricFilterConfigurator"/>
        /// This method can be called multiple times. Each call to this method is stacking, so all filters defined in
        /// each call to Where() will be stacked together and included in the query request.
        ///
        /// Under most circumstances it makes sense to call the Where() method a single time. Use the action to define
        /// all the filters desired. 
        /// </summary>
        /// <param name="configure">The action used to configure the list of filters.</param>
        /// <returns>this <see cref="MetricQueryRequestBuilder"/></returns>
        /// <example>
        /// This shows how to configure a filter for the Name of the metric:
        /// <code>
        ///     var request = new MetricQueryRequestBuilder().
        ///         .Where(filters =>
        ///         {
        ///             filters.AddNameFilter("GET", StringCompareAsType.Equals);
        ///         })
        ///         .Build();
        /// </code>
        /// </example>
        public MetricQueryRequestBuilder Where(Action<WhereMetricFilterConfigurator> configure)
        {
            configure(_whereMetricFilterConfigurator);
            return this;
        }

        /// <summary>
        /// Builds a <see cref="MetricQueryRequest"/> using the setup of this <see cref="MetricQueryRequestBuilder"/>.
        /// </summary>
        /// <returns>The <see cref="MetricQueryRequest"/>. This can be used to make a query.</returns>
        public MetricQueryRequest Build()
        {
            _request.Filters.AddRange(_whereMetricFilterConfigurator.Filters);
            return _request;
        }
        
    }
}