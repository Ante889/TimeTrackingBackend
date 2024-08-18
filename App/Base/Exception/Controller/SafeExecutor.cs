using Microsoft.AspNetCore.Mvc;

namespace TimeTracking.App.Base.Controllers
{
    public class SafeExecutor: SafeExecutorInterface
    {
        public async Task<IActionResult> ExecuteSafe(Func<Task<IActionResult>> func)
        {
            try
            {
                return await func();
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { Error = ex.Message })
                {
                    StatusCode = 500
                };
            }
        }
    }
}
