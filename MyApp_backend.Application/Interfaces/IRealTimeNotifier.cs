using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Application.Interfaces
{
    public interface IRealTimeNotifier
    {
        Task NotifyUserAsync(string userId, object notification);
    }
}
