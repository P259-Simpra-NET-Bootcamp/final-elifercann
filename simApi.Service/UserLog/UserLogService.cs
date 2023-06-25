using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using simApi.Base;
using simApi.Data;
using simApi.Data.UnitOfWork;
using simApi.Schema;

namespace simApi.Service;

public class UserLogService : BaseService<UserLog, UserLogRequest, UserLogResponse>, IUserLogService
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMemoryCache memoryCache;
    private readonly IMapper _mapper;
    public UserLogService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
       _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    public ApiResponse<List<UserLogResponse>> GetByUserName(string username)
    {
        var list = _unitOfWork.Repository<UserLog>().Where(x => x.UserName == username).ToList();
        var mapped = _mapper.Map<List<UserLogResponse>>(list);
        return new ApiResponse<List<UserLogResponse>>(mapped);
    }

    public void Log(string username, string logType)
    {
        UserLog log = new();
        log.LogType = logType;
        log.CreatedAt = DateTime.UtcNow;
        log.TransactionDate = DateTime.UtcNow;
        log.UserName = username;

        _unitOfWork.Repository<UserLog>().Insert(log);
        _unitOfWork.Complete();
    }
}