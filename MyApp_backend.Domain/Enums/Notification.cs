using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Domain.Enums
{
    public enum NotificationType
    {
        BookingConfirmation,
        PaymentUpdate,
        ArrivalAlert,
        WarrantyReminder,
        AdminBroadcast
    }
    public enum NotificationStatus
    {
        Pending,
        Sent,
        Failed,
        Read
    }
}
