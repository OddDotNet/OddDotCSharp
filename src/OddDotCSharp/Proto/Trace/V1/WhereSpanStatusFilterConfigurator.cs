using OddDotNet.Proto.Common.V1;
using OddDotNet.Proto.Trace.V1;
using OpenTelemetry.Proto.Trace.V1;

namespace OddDotCSharp
{
    public class WhereSpanStatusFilterConfigurator
    {
        private readonly WhereSpanFilterConfigurator _configurator;

        public WhereSpanStatusFilterConfigurator(WhereSpanFilterConfigurator configurator)
        {
            _configurator = configurator;
        }
        
        /// <summary>
        /// Adds a <see cref="SpanStatusCodeProperty"/> filter to the list of filters.
        /// </summary>
        /// <param name="compare">The <see cref="Status.Types.StatusCode"/> to compare the property against.</param>
        /// <param name="compareAs">The type of comparison to do. See <see cref="EnumCompareAsType"/> for more details.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/>.</returns>
        public WhereSpanFilterConfigurator AddCodeFilter(Status.Types.StatusCode compare,
            EnumCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Status = new StatusFilter
                    {
                        Code = new SpanStatusCodeProperty
                        {
                            CompareAs = compareAs,
                            Compare = compare
                        }
                    }
                }
            };
            
            _configurator.Filters.Add(filter);
            return _configurator;
        }
        
        /// <summary>
        /// Adds a <see cref="StringProperty"/> filter to the list of filters.
        /// </summary>
        /// <param name="compare">The string to compare the property against.</param>
        /// <param name="compareAs">The type of comparison to do. See <see cref="StringCompareAsType"/> for more details.</param>
        /// <returns>this <see cref="WhereSpanFilterConfigurator"/>.</returns>
        public WhereSpanFilterConfigurator AddMessageFilter(string compare,
            StringCompareAsType compareAs)
        {
            var filter = new Where
            {
                Property = new PropertyFilter
                {
                    Status = new StatusFilter
                    {
                        Message = new StringProperty
                        {
                            CompareAs = compareAs,
                            Compare = compare
                        }
                    }
                }
            };
            
            _configurator.Filters.Add(filter);
            return _configurator;
        }
    }
}