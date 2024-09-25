using FreshCode.DbModels;
using FreshCode.ModelsDTO;
using Microsoft.AspNetCore.Mvc;

namespace FreshCode.Controllers
{
    [ApiController]
    [Route("fortune-wheel")]
    public class FortuneWheelController(FreshCodeContext dbContext) : BaseController
    {
        
    }
}
