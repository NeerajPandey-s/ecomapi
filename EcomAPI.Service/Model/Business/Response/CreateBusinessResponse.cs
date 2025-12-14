using System;
using System.Collections.Generic;
using System.Text;

namespace EcomAPI.Service.Model.Business.Response
{
    public class CreateBusinessResponse
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string DomainName { get; set; }
        public required string Email { get; set; }
    }
}
