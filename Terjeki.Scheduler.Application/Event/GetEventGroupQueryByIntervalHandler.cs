using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terjeki.Scheduler.Application.Event
{
    public class GetEventGroupQueryByIntervalHandler : IRequestHandler<GetEventGroupQueryByInterval, IEnumerable<EventGroupModel>>
    {
        private readonly IMockDatabase _mockDatabase;
        public GetEventGroupQueryByIntervalHandler(IMockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }
        public async Task<IEnumerable<EventGroupModel>> Handle(GetEventGroupQueryByInterval request, CancellationToken cancellationToken)
        {
            return await _mockDatabase.GetEventGroups(request.Start, request.End, cancellationToken);
        }
    }
}
