using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.InvoiceLines
{
    public class ListInvoiceLinesQuery
        : IRequest<OperationResult<PagedResult<InvoiceLineDto>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}