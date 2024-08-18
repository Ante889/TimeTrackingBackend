using Microsoft.AspNetCore.Mvc;

namespace TimeTracking.App.Base.Controllers
{
    public interface SafeExecutorInterface
    {
        Task<IActionResult> ExecuteSafe(Func<Task<IActionResult>> func);
    }
}