using Lms.Domain.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.LmsWeb.Tests
{
    public class BaseTest
    {
        protected Mock<IUnitOfWork> unitOfWork;

        [TestInitialize]
        public void Initialize()
        {
            unitOfWork = new Mock<IUnitOfWork>();
        }
    }
}
