using simApi.Base;
using simApi.Data;
using simApi.Schema;

namespace simApi.Service;

public interface IUserLogService : IBaseService<UserLog, UserLogRequest, UserLogResponse>
{
    void Log(string username, string logType);
    ApiResponse<List<UserLogResponse>> GetByUserName(string username);
}