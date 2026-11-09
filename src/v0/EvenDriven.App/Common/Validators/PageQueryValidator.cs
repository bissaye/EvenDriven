using EvenDriven.Domain.Primitives;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvenDriven.App.Common.Validators
{
    public sealed record PagedQueryDto(int Page, int PageSize);

    public static class PagedQueryDtoExtensions
    {
        public static PagedQuery ToPagedQuery(this PagedQueryDto pagedQueryDto)
        {
            return new PagedQuery
            {
                Page = pagedQueryDto.Page,
                PageSize = pagedQueryDto.PageSize
            };
        }
    }

    public sealed class PageQueryValidator :  AbstractValidator<PagedQueryDto>
    {
        private const int MaxPageSize = 100;
        public PageQueryValidator()
        {
            RuleFor(p => p.PageSize)
                .NotEmpty().WithMessage("the page size is required")
                .Must(pageSize => pageSize <= MaxPageSize).WithMessage("Max value for page size is 100")
                .Must(pageSize => pageSize > 0).WithMessage("Min value for page size is 1");

            RuleFor(p => p.Page).
                Must(page => page > 0).WithMessage("Min value for page is 1");  
        }
    }
}
