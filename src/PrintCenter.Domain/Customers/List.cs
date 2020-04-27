using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.EntityFrameworkCore;
using PrintCenter.Data;

namespace PrintCenter.Domain.Customers
{
    public class List
    {
        public class Query : IRequest<CustomersEnvelope>
        {
            public string Sort { get; }
            public string Order { get; }
            public string Filter { get; }
            public string SearchString { get; }
            public int Page { get; }
            public int PerPage { get; }

            public Query(
                string sort,
                string order,
                string filter, 
                string searchString, 
                int page,
                int perPage)
            {
                Sort = sort;
                Order = order;
                Filter = filter;
                SearchString = searchString;
                Page = page;
                PerPage = perPage;
            }
        }
        
        public class QueryHandler : IRequestHandler<Query, CustomersEnvelope>
        {
            private readonly DataContext context;
            private readonly IMapper mapper;

            public QueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<CustomersEnvelope> Handle(Query query, CancellationToken cancellationToken)
            {
                var customers = context.Customers.AsQueryable();
                
                if (!string.IsNullOrEmpty(query.SearchString))
                {
                    customers = customers.Where(s => s.Name.Contains(query.SearchString));
                }
                
                var result = await customers.Skip((query.Page - 1) * query.PerPage).Take(query.PerPage).ToListAsync(cancellationToken);
                var total = context.Customers.Count();
                return new CustomersEnvelope(mapper.Map<List<Customer>>(result), total);
            }
        }
    }
    
    
}