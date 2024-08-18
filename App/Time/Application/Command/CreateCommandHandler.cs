using MediatR;
using TimeTracking.App.Time.Domain.Entity;
using TimeTracking.App.Time.Domain.Interface;

namespace TimeTracking.App.Time.Application.Command
{
    public class CreateCommandHandler : IRequestHandler<CreateCommand, TimeEntity?>
    {
        private readonly TimeRepositoryInterface _timeRepository;

        public CreateCommandHandler(TimeRepositoryInterface timeRepository)
        {
            _timeRepository = timeRepository;
        }

        public async Task<TimeEntity?> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            var time = new TimeEntity
            {
               Category = request.Category.Id,
               TimeInMinutes = request.TimeInMinutes,
               Title = request.Title,   
               Description = request.Description,
            };

            await _timeRepository.AddAsync(time);

            return time;
        }
    }
}
