using MediatR;
using TimeTracking.App.Time.Domain.Entity;
using TimeTracking.App.Time.Domain.Interface;

namespace TimeTracking.App.Time.Application.Query
{
    public class FindTimesByCategoryIdQueryHandler : IRequestHandler<FindTimesByCategoryIdQuery, IEnumerable<TimeEntity>>
    {
        private readonly TimeRepositoryInterface _timeRepository;

        public FindTimesByCategoryIdQueryHandler(TimeRepositoryInterface timeRepository)
        {
            _timeRepository = timeRepository;
        }

        public async Task<IEnumerable<TimeEntity>> Handle(FindTimesByCategoryIdQuery request, CancellationToken cancellationToken)
        {
            return await _timeRepository.GetByCategoryAsync(request.Category);          
        }
    }
}
