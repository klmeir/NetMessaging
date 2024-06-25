using MediatR;
using NetMessaging.Domain.Dtos;
using NetMessaging.Domain.Services;

namespace NetMessaging.Application.Persons
{
    public class PersonAddCommandHandler : IRequestHandler<PersonAddCommand, ResponseDto>
    {
        private readonly PersonService _service;

        public PersonAddCommandHandler(PersonService service) =>
            _service = service ?? throw new ArgumentNullException(nameof(service));


        public async Task<ResponseDto> Handle(PersonAddCommand request, CancellationToken cancellationToken)
        {            
            var result = await _service.SendMsg(new Domain.Entities.Person { Id = request.Id, Name = request.Name, Surname = request.Surname });

            return result ? new ResponseDto(result, string.Empty) : new ResponseDto(result, "error in the queue");
        }
    }
}
