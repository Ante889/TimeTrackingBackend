using MediatR;
using TimeTracking.App.Time.Domain.Entity;
using TimeTracking.App.Time.Domain.Interface;

namespace TimeTracking.App.Time.Application.Query
{
    public class FindTimeByIdQueryHandler : IRequestHandler<FindTimeByIdQuery, TimeEntity?>
    {
        private readonly TimeRepositoryInterface _timeRepository;

        public FindTimeByIdQueryHandler(TimeRepositoryInterface timeRepository)
        {
            _timeRepository = timeRepository;
        }

        public async Task<TimeEntity?> Handle(FindTimeByIdQuery request, CancellationToken cancellationToken)
        {
            return await _timeRepository.GetByIdAsync(request.Id);
        }
    }
}
