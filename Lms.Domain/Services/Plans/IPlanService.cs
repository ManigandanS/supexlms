using Lms.Domain.Models.Plans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Services.Plans
{
    public interface IPlanService
    {
        IEnumerable<Plan> LoadActivePlans();
    }
}
