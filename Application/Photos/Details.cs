using Application.Core;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;


namespace Application.Photos
{
    public class Details
    {
        public class Query : IRequest<Result<Profiles.Profile>> { 
            public string Username { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<Profiles.Profile>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context, IMapper mapper, IUserAccessor userAccessor)
            {
                _context = context;
                _mapper = mapper;
                _userAccessor = userAccessor;
            }
            public async Task<Result<Profiles.Profile>> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _context.Users
                    .ProjectTo<Profiles.Profile>(_mapper.ConfigurationProvider,
                    new {currentUsername = _userAccessor.GetUsername()})
                    .SingleOrDefaultAsync(x => x.Username == request.Username);
                if (user == null) return null;
              
                return Result<Profiles.Profile>.Success(user);
            }
        }

    }
}
