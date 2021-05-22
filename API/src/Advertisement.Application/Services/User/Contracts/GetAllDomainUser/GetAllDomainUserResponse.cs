using System.Collections.Generic;
using System.Linq;

namespace Advertisement.Application.Services.User.Contracts.GetAllDomainUser
{
    public class  GetAllDomainUserResponse
    {
        public IEnumerable<GetAllDomainUserResponseItem> Items { get; set; } = Enumerable.Empty<GetAllDomainUserResponseItem>();
    }
}