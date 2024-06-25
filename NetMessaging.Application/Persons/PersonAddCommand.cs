using MediatR;
using NetMessaging.Domain.Dtos;

namespace NetMessaging.Application.Persons
{
    public record PersonAddCommand(int Id, string Name, string Surname) : IRequest<ResponseDto>;
}
