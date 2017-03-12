using Lms.Domain.Models.Companies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Services.Companies
{
    public interface INotificationService
    {
        IEnumerable<Notification> LoadAllNotifications(string companyId);
        IEnumerable<Notification> LoadActiveNotifications(string companyId);
        Notification GetNotificationById(string companyId, string notificationId);
        void DeleteNotification(string companyId, string updaterId, string notificationId);
        Notification CreateNotification(string companyId, string updaterId, string title, string details, string startDate, string endDate);
        Notification EditNotification(string companyId, string updaterId, string notificationId, string title, string details, string startDate, string endDate);
    }
}
