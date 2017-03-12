using Lms.Domain.Models.Plans;
using Lms.Domain.Repositories;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Services.Plans
{
    public class PlanServiceImpl : IPlanService
    {
        readonly static Logger logger = LogManager.GetCurrentClassLogger();
        protected readonly IUnitOfWork unitOfWork;

        private PlanServiceImpl()
        {

        }

        public PlanServiceImpl(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        public IEnumerable<Plan> LoadActivePlans()
        {
            return unitOfWork.PlanRepository.GetAll().Where(x => !x.IsDeleted);
        }
    }
}
