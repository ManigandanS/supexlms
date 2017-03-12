using Lms.Domain.Models.Companies;
using Lms.Domain.Repositories;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Services.Companies
{
    public class NotificationServiceImpl : INotificationService
    {
        readonly static Logger logger = LogManager.GetCurrentClassLogger();
        protected IUnitOfWork unitOfWork;

        public NotificationServiceImpl()
        {

        }

        public NotificationServiceImpl(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Notification CreateNotification(string companyId, string updaterId, string title, string details, string startDate, string endDate)
        {
            var company = unitOfWork.CompanyRepository.GetById(companyId);
            var notification = company.AddNotification(companyId, updaterId, title, details, DateTime.Parse(startDate), DateTime.Parse(endDate));

            unitOfWork.CompanyRepository.Update(company);
            unitOfWork.SaveChanges();

            return notification;
        }

        public Notification GetNotificationById(string companyId, string notificationId)
        {
            var company = unitOfWork.CompanyRepository.GetById(companyId);
            return company.Notifications.SingleOrDefault(x => x.Id == notificationId);
        }

        public IEnumerable<Notification> LoadAllNotifications(string companyId)
        {
            return unitOfWork.CompanyRepository.GetById(companyId).Notifications.Where(x => !x.IsDeleted);
        }

        public IEnumerable<Notification> LoadActiveNotifications(string companyId)
        {
            return unitOfWork.CompanyRepository.GetById(companyId).Notifications.Where(x => !x.IsDeleted && DateTime.UtcNow >= x.StartDate && DateTime.UtcNow < x.EndDate);
        }

        public Notification EditNotification(string companyId, string updaterId, string notificationId, string title, string details, string startDate, string endDate)
        {
            var company = unitOfWork.CompanyRepository.GetById(companyId);
            var notification = company.Notifications.SingleOrDefault(x => x.Id == notificationId);

            logger.Debug("{0}...{1}", startDate, endDate);

            if (notification != null)
            {
                notification.UpdateNotification(updaterId, title, details, DateTime.Parse(startDate), DateTime.Parse(endDate));
                unitOfWork.CompanyRepository.Update(company);
                unitOfWork.SaveChanges();
            }

            return notification;
        }

        public void DeleteNotification(string companyId, string updaterId, string notificationId)
        {
            var company = unitOfWork.CompanyRepository.GetById(companyId);
            var notification = company.Notifications.Single(x => x.Id == notificationId);
            notification.IsDeleted = true;

            unitOfWork.CompanyRepository.Update(company);
            unitOfWork.SaveChanges();
        }

    }
}
