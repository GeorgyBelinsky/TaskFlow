using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFlow.Application.DTOs
{
    public class UploadProjectImageRequest
    {
        public IFormFile Image { get; set; } = null!;
    }
}
