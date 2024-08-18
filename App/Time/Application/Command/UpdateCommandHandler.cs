using MediatR;
using TimeTracking.App.Phase.Domain.Entity;
using TimeTracking.App.Time.Domain.Entity;
using TimeTracking.App.Time.Domain.Interface;

namespace TimeTracking.App.Time.Application.Command
{
    public class UpdateCommandHandler : IRequestHandler<UpdateCommand, TimeEntity?>
    {
        private readonly TimeRepositoryInterface _timeRepository;

        public UpdateCommandHandler(TimeRepositoryInterface timeRepository)
        {
            _timeRepository = timeRepository;
        }

        public async Task<TimeEntity?> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var time = await _timeRepository.GetByIdAsync(request.Id);

            if (time == null) return null;

            time.TimeInMinutes = request.TimeInMinutes;
            time.Title = request.Title;
            time.Description = request.Description;

            await _timeRepository.UpdateAsync(time);

            return time;
        }
    }
}
