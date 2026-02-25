using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFlow.Application.DTOs
{
    public record ProjectResponse
    (
        Guid Id,
        string? ImageUrl,
        string Name = null!
    );
}
