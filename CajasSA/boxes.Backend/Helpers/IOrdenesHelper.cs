using Boxes.Shared.Responses;

namespace boxes.Backend.Helpers
{
    public interface IOrdenesHelper
    {
        Task<ActionResponse<bool>> ProcessOrderAsync(string email, string remarks);
    }
}
